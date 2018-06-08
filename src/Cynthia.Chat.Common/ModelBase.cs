using System.ComponentModel.DataAnnotations;

namespace Cynthia.DataBase.Common
{
    public class ModelBase : IModel
    {
        [Key]
        public string Id { get; set; }
    }
}