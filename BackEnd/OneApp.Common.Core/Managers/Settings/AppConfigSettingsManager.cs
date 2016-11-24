using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Common.Core.Managers.Settings
{
    public class AppConfigSettingsManager: ISettingsManager
    {
        public string GetValue(string key)
        {
          return  System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public static string GetConnectionString(string name)
        {
            var connection= System.Configuration.ConfigurationManager.ConnectionStrings[name];

            if (connection == null)
            {
                throw new Exception("no connection string with name="+name);
            }
            return connection.ConnectionString;
        }
    }
}
