using System.IO;

namespace f_core
{
    public class FConfig
    {
        public class ServerSide
        {
            public string RootStorageFolder { get; set; } = ".";
            public string LogFileName { get; set; } = "f-server.x.log";
            public int DelayBeforeClose { get; set; } = 1000;
        }

        public class ClientSide
        { }

        public int FAILED_TO_ACCEPT_TCP_RETRY_TIMEOUT { get; set; } = 2000;
        public int TcpPort { get; set; }

        public ServerSide Server { get; set; } = new ServerSide();
        public ClientSide Client { get; set; } = new ClientSide();

        public static FConfig LoadFrom(string path)
        {
            var jConfig = File.ReadAllText(path);
            var config = jConfig.Parse(new FConfig());

            return config;
        }
    }
}
