using System.Data;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace f_core
{
    public class FClient : IStorage
    {
        public static IStorage New(string serverName, int port)
        {
            return new FClient(serverName, port);
        }

        private FClient(string serverName, int port)
        { }

        private async Task<ITcpOp> startOp(SrvRequest request)
        {
            var tcp = new TcpClient();
            await tcp.ConnectAsync(request.ServerName, request.Port);
            var op = TcpOp.New(tcp.GetStream());
            return op;
        }

        async Task<SrvListResponse> IStorage.ListFiles(SrvListRequest request, ITcpOp tcp)
        {
            var jRequest = request.ToJson();
            await tcp.WriteString(jRequest);

            var jResponse = await tcp.ReadString();
            var response = jResponse.Parse<SrvListResponse>();

            return response;
        }

        async Task<SrvUploadResponse> IStorage.Upload(SrvUploadRequest request, ITcpOp tcp)
        {
            var file = File.Open(request.SrcPath, FileMode.Open);
            request.FileSize = file.Length;

            var jRequest = request.ToJson();
            await tcp.WriteString(jRequest);

            await tcp.WriteBytesFrom(file, file.Length);

            var jResponse = await tcp.ReadString();
            var response = jResponse.Parse<SrvUploadResponse>();

            return response;
        }

        async Task<SrvDownloadResponse> IStorage.Download(SrvDownloadRequest request, ITcpOp tcp)
        {
            var file = File.Open(request.LocalPath, FileMode.Create);

            var jRequest = request.ToJson();
            await tcp.WriteString(jRequest);

            var jResponse = await tcp.ReadString();
            var response = jResponse.Parse<SrvDownloadResponse>();

            await tcp.ReadBytesTo(file, response.FileSize);

            return response;
        }

        async Task<SrvDeleteResponse> IStorage.Delete(SrvDeleteRequest request, ITcpOp tcp)
        {
            var jRequest = request.ToJson();
            await tcp.WriteString(jRequest);

            var jResponse = await tcp.ReadString();
            var response = jResponse.Parse<SrvDeleteResponse>();

            return response;
        }
    }
}
