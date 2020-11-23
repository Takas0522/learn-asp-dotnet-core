using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LearnGraphSubscriptionLifeCycleManagement
{
    public static class ResourceNotifications
    {
        [FunctionName("ResourceNotifications")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("ResourceNotifications Run");

            string token = req.Query["validationToken"];
            if (token != null)
            {
                log.LogInformation($"Return token {token}");
                return new ContentResult { Content = token, ContentType = "text/plain", StatusCode = 200 };
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            log.LogInformation(requestBody);

            return new AcceptedResult();

        }
    }
}
