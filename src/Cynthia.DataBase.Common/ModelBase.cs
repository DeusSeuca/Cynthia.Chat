using System.ComponentModel.DataAnnotations;

namespace Cynthia.DataBase.Common
{
    public class ModelBase
    {
        [Key]
        public string Id { get; set; }
    }
}