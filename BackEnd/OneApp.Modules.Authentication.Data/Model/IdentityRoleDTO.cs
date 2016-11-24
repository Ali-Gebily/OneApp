// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace OneApp.Modules.Authentication.Data.Model
{
    /// <summary>
    ///     Represents a Role entity
    /// </summary>
    public class IdentityRoleDTO : IdentityRoleDTO<string, IdentityUserRoleDTO>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public IdentityRoleDTO()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="roleName"></param>
        public IdentityRoleDTO(string roleName)
            : this()
        {
            Name = roleName;
        }
    }

    /// <summary>
    ///     Represents a Role entity
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TUserRole"></typeparam>
    public class IdentityRoleDTO<TKey, TUserRole> : IRole<TKey> where TUserRole : IdentityUserRoleDTO<TKey>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public IdentityRoleDTO()
        {
            Users = new List<TUserRole>();
        }

        /// <summary>
        ///     Navigation property for users in the role
        /// </summary>
        public virtual ICollection<TUserRole> Users { get; private set; }

        /// <summary>
        ///     Role id
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        ///     Role name
        /// </summary>
        public string Name { get; set; }
    }
}