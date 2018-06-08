using System;
using Cynthia.DataBase.Common;

namespace Cynthia.Chat.Common.Models
{
    public class ChatMessage : ModelBase
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }
    }
}