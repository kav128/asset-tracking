using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;
using System.Collections.Generic;
using Serialization = DynamicTableEntityJsonSerializer;
using Newtonsoft.Json;

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
            CloudStorageAccount account = CloudStorageAccount.Parse(_configuration.ConnectionString);
            CloudTableClient client = account.CreateCloudTableClient();
            CloudTable table = client.GetTableReference(_configuration.Table);
            TableQuery<DynamicTableEntity> projectionQuery = new TableQuery<DynamicTableEntity>().Select(new string[] { });
            int result = table.ExecuteQuery(projectionQuery).Count();
            return result;
        }

        internal string MostExpensiveAssetName()
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(_configuration.ConnectionString);
            CloudTableClient client = account.CreateCloudTableClient();
            CloudTable table = client.GetTableReference(_configuration.Table);
            TableQuery<DynamicTableEntity> projectionQuery = new TableQuery<DynamicTableEntity>();
            string result = table.ExecuteQuery(projectionQuery).OrderByDescending(t => t["cost"].Int32Value).Select(t => t["name"].StringValue).FirstOrDefault();
            return result;
        }

        internal int AverageCost()
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(_configuration.ConnectionString);
            CloudTableClient client = account.CreateCloudTableClient();
            CloudTable table = client.GetTableReference(_configuration.Table);
            TableQuery<DynamicTableEntity> projectionQuery = new TableQuery<DynamicTableEntity>();
            int result = Convert.ToInt32(table.ExecuteQuery(projectionQuery).Average(t => t["cost"].Int32Value));
            return result;
        }

        public IEnumerable<dynamic> GetDocuments()
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(_configuration.ConnectionString);
            CloudTableClient client = account.CreateCloudTableClient();
            CloudTable table = client.GetTableReference(_configuration.Table);
            TableQuery<DynamicTableEntity> projectionQuery = new TableQuery<DynamicTableEntity>();
            Serialization.DynamicTableEntityJsonSerializer serializer = new Serialization.DynamicTableEntityJsonSerializer();
            IEnumerable<string> resultStrings = table.ExecuteQuery(projectionQuery).Select(t => serializer.Serialize(t));
            IEnumerable<dynamic> results = resultStrings.Select(s => JsonConvert.DeserializeObject(s));
            return results;
        }
    }
}