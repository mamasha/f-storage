using System.Threading.Tasks;

namespace f_core
{
    public interface IStorage
    {
        Task<SrvListResponse> ListFiles(SrvListRequest request, ITcpOp tcp);
        Task<SrvUploadResponse> Upload(SrvUploadRequest request, ITcpOp tcp);
        Task<SrvDownloadResponse> Download(SrvDownloadRequest request, ITcpOp tcp);
        Task<SrvDeleteResponse> Delete(SrvDeleteRequest request, ITcpOp tcp);
    }
}
