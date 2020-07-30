using f_core;
using NUnit.Framework;
using System.Threading.Tasks;

namespace f_nunit
{
    public class IntegrationTests
    {
        private static readonly UserInfo _goga = new UserInfo {
            UserName = "Goga",
            Password = "aabbcc",
            Folder = "goga-root"
        };

        class DummyUsers : IUsersDal
        {
            public Task<UserInfo> Get(string userName) { return Task.FromResult(_goga); }
            public Task<UserInfo[]> List() { return Task.FromResult(new[] {_goga}); }
            public Task Create(UserInfo user) { return Task.CompletedTask; }
            public Task Update(UserInfo user) { return Task.CompletedTask; }
            public Task Delete(UserInfo user) { return Task.CompletedTask; }
            public void Close() { }
        }

        [Test]
        public async Task RunMajorUseCasesWithDummyUser()
        {
            var config = new FConfig() { TcpPort = 0 };
            var dummyUsers = new DummyUsers();

            var fserver = new FServer(Logger.Null, config, dummyUsers);
            IUserManagement users = fserver;

            await users.Create(_goga);
            await users.Update(_goga);

            var all = await users.List();
            Assert.That(all, Is.Not.Empty);

            var fclient = await FClient.New(fserver.ServerName, fserver.Port, "goga", "aabbcc");

            await fclient.Upload("f-config.goga.json", "f-config.json");
            await fclient.Upload("NUnit3.TestAdapter.dll.goga", "NUnit3.TestAdapter.dll");

            await fclient.Download("f-config.goga.json", "f-config.copy.json");
            await fclient.Download("NUnit3.TestAdapter.dll.goga", "NUnit3.TestAdapter.dll.copy");

            var list = await fclient.ListFiles();
            Assert.That(list, Is.Not.Empty);

            await fclient.Delete("f-config.goga.json");

            await users.Delete(_goga);
        }

        [Test]
        public async Task RunMajorUseCasesWith()
        {
            var config = new FConfig() { TcpPort = 0 };

            var fserver = new FServer(Logger.Null, config);
            IUserManagement users = fserver;

            await users.Create(_goga);
            await users.Update(_goga);

            var all = await users.List();
            Assert.That(all, Is.Not.Empty);

            var fclient = await FClient.New(fserver.ServerName, fserver.Port, "goga", "aabbcc");

            await fclient.Upload("f-config.goga.json", "f-config.json");
            await fclient.Upload("NUnit3.TestAdapter.dll.goga", "NUnit3.TestAdapter.dll");

            await fclient.Download("f-config.goga.json", "f-config.copy.json");
            await fclient.Download("NUnit3.TestAdapter.dll.goga", "NUnit3.TestAdapter.dll.copy");

            var list = await fclient.ListFiles();
            Assert.That(list, Is.Not.Empty);

            await fclient.Delete("f-config.goga.json");

            await users.Delete(_goga);
        }
    }
}