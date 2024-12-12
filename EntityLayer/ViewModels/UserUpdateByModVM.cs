using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class UserUpdateByModVM
    {
        public string Id { get; set; }
        [Display(Name = "İsim - Soyisim")]
        [Required(ErrorMessage ="İsim - Soyisim alanı boş geçilemez!")]
        public string NameSurname { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email alanı boş geçilemez!")]
        public string Email { get; set; }
        [Display(Name = "GSM No")]
        [Required(ErrorMessage = "GSM No alanı boş geçilemez!")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Rol alanı boş geçilemez!")]
        public string RoleId { get; set; }
        public List<RoleItem>? Roles { get; set; }
    }
    public class RoleItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}