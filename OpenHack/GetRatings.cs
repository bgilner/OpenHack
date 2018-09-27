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
            var userId = Guid.Parse(req.Query["userId"]);

            var ratings = client.CreateDocumentQuery<RatingModel>(collectionUri)
                .Where(x => x.UserId == userId)
                .AsEnumerable()
                .Select(x => x.ToViewModel());

            return new OkObjectResult(ratings);
        }
    }
}