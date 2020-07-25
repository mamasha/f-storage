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
        Task<UserInfo[]> List();
        Task Create(UserInfo request);
        Task Update(UserInfo request);
        Task Delete(UserInfo request);
    }
}
