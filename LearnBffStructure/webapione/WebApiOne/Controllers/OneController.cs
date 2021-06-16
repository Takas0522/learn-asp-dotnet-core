using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiOne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OneController : ControllerBase
    {

        private readonly ILogger<OneController> _logger;

        public OneController(ILogger<OneController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ResultData Get()
        {
            return new ResultData {
                RunWebApi = "WebApiOne",
                Result = "HOGE"
            };
        }
    }

    public class ResultData
    {
        public string RunWebApi { get; set; }
        public string Result { get; set; }
    }
}
