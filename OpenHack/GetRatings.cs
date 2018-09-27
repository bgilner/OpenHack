using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace OpenHack
{
    public static class GetRatings
    {
        [FunctionName(nameof(GetRatings))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethod.Get), Route = null)]
            HttpRequest req,
            ILogger log)
        {

            log.Log(LogLevel.Information, "C# HTTP trigger function processed a get ratings request.");
            var authKey = new SecureString();

            foreach (char c in "qNdQLb6Edv2yN90KJYejQNXsr4YOEFMdbvbxz3h3ma3UfS9fHrzKXbvBjTQWxtMJKnIwBqQ7V5qHdxOomCQxoQ==")
            {
                authKey.AppendChar(c);
            }

            var client = new DocumentClient(new Uri("https://openhack-team-8.documents.azure.com:443/"), authKey);
            var collectionUri = UriFactory.CreateDocumentCollectionUri("ProductRatings", "Items");
            var userId = req.Query["userId"];
            var sqlQuery = new SqlQuerySpec("SELECT * FROM Items c WHERE c.userId=@userId",
                new SqlParameterCollection(new[] {new SqlParameter("@userId", Guid.Parse(userId))}));

            var query = client.CreateDocumentQuery<RatingModel>(collectionUri, sqlQuery).AsDocumentQuery();
               
            var ratings = new List<FeedbackViewModel>();

            while (query.HasMoreResults)
            {
                foreach (var rating in await query.ExecuteNextAsync<RatingModel>())
                {
                    ratings.Add(rating.ToViewModel());
                }
            }

            return new OkObjectResult(ratings);
        }
    }
}