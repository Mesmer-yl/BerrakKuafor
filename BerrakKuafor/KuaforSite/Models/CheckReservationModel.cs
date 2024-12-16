namespace KuaforSite.Models
{
    public class CheckReservationModel
    {
        public string SelectedServices { get; set; }
        public int TotalDuration { get; set; } 
        public int TotalMoney { get; set; } 
        public int CheckEmployeeId { get; set; } 
        public int checkHairdresserId { get; set; }
        public DateTime CheckDate { get; set; } 
        public int CheckTime { get; set; } 
    }

}
