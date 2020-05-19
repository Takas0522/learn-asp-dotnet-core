using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnWebApiUsingOData.API.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnWebApiUsingOData.API.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesApiController : ControllerBase
    {
        private readonly LearnWebApiUsingODataDBContext _context;
        public CompaniesApiController(
            LearnWebApiUsingODataDBContext context
        )
        {
            _context = context;
        }

        [HttpPost]
        public void PostData(Company data)
        {
            _context.Add(data);
            _context.SaveChanges();
        }
    }
}