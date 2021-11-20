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
    public class ApiConnectorSample
    {
        private readonly IClientCetificate _cert;
        public ApiConnectorSample(
            IClientCetificate cert
        )
        {
            _cert = cert;
        }

        [FunctionName("AfterSigninWithIdentityProvider")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("AfterSigninWithIdentityProvider");
            var certNotErr = true;
#if RELEASE
            certNotErr = _cert.Validator(req);
#endif

            if (!certNotErr)
            {
                var badres = new
                {
                    version = "1.0.0",
                    action = "ShowBlockPage",
                    userMessage = "There was a problem with your request. You are not able to sign up at this time."
                };
                return new OkObjectResult(badres);
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SigninWithIdentityProviderRequest data = JsonSerializer.Deserialize<SigninWithIdentityProviderRequest>(requestBody);

            var response = new {
                version = "1.0.0",
                action = "Continue",
                email = "hoge@example.com",
                displayName = "SendToFunctionsAPI"
            };

            return new OkObjectResult(response);
        }

        [FunctionName("BeforeCreatingUser")]
        public async Task<IActionResult> BeforeCreatingUser(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log
        )
        {
            log.LogInformation("BeforeCreatingUser");
            var certNotErr = true;
#if RELEASE
            certNotErr = _cert.Validator(req);
#endif
            if (!certNotErr)
            {
                var badres = new
                {
                    version = "1.0.0",
                    action = "ShowBlockPage",
                    userMessage = "Ç»ÇÒÇ©ÇÃÇ¶ÇÁÅ["
                };
                return new JsonResult(badres) { StatusCode = 400 };
            }

            var badress = new
            {
                version = "1.0.0",
                action = "Continue"
            };
            return new JsonResult(badress) { StatusCode = 200 };

        }
    }
}
