using Bff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bff
{
    public class SampleFunc
    {
        private readonly ILogger<SampleFunc> _logger;
        private readonly IRequestMessageGenerator _generator;

        public SampleFunc(
            ILogger<SampleFunc> logger,
            IRequestMessageGenerator generator
        )
        {
            _logger = logger;
            _generator = generator;
        }

        [FunctionName("Bff")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", "delete", Route = "{*bff}")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var (authenticationStatus, authenticationResponse) = await req.HttpContext.AuthenticateAzureFunctionAsync();
            if (!authenticationStatus) return authenticationResponse;

            using (var client = new HttpClient())
            {
                var reqMessage = await _generator.GenRequestMessageFromRequest(req);
                var res = await client.SendAsync(reqMessage);
                if (res.IsSuccessStatusCode)
                {
                    var clientRes = await res.Content.ReadAsStreamAsync();
                    return new OkObjectResult(clientRes);
                }
            }

            return new NotFoundResult();
        }

    }
}
