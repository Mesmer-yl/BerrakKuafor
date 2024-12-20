using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels
{
    public class EmployeeWithServiceVM
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } 
        public List<EServiceModel>? Services { get; set; }  
    }
    public class EmployeeWithDetailsViewModel
    {
        public int EmployeeId { get; set; }
        public int ServiceId { get; set; }
        public string EmployeeName { get; set; }
        public bool IsPro { get; set; }         
        public int Money { get; set; }        
        public TimeSpan Duration { get; set; }  
        public string ServiceName { get; set; }  
    }
    public class EServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPro { get; set; }      
        public int Money { get; set; }         
        public TimeSpan Duration { get; set; } 
    }
}
