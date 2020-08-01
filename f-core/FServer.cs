using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace f_core
{
    interface IServer
    {
        Task Ping(SrvPingRequest request, ITcpOp tcpOp);
        Task ListFiles(SrvListRequest request, ITcpOp tcpOp);
        Task UploadFile(SrvUploadRequest request, ITcpOp tcpOp);
        Task DownloadFile(SrvDownloadRequest request, ITcpOp tcpOp);
        Task DeleteFile(SrvDeleteRequest request, ITcpOp tcpOp);
    }

    public class FServer : IDisposable, IServer, IUserManagement
    {
        private readonly ILogger _log;
        private readonly FConfig _config;
        private readonly IUsersDal _users;
        private readonly ITcpAcceptor _tcpAcceptor;

        public FServer(ILogger log, FConfig config, IUsersDal usersDal = null)
        {
            var tcpAcceptor = TcpAcceptor.New(config, log, this);
            var users = usersDal ?? UsersDal.New(log, config);

            _log = log;
            _config = config;
            _tcpAcceptor = tcpAcceptor;
            _users = users;
        }

        private TSrvResponse makeResponse<TSrvResponse>(SrvRequest request)
            where TSrvResponse : SrvResponse, new()
        {
            return new TSrvResponse {
                TrackingId = request.TrackingId
            };
        }

        private void validate(SrvRequest request)
        { }

        private async Task<UserInfo> getUser(SrvRequest request)
        {
            var user = await _users.Get(request.UserName);
            return user;
        }

        private void authenticate(UserInfo user, SrvRequest request)
        {
            if (user == null || user.Password != request.Password)
                throw new ApplicationException($"Wrong user '{request.UserName}' or password");
        }

        private string makeFilePath(UserInfo user, string fileName)
        {
            fileName = Path.GetFileName(fileName);      // remove any path in name
            var path = Path.Combine(user.Folder, fileName);

            return path;
        }

        void IDisposable.Dispose()
        {
            _users.Close();
            _log.Stop();
        }

        async Task IServer.Ping(SrvPingRequest request, ITcpOp tcpOp)
        {
            validate(request);

            var user = await getUser(request);
            authenticate(user, request);

            var response = makeResponse<SrvPongResponse>(request);
            await tcpOp.Write(response);
        }

        async Task IServer.ListFiles(SrvListRequest request, ITcpOp tcpOp)
        {
            _log.Info(request.TrackingId, request);

            validate(request);

            var user = await getUser(request);
            authenticate(user, request);

            Directory.CreateDirectory(user.Folder);

            var fileList = Directory
                .GetFiles(user.Folder)
                .Select(path => Path.GetFileName(path))
                .ToArray();

            var response = makeResponse<SrvListResponse>(request);
            response.FileNames = fileList;
            await tcpOp.Write(response);

            _log.Info(request.TrackingId, response);
        }

        async Task IServer.UploadFile(SrvUploadRequest request, ITcpOp tcpOp)
        {
            _log.Info(request.TrackingId, request);

            validate(request);

            var user = await getUser(request);
            authenticate(user, request);

            var dstPath = makeFilePath(user, request.FileName);
            Directory.CreateDirectory(user.Folder);

            var file = File.Open(dstPath, FileMode.Create);

            var response = makeResponse<SrvUploadResponse>(request);
            await tcpOp.Write(response);

            await tcpOp.ReadBytesTo(file, request.FileSize);

            file.Flush();
            file.Close();

            await tcpOp.Write(response);

            _log.Info(request.TrackingId, response);
        }

        async Task IServer.DownloadFile(SrvDownloadRequest request, ITcpOp tcpOp)
        {
            _log.Info(request.TrackingId, request);

            validate(request);

            var user = await getUser(request);
            authenticate(user, request);

            var srcPath = makeFilePath(user, request.FileName);

            if (!File.Exists(srcPath))
                throw new ApplicationException($"Can't find '{request.FileName}'");

            var file = File.Open(srcPath, FileMode.Open, FileAccess.Read);

            var response = makeResponse<SrvDownloadResponse>(request);
            response.FileSize = file.Length;
            await tcpOp.Write(response);

            await tcpOp.WriteBytesFrom(file, file.Length);

            file.Close();

            _log.Info(request.TrackingId, response);
        }

        async Task IServer.DeleteFile(SrvDeleteRequest request, ITcpOp tcpOp)
        {
            _log.Info(request.TrackingId, request);

            validate(request);

            var user = await getUser(request);
            authenticate(user, request);

            var path = makeFilePath(user, request.FileName);

            if (!File.Exists(path))
                throw new ApplicationException($"'{request.FileName}' is not found");

            File.Delete(path);

            var response = makeResponse<SrvDeleteResponse>(request);
            await tcpOp.Write(response);

            _log.Info(request.TrackingId, response);
        }

        async Task<string[]> IUserManagement.List()
        {
            var queue =
                from user in await _users.List()
                select $"{user.UserName}: {user.Folder}";

            var list = queue.ToArray();

            _log.Info("fserver.gui", new { list.Length });

            return list;
        }

        async Task IUserManagement.Create(UserInfo user)
        {
            await _users.Create(user);

            _log.Info("fserver.gui", $"User '{user.UserName}' is created");
        }

        async Task IUserManagement.Update(UserInfo user)
        {
            await _users.Update(user);

            _log.Info("fserver.gui", $"User '{user.UserName}' is updated");
        }

        async Task IUserManagement.Delete(UserInfo user, bool withFiles)
        {
            await _users.Delete(user);

            if (withFiles)
                Directory.Delete(user.Folder, withFiles);

            _log.Info("fserver.gui", $"User '{user.UserName}' is deleted (withFiles={withFiles})");
        }

        public string ServerName { get { return _tcpAcceptor.ServerName; } }
        public int Port { get { return _tcpAcceptor.Port; } }

        public void Close()
        {
            try
            {
                _users.Close();
                _tcpAcceptor.Close();

                Thread.Sleep(_config.Server.DelayBeforeCloseMilliseconds);

                _log.Stop();
            }
            catch (Exception)
            { }
        }
    }
}
