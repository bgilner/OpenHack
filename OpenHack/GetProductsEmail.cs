using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OpenHack
{
    public static class GetProductsEmail
    {
        private static string GetEmail(IEnumerable<Product> products)
        {
            string template;
            var templateStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OpenHack.EmailTemplate.html");
            using (templateStream)
                template = new StreamReader(templateStream).ReadToEnd();

            var productsHtml = new StringBuilder();
            foreach (var product in products)
                productsHtml.AppendLine($"<tr><td>{product.ProductName}</td><td>{product.ProductDescription}</td><td>{product.ProductId}</td></tr>");

            return template.Replace("{{Products}}", productsHtml.ToString());
        }

        [FunctionName(nameof(GetProductsEmail))]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var productsResponse = await Client.Instance.GetAsync(WrappedApi.GET_PRODUCTS);
            var productsJson = await productsResponse.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<Product[]>(productsJson);
            var emailBody = GetEmail(products);
            return new OkObjectResult(emailBody);
        }
    }
}
