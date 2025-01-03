﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concretes
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<EmployeeService> EmployeeServices { get; set; }
    }
}
