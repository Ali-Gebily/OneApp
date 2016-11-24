using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneApp.Common.Core.Models;

namespace OneApp.Common.Core.Managers.Settings
{

    public class SettingsManager: ISettingsManager
    {
        SettingsManager()
        {
            _settingsManager = GetSettingsManager();
        }
        readonly static SettingsManager _instance = new SettingsManager();
        public static SettingsManager Instance
        {
            get { return _instance; }
        }

        private ISettingsManager _settingsManager;
 
        ISettingsManager GetSettingsManager()
        {
            var settingsSource = OneAppConfigurationKeys.ConfigurationDataSource; 

            switch (settingsSource)
            {
                case ConfigurationDataSource.AppConfig:
                    return new AppConfigSettingsManager();
                case ConfigurationDataSource.Sql:
                    return new EFSettingsManager();
                case ConfigurationDataSource.Mongo:
                    return new MongoSettingsManager();
                default:
                    throw new NotImplementedException(settingsSource.ToString());
            }

        }

        public string GetValue(string key)
        {
            return _settingsManager.GetValue(key);
        }

     
    }
}
