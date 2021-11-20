using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using Utf8Json;

namespace ProxyApiOne.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProxyOneController : ControllerBase
    {

        private readonly ILogger<ProxyOneController> _logger;

        public ProxyOneController(ILogger<ProxyOneController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ResModel Get()
        {
            return new ResModel { Hoge = "REQ GET" };
        }

        [HttpGet]
        [Route("withquery")]
        public ResModel GetQuery([FromQuery] string name)
        {
            return new ResModel { Hoge = $"ProxyOne GET With Query Name = {name}" };
        }

        [HttpGet("{id}")]
        public ResModel GetRoute(string id)
        {
            return new ResModel { Hoge = $"ProxyOne GET With Route Id = {id}" };
        }

        [HttpPost()]
        public ResModel Post(ReqModel data)
        {
            string ret = JsonSerializer.ToJsonString<ReqModel>(data);
            return new ResModel { Hoge = $"ProxyOne POST data= {ret}" };
        }

        [HttpPost]
        [Route("withform")]
        public ResModel PostWithForm([FromForm] ReqModel data)
        {
            string ret = JsonSerializer.ToJsonString<ReqModel>(data);
            return new ResModel { Hoge = $"ProxyOne POST With Form data= {ret}" };
        }

        [HttpPost]
        [Route("withfile")]
        public ResModel PostWithFile([FromForm] IFormFileCollection datas)
        {
            string ret = datas.Count().ToString();
            return new ResModel { Hoge = $"ProxyOne POST With File FileCount= {ret}" };
        }

        [HttpPost]
        [Route("files-and-param")]
        public ResModel PostWithFileAndParam([FromForm] IFormFileCollection datas, [FromForm] ReqModel paramData)
        {
            string ret = datas.Count().ToString();
            string ret2 = JsonSerializer.ToJsonString<ReqModel>(paramData);
            return new ResModel { Hoge = $"ProxyOne POST With File FileCount= {ret} And Data = {ret2}" };
        }

        [HttpPut()]
        public ResModel Put(ReqModel data)
        {
            string ret = JsonSerializer.ToJsonString<ReqModel>(data);
            return new ResModel { Hoge = $"ProxyOne PUT data= {ret}" };
        }

        [HttpDelete("{id}")]
        public ResModel Delete(string id)
        {
            return new ResModel { Hoge = $"ProxyOne DELETE id= {id}" };
        }

        [HttpDelete()]
        [Route("multi")]
        public ResModel MultiDelete([FromBody] ReqModel data)
        {
            string ret = JsonSerializer.ToJsonString<ReqModel>(data);
            return new ResModel { Hoge = $"ProxyOne DELETE MULTI With Form data= {ret}" };
        }


    }

    public class ReqModel
    {
        [DataMember(Name = "hoge")]
        public string Hoge { get; set; }
        [DataMember(Name = "fuga")]
        public string Fuga { get; set; }
        [DataMember(Name = "piyo")]
        public string Piyo { get; set; }
    }

    public class ResModel
    {
        [DataMember(Name = "hoge")]
        public string Hoge { get; set; }
    }
}
