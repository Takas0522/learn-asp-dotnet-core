using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bff.Util.TokenProvider
{
    public class TokenProvider : ITokenProvider
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly AuthenticationContext _v1AuthContext;
        private readonly ClientCredential _clientCredential;

        public TokenProvider(
            ITokenAcquisition tokenAcquisition,
            IConfiguration config
        )
        {
            _tokenAcquisition = tokenAcquisition;
            var tenantId = config["AzureAd:TenantId"];
            _v1AuthContext = new AuthenticationContext($"https://login.microsoftonline.com/{tenantId}/");
            _clientCredential = new ClientCredential(config["AzureAd:ClientId"], config["AzureAd:ClientSecret"]);
        }

        public async Task<string> GetAccessToken(IEnumerable<string> scopeResources, int version, string accessToken = "")
        {
            if (version == 2) {
                return await _tokenAcquisition.GetAccessTokenForUserAsync(scopeResources);
            }
            var resouce = scopeResources.First();
            var res = await _v1AuthContext.AcquireTokenAsync(resouce, _clientCredential, new UserAssertion(accessToken));
            return res.AccessToken;
        }
    }
}
