namespace f_core
{
    class SrvRequest
    {
        public string TrackingId { get; set; }
        public string ServerName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Command { get; set; }

        public const string LIST = "LIST";
        public const string UPLOAD = "UPLOAD";
        public const string DOWNLOAD = "DOWNLOAD";
        public const string DELETE = "DELETE";
    }

    class SrvResponse
    {
        public string TrackingId { get; set; }
        public string Error { get; set; }
    }

    class SrvListRequest : SrvRequest
    {
        public int PageSize { get; set; }

        public SrvListRequest() { Command = LIST; }
    }

    class SrvListResponse : SrvResponse
    {
        public string[] FileNames { get; set; }
    }

    class SrvUploadRequest : SrvRequest
    {
        public string FileName { get; set; }
        public string SrcPath { get; set; }
        public long FileSize { get; set; }

        public SrvUploadRequest() { Command = UPLOAD; }
    }

    class SrvUploadResponse : SrvResponse
    { }

    class SrvDownloadRequest : SrvRequest
    {
        public string FileName { get; set; }
        public string LocalPath { get; set; }

        public SrvDownloadRequest() { Command = DOWNLOAD; }
    }

    class SrvDownloadResponse : SrvResponse
    {
        public long FileSize { get; set; }
    }

    class SrvDeleteRequest : SrvRequest
    {
        public string FileName { get; set; }

        public SrvDeleteRequest() { Command = DELETE; }
    }

    class SrvDeleteResponse : SrvResponse
    { }
}
