// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using OneApp.Modules.Authentication.Data.Repositories.Base;

namespace OneApp.Modules.Authentication.Data.Repositories.Mock
{
    /// <summary>
    ///     Represents a Role entity
    /// </summary>
    internal class MockIdentityRole : MockIdentityRole<string, MockIdentityUserRole>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public MockIdentityRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="roleName"></param>
        public MockIdentityRole(string roleName)
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
    internal class MockIdentityRole<TKey, TUserRole> :IUniqueEntity<TKey>,  IRole<TKey> where TUserRole : MockIdentityUserRole<TKey>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public MockIdentityRole()
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