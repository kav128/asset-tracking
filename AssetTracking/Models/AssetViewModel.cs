using System.Collections.Generic;

namespace AssetTracking.Models
{
    public class AssetViewModel
    {
        public bool Ready { get; internal set; }

        public int Count { get; set; }

        public int AverageCost { get; set; }

        public string LatestAssetName { get; set; }

        public IEnumerable<dynamic> Assets { get; set; }
    }
}