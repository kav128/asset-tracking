using System;
using System.Configuration;

namespace AssetTracking.Models
{
    public class DocumentDBConfiguration
    {
        const string PREFIX = "MONGODB";

        public string Host { get; set; }

        public string Key { get; set; }

        public string Database { get; set; }

        public string Collection { get; set; }
        
        public static DocumentDBConfiguration RetrieveAppSettings()
        {
            return new DocumentDBConfiguration
            {
                Host = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Host)}"],
                Key = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Key)}"],
                Database = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Database)}"],
                Collection = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Collection)}"]
            };
        }

        public static void SaveAppSettings(DocumentDBConfiguration config)
        {
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Host)}"] = config.Host;
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Key)}"] = config.Key;
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Database)}"] = config.Database;
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Collection)}"] = config.Collection;
        }

        public bool Validate()
        {
            if (String.IsNullOrWhiteSpace(Host))
            {
                return false;
            }
            else if (String.IsNullOrWhiteSpace(Key))
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