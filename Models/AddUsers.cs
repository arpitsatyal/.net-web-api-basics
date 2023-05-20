using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class AddUsers
    {
        [Key]
        public string Username { get; set; }

        public string Email { get; set; }

    }
}
