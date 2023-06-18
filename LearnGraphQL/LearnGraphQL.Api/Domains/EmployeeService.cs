using LearnGraphQL.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnGraphQL.Api.Domains
{
    public class EmployeeService : IEmployeeService
    {
        private List<Employee> _EmployeeDatas = new()
        {
            new Employee { Id = "1", Name="EmployeeOne", Department="SectionOne", PhoneNumber="xxx-xxx-xxxx"},
            new Employee { Id = "2", Name="EmployeeTwo", Department="SectionOne", PhoneNumber="yxx-xxx-xxxx"},
            new Employee { Id = "3", Name="EmployeeThree", Department="SectionTwo", PhoneNumber="zxx-xxx-xxxx"}
        };

        public List<Employee> GetEmployees()
        {
            return _EmployeeDatas;
        }

        public Employee? GetEmployee(string id)
        {
            var data = _EmployeeDatas.Where(w => w.Id == id);
            if (data.Any())
            {
                return data.First();
            }
            return null;
        }

        public void AddEmployee(Employee value)
        {
            _EmployeeDatas.Add(value);
        }

        public void PatchEmployee(string id, Employee value)
        {
            var data = _EmployeeDatas.Where(w => w.Id == id);
            if (data.Any())
            {
                data.First().Name = value.Name;
                data.First().Department = value.Department;
                data.First().PhoneNumber = value.PhoneNumber;
            }
        }

        public void DeleteEmployee(string id)
        {
            var data = _EmployeeDatas.Where(w => w.Id == id);
            if (data.Any())
            {
                _EmployeeDatas = _EmployeeDatas.Where(w => w.Id != id).ToList();
            }
        }
    }
}
