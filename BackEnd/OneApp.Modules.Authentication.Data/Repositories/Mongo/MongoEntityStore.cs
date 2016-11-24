// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using OneApp.Common.Core.Managers.Settings;
using OneApp.Modules.Authentication.Data.Repositories.Base;

namespace OneApp.Modules.Authentication.Data.Repositories.Mongo
{
    /// <summary>
    ///     Mongo based IIdentityEntityStore that allows query/manipulation of a TEntity set
    /// </summary>
    /// <typeparam name="TEntity">Concrete entity type, i.e .User</typeparam>
    internal class MongoEntityStore<TEntity, Tkey> where
        TEntity : class , IUniqueEntity<Tkey> 
    {
        /// <summary>
        ///     Constructor that takes a Context
        /// </summary>
        /// <param name="context"></param>
        public MongoEntityStore(string collectionName)
        {
            EntityCollection = MongoManager.GetMongoDatabase().GetCollection<TEntity>(collectionName);
        }


        /// <summary>
        ///     Used to query the entities
        /// </summary>
        public IQueryable<TEntity> EntitySet
        {
            get { return EntityCollection.AsQueryable(); }
        }

        /// <summary>
        ///     EntitySet for this store
        /// </summary>
        public IMongoCollection<TEntity> EntityCollection { get; private set; }

        /// <summary>
        ///     FindAsync an entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<TEntity> GetByIdAsync(Tkey id)
        {
            return Task.FromResult(EntityCollection.Find(e =>  e.Id as object == id as object).FirstOrDefault());
        }

        /// <summary>
        ///     Insert an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Create(TEntity entity)
        {
            EntityCollection.InsertOne(entity);
        }
        /// <summary>
        ///     Insert an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            EntityCollection.ReplaceOne(doc => doc.Id as object== entity.Id as object, entity);
        }

        /// <summary>
        ///     Mark an entity for deletion
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Tkey id)
        {
            Task.FromResult(EntityCollection.DeleteOne(e => e.Id as object == id as object));
        }


    }
}