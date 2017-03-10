using System;
using System.Configuration;

namespace AssetTracking.Models
{
    public class DocumentDBConfiguration
    {
        const string PREFIX = "DOCUMENTDB";

        [Display(Name = "Host URL")]
        public string Host { get; set; }

        [Display(Name = "Read-Only Key")]
        public string Key { get; set; }

        [Display(Name = "Database Name")]
        public string Database { get; set; }

        [Display(Name = "Collection Name")]
        public string Collection { get; set; }

        public string CollectionUrl
        {
            get { return $"dbs/{Database}/colls/{Collection}"; }
        }

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