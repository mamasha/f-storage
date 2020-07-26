using System.Threading.Tasks;

namespace f_core
{
    public class FConfig
    {
        public string RootStorageFolder { get; set; }
    }

    public interface IServer : IUserManagement
    {
        Task ProcessRequest(ITcpOp tcp);
    }

    public class FServer : IServer
    {
        public FServer(FConfig config)
        { }

        private async Task listFiles(SrvListRequest request, ITcpOp tcp)
        {
            var response = new SrvListResponse {
                RequestId = request.RequestId,
                FileNames = new string[] {"foo.txt", "boo.txt"}
            };

            await tcp.WriteString(response.ToJson());
        }

        private async Task uploadFile(SrvUploadRequest request, ITcpOp tcp)
        {
            var response = new SrvUploadResponse {
                RequestId = request.RequestId
            };

            await tcp.WriteString(response.ToJson());
        }

        private async Task downloadFile(SrvDownloadRequest request, ITcpOp tcp)
        {
            var response = new SrvDownloadResponse {
                RequestId = request.RequestId
            };

            await tcp.WriteString(response.ToJson());
        }

        private async Task deleteFile(SrvDeleteRequest request, ITcpOp tcp)
        {
            var response = new SrvDeleteResponse {
                RequestId = request.RequestId
            };

            await tcp.WriteString(response.ToJson());
        }

        async Task IServer.ProcessRequest(ITcpOp tcp)
        {
            var jRequest = await tcp.ReadString();
            var request = jRequest.Parse<SrvRequest>();

            switch (request.Command)
            {
                case SrvRequest.LIST:
                    await listFiles(jRequest.Parse<SrvListRequest>(), tcp);
                    break;

                case SrvRequest.UPLOAD:
                    await uploadFile(jRequest.Parse<SrvUploadRequest>(), tcp);
                    break;

                case SrvRequest.DOWNLOAD:
                    await downloadFile(jRequest.Parse<SrvDownloadRequest>(), tcp);
                    break;

                case SrvRequest.DELETE:
                    await deleteFile(jRequest.Parse<SrvDeleteRequest>(), tcp);
                    break;
            }
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
