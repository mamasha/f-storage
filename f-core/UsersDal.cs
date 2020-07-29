using System.Threading.Tasks;

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
        public static IUsersDal New(FConfig config)
        {
            return
                new UsersDal(config);
        }

        private UsersDal(FConfig config)
        { }

        async Task<UserInfo> IUsersDal.Get(string userName)
        {
            return new UserInfo { 
                UserName = "Goga",
                Password = "aabbcc",
                Folder = "goga"
            };
        }

        async Task<UserInfo[]> IUsersDal.List()
        {
            return new [] { new UserInfo {
                UserName = "Goga",
                Password = "aabbcc",
                Folder = "goga"
            }};
        }

        async Task IUsersDal.Create(UserInfo user)
        { }

        async Task IUsersDal.Update(UserInfo user)
        { }

        async Task IUsersDal.Delete(UserInfo user)
        { }

        void IUsersDal.Close()
        { }
    }
}
