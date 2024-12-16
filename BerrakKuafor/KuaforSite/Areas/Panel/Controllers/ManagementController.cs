using DataLayer.Repos.Abstracts;
using EntityLayer.Concretes;
using EntityLayer.ViewModels;
using KuaforSite.Areas.Panel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstracts;

namespace KuaforSite.Areas.Panel.Controllers
{
    [Area("Panel")]
    public class ManagementController : Controller
    {
        private readonly IHairdresserService _hairdresserService;
        private readonly IEmployeeService _employeeService;
        private readonly UserManager<AppUser> _userManager;
        private string _email => User.Identity!.Name!;

        public ManagementController(IHairdresserService hairdresserService, UserManager<AppUser> userManager, 
            IEmployeeService employeeService)
        {
            _hairdresserService = hairdresserService;
            _userManager = userManager;
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var updHairdresser = await _hairdresserService.GetHairdresserIdByUserAsync(_email);
            return View(updHairdresser);
        }
        [HttpPost]
        public IActionResult Index(HairdresserUpdateVM _hairdresserUpdateVM)
        {
            if (!ModelState.IsValid) return View();
            _hairdresserService.UpdateHairdresserAsync(_hairdresserUpdateVM);
            TempData["UpdHairdresserMessage"] = "Güncelleme işlemi başarılı.";
            return RedirectToAction(nameof(Index),"Management");
        }
        [HttpGet]
        public async Task<IActionResult> Employees()
        {
            var updHairdresser = await _hairdresserService.GetHairdresserIdByUserAsync(_email);
            var employeesModel = _employeeService.GetAllEmployeesWithUser(updHairdresser.Id);
            return View(employeesModel);
        }
        [HttpPost]
        public async Task<JsonResult> Employees([FromBody] EmployeeAddVM _employeeAddVM)
        {
            try
            {
                var updHairdresser = await _hairdresserService.GetHairdresserIdByUserAsync(_email);
                await _employeeService.CreateEmployee(_employeeAddVM, updHairdresser.Id);
                var jsonDepartment = new { saveText = _employeeAddVM.Email + " kullanıcı başarıyla çalışan olarak eklendi" };
                Response.ContentType = "application/json";
                return Json(jsonDepartment);
            }
            catch (Exception ex)
            {
                var errorResponse = new { message = "Bir hata oluştu. Lütfen daha sonra tekrar deneyin." };
                return Json(errorResponse);
            }
        }
        [HttpGet]
        public IActionResult UpdateEmployee(int employeeId)
        {
            ViewBag.Services = _hairdresserService.GetAllServices();
            ViewBag.EmployeeId = employeeId;
            var employeeServiceVM = _employeeService.GetAllEmployeeService(employeeId);
            return View(employeeServiceVM);
        }

        [HttpPost]
        public JsonResult AddEmployeeService([FromBody] EmployeeServiceAddModel addEmployeeServiceVM)
        {
            try
            {
                EmployeeServiceAddVM addEmployeeServiceVM2 = new EmployeeServiceAddVM()
                {
                    EmployeeId = addEmployeeServiceVM.EmployeeId,
                    ServiceId= addEmployeeServiceVM.ServiceId,
                    Duration = TimeSpan.FromMinutes(addEmployeeServiceVM.Duration),
                    Money = addEmployeeServiceVM.Money,
                    IsPro= addEmployeeServiceVM.IsPro
                };
                 _employeeService.CreateEmployeeService(addEmployeeServiceVM2);
                var jsonDepartment = new { saveText = "Eklendi." };
                Response.ContentType = "application/json";
                return Json(jsonDepartment);
            }
            catch (Exception ex)
            {
                var errorResponse = new { message = "Bir hata oluştu. Lütfen daha sonra tekrar deneyin." };
                return Json(errorResponse);
            }
        }
        [HttpPost]
        public JsonResult UpdateEmployeeService([FromBody] EmployeeServiceUpdateModel _employeeServiceUpdateVM)
        {
            try
            {
                EmployeeServiceUpdateVM _employeeServiceUpdateVM2 = new EmployeeServiceUpdateVM()
                {
                    Id= _employeeServiceUpdateVM.Id,
                    EmployeeId = _employeeServiceUpdateVM.EmployeeId,
                    ServiceId = _employeeServiceUpdateVM.ServiceId,
                    Duration = TimeSpan.FromMinutes(_employeeServiceUpdateVM.Duration),
                    Money = _employeeServiceUpdateVM.Money,
                    IsPro = _employeeServiceUpdateVM.IsPro
                };
                _employeeService.UpdateEmployeeService(_employeeServiceUpdateVM2);
                var jsonDepartment = new { saveText = "Güncellendi." };
                Response.ContentType = "application/json";
                return Json(jsonDepartment);
            }
            catch (Exception ex)
            {
                var errorResponse = new { message = "Bir hata oluştu. Lütfen daha sonra tekrar deneyin." };
                return Json(errorResponse);
            }
        }
        [HttpGet]
        public IActionResult Shifts(int employeeId)
        {
            ViewBag.Shifts = _employeeService.GetAllShiftByEmployee(employeeId);
            ViewBag.EmployeeId = employeeId;
            return View();
        }
        [HttpPost]
        public IActionResult Shifts(ShiftAddVM _shiftAddVM)
        {
            if (!ModelState.IsValid) return View();
            _employeeService.AddOrUpdateShift(_shiftAddVM);
            return RedirectToAction(nameof(Shifts),"Management", new {employeeId = _shiftAddVM.EmployeeId});
        }
        [HttpGet]
        public IActionResult Reservations()
        {
            return View();
        }
    }
}
