using EntityLayer.Concretes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class ReservationAddVM
    {
        public int HairdresserId { get; set; }
        public int EmployeeId { get; set; }
        public string UserId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime Date { get; set; }
        public int Money { get; set; }
        public string Services { get; set; }
    }
}
