using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class ServiceAddVM
    {
        [Required(ErrorMessage ="Servis adı alanı boş geçilemez")]
        [Display(Name ="Servis Adı")]
        public string Name { get; set; }
    }
}
