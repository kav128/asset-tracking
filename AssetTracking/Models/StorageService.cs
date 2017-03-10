using System;
using System.Collections.Generic;

namespace AssetTracking.Models
{
    public class StorageService
    {
        private StorageConfiguration _configuration;

        public StorageService(StorageConfiguration configuration)
        {
            _configuration = configuration;
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