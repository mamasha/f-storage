using System.Threading.Tasks;

namespace f_core
{
    public interface IStorage
    {
        Task<SrvListResponse> ListFiles(SrvListRequest request);
        Task<SrvUploadResponse> Upload(SrvUploadRequest request);
        Task<SrvDownloadResponse> Download(SrvDownloadRequest request, string fileName, string dstFolder);
        Task<SrvDeleteResponse> Delete(SrvDeleteRequest request, string fileName);
    }
}
