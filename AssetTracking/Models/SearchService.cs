using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace AssetTracking.Models
{
    public class SearchService
    {
        private const string CACHE_LATEST_ASSET_NAME = "values:latest_asset_name";
        private const string CACHE_DOCUMENT_COUNT = "values:document_count";
        private const string CACHE_DOCUMENTS_TIMESTAMP = "set:documents:timestamp";
        private const string CACHE_DOCUMENTS = "set:documents";

        private SearchConfiguration _searchConfiguration;
        private CacheConfiguration _cacheConfiguration;

        public SearchService(SearchConfiguration searchConfiguration, CacheConfiguration cacheConfiguration)
        {
            _searchConfiguration = searchConfiguration;
            _cacheConfiguration = cacheConfiguration;
        }

        internal int CountDocuments()
        {
            Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(_cacheConfiguration.ConnectionString);
            });
            ConnectionMultiplexer connection = lazyConnection.Value;
            IDatabase cache = connection.GetDatabase();
            var cacheValue = cache.StringGet(CACHE_DOCUMENT_COUNT);
            if (cacheValue.HasValue && cacheValue.IsInteger)
            {
                return Int32.Parse(cacheValue.ToString());
            }
            else
            {
                SearchServiceClient serviceClient = new SearchServiceClient(_searchConfiguration.Name, new SearchCredentials(_searchConfiguration.Key));
                ISearchIndexClient index = serviceClient.Indexes.GetClient(_searchConfiguration.Index);
                int result = Convert.ToInt32(index.Documents.Count());
                cache.StringSet(CACHE_DOCUMENT_COUNT, result);
                return result;
            }
        }

        internal string GetLatestAssetName()
        {
            Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(_cacheConfiguration.ConnectionString);
            });
            ConnectionMultiplexer connection = lazyConnection.Value;
            IDatabase cache = connection.GetDatabase();
            var cacheValue = cache.StringGet(CACHE_LATEST_ASSET_NAME);
            if (cacheValue.HasValue)
            {
                return cacheValue.ToString();
            }
            else
            {
                SearchServiceClient serviceClient = new SearchServiceClient(_searchConfiguration.Name, new SearchCredentials(_searchConfiguration.Key));
                ISearchIndexClient index = serviceClient.Indexes.GetClient(_searchConfiguration.Index);
                SearchParameters parameters = new SearchParameters
                {
                    OrderBy = new List<string> { "id desc" },
                    Top = 1
                };
                string result = index.Documents.Search<ExpandoObject>("*", parameters).Results.SingleOrDefault()?.Document.SingleOrDefault(r => r.Key == "name").Value?.ToString();
                cache.StringSet(CACHE_LATEST_ASSET_NAME, result);
                return result;
            }
        }

        internal string CacheLastUpdated()
        {
            Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(_cacheConfiguration.ConnectionString);
            });
            ConnectionMultiplexer connection = lazyConnection.Value;
            IDatabase cache = connection.GetDatabase();
            string result = cache.StringGet(CACHE_DOCUMENTS_TIMESTAMP).ToString();
            return result;
        }

        public IEnumerable<dynamic> GetDocuments()
        {
            Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(_cacheConfiguration.ConnectionString);
            });
            ConnectionMultiplexer connection = lazyConnection.Value;
            IDatabase cache = connection.GetDatabase();
            var cacheValue = cache.StringGet(CACHE_DOCUMENTS);
            if (cacheValue.HasValue)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ExpandoObject>>(cacheValue.ToString());
            }
            else
            {
                SearchServiceClient serviceClient = new SearchServiceClient(_searchConfiguration.Name, new SearchCredentials(_searchConfiguration.Key));
                ISearchIndexClient index = serviceClient.Indexes.GetClient(_searchConfiguration.Index);
                IEnumerable<dynamic> results = index.Documents.Search<ExpandoObject>("*").Results.Select(r => r.Document);
                cache.StringSet(CACHE_DOCUMENTS, JsonConvert.SerializeObject(results));
                cache.StringSet(CACHE_DOCUMENTS_TIMESTAMP, DateTime.Now.ToShortTimeString());
                return results;
            }
        }
    }
}