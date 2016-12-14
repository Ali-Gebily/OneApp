using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneApp.Common.Core.Managers.Settings;

namespace OneApp.Common.Core.DAL.EntityFramework
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext() : base(OneAppConfigurationKeys.DefaultConnectionKey)
        {

        }
    }
}
