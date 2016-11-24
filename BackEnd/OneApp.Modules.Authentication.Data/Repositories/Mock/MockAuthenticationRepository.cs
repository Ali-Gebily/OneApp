using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using OneApp.Modules.Authentication.Data.Model;

namespace OneApp.Modules.Authentication.Data.Repositories.Mock
{
    public class MockAuthenticationRepository : IAuthenticationRepository
    {
       
        private   UserManager<MockIdentityUser> _userManager;

        public MockAuthenticationRepository()
        { 
            _userManager = new UserManager<MockIdentityUser>(new MockUserStore<MockIdentityUser>());

            var provider = new DpapiDataProtectionProvider("OneApp");
            _userManager.UserTokenProvider = new DataProtectorTokenProvider<MockIdentityUser, string>(provider.Create("UserToken"))
                  as IUserTokenProvider<MockIdentityUser, string>;
             
        } 

        public async Task<IdentityResult> RegisterUser(string username, string password, string email, string phoneNumber,
            bool isEmailConfirmed)
        {
            MockIdentityUser user = new MockIdentityUser
            {
                UserName = username,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = isEmailConfirmed
            };
            var result = await _userManager.CreateAsync(user, password);

            return result;
        }

        public async Task<IdentityUserDTO> FindUser(string userName, string password)
        {
            MockIdentityUser user = await _userManager.FindAsync(userName, password);

            return MemoryModelToDTOMapper.Map(user);
        }
        public async Task<IdentityUserDTO> FindUserWithEmail(string email)
        {
            MockIdentityUser user = await _userManager.FindByEmailAsync(email);

            return MemoryModelToDTOMapper.Map(user);
        }
        public async Task<IdentityResult> ResetPassword(string userId, string token, string newPassword)
        {
            IdentityResult result = await _userManager.ResetPasswordAsync(userId, token, newPassword);
            return result;
        }
        public async Task<string> GeneratePasswordResetToken(string userId)
        {

            return await _userManager.GeneratePasswordResetTokenAsync(userId);
        }
        public async Task<IdentityResult> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(userId, currentPassword, newPassword);
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