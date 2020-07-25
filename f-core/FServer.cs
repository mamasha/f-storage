using System.Threading.Tasks;

namespace f_core
{
    public class FConfig
    { }

    public class FServer : IStorage, IUserManagement
    {
        public FServer(FConfig config)
        { }

        async Task<SrvListResponse> IStorage.ListFiles(SrvListRequest request)
        {
            var response = new SrvListResponse {
                RequestId = request.RequestId,
                FileNames = new string[] {"foo.txt", "boo.txt"}
            };

            await Task.Delay(1000);
            return response;
        }

        async Task<SrvUploadResponse> IStorage.Upload(SrvUploadRequest request)
        {
            var response = new SrvUploadResponse {
                RequestId = request.RequestId
            };

            await Task.Delay(1000);
            return response;
        }

        async Task<SrvDownloadResponse> IStorage.Download(SrvDownloadRequest request, string fileName, string dstFolder)
        {
            var response = new SrvDownloadResponse {
                RequestId = request.RequestId
            };

            await Task.Delay(1000);
            return response;
        }

        async Task<SrvDeleteResponse> IStorage.Delete(SrvDeleteRequest request, string fileName)
        {
            var response = new SrvDeleteResponse {
                RequestId = request.RequestId
            };

            await Task.Delay(1000);
            return response;
        }

        async Task<UserInfo[]> IUserManagement.List()
        {
            var list = new UserInfo[0];
            return list;
        }

        async Task IUserManagement.Create(UserInfo request)
        { }

        async Task IUserManagement.Update(UserInfo request)
        { }

        async Task IUserManagement.Delete(UserInfo request)
        { }
    }
}
