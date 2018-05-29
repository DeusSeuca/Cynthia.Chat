using System;

namespace Cynthia.Chat.Server.Models
{
    public class JsonData
    {
        public JsonData(string name, string content)
        {
            Name = name;
            Content = content;
        }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }
    }
}