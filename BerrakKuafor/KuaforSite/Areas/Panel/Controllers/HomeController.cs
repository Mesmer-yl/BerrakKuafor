
using Microsoft.AspNetCore.Mvc;

namespace KuaforSite.Areas.Panel.Controllers
{
    [Area("Panel")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
