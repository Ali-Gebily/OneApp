// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

namespace OneApp.Modules.Authentication.Data.Model
{
    /// <summary>
    ///     EntityType that represents a user belonging to a role
    /// </summary>
    public class IdentityUserRoleDTO : IdentityUserRoleDTO<string>
    {
    }

    /// <summary>
    ///     EntityType that represents a user belonging to a role
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class IdentityUserRoleDTO<TKey>
    {
        /// <summary>
        ///     UserId for the user that is in the role
        /// </summary>
        public virtual TKey UserId { get; set; }

        /// <summary>
        ///     RoleId for the role
        /// </summary>
        public virtual TKey RoleId { get; set; }
    }
}