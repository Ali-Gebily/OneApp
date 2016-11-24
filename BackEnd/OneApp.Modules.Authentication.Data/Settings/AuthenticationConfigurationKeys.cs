using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneApp.Common.Core.Managers.Settings;
using OneApp.Modules.Authentication.Data.Model;

namespace OneApp.Modules.Authentication.Data.Settings
{
 public sealed   class AuthenticationConfigurationKeys
    {
        const string _AuthenticationDataSourceKey = "AuthenticationDataSource";

        public static AuthenticationDataSource AuthenticationDataSource
        {
            get
            {
                return (AuthenticationDataSource)int.Parse(SettingsManager.Instance.GetValue(_AuthenticationDataSourceKey));
            }
        }
    }
}
