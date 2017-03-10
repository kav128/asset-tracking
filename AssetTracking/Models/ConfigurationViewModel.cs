namespace AssetTracking.Models
{
    public class ConfigurationViewModel
    {
        public bool Saved { get; set; }

        public MongoDBConfiguration Configuration { get; set; }
    }
}