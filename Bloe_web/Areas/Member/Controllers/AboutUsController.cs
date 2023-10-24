using Microsoft.AspNetCore.Mvc;

namespace Bloe_web.Areas.Member
{
    [Area("Member")]
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
