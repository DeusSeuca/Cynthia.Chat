using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cynthia.Chat.Server.Services;
using Cynthia.Chat.Common.Models;

namespace Cynthia.Chat.Server.Controllers
{
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        public IMessagesService Message { get; set; }
        public ChatMessageCacheService cache { get; set; }
        // GET api/chat 获得数据总数量
        [HttpGet]
        public int GetCount()
        {
            return cache.GetDataCount();
        }

        // GET api/chat/5 获得某条之后的所有聊天记录
        [HttpGet("{count}")]
        public IEnumerable<ChatMessage> Get(int count)
        {
            return Message.GetMessage(count);
        }

        // POST api/chat 将一条消息添加进聊天记录
        [HttpPost]
        public void Post([FromBody]ChatMessage data)
        {
            data.Time = DateTime.Now;
            Message.AddMessage(data);
        }

        // PUT api/chat/5 暂时无用
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/chat/5 暂时无用
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
