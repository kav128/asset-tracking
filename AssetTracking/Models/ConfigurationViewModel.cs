namespace AssetTracking.Models
{
    public class ConfigurationViewModel
    {
        public bool Saved { get; set; }

        public DocumentDBConfiguration Configuration { get; set; }
    }
}