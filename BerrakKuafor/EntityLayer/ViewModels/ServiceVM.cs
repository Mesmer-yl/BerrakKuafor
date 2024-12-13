using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class ServiceVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Servis adı zorunludur!")]
        [Display(Name ="Servis Adı")]
        public string Name { get; set; }
    }
}
