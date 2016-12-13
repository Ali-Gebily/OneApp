using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Common.Core.DAL.Mock
{
    public interface IMockUnitOfWork
    {
        int SaveChanges();
        IEntityRepository<T> Repository<T>() 
            where T : class, IUniqueMockEntity;
    }
}
