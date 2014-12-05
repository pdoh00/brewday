using brewday.auth.endpoint.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace brewday.auth.endpoint
{
    public interface IAuthRepository
    {
        Task<User> FindUser(string userName, string password);
        Task<IdentityResult> RegisterUser(User user, string password);
        Task<IdentityResult> ChangePassword(string id, string oldPassword, string newPassword);
    }
}
