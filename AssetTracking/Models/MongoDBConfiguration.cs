using System;
using System.Configuration;

namespace AssetTracking.Models
{
    public class MongoDBConfiguration
    {
        const string PREFIX = "MONGODB";

        public string ConnectionString { get; set; }

        public string Database { get; set; }

        public string Collection { get; set; }
        
        public static MongoDBConfiguration RetrieveAppSettings()
        {
            return new MongoDBConfiguration
            {
                ConnectionString = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(ConnectionString)}"],
                Database = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Database)}"],
                Collection = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Collection)}"]
            };
        }

        public static void SaveAppSettings(MongoDBConfiguration config)
        {
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(ConnectionString)}"] = config.ConnectionString;
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Database)}"] = config.Database;
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Collection)}"] = config.Collection;
        }

        public bool Validate()
        {
            if (String.IsNullOrWhiteSpace(ConnectionString))
            {
                return false;
            }
            else if (String.IsNullOrWhiteSpace(Database))
            {
                return false;
            }
            else if (String.IsNullOrWhiteSpace(Collection))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}