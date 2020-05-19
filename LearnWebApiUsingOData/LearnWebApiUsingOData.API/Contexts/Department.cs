using System;
using System.Collections.Generic;

namespace LearnWebApiUsingOData.API.Contexts
{
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
        }

        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
