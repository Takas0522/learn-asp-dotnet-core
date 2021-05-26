using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTwo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TwoController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TwoController> _logger;

        public TwoController(ILogger<TwoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ResultData> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new ResultData
            {
                ResultNumber = index,
                RunWebApi = "WebApiTwo"
            })
            .ToArray();
        }
    }

    public class ResultData
    {
        public string RunWebApi { get; set; }
        public int ResultNumber { get; set; }
    }
}
