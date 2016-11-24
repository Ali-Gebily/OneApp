using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using MongoDB.Bson;
using OneApp.Modules.Authentication.Data.Model;

namespace OneApp.Modules.Authentication.Data.Repositories.Mongo
{
    public class MongoAuthenticationRepository : IAuthenticationRepository
    {
        
        private UserManager<MongoIdentityUser,ObjectId> _userManager;

        public MongoAuthenticationRepository()
        { 
            _userManager = new UserManager<MongoIdentityUser, ObjectId>(new MongoUserStore<MongoIdentityUser>());

            var provider = new DpapiDataProtectionProvider("OneApp");
            _userManager.UserTokenProvider = new DataProtectorTokenProvider<MongoIdentityUser, ObjectId>(provider.Create("UserToken"))
                  as IUserTokenProvider<MongoIdentityUser, ObjectId>;

        }


        public async Task<IdentityResult> RegisterUser(string username, string password, string email, string phoneNumber,
            bool isEmailConfirmed)
        {
            MongoIdentityUser user = new MongoIdentityUser
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
            MongoIdentityUser user = await _userManager.FindAsync(userName, password);

            return MongoModelToDTOMapper.Map(user);
        }
        public async Task<IdentityUserDTO> FindUserWithEmail(string email)
        {
            MongoIdentityUser user = await _userManager.FindByEmailAsync(email);

            return MongoModelToDTOMapper.Map(user);
        }
        public async Task<IdentityResult> ResetPassword(string userId, string token, string newPassword)
        {
            
            IdentityResult result = await _userManager.ResetPasswordAsync( ObjectId.Parse(userId), token, newPassword);
            return result;
        }
        public async Task<string> GeneratePasswordResetToken(string userId)
        {

            return await _userManager.GeneratePasswordResetTokenAsync(ObjectId.Parse(userId));
        }
        public async Task<IdentityResult> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(ObjectId.Parse(userId), currentPassword, newPassword);
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