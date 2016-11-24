// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using OneApp.Common.Core.Managers.Settings;
using OneApp.Modules.Authentication.Data.Resources;

namespace OneApp.Modules.Authentication.Data.Repositories.Mongo
{
    /// <summary>
    ///     Mongo based user store implementation that supports IUserStore, IUserLoginStore, IUserClaimStore and
    ///     IUserRoleStore
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    internal class MongoUserStore<TUser> :
        MongoUserStore<TUser, MongoIdentityRole, ObjectId, MongoIdentityUserLogin, MongoIdentityUserRole, MongoIdentityUserClaim>,
        IUserStore<TUser, ObjectId> where TUser : MongoIdentityUser
    {
        /// <summary>
        ///     Default constuctor which uses a new instance of a default EntityyDbContext
        /// </summary>
        public MongoUserStore()
        {
        }
        public void Dispose()
        {

        }

    }

    /// <summary>
    ///     Mongo based user store implementation that supports IUserStore, IUserLoginStore, IUserClaimStore and
    ///     IUserRoleStore
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TUserLogin"></typeparam>
    /// <typeparam name="TUserRole"></typeparam>
    /// <typeparam name="TUserClaim"></typeparam>
    internal class MongoUserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> :
        IUserLoginStore<TUser, TKey>,
        IUserClaimStore<TUser, TKey>,
        IUserRoleStore<TUser, TKey>,
        IUserPasswordStore<TUser, TKey>,
        IUserSecurityStampStore<TUser, TKey>,
        IQueryableUserStore<TUser, TKey>,
        IUserEmailStore<TUser, TKey>,
        IUserPhoneNumberStore<TUser, TKey>,
        IUserTwoFactorStore<TUser, TKey>,
        IUserLockoutStore<TUser, TKey>
        where TKey : IEquatable<TKey>
        where TUser : MongoIdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TRole : MongoIdentityRole<TKey, TUserRole>
        where TUserLogin : MongoIdentityUserLogin<TKey>, new()
        where TUserRole : MongoIdentityUserRole<TKey>, new()
        where TUserClaim : MongoIdentityUserClaim<TKey>, new()
    {
        private readonly IMongoCollection<TUserLogin> _logins;
        private readonly MongoEntityStore<TRole,TKey> _roleStore;
        private readonly IMongoCollection<TUserClaim> _userClaims;
        private readonly IMongoCollection<TUserRole> _userRoles;
        private MongoEntityStore<TUser,TKey> _userStore;

        /// <summary>
        ///     Constructor which takes a db context and wires up the stores with default instances using the context
        /// </summary>
        /// <param name="context"></param>
        public MongoUserStore()
        {

            AutoSaveChanges = true;
            _userStore = new MongoEntityStore<TUser,TKey>(MongoSettings.Collections.AspNetUsers);
            _roleStore = new MongoEntityStore<TRole,TKey>(MongoSettings.Collections.AspNetRoles);
            _logins = MongoManager.GetMongoDatabase().GetCollection<TUserLogin>(MongoSettings.Collections.AspNetUserLogins);
            _userClaims = MongoManager.GetMongoDatabase().GetCollection<TUserClaim>(MongoSettings.Collections.AspNetUserClaims);
            _userRoles = MongoManager.GetMongoDatabase().GetCollection<TUserRole>(MongoSettings.Collections.AspNetUserRoles);
        }
        public void Dispose()
        {

        }


        /// <summary>
        ///     If true will call SaveChanges after Create/Update/Delete
        /// </summary>
        public bool AutoSaveChanges { get; set; }

        /// <summary>
        ///     Returns an IQueryable of users
        /// </summary>
        public IQueryable<TUser> Users
        {
            get { return _userStore.EntitySet.Select(x => x).AsQueryable(); }
        }

        /// <summary>
        ///     Return the claims for a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<IList<Claim>> GetClaimsAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
        }

        /// <summary>
        ///     Add a claim to a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        /// <returns></returns>
        public virtual Task AddClaimAsync(TUser user, Claim claim)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            _userClaims.InsertOne(new TUserClaim { UserId = user.Id, ClaimType = claim.Type, ClaimValue = claim.Value });
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Remove a claim from a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        /// <returns></returns>
        public virtual async Task RemoveClaimAsync(TUser user, Claim claim)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            IEnumerable<TUserClaim> claims;
            var claimValue = claim.Value;
            var claimType = claim.Type;

            claims = user.Claims.Where(uc => uc.ClaimValue == claimValue && uc.ClaimType == claimType).ToList();

