using EntityLayer.Concretes;
using EntityLayer.ViewModels;
using KuaforSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstracts;

namespace KuaforSite.Controllers
{
    public class HairdresserController : Controller
    {
        private readonly IHairdresserService _hairdresserService;
        private readonly IReservationService _reservationService;
        private readonly UserManager<AppUser> _userManager;

        public HairdresserController(IHairdresserService hairdresserService, IReservationService reservationService, UserManager<AppUser> userManager)
        {
            _hairdresserService = hairdresserService;
            _reservationService = reservationService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(int hairdresserId)
        {
            var hairdresser = _hairdresserService.GetHairdresserById(hairdresserId);
            ViewBag.EmployeesWithServices = _reservationService.GetEmployeesByHairdresserId(hairdresserId);
            ViewBag.ServiceList = _reservationService.GetServiceByHairdresserId(hairdresserId);

            return View(hairdresser);
        }

        [HttpGet]
        public IActionResult Reservation(int hairdresserId, string hairdresserName, int employeeId)
        {
            if(!User.Identity.IsAuthenticated)return RedirectToAction("SignIn", "Authentication");
            ViewBag.HairdresserName= hairdresserName;
            ViewBag.HairdresserId= hairdresserId;
            var employee = _reservationService.GetEmployeesByHairdresserId(hairdresserId).Where(x=>x.EmployeeId==employeeId).Single();

            return View(employee);
        }

        [Authorize(Roles = "Admin,Moderatör,Employee,Member")]
        [HttpPost]
        public async Task<JsonResult> Reservation([FromBody] CheckReservationModel model)
        {
            model.CheckDate = model.CheckDate.Date;
            bool isOk = _reservationService.CheckReservation(model.CheckEmployeeId,model.CheckDate,model.CheckTime,model.TotalDuration);
            if (!isOk) {
                var jsonError = new { message = "Seçtiğiniz saat aralığında başka bir randevu veya seçilen tarihte çalışmama durumu olabilir!"};
                return Json(jsonError);
            }
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var addReservationVM = new ReservationAddVM()
            {
                HairdresserId=model.checkHairdresserId,
                EmployeeId = model.CheckEmployeeId,
                UserId = currentUser!.Id,
                StartTime=TimeSpan.FromMinutes(model.CheckTime),
                EndTime=TimeSpan.FromMinutes(model.TotalDuration + model.CheckTime),
                Date = model.CheckDate,
                Money = model.TotalMoney,
                Services = model.SelectedServices
            };
            _reservationService.CreateReservation(addReservationVM);
            var jsonSuccess = new { message = "Randevunuz başarıyla oluşturulmuştur" };
            return Json(jsonSuccess);
        }
    }
}
