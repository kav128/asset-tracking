using Newtonsoft.Json;

namespace AssetTracking.Models
{
    public class DataHelpers
    {
        public static string GetJsonString(dynamic target)
        {
            return JsonConvert.SerializeObject(target);
        }
    }
}