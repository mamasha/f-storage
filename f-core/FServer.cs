using System.Threading.Tasks;

namespace f_core
{
    public class FServer : IClient, IServer
    {
        public static IClient New()
        {
            return new FServer();
        }

        private FServer()
        { }

        async Task<SrvListResponse> IClient.ListFiles(SrvListRequest request)
        {
            var response = new SrvListResponse {
                RequestId = request.RequestId,
                FileNames = new string[] {"foo.txt", "boo.txt"}
            };

            await Task.Delay(1000);
            return response;
        }

        async Task<SrvUploadResponse> IClient.Upload(SrvUploadRequest request)
        {
            var response = new SrvUploadResponse {
                RequestId = request.RequestId
            };

            await Task.Delay(1000);
            return response;
        }

        async Task<SrvDownloadResponse> IClient.Download(SrvDownloadRequest request, string fileName, string dstFolder)
        {
            var response = new SrvDownloadResponse {
                RequestId = request.RequestId
            };

            await Task.Delay(1000);
            return response;
        }

        async Task<SrvDeleteResponse> IClient.Delete(SrvDeleteRequest request, string fileName)
        {
            var response = new SrvDeleteResponse {
                RequestId = request.RequestId
            };

            await Task.Delay(1000);
            return response;
        }
    }
}
