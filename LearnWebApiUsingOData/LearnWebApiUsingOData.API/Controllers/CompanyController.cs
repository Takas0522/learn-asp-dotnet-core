using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnWebApiUsingOData.API.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnWebApiUsingOData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly LearnWebApiUsingODataDBContext _context;
        public CompanyController(
            LearnWebApiUsingODataDBContext context
        ) {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Company> GetCompanies()
        {
            return _context.Company;
        }

        [HttpGet("{id}")]
        public Company GetCompany(int id)
        {
            IEnumerable<Company> data = _context.Company.Where(w => w.Id == id);
            if (data.Any())
            {
                return data.FirstOrDefault();
            }
            return null;
        }
    }
}