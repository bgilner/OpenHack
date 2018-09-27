using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace OpenHack.FunctionApp
{
    public static class Product
    {
        [FunctionName(nameof(GetProductName))]
        public static async Task<HttpResponseMessage> GetProductName(
            [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethod.Get), Route = "products/{id}")]
            HttpRequestMessage req,
            string id,
            TraceWriter log)
        {
            log.Info($"{typeof(Product).Namespace}.{nameof(GetProductName)} called with id {id}");
            
            return req.CreateResponse(HttpStatusCode.OK, $"The product name for your product id {id} is Starfruit Explosion");
        }
    }
}
