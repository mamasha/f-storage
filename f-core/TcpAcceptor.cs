using System;
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

        private readonly ILogger _log;
        private readonly IServer _server;
        private readonly TcpListener _listener;

        public static ITcpAcceptor New(FConfig config, ILogger log, IServer server)
        {
            return new TcpAcceptor(config, log, server);
        }

        private TcpAcceptor(FConfig config, ILogger log, IServer server)
        {
            FAILED_TO_ACCEPT_TCP_RETRY_TIMEOUT = config.Server.FAILED_TO_ACCEPT_TCP_RETRY_TIMEOUT;

            var listener = new TcpListener(IPAddress.Any, config.TcpPort);
            listener.Start();

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
                    case SrvRequest.LIST:
                        await _server.ListFiles(jRequest.Parse<SrvListRequest>(), tcpOp);
                        break;

                    case SrvRequest.UPLOAD:
                        await _server.UploadFile(jRequest.Parse<SrvUploadRequest>(), tcpOp);
                        break;

                    case SrvRequest.DOWNLOAD:
                        await _server.DownloadFile(jRequest.Parse<SrvDownloadRequest>(), tcpOp);
                        break;

                    case SrvRequest.DELETE:
                        await _server.DeleteFile(jRequest.Parse<SrvDeleteRequest>(), tcpOp);
                        break;
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
                for (;;)
                {
                    var tcpOp = TcpOp.New(tcpClient.GetStream());

                    var jRequest = await tcpOp.ReadString();
                    var request = jRequest.Parse<SrvRequest>();

                    bool ok = await dispathRequest(request, jRequest, tcpOp);

                    if (!ok)
                        break;
                }
            }
            catch (Exception ex)
            {
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
            try
            {
                _listener.Stop();
            }
            catch (Exception)
            { }
        }
    }
}
