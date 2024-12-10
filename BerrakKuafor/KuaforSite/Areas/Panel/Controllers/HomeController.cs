using EntityLayer.Concretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KuaforSite.Areas.Panel.Controllers
{
    [Area("Panel")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Rolata()
        {
            var user = await _userManager.FindByEmailAsync("oktay@gmail.com");
            await _userManager.AddToRoleAsync(user, "Member");
            return RedirectToAction(nameof(Index),"Home");
        }
    }
}
