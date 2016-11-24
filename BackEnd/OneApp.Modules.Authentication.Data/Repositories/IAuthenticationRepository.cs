using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OneApp.Modules.Authentication.Data.Model;

namespace OneApp.Modules.Authentication.Data.Repositories
{
    public interface IAuthenticationRepository : IDisposable
    {
        Task<IdentityResult> RegisterUser(string username, string password, string email, string phoneNumber,
            bool isEmailConfirmed);
        Task<IdentityUserDTO> FindUser(string userName, string password);
        Task<IdentityUserDTO> FindUserWithEmail(string email);
        Task<List<IdentityUserDTO>> FindUsers(string usernamePart);
        Task<IdentityResult> ResetPassword(string userId, string token, string newPassword);
        Task<string> GeneratePasswordResetToken(string id);
        Task<IdentityResult> ChangePassword(string userId, string oldPassword, string newPassword);
        Task<List<IdentityRoleDTO>> GetRoles();
    }
}