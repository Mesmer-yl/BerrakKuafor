using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class ReservationsForUserVM
    {
        public string HairdresserName { get; set; }
        public string EmployeeName { get; set; }
        public string Services { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Money { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
