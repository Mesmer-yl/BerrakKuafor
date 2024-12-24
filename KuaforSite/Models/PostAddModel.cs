using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace KuaforSite.Models
{
    public class PostAddModel
    {
        [Required(ErrorMessage ="Kullanıcı adı alınamadı!")]
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Kuaför seçmediniz!")]
        [Display(Name ="Kuaför")]
        [JsonProperty("hairdresserId")]
        public int HairdresserId { get; set; }
        [Required(ErrorMessage = "Lütfen bir fotoğraf seçiniz!")]
        [JsonProperty("imageUrl")]
        [Display(Name = "Fotoğraf")]
        public IFormFile? ImageUrl { get; set; }
        [Required(ErrorMessage = "Detaylar boş bırakılamaz!")]
        [Display(Name = "Açıklama")]
        [JsonProperty("detail")]
        public string Detail { get; set; }
    }
}
