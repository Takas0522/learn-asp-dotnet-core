using System;
using System.Collections.Generic;

namespace LearnWebApiUsingOData.API.Contexts
{
    public partial class Employee
    {
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public virtual Department Department { get; set; }
    }
}
