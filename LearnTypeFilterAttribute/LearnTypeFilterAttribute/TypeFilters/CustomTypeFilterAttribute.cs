using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnTypeFilterAttribute.TypeFilters
{
    public class CustomTypeFilterAttribute : TypeFilterAttribute
    {
        public CustomTypeFilterAttribute(AccessType accessType, string setting) : base(typeof(CustumRequirementFilter))
        {
            Arguments = new object[] { accessType, setting };
        }

        private class CustumRequirementFilter : IAuthorizationFilter
        {
            private readonly AccessType _accessType;
            private readonly string _setting;
            private readonly ICustomTypeFilterService _service;

            public CustumRequirementFilter(
                AccessType accessType,
                string setting,
                ICustomTypeFilterService service
            )
            {
                _accessType = accessType;
                _setting = setting;
                _service = service;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var k = context.HttpContext.Request.Headers.Where(w => w.Key == "customvalue");
                if (!k.Any())
                {
                    context.Result = new ForbidResult();
                    return;
                }
                var customvalue = k.First().Value.ToString();
                if (!_service.AuthCheck(_accessType, _setting, customvalue))
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
        }
    }

    public enum AccessType {
        NOT_AUTH
    }
}
