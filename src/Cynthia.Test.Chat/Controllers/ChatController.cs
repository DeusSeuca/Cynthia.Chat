using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cynthia.Test.Chat.Services;

namespace Cynthia.Test.Chat.Controllers
{
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        public IDataService ChatData { get; set; }
        // GET api/chat
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Cynthia的", "数据测试" };
        }

        // GET api/chat/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/chat
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/chat/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/chat/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
