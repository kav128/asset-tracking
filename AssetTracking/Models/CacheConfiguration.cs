using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace AssetTracking.Models
{
    public class CacheConfiguration
    {
        const string PREFIX = "REDIS";

        [Display(Name = "Redis Cache Connection String")]
        public string ConnectionString { get; set; }
        
        public static CacheConfiguration RetrieveAppSettings()
        {
            return new CacheConfiguration
            {
                ConnectionString = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(ConnectionString)}"]
            };
        }

        public static void SaveAppSettings(CacheConfiguration config)
        {
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(ConnectionString)}"] = config.ConnectionString;
        }

        public bool Validate()
        {
            if (String.IsNullOrWhiteSpace(ConnectionString))
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