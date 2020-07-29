using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace f_core
{
    public interface IClient : IDisposable
    {
        Task<string[]> ListFiles();
        Task Upload(string fileName, string srcPath);
        Task Download(string fileName, string dstPath);
        Task Delete(string fileName);
    }

    public class FClient : IClient
    {
        private readonly string _serverName;
        private readonly int _port;
        private readonly string _userName;
        private readonly string _password;

        private readonly TcpClient _tcpClient;

        public static async Task<IClient> New(string serverName, int port, string userName, string password)
        {
            var tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(serverName, port);

            return new FClient(serverName, port, userName, password, tcpClient);
        }

        private FClient(string serverName, int port, string userName, string password, TcpClient tcpClient)
        {
            _serverName = serverName;
            _port = port;
            _userName = userName;
            _password = password;
            _tcpClient = tcpClient;
        }

        private ITcpOp startTcpOp(SrvRequest request)
        {
            request.TrackingId = Helpers.MakeTrackingId();
            request.ServerName = _serverName;
            request.Port = _port;
            request.UserName = _userName;
            request.Password = _password;

            return
                TcpOp.New(_tcpClient.GetStream());
        }

        private void validate(SrvResponse response)
        {
            if (response == null)
                throw new ApplicationException("Protocol error: no response is received");

            if (response.Error.IsNotEmpty())
                throw new ApplicationException(response.Error);
        }

        void IDisposable.Dispose()
        { 
            try
            {
                _tcpClient.Close();
            }
            catch (Exception)
            { }
        }

        async Task<string[]> IClient.ListFiles()
        {
            var request = new SrvListRequest();

            var tcpOp = startTcpOp(request);

            var jRequest = request.ToJson();
            await tcpOp.WriteString(jRequest);

            var jResponse = await tcpOp.ReadString();
            var response = jResponse.Parse<SrvListResponse>();

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

            var jRequest = request.ToJson();
            await tcpOp.WriteString(jRequest);

            await tcpOp.WriteBytesFrom(file, file.Length);

            file.Close();

            var jResponse = await tcpOp.ReadString();
            var response = jResponse.Parse<SrvUploadResponse>();

            validate(response);
        }

        async Task IClient.Download(string fileName, string dstPath)
        {
            var request = new SrvDownloadRequest { 
                FileName = fileName,
                LocalPath = dstPath
            };

            var tcpOp = startTcpOp(request);

            var jRequest = request.ToJson();
            await tcpOp.WriteString(jRequest);

            var jResponse = await tcpOp.ReadString();
            var response = jResponse.Parse<SrvDownloadResponse>();

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

            var jRequest = request.ToJson();
            await tcpOp.WriteString(jRequest);

            var jResponse = await tcpOp.ReadString();
            var response = jResponse.Parse<SrvDeleteResponse>();

            validate(response);
        }
    }
}
