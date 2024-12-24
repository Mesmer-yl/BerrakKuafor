using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class PostUpdateVM
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("detail")]
        public string Detail { get; set; }
    }
}
