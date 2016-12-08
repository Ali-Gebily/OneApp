using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OneApp.Common.Core.Models;

namespace OneApp.Common.Core.Managers.Settings
{
    public sealed class OneAppConfigurationKeys
    {
        public const string DefaultConnectionKey = "DefaultConnection";

        const string _ConfigurationDataSourceKey = "ConfigurationDataSource";
        const string _AppDataSourceKey = "AppDataSource";
        const string _ShowExceptionKey = "ShowException";
        const string _EmailChannelKey = "EmailChannel";
        public static ConfigurationDataSource ConfigurationDataSource
        {
            get
            {
                return (ConfigurationDataSource)int.Parse(new AppConfigSettingsManager().GetValue(_ConfigurationDataSourceKey));
            }
        }

        public static AppDataSource AppDataSource
        {
            get
            {
                return (AppDataSource)int.Parse(SettingsManager.Instance.GetValue(_AppDataSourceKey));
            }
        }

        public static Emailchannel Emailchannel
        {
            get
            {
                return (Emailchannel)int.Parse(SettingsManager.Instance.GetValue(_EmailChannelKey));
            }
        }

        public static bool ShowException
        {
            get
            {
                return bool.Parse(SettingsManager.Instance.GetValue(_ShowExceptionKey));
            }
        }
    }

}