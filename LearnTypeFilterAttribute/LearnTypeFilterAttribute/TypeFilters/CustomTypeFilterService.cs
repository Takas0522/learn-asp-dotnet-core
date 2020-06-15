using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnTypeFilterAttribute.TypeFilters
{
    public class CustomTypeFilterService : ICustomTypeFilterService
    {
        public bool AuthCheck(AccessType accessType, string setting, string customValue)
        {
            if (customValue == "")
            {
                return false;
            }
            return customValue.Contains("Please");
        }
    }
}
