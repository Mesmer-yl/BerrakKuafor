using EntityLayer.ViewModels;

namespace KuaforSite.Models
{
    public class HairdressersModel
    {
        public HairdresserVM Hairdresser { get; set; }
        public List<ServiceVM> Services { get; set; }
    }
}
