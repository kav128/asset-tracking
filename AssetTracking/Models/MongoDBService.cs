using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace AssetTracking.Models
{
    public class DocumentDBService
    {
        private DocumentDBConfiguration _configuration;

        public DocumentDBService(DocumentDBConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal int CountDocuments()
        {
            DocumentClient client = new DocumentClient(new Uri(_configuration.Host), _configuration.Key);
            IEnumerable<int> results = client.CreateDocumentQuery<int>(new Uri(_configuration.CollectionUrl, UriKind.Relative), "SELECT VALUE COUNT(assets.id) FROM assets");
            return results.FirstOrDefault();
        }

        internal string GetLatestAssetName()
        {
            DocumentClient client = new DocumentClient(new Uri(_configuration.Host), _configuration.Key);
            IEnumerable<string> results = client.CreateDocumentQuery<string>(new Uri(_configuration.CollectionUrl, UriKind.Relative), "SELECT TOP 1 VALUE assets.name FROM assets ORDER BY assets._ts DESC");
            return results.FirstOrDefault();
        }

        internal int AverageCost()
        {
            DocumentClient client = new DocumentClient(new Uri(_configuration.Host), _configuration.Key);
            IEnumerable<int> results = client.CreateDocumentQuery<int>(new Uri(_configuration.CollectionUrl, UriKind.Relative), "SELECT VALUE AVG(assets.cost) FROM assets");
            return results.FirstOrDefault();
        }

        public IEnumerable<dynamic> GetDocuments()
        {
            DocumentClient client = new DocumentClient(new Uri(_configuration.Host), _configuration.Key);
            IEnumerable<dynamic> results = client.CreateDocumentQuery(new Uri(_configuration.CollectionUrl, UriKind.Relative), "SELECT * FROM assets");
            return results;
        }
    }
}