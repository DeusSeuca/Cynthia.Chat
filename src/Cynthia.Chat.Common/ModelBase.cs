using System.ComponentModel.DataAnnotations;

namespace Cynthia.Chat.Common
{
    public class ModelBase : IModel
    {
        [Key]
        public string Id { get; set; }
    }
}