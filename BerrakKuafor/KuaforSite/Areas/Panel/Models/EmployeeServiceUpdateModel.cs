namespace KuaforSite.Areas.Panel.Models
{
    public class EmployeeServiceUpdateModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ServiceId { get; set; }
        public int Money { get; set; }
        public bool IsPro { get; set; }
        public int Duration { get; set; }
    }
}
