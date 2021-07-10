using Bff.Util.TokenProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bff
{
    public class RequestMessageGenerator : IRequestMessageGenerator
    {
        private readonly ITokenProvider _tokenProvider;
        public RequestMessageGenerator(
             ITokenProvider tokenProvider
        )
        {
            _tokenProvider = tokenProvider;
        }

        public async Task<HttpRequestMessage> GenRequestMessageFromRequest(HttpRequest req)
        {
            var res = new HttpRequestMessage();
            res.Method = new HttpMethod(req.Method);
            string path = req.Path.Value.ToLower();

            var absUrl = req.Path.Value;
            var splUrl = absUrl.Split("/");

            var reqAbsUrls = splUrl.Where((w, index) => index > 3).Select((s) =>
            {
                return s;
            });
            var reqAbsUrl = string.Join("/", reqAbsUrls);

            if (req.Body != null)
            {
                res.Content = new StreamContent(req.Body);
            }

            if (path.Contains("/one/"))
            {
                var token = await _tokenProvider.GetAccessToken(new List<string> { "api://85d4ca43-7c3c-4f69-98cf-af49f157cdb7/access" }, 2);
                res.RequestUri = new Uri($"http://okawa-hub-endpoint.azurewebsites.net/{reqAbsUrl}");
                res.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return res;
            }

            if (path.Contains("/two/"))
            {
                req.Headers.TryGetValue("Authorization", out var val);
                var tokenRep = val.ToString().Replace("Bearer ", "");
                var token = await _tokenProvider.GetAccessToken(new List<string> { "<WebAPI2(v1AppのClientId)>" }, 1, tokenRep);
                res.RequestUri = new Uri($"https://localhost:44305/{reqAbsUrl}");
                res.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return res;
            }
            return null;
        }
    }
}
