using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Common.Core.DAL.Mock
{
    public class MockUnitOfWork : IMockUnitOfWork
    {
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public IEntityRepository<T> Repository<T>() where T : class, IUniqueMockEntity
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IEntityRepository<T>;
            }
            IEntityRepository<T> repo = new MockEntityRepository<T>();
            repositories.Add(typeof(T), repo);
            return repo;
        }

 


        public int SaveChanges()
        {
            int affected = 0;
            foreach (var item in repositories.Keys)
            {
                var repo = (repositories[item] as IMockSavableEntityRepository);
                if (repo == null) {
                    throw new Exception("entity mock repository must implement IMockSavableEntityRepository");
                }
                affected += repo.SaveChanges();
            }
            return affected;
        }
    }
}
