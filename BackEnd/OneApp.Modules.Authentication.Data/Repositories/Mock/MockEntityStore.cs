// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneApp.Modules.Authentication.Data.Repositories.Base;

namespace OneApp.Modules.Authentication.Data.Repositories.Mock
{
    /// <summary>
    ///     Memory based IIdentityEntityStore that allows query/manipulation of a TEntity set
    /// </summary>
    /// <typeparam name="TEntity">Concrete entity type, i.e .User</typeparam>
    internal class MockEntityStore<TEntity, Tkey> where TEntity : class, IUniqueEntity<Tkey>
    {
        static MockEntityStore()
        {
            _DbEntitySet = new List<TEntity>();
        }


        /// <summary>
        ///     Used to query the entities
        /// </summary>
        public IQueryable<TEntity> EntitySet
        {
            get { return DbEntitySet.Select(x => x).AsQueryable(); }
        }

        public static List<TEntity> _DbEntitySet { get; private set; }


        /// <summary>
        ///     EntitySet for this store
        /// </summary>
        public List<TEntity> DbEntitySet { get { return _DbEntitySet; } private set { _DbEntitySet = value; } }

        /// <summary>
        ///     FindAsync an entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<TEntity> GetByIdAsync(Tkey id)
        { 
            return Task.FromResult(DbEntitySet.Find(e => e.Id as object == id as object));
        }

        /// <summary>
        ///     Insert an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Create(TEntity entity)
        {
            DbEntitySet.Add(entity);
        }

        /// <summary>
        ///     Mark an entity for deletion
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            DbEntitySet.Remove(entity);
        }


    }
}