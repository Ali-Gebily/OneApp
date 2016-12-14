using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneApp.Common.Core.Injectors;
using OneApp.Common.Core.Managers.Settings;
using OneApp.Common.Core.Models;
using OneApp.Modules.Styles.Repositories;
using OneApp.Modules.Styles.Repositories.Mock;
using OneApp.Modules.Styles.WebServices.Repositories.EntityFramework;

namespace OneApp.Modules.Styles.Injectors
{
    class AuthenticationInjector : IOneAppNinjectResolver
    {

        public void RegisterServices(IOneAppKernel kernel)
        {
            var dataSource = OneAppConfigurationKeys.AppDataSource;
            switch (dataSource)
            {
                case AppDataSource.Mock:
                    kernel.BindConcerteToAbstact<IStylesRepository, StylesMockRepository>();
                    break;
                case AppDataSource.SqlServer:
                    kernel.BindConcerteToAbstact<IStylesRepository, StylesEFRepository>();
                    break;
                default:
                    throw new NotImplementedException(dataSource.ToString());
            }
        } 
    }
}