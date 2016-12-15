using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Common.Core.DAL.EntityFramework
{
    public class EFUnitOfWork<TContext> : IEFUnitOfWork<TContext>, IDisposable
        where TContext : DbContext
    {
        public TContext Context { get; private set; }
        public EFUnitOfWork(TContext context)
        {
            this.Context = context;
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }
        private DbContextTransaction _transaction;
        public void BeginTransaction()
        {
            _transaction =this.Context.Database.BeginTransaction();
        }
        public void Rollback()
        {
            _transaction.Rollback();
        }
        public void Commit()
        { 
            _transaction.Commit();
          
        }

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public IEntityRepository<T> Repository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IEntityRepository<T>;
            }
            IEntityRepository<T> repo = new EFEntityRepository<T>(this.Context);
            repositories.Add(typeof(T), repo);
            return repo;
        }

 
    }
}
