using System.Collections.Generic;

namespace AssetTracking.Models
{
    public class AssetViewModel
    {
        public bool Ready { get; internal set; }

        public int Count { get; set; }

        public string CacheLastUpdated { get; set; }

        public string LatestAssetName { get; set; }

        public string WarhouseStatus { get; set; }

        public IEnumerable<dynamic> Assets { get; set; }
    }
}