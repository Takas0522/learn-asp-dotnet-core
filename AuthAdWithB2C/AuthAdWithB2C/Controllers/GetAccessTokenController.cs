using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace AuthAdWithB2C.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetAccessTokenController
    {
        private readonly IConfidentialClientApplication _client;
        public GetAccessTokenController(
            IConfiguration config
        )
        {
            var tenant = config["AzureAd:TenantId"];
            var secret = config["AzureAd:Secret"];
            var authority = $"https://login.microsoftonline.com/{tenant}";
            _client = ConfidentialClientApplicationBuilder.Create(config["AzureAd:ClientId"])
                .WithClientSecret(secret)
                .WithAuthority(new Uri(authority)).Build();
        }

        [HttpGet]
        public async Task<string> GetAccessToken()
        {
            var res = await _client.AcquireTokenForClient(new List<string> { "b9982235-a2b5-4349-8e11-8196f60336ce/.default" }).ExecuteAsync();
            return res.AccessToken;
        }
    }
}
