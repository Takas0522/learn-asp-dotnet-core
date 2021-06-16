using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bff.Util.TokenProvider
{
    public interface ITokenProvider
    {
        Task<string> GetAccessToken(IEnumerable<string> scopeResources, int version, string accessToken = "");
    }
}