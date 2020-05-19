using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnWebApiUsingOData.API.Contexts;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnWebApiUsingOData.API.Controllers
{
    public class CompaniesController : ControllerBase
    {
        private readonly LearnWebApiUsingODataDBContext _context;
        public CompaniesController(
            LearnWebApiUsingODataDBContext context
        ) {
            _context = context;
        }

        [HttpGet]
        [EnableQuery]
        public IEnumerable<Company> GetCompanies()
        {
            return _context.Company;
        }

    }
}