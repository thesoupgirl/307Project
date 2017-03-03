using System.ComponentModel.DataAnnotations;

namespace Roost.Models
{
    public class UserItem
    {
        [Required]
        public string ID { get; set; }

        //[Required]
        public string Name { get; set; }
    }
}
