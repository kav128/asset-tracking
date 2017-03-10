using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace AssetTracking.Models
{
    public class SearchConfiguration
    {
        const string PREFIX = "SEARCH";

        [Display(Name = "Azure Search Instance Name")]
        public string Name { get; set; }

        [Display(Name = "Azure Search Instance Key")]
        public string Key { get; set; }

        [Display(Name = "Azure Search Index Name")]
        public string Index { get; set; }

        public static SearchConfiguration RetrieveAppSettings()
        {
            return new SearchConfiguration
            {
                Name = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Name)}"],
                Key = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Key)}"],
                Index = ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Index)}"]
            };
        }

        public static void SaveAppSettings(SearchConfiguration config)
        {
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Name)}"] = config.Name;
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Key)}"] = config.Key;
            ConfigurationManager.AppSettings[$"{PREFIX}_{nameof(Index)}"] = config.Index;
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
            else if (String.IsNullOrWhiteSpace(Index))
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