using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class ReservationsForEmpVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserNameSurname { get; set; }
        public string PhoneNumber { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime Date { get; set; }
        public int Money { get; set; }
        public string Services { get; set; }
        public bool? IsStatus { get; set; }
        public string? Reason { get; set; }
    }
}
