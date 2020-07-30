using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace f_core
{
    public interface IUsersDal
    {
        Task<UserInfo> Get(string userName);
        Task<UserInfo[]> List();
        Task Create(UserInfo user);
        Task Update(UserInfo user);
        Task Delete(UserInfo user);
        void Close();
    }

    class UsersDal : IUsersDal
    {
        private readonly ILogger _log;
        private readonly SqliteConnection _sqlite;
        private readonly string _tableName;

        public static IUsersDal New(ILogger log, FConfig config)
        {
            return
                new UsersDal(log, config);
        }

        private static void ensureUsersTable(FConfig config, SqliteConnection sqlite)
        {
            var makeCmd = sqlite.CreateCommand();
            makeCmd.CommandText = $@"
                CREATE TABLE IF NOT EXISTS {config.Sqlite.UsersName}(
                    username TEXT NOT NULL COLLATE NOCASE PRIMARY KEY,
                    password TEXT NOT NULL,
                    folder TEXT NOT NULL
                )";

            makeCmd.ExecuteNonQuery();
        }

        private UsersDal(ILogger log, FConfig config)
        {
            var csb = new SqliteConnectionStringBuilder {
                DataSource = $"./{config.Sqlite.DbName}"
            };

            var sqlite = new SqliteConnection(csb.ConnectionString);
            sqlite.Open();

            ensureUsersTable(config, sqlite);
            log.Info("sqlite", $"'{config.Sqlite.DbName}' is connected to");

            _log = log;
            _sqlite = sqlite;
            _tableName = config.Sqlite.UsersName;
        }

        private UserInfo getUserInfo(SqliteDataReader reader)
        {
            var userName = reader.GetString(0);
            var password = reader.GetString(1);
            var folder = reader.GetString(2);

            return new UserInfo {
                UserName = userName,
                Password = password,
                Folder = folder
            };
        }

        async Task<UserInfo> IUsersDal.Get(string userName)
        {
            var cmd = _sqlite.CreateCommand();
            cmd.CommandText = $"SELECT username, password, folder FROM {_tableName} WHERE username=@username";
            cmd.Parameters.Add(new SqliteParameter("@username", userName));

            try
            {
                var list = new List<UserInfo>();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        list.Add(getUserInfo(reader));
                    }
                }

                if (list.Count == 0)
                    return null;

                return list[0];
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to select a user '{userName}'. ({ex.Message})");
            }
        }

        async Task<UserInfo[]> IUsersDal.List()
        {
            var cmd = _sqlite.CreateCommand();
            cmd.CommandText = $"SELECT username, password, folder FROM {_tableName}";

            try
            {
                var list = new List<UserInfo>();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        list.Add(getUserInfo(reader));
                    }
                }

                return list.ToArray();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to list users. ({ex.Message})");
            }
        }

        async Task IUsersDal.Create(UserInfo user)
        {
            using (var transaction = _sqlite.BeginTransaction())
            {
                var cmd = _sqlite.CreateCommand();
                cmd.CommandText = $"INSERT INTO {_tableName} VALUES(@username, @password, @folder)";
                cmd.Parameters.Add(new SqliteParameter("@username", user.UserName));
                cmd.Parameters.Add(new SqliteParameter("@password", user.Password));
                cmd.Parameters.Add(new SqliteParameter("@folder", user.Folder));

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Failed to create a user '{user.UserName}' ({ex.Message})");
                }
            }
        }

        async Task IUsersDal.Update(UserInfo user)
        {
            using (var transaction = _sqlite.BeginTransaction())
            {
                var cmd = _sqlite.CreateCommand();
                cmd.CommandText = $"UPDATE {_tableName} SET password=@password, folder=@folder WHERE username=@username";
                cmd.Parameters.Add(new SqliteParameter("@username", user.UserName));
                cmd.Parameters.Add(new SqliteParameter("@password", user.Password));
                cmd.Parameters.Add(new SqliteParameter("@folder", user.Folder));

                try
                {
                    var count = await cmd.ExecuteNonQueryAsync();
                    transaction.Commit();

                    if (count == 0)
                        throw new ApplicationException($"User '{user.UserName}' is not found");
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Failed to update a user '{user.UserName}' ({ex.Message})");
                }
            }
        }

        async Task IUsersDal.Delete(UserInfo user)
        {
            using (var transaction = _sqlite.BeginTransaction())
            {
                var cmd = _sqlite.CreateCommand();
                cmd.CommandText = $"DELETE FROM {_tableName} WHERE username=@username";
                cmd.Parameters.Add(new SqliteParameter("@username", user.UserName));

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Failed to delete a user '{user.UserName}' ({ex.Message})");
                }
            }
        }

        void IUsersDal.Close()
        {
            _sqlite.Close();
        }
    }
}
