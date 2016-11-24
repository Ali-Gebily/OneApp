// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace OneApp.Modules.Authentication.Data.Repositories.Mock
{
    /// <summary>
    ///     Memory based implementation
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    internal class MockRoleStore<TRole> : MockRoleStore<TRole, string, MockIdentityUserRole>, IQueryableRoleStore<TRole>
        where TRole : MockIdentityRole, new()
    {
        
 
    }

    /// <summary>
    ///     Memory based implementation
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TUserRole"></typeparam>
    internal class MockRoleStore<TRole, TKey, TUserRole> :  IQueryableRoleStore<TRole, TKey>
        where TUserRole : MockIdentityUserRole<TKey>, new()
        where TRole : MockIdentityRole<TKey, TUserRole>, new()
    {
        private MockEntityStore<TRole,TKey> _roleStore;
 
        public MockRoleStore()
        { 
            _roleStore = new MockEntityStore<TRole, TKey>( );
        }

          
        /// <summary>
        ///     Find a role by id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Task<TRole> FindByIdAsync(TKey roleId)
        {
                  return _roleStore.GetByIdAsync(roleId);
        }

        /// <summary>
        ///     Find a role by name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public Task<TRole> FindByNameAsync(string roleName)
        {
            return Task.FromResult(_roleStore.EntitySet.FirstOrDefault(u => u.Name.ToUpper() == roleName.ToUpper()));
        }

        /// <summary>
        ///     Insert an entity
        /// </summary>
        /// <param name="role"></param>
        public virtual async Task CreateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            _roleStore.Create(role);
        }

        /// <summary>
        ///     Mark an entity for deletion
        /// </summary>
        /// <param name="role"></param>
        public virtual async Task DeleteAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            _roleStore.Delete(role);
        }

        /// <summary>
        ///     Update an entity
        /// </summary>
        /// <param name="role"></param>
        public virtual async Task UpdateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            //the object is already updated in memory
        }

        public void Dispose()
        {
        }

        /// <summary>
        ///     Returns an IQueryable of users
        /// </summary>
        public IQueryable<TRole> Roles
        {
            get { return _roleStore.EntitySet; }
        }

    }
}