using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            ILogger log,
            [CosmosDB(
                databaseName: "ProductRatings",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection" //,
//                SqlQuery = "select * from Items c where c.userId=\"{Query.userId}\""
            )]
            DocumentClient client)

        {
            log.Log(LogLevel.Information, "C# HTTP trigger function processed a get ratings request.");
            var collectionUri = UriFactory.CreateDocumentCollectionUri("ProductRatings", "Items");
            var userId = req.Query["userId"];
            var query = client.CreateDocumentQuery<RatingModel>(collectionUri)
                .Where(x => x.UserId == userId)
                .AsDocumentQuery();

            var ratings = new List<RatingModel>();
            while (query.HasMoreResults)
            {
                foreach (var rating in await query.ExecuteNextAsync())
                {
                    ratings.Add(rating.ToViewModel());
                }
            }

            return new OkObjectResult(ratings);
        }
    }
}