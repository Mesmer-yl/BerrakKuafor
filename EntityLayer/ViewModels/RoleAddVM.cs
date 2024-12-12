using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class RoleAddVM
    {
        [Required(ErrorMessage ="Rol ismi girilmeli!")]
        [Display(Name = "Rol ismi")]
        public string Name { get; set; }
    }
}
