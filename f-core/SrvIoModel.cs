namespace f_core
{
    public class SrvRequest
    {
        public string RequestId { get; set; }
        public string ServerName { get; set; }
        public int Port { get; set; }
        public string Command { get; set; }
    }

    public class SrvResponse
    {
        public string RequestId { get; set; }
        public string Error { get; set; }
    }

    public class SrvListRequest : SrvRequest
    {
        public int PageSize { get; set; }
    }

    public class SrvListResponse : SrvResponse
    {
        public string[] FileNames { get; set; }
    }

    public class SrvUploadRequest : SrvRequest
    {
        public string FileName { get; set; }
        public string SrcPath { get; set; }
        public long FileSize { get; set; }
    }

    public class SrvUploadResponse : SrvResponse
    { }

    public class SrvDownloadRequest : SrvRequest
    {
        public string FineName { get; set; }
        public string LocalPath { get; set; }
    }

    public class SrvDownloadResponse : SrvResponse
    {
        public long FileSize { get; set; }
    }

    public class SrvDeleteRequest : SrvRequest
    {
        public string FileName { get; set; }
    }

    public class SrvDeleteResponse : SrvResponse
    { }
}
