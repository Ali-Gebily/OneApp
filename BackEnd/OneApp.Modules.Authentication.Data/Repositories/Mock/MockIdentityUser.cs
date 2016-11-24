// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using OneApp.Modules.Authentication.Data.Repositories.Base;

namespace OneApp.Modules.Authentication.Data.Repositories.Mock
{
    /// <summary>
    ///     Default Memory IUser implementation
    /// </summary>
    internal class MockIdentityUser : MockIdentityUser<string, MockIdentityUserLogin, MockIdentityUserRole, MockIdentityUserClaim>, IUser
    {
        /// <summary>
        ///     Constructor which creates a new Guid for the Id
        /// </summary>
        public MockIdentityUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Constructor that takes a userName
        /// </summary>
        /// <param name="userName"></param>
        public MockIdentityUser(string userName)
            : this()
        {
            UserName = userName;
        }
    }

    /// <summary>
    ///     Default Memory IUser implementation
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLogin"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TClaim"></typeparam>
    internal class MockIdentityUser<TKey, TLogin, TRole, TClaim> : IUniqueEntity<TKey>,  IUser<TKey>
        where TLogin : MockIdentityUserLogin<TKey>
        where TRole : MockIdentityUserRole<TKey>
        where TClaim : MockIdentityUserClaim<TKey>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public MockIdentityUser()
        {
            Claims = new List<TClaim>();
            Roles = new List<TRole>();
            Logins = new List<TLogin>();
        }

        /// <summary>
        ///     Email
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        ///     True if the email is confirmed, default is false
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        ///     The salted/hashed form of the user password
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        ///     A random value that should change whenever a users credentials have changed (password changed, login removed)
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        ///     PhoneNumber for the user
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        ///     True if the phone number is confirmed, default is false
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        ///     Is two factor enabled for the user
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        ///     DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        ///     Is lockout enabled for this user
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        ///     Used to record failures for the purposes of lockout
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        ///     Navigation property for user roles
        /// </summary>
        public virtual ICollection<TRole> Roles { get; private set; }

        /// <summary>
        ///     Navigation property for user claims
        /// </summary>
        public virtual ICollection<TClaim> Claims { get; private set; }

        /// <summary>
        ///     Navigation property for user logins
        /// </summary>
        public virtual ICollection<TLogin> Logins { get; private set; }

        /// <summary>
        ///     User ID (Primary Key)
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        ///     User name
        /// </summary>
        public virtual string UserName { get; set; }
    }
}