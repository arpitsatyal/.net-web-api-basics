using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class AddContact
    {
        [Key]
        [JsonProperty("id")]

        public string PhoneNumber { get; set; }

         public string Address { get; set; }

        [JsonProperty("userId")]
        public string? UserId { get; set; }

    }
}
