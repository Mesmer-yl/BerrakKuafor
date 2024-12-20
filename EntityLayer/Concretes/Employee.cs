using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concretes
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public AppUser AppUser { get; set; }
        public int HairdresserId { get; set; }
        [ForeignKey("HairdresserId")]
        public Hairdresser Hairdresser { get; set; }
        public ICollection<EmployeeService> EmployeeServices { get; set; }
        public ICollection<Shift> Shifts { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
