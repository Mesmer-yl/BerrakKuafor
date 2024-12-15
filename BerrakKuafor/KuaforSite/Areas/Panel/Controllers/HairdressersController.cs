using EntityLayer.Concretes;
using EntityLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServiceLayer.Services.Abstracts;

namespace KuaforSite.Areas.Panel.Controllers
{
    [Area("Panel")]
    public class HairdressersController : Controller
    {
        private readonly IHairdresserService _hairdresserService;
        public HairdressersController(IHairdresserService hairdresserService)
        {
            _hairdresserService = hairdresserService;
        }

        [HttpGet]
        public IActionResult NewHairdresser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewHairdresser(HairdresserAddVM _hairdresserAddVM)
        {
            if(!ModelState.IsValid) return View();
            var isSuccess = await _hairdresserService.CreateHairdresserAsync(_hairdresserAddVM);
            if(!isSuccess)
            {
                TempData["HairdresserErrorMessage"] = "Belirtilen yönetici sistemimizde üye değildir!";
                return View();
            }
            TempData["HairdresserMessage"] = "Yeni kuaför başarıyla eklendi.";
            return RedirectToAction(nameof(Index),"Hairdressers");
        }
        [HttpGet]
        public IActionResult Index()
        {
            var hairdresserListModel = _hairdresserService.GetAllHairdresser();
            return View(hairdresserListModel);
        }
        [HttpGet]
        public IActionResult UpdateHairdresser(int hairdresserId)
        {
            var updHairdresserModel = _hairdresserService.GetHairdresserById(hairdresserId);
            return View(updHairdresserModel);
        }
        [HttpPost]
        public IActionResult UpdateHairdresser(HairdresserUpdateVM _hairdresserUpdateVM)
        {
            if(!ModelState.IsValid) return View();
            _hairdresserService.UpdateHairdresserAsync(_hairdresserUpdateVM);
            TempData["HairdresserMessage"] = "Kuaför başarıyla güncellendi.";
            return RedirectToAction(nameof(UpdateHairdresser),"Hairdressers", new { hairdresserId = _hairdresserUpdateVM.Id});
        }
        [HttpGet]
        public IActionResult Services()
        {
            var serviceList = _hairdresserService.GetAllServices();
            return View(serviceList);
        }
        [HttpPost]
        public JsonResult AddService([FromBody] ServiceAddVM _serviceAddVM)
        {
            try
            {
                _hairdresserService.CreateService(_serviceAddVM);
                var jsonDepartment = new { saveText = _serviceAddVM.Name+" hizmeti başarıyla eklendi" };
               
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
        public JsonResult UpdateService([FromBody] ServiceVM _serviceVM)
        {
            try
            {
                _hairdresserService.UpdateService(_serviceVM);
                var jsonDepartment = new { saveText = _serviceVM.Name + " hizmeti başarıyla güncellendi" };
              
                Response.ContentType = "application/json";
                return Json(jsonDepartment);
            }
            catch (Exception ex)
            {
                var errorResponse = new { message = "Bir hata oluştu. Lütfen daha sonra tekrar deneyin." };
                return Json(errorResponse);
            }
        }
    }
}
