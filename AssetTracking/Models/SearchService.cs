using System;
using System.Collections.Generic;

namespace AssetTracking.Models
{
    public class SearchService
    {
        private SearchConfiguration _searchConfiguration;
        private CacheConfiguration _cacheConfiguration;

        public SearchService(SearchConfiguration searchConfiguration, CacheConfiguration cacheConfiguration)
        {
            _searchConfiguration = searchConfiguration;
            _cacheConfiguration = cacheConfiguration;
        }

        internal int CountDocuments()
        {
            return default(int);
        }

        internal string GetLatestAssetName()
        {
            return String.Empty;
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