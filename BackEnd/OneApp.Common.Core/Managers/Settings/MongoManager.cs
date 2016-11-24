using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver; 

namespace OneApp.Common.Core.Managers.Settings
{
    public class MongoManager
    {

        public static IMongoDatabase GetMongoDatabase()
        {
            var connectionString = AppConfigSettingsManager.GetConnectionString(OneAppConfigurationKeys.DefaultConnectionKey);

            var mongoUrl = MongoUrl.Create(connectionString);
            var client = new MongoClient(string.Format("mongodb://{0}:{1}", mongoUrl.Server.Host, mongoUrl.Server.Port));
            return client.GetDatabase(mongoUrl.DatabaseName);
        }

    }
}
