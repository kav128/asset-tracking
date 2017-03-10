using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetTracking.Models
{
    public class MongoDBService
    {
        private MongoDBConfiguration _configuration;

        public MongoDBService(MongoDBConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal int CountDocuments()
        {
            MongoClient client = new MongoClient(_configuration.ConnectionString);
            IMongoCollection<dynamic> collection = client.GetDatabase(_configuration.Database).GetCollection<dynamic>(_configuration.Collection);
            int result = Convert.ToInt32(collection.Count(a => true));
            return result;
        }

        internal string GetLatestAssetName()
        {
            MongoClient client = new MongoClient(_configuration.ConnectionString);
            IMongoCollection<dynamic> collection = client.GetDatabase(_configuration.Database).GetCollection<dynamic>(_configuration.Collection);
            BsonDocument document = collection.Find(a => true).Sort("{ _id: -1 }").Project("{ name: 1, _id: 0 }").FirstOrDefault<BsonDocument>();
            string result = document["name"].AsString;
            return result;
        }

        internal int MostExpensiveCost()
        {
            MongoClient client = new MongoClient(_configuration.ConnectionString);
            IMongoCollection<dynamic> collection = client.GetDatabase(_configuration.Database).GetCollection<dynamic>(_configuration.Collection);
            BsonDocument document = collection.Find(a => true).Sort("{ cost: -1 }").Project("{ cost: 1, _id: 0 }").FirstOrDefault();
            int result = Convert.ToInt32(document["cost"].AsDouble);
            return result;
        }

        public IEnumerable<dynamic> GetDocuments()
        {
            MongoClient client = new MongoClient(_configuration.ConnectionString);
            IMongoCollection<dynamic> collection = client.GetDatabase(_configuration.Database).GetCollection<dynamic>(_configuration.Collection);
            IEnumerable<dynamic> results = collection.AsQueryable().ToEnumerable();
            return results;
        }
    }
}