using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using OneApp.Common.Core.Managers.Settings;

namespace OneApp.Modules.Authentication.Data.Repositories.EntityFramework
{
    public class EFIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public EFIdentityDbContext()
            : base(OneAppConfigurationKeys.DefaultConnectionKey)
        {

        }
    }
}