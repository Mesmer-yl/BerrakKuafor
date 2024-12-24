using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class PostAddVM
    {
        [JsonPropertyName("userName")]
        [JsonProperty("userName")]
        public string UserName;
        [JsonProperty("hairdresserId")]
        public int HairdresserId { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty("detail")]
        public string Detail { get; set; }
    }
}
