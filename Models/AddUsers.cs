using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class AddUsers
    {
        [Key]
        public string Username { get; set; }

        public string Email { get; set; }

        [JsonProperty("contacts")]
        public List<Contact>? Contacts { get; set; }

    }
}