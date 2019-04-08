using System;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace myAbpBasic.Web.Controllers
{
    [DontWrapResult]
    [Route("api/[controller]")]
    public class DefaultController : myAbpBasicControllerBase
    {

        private string serviceName = string.Empty;
        public IConfiguration Configuration { get; }


        public DefaultController(IConfiguration configuration)
        {
            Configuration = configuration;
            serviceName = Configuration["Service:Name"];
        }



        // GET: api/Default
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] {  $"{serviceName}: {DateTime.Now.ToString()} {Environment.MachineName} " +
                                   $"OS: {Environment.OSVersion.VersionString}" };
        }

        // GET: api/Default/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return serviceName + ".value." + id;
        }

        // POST: api/Default
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
