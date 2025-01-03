﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concretes
{
    public class AppUser : IdentityUser
    {
        public string NameSurname { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
