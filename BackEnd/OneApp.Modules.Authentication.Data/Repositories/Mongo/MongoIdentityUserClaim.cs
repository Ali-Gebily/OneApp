// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using MongoDB.Bson;

namespace OneApp.Modules.Authentication.Data.Repositories.Mongo
{
    /// <summary>
    ///     EntityType that represents one specific user claim
    /// </summary>
    internal class MongoIdentityUserClaim : MongoIdentityUserClaim<ObjectId>
    {
    }

    /// <summary>
    ///     EntityType that represents one specific user claim
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    internal class MongoIdentityUserClaim<TKey>
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