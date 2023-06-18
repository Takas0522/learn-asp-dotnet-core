using LearnGraphQL.Api.Domains;
using LearnGraphQL.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LearnGraphQL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(
           IEmployeeService service
        )
        {
            _service = service;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            return _service.GetEmployees();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public ActionResult<Employee> Get(string id)
        {
            var data = _service.GetEmployee(id);
            if (data != null)
            {
                return data;
            }
            return new NotFoundResult();
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public ActionResult Post(Employee value)
        {
            _service.AddEmployee(value);
            return new NoContentResult();
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Employee value)
        {
            var data = _service.GetEmployee(id);
            if (data != null)
            {
                _service.PatchEmployee(id, value);
                return new NoContentResult();
            }
            return new NotFoundResult();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var data = _service.GetEmployee(id);
            if (data != null)
            {
                _service.DeleteEmployee(id);
                return new NoContentResult();
            }
            return new NotFoundResult();
        }
    }
}
