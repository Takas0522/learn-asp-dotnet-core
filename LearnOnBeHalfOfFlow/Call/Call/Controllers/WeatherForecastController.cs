using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;

namespace Call.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IDownstreamWebApi _downstreamWebApi;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            ITokenAcquisition tokenAcquisition,
            //IDownstreamWebApi downstreamWebApi,
            IConfiguration config
        )
        {
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;
            _config = config;
            //_downstreamWebApi = downstreamWebApi;
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var scopes = new List<string> { _config["ReceiveApi:Scopes"] };
            var res = await _tokenAcquisition.GetAccessTokenForUserAsync(scopes);
            string apiUrl = $"https://localhost:44305/weatherforecast";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            httpRequestMessage.Headers.Add("Authorization", $"bearer {res}");
            var response = await _httpClient.SendAsync(httpRequestMessage);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string resdata = await response.Content.ReadAsStringAsync();
                return "WebAPI Caa" + resdata;
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            //using var dstresponse = await _downstreamWebApi.CallWebApiForUserAsync("ReceiveApi").ConfigureAwait(false);
            //if (dstresponse.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    var apiResult = await dstresponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            //    // Do something
            //}
            //else
            //{
            //    var error = await dstresponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            //    throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {dstresponse.StatusCode}: {error}");
            //}

            return "hoge";
        }
    }
}
