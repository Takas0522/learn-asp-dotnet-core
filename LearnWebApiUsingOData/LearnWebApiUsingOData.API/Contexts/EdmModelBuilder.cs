using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnWebApiUsingOData.API.Contexts
{
    public static class EdmModelBuilder
    {

        private static IEdmModel _edmModel;

        public static IEdmModel GetEdmModel()
        {
            if (_edmModel == null)
            {
                var builder = new ODataConventionModelBuilder();
                builder.EntitySet<Company>("Companies");
                builder.EntitySet<Department>("Departments");
                _edmModel = builder.GetEdmModel();
            }

            return _edmModel;
        }
    }
}
