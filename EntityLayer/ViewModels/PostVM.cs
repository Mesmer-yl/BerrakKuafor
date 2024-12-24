using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class PostVM
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nameSurname")]
        public string NameSurname { get; set; }

        [JsonProperty("hairdresserName")]
        public string HairdresserName { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("currentTime")]
        public DateTime CurrentTime { get; set; }
    }
}
