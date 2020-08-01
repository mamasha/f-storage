using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace f_core
{
    interface ITcpAcceptor
    {
        string ServerName { get; }
        int Port { get; }

        void Close();
    }

    class TcpAcceptor : ITcpAcceptor
    {
        private readonly int FAILED_TO_ACCEPT_TCP_RETRY_TIMEOUT = 2000;

        private readonly FConfig _config;
        private readonly ILogger _log;
        private readonly IServer _server;
        private readonly TcpListener _listener;

        private volatile bool _isClosed;

        public static ITcpAcceptor New(FConfig config, ILogger log, IServer server)
        {
            return new TcpAcceptor(config, log, server);
        }

        private TcpAcceptor(FConfig config, ILogger log, IServer server)
        {
            FAILED_TO_ACCEPT_TCP_RETRY_TIMEOUT = config.Server.FAILED_TO_ACCEPT_TCP_RETRY_MILLISECONDS;

            var listener = new TcpListener(IPAddress.Any, config.TcpPort);
            listener.Start();

            _config = config;
            _log = log;
            _server = server;
            _listener = listener;

            Task.Run(() => acceptConnections());
        }

        private async Task acceptConnections()
        {
            for (;;)
            {
                try
                {
                    var tcpClient = await _listener.AcceptTcpClientAsync();
                    _ = Task.Run(() => processConnection(tcpClient));
                }
                catch (Exception ex)
                {
                    if (_isClosed)
                        break;

                    _log.Error("tcp.protocol", ex);
                    await Task.Delay(FAILED_TO_ACCEPT_TCP_RETRY_TIMEOUT);
                }
            }
        }

        private async Task<bool> dispathRequest(SrvRequest request, string jRequest, ITcpOp tcpOp)
        {
            try
            {
                switch (request.Command)
                {
                    case SrvRequest.PING:
                        var pingRequest = jRequest.Parse<SrvPingRequest>();
                        await _server.Ping(pingRequest, tcpOp);
                        break;

                    case SrvRequest.LIST:
                        var listRequest = jRequest.Parse<SrvListRequest>();
                        await _server.ListFiles(listRequest, tcpOp);
                        break;

                    case SrvRequest.UPLOAD:
                        var uploadRequest = jRequest.Parse<SrvUploadRequest>();
                        await _server.UploadFile(uploadRequest, tcpOp);
                        break;

                    case SrvRequest.DOWNLOAD:
                        var downloadRequest = jRequest.Parse<SrvDownloadRequest>();
                        await _server.DownloadFile(downloadRequest, tcpOp);
                        break;

                    case SrvRequest.DELETE:
                        var deleteRequest = jRequest.Parse<SrvDeleteRequest>();
                        await _server.DeleteFile(deleteRequest, tcpOp);
                        break;

                    default:
                        throw new IOException($"Unknown command: {request.Command}");
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error(request.TrackingId, ex);

                await tcpOp.Write(new SrvResponse {
                    TrackingId = request.TrackingId,
                    Error = ex.Message
                });

                return false;
            }
        }

        private async Task processConnection(TcpClient tcpClient)
        {
            try
            {
                var tcpOp = TcpOp.New(_config.BinaryStreamBufSize, tcpClient.GetStream());

                for (;;)
                {
                    var jRequest = await tcpOp.ReadString();

                    if (jRequest.IsEmpty())
                        break;

                    var request = jRequest.Parse<SrvRequest>();

                    bool ok = await dispathRequest(request, jRequest, tcpOp);

                    if (!ok)
                        break;
                }
            }
            catch (Exception ex)
            {
                if (_isClosed == false)
                    _log.Error("tcp.protocol", ex);
            }
            finally
            {
                try
                {
                    tcpClient.Close();
                }
                catch { }
            }
        }

        string ITcpAcceptor.ServerName 
        { 
            get { return Environment.MachineName; }
        }

        int ITcpAcceptor.Port 
        { 
            get 
            {
                var epoint = (IPEndPoint) _listener.LocalEndpoint;
                return epoint.Port;
            }
        }

        void ITcpAcceptor.Close()
        {
            _isClosed = true;

            try
            {
                _listener.Stop();
            }
            catch (Exception)
            { }
        }
    }
}
