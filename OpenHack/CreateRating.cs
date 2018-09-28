using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OpenHack
{
    public static class CreateRating
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        [FunctionName("CreateRating")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethod.Post), Route = null)]
            HttpRequest req, 
            ILogger log,
            [CosmosDB(
                databaseName: "ProductRatings",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection")]
            out RatingModel rating)
        {
            rating = null;
            log.Log(LogLevel.Debug, "C# HTTP trigger function processed a request.");

            var feedback = JsonConvert.DeserializeObject<FeedbackViewModel>(req.ReadAsStringAsync().Result);

            if (feedback.Rating < 0 || feedback.Rating > 5)
                return new BadRequestObjectResult($"Rating {feedback.UserId} is not between 0 and 5");

            //todo: inject from environment
            var response = _httpClient.GetAsync($"https://serverlessohuser.trafficmanager.net/api/GetUser/?{nameof(feedback.UserId)}={feedback.UserId}").Result;
            if (response.StatusCode != HttpStatusCode.OK)
                return new BadRequestObjectResult($"Invalid user {feedback.UserId}");
       
            //todo: inject from environment
            response = _httpClient.GetAsync($"https://serverlessohproduct.trafficmanager.net/api/GetProduct/?{nameof(feedback.ProductId)}={feedback.ProductId}").Result;
            if (response.StatusCode != HttpStatusCode.OK)
                return new BadRequestObjectResult($"Invalid product: {feedback.ProductId}");
            
            rating = feedback.ToModel();
            rating.Id = Guid.NewGuid();
            rating.TimeStamp = DateTime.UtcNow;

            return new OkObjectResult(rating.ToViewModel());
        }
    }
}
