/*
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using OpenHack.FunctionApp;

namespace OpenHack
{
    public class Repository
    {
        private const string DB_URI = "https://openhack-team-8.documents.azure.com:443/";
        private const string PrimaryKey = "qNdQLb6Edv2yN90KJYejQNXsr4YOEFMdbvbxz3h3ma3UfS9fHrzKXbvBjTQWxtMJKnIwBqQ7V5qHdxOomCQxoQ==";
        private readonly DocumentClient _client;

        public Repository(DocumentClient client) 
        {
            //DocumentClient client = new DocumentClient(DB_URI, PrimaryKey);
            //Uri documentUri = UriFactory.CreateDocumentUri("MyDatabaseName", "MyCollectionName", "DocumentId");
            //SomeClass myObject = client.ReadDocumentAsync<SomeClass>(documentUri).ToString();
            
            // please install Nuget package: Microsoft.Azure.DocumentDB.Core
            // and Microsoft.Azure.WebJobs.Extensions.CosmosDB
            _client = client;
        }

        public async Task<RatingModel> AddRating(RatingModel rating)
        {
            //todo: DB creates the Id & TimeStamp
            rating.Id = Guid.NewGuid();
            rating.TimeStamp = DateTime.UtcNow;
            await _client.CreateDocumentAsync(UriFactory.CreateDocumentUri("ProductRatings", "Items", rating.Id.ToString()), rating);
            return rating;
        }

        public RatingModel GetRating(Guid ratingId)
        {
            return new RatingModel();
        }

        public IEnumerable<RatingModel> GetRatings(Guid userId)
        {
            return new RatingModel[0]; 
        }
    }
}
*/