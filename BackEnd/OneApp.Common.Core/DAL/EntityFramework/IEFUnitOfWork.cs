using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Common.Core.DAL.EntityFramework
{
    public interface IEFUnitOfWork<TContext> 
    {
        /// <summary>
        /// Sends your changes to the database and executes them, 
        /// then if you are not in transactioncope, it persists data
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        void BeginTransaction();

        void Rollback();

        int Commit();
        void Dispose();

        IEntityRepository<T> Repository<T>() where T : class;
    }
}
