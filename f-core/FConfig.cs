using System.IO;

namespace f_core
{
    public class FConfig
    {
        public int TcpPort { get; set; }

        public ServerSide Server { get; set; } = new ServerSide();
        public ClientSide Client { get; set; } = new ClientSide();
        public SqliteDb Sqlite { get; set; } = new SqliteDb();

        public class ServerSide
        {
            public string RootStorageFolder { get; set; } = ".";
            public string LogFileName { get; set; } = "f-server.log";
            public int DelayBeforeClose { get; set; } = 1000;

            public int FAILED_TO_ACCEPT_TCP_RETRY_TIMEOUT { get; set; } = 2000;
        }

        public class ClientSide
        {
        }

        public class SqliteDb
        {
            public string DbName { get; set; } = "f-storage.db";
            public string UsersName { get; set; } = "users";
        }

        public static FConfig LoadFrom(string path)
        {
            var jConfig = File.ReadAllText(path);
            var config = jConfig.Parse(new FConfig());

            return config;
        }
    }
}
