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

namespace OneApp.Modules.Authentication.Data.Repositories.EntityFramework
{
    public class EFAuthenticationRepository : IAuthenticationRepository
    {
        private EFIdentityDbContext _ctx;

        private UserManager<IdentityUser> _userManager;

        public EFAuthenticationRepository()
        {
            _ctx = new EFIdentityDbContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
            var provider = new DpapiDataProtectionProvider("OneApp");
            _userManager.UserTokenProvider = new DataProtectorTokenProvider<IdentityUser, string>(provider.Create("UserToken"))
                as IUserTokenProvider<IdentityUser, string>;
        }

        public async Task<IdentityResult> RegisterUser(string username, string password, string email, string phoneNumber,
            bool isEmailConfirmed)
        {
            IdentityUser user = new IdentityUser
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
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return EFToDTOMapper.Map(user);
        }
        public async Task<IdentityUserDTO> FindUserWithEmail(string email)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(email);

            return EFToDTOMapper.Map(user);
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
            _ctx.Dispose();
            _userManager.Dispose();

        }

        public async Task<List<IdentityUserDTO>> FindUsers(string usernamePart)
        {
           var list= _ctx.Users.Where(u => u.UserName.ToLower().Contains(usernamePart.ToLower()));

            List<IdentityUserDTO> usersDtos = new List<IdentityUserDTO>();
            foreach (var item in list)
            {
                usersDtos.Add(EFToDTOMapper.Map(item));
            }

            return usersDtos ;
        }

        public async Task<List<IdentityRoleDTO>> GetRoles()
        {

            var list = _ctx.Roles;

            List<IdentityRoleDTO> RolesDtos = new List<IdentityRoleDTO>();
            foreach (var item in list)
            {
                RolesDtos.Add(EFToDTOMapper.Map(item));
            }

            return RolesDtos;
        }
    }
}