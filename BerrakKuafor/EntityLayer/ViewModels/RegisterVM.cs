using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email adresi boş bırakılamaz!")]
        public string Email { get; set; }
        [Display(Name = "İsim - Soyisim")]
        [Required(ErrorMessage = "İsim - Soyisim boş bırakılamaz")]
        public string NameSurname { get; set; }
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz!")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Şifreler eşleşmedi!")]
        [Required(ErrorMessage = "Şifre Tekrar alanı boş bırakılamaz!")]
        [Display(Name = "Şifre Tekrar")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Display(Name = "GSM No")]
        [Required(ErrorMessage = "GSM numarası boş bırakılamaz!")]
        public string PhoneNumber { get; set; }
    }
}
