using EntityLayer.Concretes;
using EntityLayer.ViewModels;
using KuaforSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstracts;
using System.Diagnostics;

namespace KuaforSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHairdresserService _hairdresserService;
        private readonly IReservationService _reservationService;

        public HomeController(IHairdresserService hairdresserService, IReservationService reservationService)
        {
            _hairdresserService = hairdresserService;
            _reservationService = reservationService;
        }
        public IActionResult Index()
        {
            var hairdressers = _hairdresserService.GetAllHairdresser();
            var hairdresserList = new List<HairdressersModel>();
            foreach(var  hairdresser in hairdressers)
            {
                var serviceList = _reservationService.GetServiceByHairdresserId(hairdresser.Id);
                var h = new HairdressersModel()
                {
                    Hairdresser = hairdresser,
                    Services = serviceList
                };
                hairdresserList.Add(h);
            }
            return View(hairdresserList);
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
