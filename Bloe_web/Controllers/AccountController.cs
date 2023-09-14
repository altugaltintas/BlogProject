using AutoMapper;
using Bloe_web.Models.DTOs;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Bloe_web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAppUserRepo _userRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(IMapper mapper, IAppUserRepo userRepo, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        public  async  Task<IActionResult> Register(RegisterDTO dTO)
        {
            if (ModelState.IsValid)   //automapper var unutma 
            {
                AppUser appUser = _mapper.Map<AppUser>(dTO);


                var image = Image.Load(dTO.Image.OpenReadStream());  //using SixLabors.ImageSharp;kütüphansei 

                //Mutate şekil vermek demek
                image.Mutate(a => a.Resize(70, 70));
                image.Save($"wwwroot/Resimler/{appUser.UserName}.jpg");
                appUser.ImagePath = $"/Resimler/{appUser.UserName}.jpg";

                await _userRepo.Create(appUser);
                return RedirectToAction("Login");   // kayıttan sonra login ekranına döndürüyor

            }

            return View(dTO);
        }


        public IActionResult Login(string returnUrl)  //kişinin ulaşmak istediği sayfa 
        {

            return View(new LoginDTO() { ReturnUrl = returnUrl });
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginDTO dTO)
        {

            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByEmailAsync(dTO.Email);
                if (appUser != null)
                {
                    SignInResult result = await _signInManager.PasswordSignInAsync(appUser.UserName, dTO.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(dTO.ReturnUrl ?? "/member/appuser/index");
                    }
                }

            }
            return View(dTO);

        }
    }
}
