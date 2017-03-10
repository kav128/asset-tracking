using System.Collections.Generic;

namespace AssetTracking.Models
{
    public class SearchService
    {
        private SearchConfiguration _configuration;

        public SearchService(SearchConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal int CountDocuments()
        {
            return default(int);
        }

        internal string GetLatestAssetName()
        {
            return default(string);
        }

        internal int AverageCost()
        {
            return default(int);
        }

        public IEnumerable<dynamic> GetDocuments()
        {
            return null;
        }
    }
}