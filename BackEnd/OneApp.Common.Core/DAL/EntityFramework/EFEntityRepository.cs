using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OneApp.Common.Core.DAL;

namespace OneApp.Common.Core.DAL.EntityFramework
{
    public class EFEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : class
    {
        public DbContext Context { get; private set; }

        private DbSet<TEntity> dbSet;

        public EFEntityRepository(DbContext context)
        {
            this.Context = context;
            this.dbSet = Context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetList(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
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
            dbSet.Add(entity);
        }

        public virtual void DeleteOne(Expression<Func<TEntity, bool>> filter = null)
        {
            TEntity entityToDelete = dbSet.FirstOrDefault(filter);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

       
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.FirstOrDefault(filter);
        }
    }
}