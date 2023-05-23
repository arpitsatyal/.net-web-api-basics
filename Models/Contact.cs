using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class Contact
    {
        [Key]
        [JsonProperty("id")]
        public string Id { get; set; }

        public string PhoneNumber { get; set; }

         public string Address { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

    }
}
