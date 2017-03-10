using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace AssetTracking.Models
{
    public class StorageConfiguration
    {
        const string PREFIX = "STORAGE";

        [Display(Name = "Connection String")]
        public string ConnectionString { get; set; }

        [Display(Name = "Table Name")]
        public string Table { get; set; }
        
        public static StorageConfiguration RetrieveAppSettings()
        {
            return new StorageConfiguration
            {
                ConnectionString = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(ConnectionString)}"],
                Table = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Table)}"]
            };
        }

        public static void SaveAppSettings(StorageConfiguration config)
        {
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(ConnectionString)}"] = config.ConnectionString;
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Table)}"] = config.Table;
        }

        public bool Validate()
        {
            if (String.IsNullOrWhiteSpace(ConnectionString))
            {
                return false;
            }
            else if (String.IsNullOrWhiteSpace(Table))
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