using System;
using System.Collections.Generic;

namespace LearnWebApiUsingOData.API.Contexts
{
    public partial class Company
    {
        public Company()
        {
            Department = new HashSet<Department>();
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Department> Department { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
