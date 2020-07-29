using System.Threading.Tasks;

namespace f_core
{
    public class UserInfo
    { 
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Folder { get; set; }
    }

    public interface IUserManagement
    {
        Task<string[]> List();
        Task Create(UserInfo user);
        Task Update(UserInfo user);
        Task Delete(UserInfo user);
    }
}
