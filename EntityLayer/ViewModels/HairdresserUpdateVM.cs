using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class HairdresserUpdateVM
    {

        [Required(ErrorMessage = "İlgili kuaför şu an bulunmuyor!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kuaför adı alanı boş bırakılamaz!")]
        [Display(Name = "Kuaför Adı")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Telefon no alanı boş bırakılamaz!")]
        [Display(Name = "Telefon no")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Adres alanı boş bırakılamaz!")]
        [Display(Name = "Adres")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Çalışma alanı boş bırakılamaz!")]
        [Display(Name = "Çalışma alanı")]
        public string Field { get; set; }
        [Required(ErrorMessage = "Açılış saati alanı boş bırakılamaz!")]
        [Display(Name = "Açılış saati")]
        public TimeSpan OpenTime { get; set; }
        [Required(ErrorMessage = "Kapanış saati alanı boş bırakılamaz!")]
        [Display(Name = "Kapanış saati")]
        public TimeSpan CloseTime { get; set; }
    }
}
