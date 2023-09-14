using Blog_model.Models.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bloe_web.Areas.Member.Controllers
{

    [Area("Member")]
    public class AppUserController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> LogOut()
        {

            await _signInManager.SignOutAsync();

            return Redirect("~/");   // redirectionAction("index","home"); yerine 

        }
    }
}
