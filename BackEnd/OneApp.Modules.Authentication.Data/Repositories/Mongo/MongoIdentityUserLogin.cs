// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using MongoDB.Bson;

namespace OneApp.Modules.Authentication.Data.Repositories.Mongo
{
    /// <summary>
    ///     Entity type for a user's login (i.e. facebook, google)
    /// </summary>
    internal class MongoIdentityUserLogin : MongoIdentityUserLogin<ObjectId>
    {
    }

    /// <summary>
    ///     Entity type for a user's login (i.e. facebook, google)
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    internal class MongoIdentityUserLogin<TKey>
    {
        /// <summary>
        ///     The login provider for the login (i.e. facebook, google)
        /// </summary>
        public virtual string LoginProvider { get; set; }

        /// <summary>
        ///     Key representing the login for the provider
        /// </summary>
        public virtual string ProviderKey { get; set; }

        /// <summary>
        ///     User Id for the user who owns this login
        /// </summary>
        public virtual TKey UserId { get; set; }
    }
}