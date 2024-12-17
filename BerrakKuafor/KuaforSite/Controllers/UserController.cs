using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstracts;

namespace KuaforSite.Controllers
{
    [Authorize(Roles ="Admin,Moderatör,Employee,Member")]
    public class UserController : Controller
    {
        private readonly IReservationService _reservationService;

        public UserController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<IActionResult> MyReservations()
        {
            var reservations = await _reservationService.GetAllReservationsByUserId(User.Identity!.Name!);
            return View(reservations);
        }
    }
}
