using System.ComponentModel.DataAnnotations;

namespace Cynthia.Chat.Common.Models
{
    public class ModelBase
    {
        [Key]
        public string Id { get; set; }
    }
}