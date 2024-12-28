
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KuaforSite.Areas.Panel.Controllers
{
    [Authorize(Roles = "Admin,Moderatör")]
    [Area("Panel")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
