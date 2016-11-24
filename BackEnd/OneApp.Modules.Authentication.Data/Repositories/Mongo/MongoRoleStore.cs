// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;

namespace OneApp.Modules.Authentication.Data.Repositories.Mongo
{
    /// <summary>
    ///     Mongo based implementation
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    internal class MongoRoleStore<TRole> : MongoRoleStore<TRole, ObjectId, MongoIdentityUserRole>, IQueryableRoleStore<TRole, ObjectId>
        where TRole : MongoIdentityRole, new()
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public MongoRoleStore()
        {
        }

    }

    /// <summary>
    ///     Mongo based implementation
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TUserRole"></typeparam>
    internal class MongoRoleStore<TRole, TKey, TUserRole> : IQueryableRoleStore<TRole, TKey>
        where TUserRole : MongoIdentityUserRole<TKey>, new()
        where TRole : MongoIdentityRole<TKey, TUserRole>, new()
    {
        private MongoEntityStore<TRole,TKey> _roleStore;

        /// <summary>
        ///     Constructor which takes a db context and wires up the stores with default instances using the context
        /// </summary>
        /// <param name="context"></param>
        public MongoRoleStore()
        {
            _roleStore = new MongoEntityStore<TRole,TKey>(MongoSettings.Collections.AspNetRoles);
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
            _roleStore.Delete(role.Id);
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
            _roleStore.Update(role);
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