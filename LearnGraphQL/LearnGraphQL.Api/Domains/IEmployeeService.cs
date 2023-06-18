using LearnGraphQL.Api.Models;

namespace LearnGraphQL.Api.Domains
{
    public interface IEmployeeService
    {
        void AddEmployee(Employee value);
        void DeleteEmployee(string id);
        Employee? GetEmployee(string id);
        List<Employee> GetEmployees();
        void PatchEmployee(string id, Employee value);
    }
}