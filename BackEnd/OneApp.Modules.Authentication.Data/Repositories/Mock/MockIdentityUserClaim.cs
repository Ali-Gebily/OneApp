// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

namespace OneApp.Modules.Authentication.Data.Repositories.Mock
{
    /// <summary>
    ///     EntityType that represents one specific user claim
    /// </summary>
    internal class MockIdentityUserClaim : MockIdentityUserClaim<string>
    {
    }

    /// <summary>
    ///     EntityType that represents one specific user claim
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    internal class MockIdentityUserClaim<TKey>
    {
        /// <summary>
        ///     Primary key
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     User Id for the user who owns this login
        /// </summary>
        public virtual TKey UserId { get; set; }

        /// <summary>
        ///     Claim type
        /// </summary>
        public virtual string ClaimType { get; set; }

        /// <summary>
        ///     Claim value
        /// </summary>
        public virtual string ClaimValue { get; set; }
    }
}