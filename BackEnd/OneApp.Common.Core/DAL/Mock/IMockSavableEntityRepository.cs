using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Common.Core.DAL.Mock
{
    interface IMockSavableEntityRepository
    {
         int SaveChanges();
    }
}
