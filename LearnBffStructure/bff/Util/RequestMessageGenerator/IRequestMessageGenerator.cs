using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bff
{
    public interface IRequestMessageGenerator
    {
        Task<HttpRequestMessage> GenRequestMessageFromRequest(HttpRequest req);
    }
}