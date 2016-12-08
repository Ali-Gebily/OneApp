using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using OneApp.Common.Core.Exceptions;
using OneApp.Modules.Authentication.Data.Model;

namespace OneApp.Modules.Authentication.Data.Repositories.Mock
{
    public class MockAuthenticationRepository : IAuthenticationRepository
    {
        static List<IdentityUserDTO> _users = new List<IdentityUserDTO>();

        static MockAuthenticationRepository()
        {
            _users.Add(new IdentityUserDTO
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "ali.mmr2008@gmail.com",
                Email = "ali.mmr2008@gmail.com",
                PhoneNumber = "201068042823",
                EmailConfirmed = true,
                PasswordHash = HashPassword("123456")

            });
        } 
         public MockAuthenticationRepository()
        {
                      
        }

        public async Task<IdentityResult> RegisterUser(string username, string password, string email, string phoneNumber,
            bool isEmailConfirmed)
        {
            _users.Add(new IdentityUserDTO {
                Id = Guid.NewGuid().ToString(),
                UserName = username,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = isEmailConfirmed,
                PasswordHash = HashPassword(password)
                  
             });
 
            return new IdentityResult();
        }
       static string HashPassword(string password)
        {
            return password;

        }
        public async Task<IdentityUserDTO> FindUser(string userName, string password)
        {
            return _users.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower() && u.PasswordHash == HashPassword(password));

        }
        public async Task<IdentityUserDTO> FindUserWithEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

        }
        public async Task<IdentityResult> ResetPassword(string userId, string token, string newPassword)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            user.PasswordHash = HashPassword(newPassword);
             return new IdentityResult() ;
        }
        public async Task<string> GeneratePasswordResetToken(string userId)
        {

            return Guid.NewGuid().ToString();
        }
        public async Task<IdentityResult> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user.PasswordHash != HashPassword(currentPassword))
            {
                throw new BusinessException("Current password is invalid");
            }
            user.PasswordHash = HashPassword(newPassword);
            return new IdentityResult();

        }
        public void Dispose()
        {

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