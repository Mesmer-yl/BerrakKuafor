using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class LoginVM
    {
        [Display(Name =  "Email")]
        [Required(ErrorMessage = "Email adresinizi giriniz!")]
        public string Email { get; set; }
        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifrenizi giriniz!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
