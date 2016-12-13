using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Common.Core.DAL.Mock
{
    internal class MockEntityRepository<TEntity> : IEntityRepository<TEntity>, IMockSavableEntityRepository

        where TEntity : class, IUniqueMockEntity
    {

        private static List<TEntity> _savedList = new List<TEntity>();
        private List<TEntity> _insertedButNotSavedList = new List<TEntity>();
        private List<TEntity> _deletedButNotSavedList = new List<TEntity>();
        public MockEntityRepository()
        {

        }
        public virtual IEnumerable<TEntity> GetList(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _savedList.AsQueryable<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }


        public virtual void Insert(TEntity entity)
        {
            _insertedButNotSavedList.Add(entity);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            _deletedButNotSavedList.Add(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            // entityToUpdate is updated by reference
        }

        public int SaveChanges()
        {
            int affected = 0;
            foreach (var item in _deletedButNotSavedList)
            {
                _savedList.Remove(item);
                affected++;
            }

            foreach (var item in _insertedButNotSavedList)
            {
                item.SetPrimaryKey();
                affected++;
            }
            _savedList.AddRange(_insertedButNotSavedList);

            _deletedButNotSavedList.Clear();
            _insertedButNotSavedList.Clear();
            return affected;
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return _savedList.AsQueryable().FirstOrDefault(filter);
        }
    }
}