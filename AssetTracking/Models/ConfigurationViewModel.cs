namespace AssetTracking.Models
{
    public class ConfigurationViewModel
    {
        public bool Saved { get; set; }

        public SearchConfiguration SearchConfiguration { get; set; }

        public CacheConfiguration CacheConfiguration { get; set; }
    }
}