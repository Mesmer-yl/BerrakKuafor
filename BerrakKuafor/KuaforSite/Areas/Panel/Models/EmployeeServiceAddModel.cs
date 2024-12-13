namespace KuaforSite.Areas.Panel.Models
{
    public class EmployeeServiceAddModel
    {
        public int EmployeeId { get; set; }
        public int ServiceId { get; set; }
        public int Money { get; set; }
        public bool IsPro { get; set; }
        public int Duration { get; set; }
    }
}
