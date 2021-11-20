using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Utf8Json;
using ApiConnectorSample.Utility;
using ApiConnectorSample.Models;
using System.Dynamic;

namespace ApiConnectorSample.Functions
{
    public class CustomPolicyConnectorSample
    {

        [FunctionName("CustomPolicySignupValidator")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("CustomPolicySignupValidator");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            CustomPolicySignupValidatorRequest data = JsonSerializer.Deserialize<CustomPolicySignupValidatorRequest>(requestBody);

            var badres = new
            {
                version = "1.0.1",
                status = 409,
                userMessage = "Ç»ÇÒÇ©ÇÃÇ¶ÇÁÅ["
            };
            return new JsonResult(badres) { StatusCode = 409 };

            //var response = new {
            //    displayName = "SendToFunctionsAPI"
            //};

            //return new OkObjectResult(response);
        }

    }
}
