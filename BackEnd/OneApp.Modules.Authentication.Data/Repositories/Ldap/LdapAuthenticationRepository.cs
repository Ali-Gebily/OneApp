using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using OneApp.Modules.Authentication.Data.Model;

namespace OneApp.Modules.Authentication.Data.Repositories.Ldap
{
    public class LdapAuthenticationRepository : IAuthenticationRepository
    {
        public Task<IdentityResult> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUserDTO> FindUserWithEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUserDTO> FindUser(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<string> GeneratePasswordResetToken(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> RegisterUser(string username, string password, string email, string phoneNumber, bool isEmailConfirmed)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ResetPassword(string userId, string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<List<IdentityUserDTO>> FindUsers(string usernamePart)
        {
            throw new NotImplementedException();
        }

        public Task<List<IdentityRoleDTO>> GetRoles()
        {
            throw new NotImplementedException();
        }
    }
}
