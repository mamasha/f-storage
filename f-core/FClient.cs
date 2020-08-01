using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace f_core
{
    public struct ClientInfo
    {
        public string ServerName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public interface IClient
    {
        Task Ping();
        Task<string[]> ListFiles();
        Task Upload(string fileName, string srcPath);
        Task Download(string fileName, string dstPath);
        Task Delete(string fileName);
        void Close();
    }

    public class FClient : IClient
    {
        private readonly FConfig _config;
        private readonly ClientInfo _info;

        private readonly TcpClient _tcpClient;

        public static async Task<IClient> New(FConfig config, ClientInfo info)
        {
            var tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(info.ServerName, info.Port);

            return new FClient(config, info, tcpClient);
        }

        private FClient(FConfig config, ClientInfo info, TcpClient tcpClient)
        {
            _config = config;
            _info = info;
            _tcpClient = tcpClient;
        }

        private ITcpOp startTcpOp(SrvRequest request)
        {
            request.TrackingId = Helpers.MakeTrackingId();
            request.ServerName = _info.ServerName;
            request.Port = _info.Port;
            request.UserName = _info.UserName;
            request.Password = _info.Password;

            return
                TcpOp.New(_config.BinaryStreamBufSize, _tcpClient.GetStream());
        }

        private void validate(SrvResponse response)
        {
            if (response == null)
                throw new ApplicationException("Protocol error: no response is received");

            if (response.Error.IsNotEmpty())
                throw new ApplicationException(response.Error);
        }

        async Task IClient.Ping()
        {
            var request = new SrvPingRequest();

            var tcpOp = startTcpOp(request);

            await tcpOp.Write(request);

            var response = await tcpOp.Read<SrvPongResponse>();
            validate(response);
        }

        async Task<string[]> IClient.ListFiles()
        {
            var request = new SrvListRequest();

            var tcpOp = startTcpOp(request);

            await tcpOp.Write(request);

            var response = await tcpOp.Read<SrvListResponse>();
            validate(response);

            return response.FileNames;
        }

        async Task IClient.Upload(string fileName, string srcPath)
        {
            var file = File.Open(srcPath, FileMode.Open, FileAccess.Read);

            var request = new SrvUploadRequest {
                FileName = fileName,
                FileSize = file.Length
            };

            var tcpOp = startTcpOp(request);

            await tcpOp.Write(request);

            var response = await tcpOp.Read<SrvUploadResponse>();
            validate(response);

            await tcpOp.WriteBytesFrom(file, file.Length);
            file.Close();

            response = await tcpOp.Read<SrvUploadResponse>();
            validate(response);
        }

        async Task IClient.Download(string fileName, string dstPath)
        {
            var request = new SrvDownloadRequest { 
                FileName = fileName,
                LocalPath = dstPath
            };

            var tcpOp = startTcpOp(request);

            await tcpOp.Write(request);

            var response = await tcpOp.Read<SrvDownloadResponse>();
            validate(response);

            var file = File.Open(dstPath, FileMode.Create);

            await tcpOp.ReadBytesTo(file, response.FileSize);

            file.Close();
        }

        async Task IClient.Delete(string fileName)
        {
            var request = new SrvDeleteRequest { 
                FileName = fileName
            };

            var tcpOp = startTcpOp(request);

            await tcpOp.Write(request);

            var response = await tcpOp.Read<SrvDeleteResponse>();
            validate(response);
        }

        void IClient.Close()
        { 
            try
            {
                _tcpClient.Close();
            }
            catch (Exception)
            { }
        }
    }
}
