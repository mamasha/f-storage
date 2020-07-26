namespace f_core
{
    public class SrvRequest
    {
        public string RequestId { get; set; }
        public string ServerName { get; set; }
        public int Port { get; set; }
        public string Command { get; set; }

        public const string LIST = "LIST";
        public const string UPLOAD = "UPLOAD";
        public const string DOWNLOAD = "DOWNLOAD";
        public const string DELETE = "DELETE";
    }

    public class SrvResponse
    {
        public string RequestId { get; set; }
        public string Error { get; set; }
    }

    public class SrvListRequest : SrvRequest
    {
        public int PageSize { get; set; }

        public SrvListRequest() { Command = LIST; }
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

        public SrvUploadRequest() { Command = UPLOAD; }
    }

    public class SrvUploadResponse : SrvResponse
    { }

    public class SrvDownloadRequest : SrvRequest
    {
        public string FineName { get; set; }
        public string LocalPath { get; set; }

        public SrvDownloadRequest() { Command = DOWNLOAD; }
    }

    public class SrvDownloadResponse : SrvResponse
    {
        public long FileSize { get; set; }
    }

    public class SrvDeleteRequest : SrvRequest
    {
        public string FileName { get; set; }

        public SrvDeleteRequest() { Command = DELETE; }
    }

    public class SrvDeleteResponse : SrvResponse
    { }
}
