using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace f_core
{
    public class FClient : IClient
    {
        public static IClient New(string serverName, int port)
        {
            return new FClient(serverName, port);
        }

        private FClient(string serverName, int port)
        { }

        private ITcpOp startOp(SrvRequest request)
        {
            var op = TcpOp.New(null);
            return op;
        }

        async Task<SrvListResponse> IClient.ListFiles(SrvListRequest request)
        {
            var op = startOp(request);

            var jRequest = request.ToJson();
            await op.WriteString(jRequest);

            var jResponse = await op.ReadString();
            var response = jResponse.Parse<SrvListResponse>();

            return response;
        }

        async Task<SrvUploadResponse> IClient.Upload(SrvUploadRequest request)
        {
            var file = File.Open(request.SrcPath, FileMode.Open);
            request.FileSize = file.Length;

            var op = startOp(request);

            var jRequest = request.ToJson();
            await op.WriteString(jRequest);

            await op.WriteBytesFrom(file, file.Length);

            var jResponse = await op.ReadString();
            var response = jResponse.Parse<SrvUploadResponse>();

            return response;
        }

        async Task<SrvDownloadResponse> IClient.Download(SrvDownloadRequest request, string fileName, string dstFolder)
        {
            var file = File.Open(request.LocalPath, FileMode.Create);

            var op = startOp(request);

            var jRequest = request.ToJson();
            await op.WriteString(jRequest);

            var jResponse = await op.ReadString();
            var response = jResponse.Parse<SrvDownloadResponse>();

            await op.ReadBytesTo(file, response.FileSize);

            return response;
        }

        async Task<SrvDeleteResponse> IClient.Delete(SrvDeleteRequest request, string fileName)
        {
            var op = startOp(request);

            var jRequest = request.ToJson();
            await op.WriteString(jRequest);

            var jResponse = await op.ReadString();
            var response = jResponse.Parse<SrvDeleteResponse>();

            return response;
        }
    }
}
