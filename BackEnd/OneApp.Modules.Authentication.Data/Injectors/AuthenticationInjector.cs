using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneApp.Common.Core.Injectors;
using OneApp.Modules.Authentication.Data.Repositories;
using OneApp.Modules.Authentication.Data.Repositories.EntityFramework;
using OneApp.Modules.Authentication.Data.Repositories.Ldap;
using OneApp.Modules.Authentication.Data.Repositories.Mock;
using OneApp.Modules.Authentication.Data.Repositories.Mongo;
using OneApp.Modules.Authentication.Data.Settings;

namespace OneApp.Modules.Authentication.Data.Injectors
{
    class AuthenticationInjector : IOneAppNinjectResolver
    {

        public void RegisterServices(IOneAppKernel kernel)
        {
            var dataSource = AuthenticationConfigurationKeys.AuthenticationDataSource;
            switch (dataSource)
            {
                case Model.AuthenticationDataSource.Mock:
                    kernel.BindConcerteToAbstact<IAuthenticationRepository, MockAuthenticationRepository>();
                    break;
                case Model.AuthenticationDataSource.SqlServer:
                    kernel.BindConcerteToAbstact<IAuthenticationRepository, EFAuthenticationRepository>();
                    break;
                case Model.AuthenticationDataSource.MongoDb:
                    kernel.BindConcerteToAbstact<IAuthenticationRepository, MongoAuthenticationRepository>();
                    break;
                case Model.AuthenticationDataSource.LDAP:
                    kernel.BindConcerteToAbstact<IAuthenticationRepository, LdapAuthenticationRepository>();
                    break;
                default:
                    throw new NotImplementedException(dataSource.ToString());
            }
        } 
    }
}