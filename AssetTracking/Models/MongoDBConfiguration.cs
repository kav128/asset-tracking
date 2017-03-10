using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace AssetTracking.Models
{
    public class MongoDBConfiguration
    {
        const string PREFIX = "MONGODB";

        [Display(Name = "Connection String")]
        public string ConnectionString { get; set; }

        [Display(Name = "Database Name")]
        public string Database { get; set; }

        [Display(Name = "Collection Name")]
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