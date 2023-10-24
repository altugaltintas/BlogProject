using Microsoft.AspNetCore.Mvc;

namespace Bloe_web.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
