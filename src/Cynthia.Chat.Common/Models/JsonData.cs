using System;

namespace Cynthia.Chat.Common.Models
{
    public class JsonData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }
    }
}