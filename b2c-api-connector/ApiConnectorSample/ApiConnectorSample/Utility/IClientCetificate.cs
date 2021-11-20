using Microsoft.AspNetCore.Http;

namespace ApiConnectorSample.Utility
{
    public interface IClientCetificate
    {
        bool Validator(HttpRequest req);
    }
}