            foreach (var c in claims)
            {
                _userClaims.DeleteOne(c1 => c1.Id == c.Id);
            }
        }

        /// <summary>
        ///     Returns whether the user email is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<bool> GetEmailConfirmedAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.EmailConfirmed);
        }

        /// <summary>
        ///     Set IsConfirmed on the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Set the user email
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual Task SetEmailAsync(TUser user, string email)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.Email = email;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Get the user's email
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<string> GetEmailAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.Email);
        }

        /// <summary>
        ///     Find a user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual Task<TUser> FindByEmailAsync(string email)
        {

            return GetUserAggregateAsync(u => u.Email.ToUpper() == email.ToUpper());
        }

        /// <summary>
        ///     Returns the DateTimeOffset that represents the end of a user's lockout, any time in the past should be considered
        ///     not locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return
                Task.FromResult(user.LockoutEndDateUtc.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }

        /// <summary>
        ///     Locks a user out until the specified end date (set to a past date, to unlock a user)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="lockoutEnd"></param>
        /// <returns></returns>
        public virtual Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? (DateTime?)null : lockoutEnd.UtcDateTime;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Used to record when an attempt to access the user has failed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<int> IncrementAccessFailedCountAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        ///     Used to reset the account access count, typically after the account is successfully accessed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task ResetAccessFailedCountAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Returns the current number of failed access attempts.  This number usually will be reset whenever the password is
        ///     verified or the account is locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<int> GetAccessFailedCountAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        ///     Returns whether the user can be locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<bool> GetLockoutEnabledAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.LockoutEnabled);
        }

        /// <summary>
        ///     Sets whether the user can be locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public virtual Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Find a user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual Task<TUser> FindByIdAsync(TKey userId)
        {

            return GetUserAggregateAsync(u => u.Id.Equals(userId));
        }

        /// <summary>
        ///     Find a user by name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual Task<TUser> FindByNameAsync(string userName)
        {

            return GetUserAggregateAsync(u => u.UserName.ToUpper() == userName.ToUpper());
        }

        /// <summary>
        ///     Insert an entity
        /// </summary>
        /// <param name="user"></param>
        public virtual async Task CreateAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _userStore.Create(user);
        }
        /// <summary>
        ///     update an entity
        /// </summary>
        /// <param name="user"></param>
        public virtual async Task UpdateAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _userStore.Update(user);
        }

        /// <summary>
        ///     Mark an entity for deletion
        /// </summary>
        /// <param name="user"></param>
        public virtual async Task DeleteAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _userStore.Delete(user.Id);
        }

         

        // IUserLogin implementation

        /// <summary>
        ///     Returns the user associated with this login
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TUser> FindAsync(UserLoginInfo login)
        {

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            var provider = login.LoginProvider;
            var key = login.ProviderKey;
            var userLogin =
                  _logins.Find(l => l.LoginProvider == provider && l.ProviderKey == key).FirstOrDefault();
            if (userLogin != null)
            {
                var userId = userLogin.UserId;
                return await GetUserAggregateAsync(u => u.Id.Equals(userId));
            }
            return null;
        }

        /// <summary>
        ///     Add a login to the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            _logins.InsertOne(new TUserLogin
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider
            });
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Remove a login from a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public virtual async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            TUserLogin entry;
            var provider = login.LoginProvider;
            var key = login.ProviderKey;

            entry = user.Logins.SingleOrDefault(ul => ul.LoginProvider == provider && ul.ProviderKey == key);

            if (entry != null)
            {
                _logins.DeleteOne(e1 => e1.UserId as object == entry.UserId as object &&
                e1.LoginProvider == entry.LoginProvider && e1.ProviderKey == entry.ProviderKey
                );
            }
        }

        /// <summary>
        ///     Get the logins for a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return user.Logins.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList();
        }

        /// <summary>
        ///     Set the password hash for a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public virtual Task SetPasswordHashAsync(TUser user, string passwordHash)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Get the password hash for a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<string> GetPasswordHashAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.PasswordHash);
        }

        /// <summary>
        ///     Returns true if the user has a password set
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        /// <summary>
        ///     Set the user's phone number
        /// </summary>
        /// <param name="user"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public virtual Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Get a user's phone number
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<string> GetPhoneNumberAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.PhoneNumber);
        }

        /// <summary>
        ///     Returns whether the user phoneNumber is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        /// <summary>
        ///     Set PhoneNumberConfirmed on the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public virtual Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Add a user to a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual async Task AddToRoleAsync(TUser user, string roleName)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(OneAppIdentityResources.ValueCannotBeNullOrEmpty, "roleName");
            }
            var roleEntity = _roleStore.EntityCollection.Find(r => r.Name.ToUpper() == roleName.ToUpper()).FirstOrDefault();
            if (roleEntity == null)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,
                    OneAppIdentityResources.RoleNotFound, roleName));
            }

            var ur = new TUserRole { UserId = user.Id, RoleId = roleEntity.Id };
            _userRoles.InsertOne(ur);
        }

        /// <summary>
        ///     Remove a user from a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual async Task RemoveFromRoleAsync(TUser user, string roleName)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(OneAppIdentityResources.ValueCannotBeNullOrEmpty, "roleName");
            }
            var roleEntity = _roleStore.EntityCollection.Find(r => r.Name.ToUpper() == roleName.ToUpper()).FirstOrDefault();
            if (roleEntity != null)
            {
                var roleId = roleEntity.Id;
                var userId = user.Id;
                var userRole = _userRoles.FindOneAndDelete(r => roleId.Equals(r.RoleId) && r.UserId.Equals(userId));
            }
        }

        /// <summary>
        ///     Get the names of the roles a user is a member of
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<IList<string>> GetRolesAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
           
            var userId = user.Id;
            var query = from userRole in _userRoles.AsQueryable()
                        where userRole.UserId.Equals(userId)
                        join role in _roleStore.EntityCollection.AsQueryable() on userRole.RoleId equals role.Id
                        select role.Name;
            return query.ToList();
        }

        /// <summary>
        ///     Returns true if the user is in the named role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual async Task<bool> IsInRoleAsync(TUser user, string roleName)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(OneAppIdentityResources.ValueCannotBeNullOrEmpty, "roleName");
            }
            var role = _roleStore.EntityCollection.Find(r => r.Name.ToUpper() == roleName.ToUpper()).FirstOrDefault();
            if (role != null)
            {
                var userId = user.Id;
                var roleId = role.Id;
                return _userRoles.Count(ur => ur.RoleId.Equals(roleId) && ur.UserId.Equals(userId))> 0;
            }
            return false;
        }

        /// <summary>
        ///     Set the security stamp for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stamp"></param>
        /// <returns></returns>
        public virtual Task SetSecurityStampAsync(TUser user, string stamp)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Get the security stamp for a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<string> GetSecurityStampAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.SecurityStamp);
        }

        /// <summary>
        ///     Set whether two factor authentication is enabled for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public virtual Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Gets whether two factor authentication is enabled for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.TwoFactorEnabled);
        }


        /// <summary>
        /// Used to attach child entities to the User aggregate, i.e. Roles, Logins, and Claims
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected virtual async Task<TUser> GetUserAggregateAsync(Expression<Func<TUser, bool>> filter)
        {
            TKey id;
            TUser user;
            if (MongoFindByIdFilterParser.TryMatchAndGetId(filter, out id))
            {
                user = await _userStore.GetByIdAsync(id);
            }
            else
            {
                user = Users.FirstOrDefault(filter);
            }

            return user;
        }




        // We want to use FindAsync() when looking for an User.Id instead of LINQ to avoid extra 
        // database roundtrips. This class cracks open the filter expression passed by 
        // UserStore.FindByIdAsync() to obtain the value of the id we are looking for 
        private static class MongoFindByIdFilterParser
        {
            // expression pattern we need to match
            private static readonly Expression<Func<TUser, bool>> Predicate = u => u.Id.Equals(default(TKey));
            // method we need to match: Object.Equals() 
            private static readonly MethodInfo EqualsMethodInfo = ((MethodCallExpression)Predicate.Body).Method;
            // property access we need to match: User.Id 
            private static readonly MemberInfo UserIdMemberInfo = ((MemberExpression)((MethodCallExpression)Predicate.Body).Object).Member;

            public static bool TryMatchAndGetId(Expression<Func<TUser, bool>> filter, out TKey id)
            {
                // default value in case we can’t obtain it 
                id = default(TKey);

                // lambda body should be a call 
                if (filter.Body.NodeType != ExpressionType.Call)
                {
                    return false;
                }

                // actually a call to object.Equals(object)
                var callExpression = (MethodCallExpression)filter.Body;
                if (callExpression.Method != EqualsMethodInfo)
                {
                    return false;
                }
                // left side of Equals() should be an access to User.Id
                if (callExpression.Object == null
                    || callExpression.Object.NodeType != ExpressionType.MemberAccess
                    || ((MemberExpression)callExpression.Object).Member != UserIdMemberInfo)
                {
                    return false;
                }

                // There should be only one argument for Equals()
                if (callExpression.Arguments.Count != 1)
                {
                    return false;
                }

                MemberExpression fieldAccess;
                if (callExpression.Arguments[0].NodeType == ExpressionType.Convert)
                {
                    // convert node should have an member access access node
                    // This is for cases when primary key is a value type
                    var convert = (UnaryExpression)callExpression.Arguments[0];
                    if (convert.Operand.NodeType != ExpressionType.MemberAccess)
                    {
                        return false;
                    }
                    fieldAccess = (MemberExpression)convert.Operand;
                }
                else if (callExpression.Arguments[0].NodeType == ExpressionType.MemberAccess)
                {
                    // Get field member for when key is reference type
                    fieldAccess = (MemberExpression)callExpression.Arguments[0];
                }
                else
                {
                    return false;
                }

                // and member access should be a field access to a variable captured in a closure
                if (fieldAccess.Member.MemberType != MemberTypes.Field
                    || fieldAccess.Expression.NodeType != ExpressionType.Constant)
                {
                    return false;
                }

                // expression tree matched so we can now just get the value of the id 
                var fieldInfo = (FieldInfo)fieldAccess.Member;
                var closure = ((ConstantExpression)fieldAccess.Expression).Value;

                id = (TKey)fieldInfo.GetValue(closure);
                return true;
            }
        }
    }
}