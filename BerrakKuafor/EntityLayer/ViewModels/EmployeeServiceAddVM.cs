using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class EmployeeServiceAddVM
    {
        public int EmployeeId { get; set; }
        public int ServiceId { get; set; }
        public int Money { get; set; }
        public bool IsPro { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
