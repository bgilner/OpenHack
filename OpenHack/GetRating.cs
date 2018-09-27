using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace OpenHack
{
    public static class GetRating
    {
        [FunctionName(nameof(GetRating))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethod.Get), Route = null)]
            HttpRequest req,
            ILogger log,
            [CosmosDB(
                databaseName: "ProductRatings",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{Query.ratingId}"
            )]
            RatingModel rating)
        {
            log.Log(LogLevel.Debug, "C# HTTP trigger function processed a request.");
            return new OkObjectResult(rating.ToViewModel());
        }
    }
}
