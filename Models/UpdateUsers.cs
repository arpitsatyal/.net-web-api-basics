using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class UpdateUsers
    {
        [Key]
        public string Username { get; set; }

        public string Email { get; set; }

    }
}
