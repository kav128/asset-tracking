using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace AssetTracking.Models
{
    public class CacheConfiguration
    {
        const string PREFIX = "REDIS";

        [Display(Name = "Redis Cache Instance Name")]
        public string Name { get; set; }

        [Display(Name = "Redis Cache Instance Key")]
        public string Key { get; set; }

        public static CacheConfiguration RetrieveAppSettings()
        {
            return new CacheConfiguration
            {
                Name = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Name)}"],
                Key = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Key)}"]
            };
        }

        public static void SaveAppSettings(CacheConfiguration config)
        {
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Name)}"] = config.Name;
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Key)}"] = config.Key;
        }

        public bool Validate()
        {
            if (String.IsNullOrWhiteSpace(Name))
            {
                return false;
            }
            else if (String.IsNullOrWhiteSpace(Key))
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