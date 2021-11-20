using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace YarpSite.Factory
{
    public class ProxyAuthFactory : ITransformFactory
    {

        public bool Build(TransformBuilderContext context, IReadOnlyDictionary<string, string> transformValues)
        {
            if (transformValues.TryGetValue("OnBeHalfOfProxy", out var scope))
            {
                if (string.IsNullOrEmpty(scope))
                {
                    throw new ArgumentException("A non-empty OnBeHalfOfProxy value is required");
                }
                context.AddRequestTransform(async transformContext => {
                    var tokenAcquisition = transformContext.HttpContext.RequestServices.GetRequiredService<ITokenAcquisition>();
                    var res = await tokenAcquisition.GetAccessTokenForUserAsync(new List<string> { scope });
                    transformContext.ProxyRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", res);
                });
                return true;
            }
            return false;
        }

        public bool Validate(TransformRouteValidationContext context, IReadOnlyDictionary<string, string> transformValues)
        {
            if (transformValues.TryGetValue("OnBeHalfOfProxy", out var value))
            {
                if (string.IsNullOrEmpty(value))
                {
                    context.Errors.Add(new ArgumentException("A non-empty OnBeHalfOfProxy value is required"));
                }

                return true; // Matched
            }
            return false;
        }
    }
}
