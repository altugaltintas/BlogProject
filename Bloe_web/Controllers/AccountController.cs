﻿using AutoMapper;
using Bloe_web.Models.DTOs;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Blog_model.Models.Enums;
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
        public async Task<IActionResult> Register(RegisterDTO dTO)
        {
            if (ModelState.IsValid)   //automapper var unutma 
            {
                bool isEmailUnique = _userRepo.IsEmailUnique(dTO.Email);
                bool isUserNameUnique = _userRepo.IsUserlUnique(dTO.UserName);

                if (isEmailUnique && isUserNameUnique)
                {
                    AppUser appUser = _mapper.Map<AppUser>(dTO);

                    var image = Image.Load(dTO.Image.OpenReadStream());
                    image.Mutate(a => a.Resize(70, 70));
                    image.Save($"wwwroot/Resimler/{appUser.UserName}.jpg");
                    appUser.ImagePath = $"/Resimler/{appUser.UserName}.jpg";

                    await _userRepo.Create(appUser);
                    return RedirectToAction("Login");
                }

                if (!isEmailUnique)
                {
                    ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılıyor.");
                }

                if (!isUserNameUnique)
                {
                    ModelState.AddModelError("UserName", "Bu kullanıcı adı zaten kullanılıyor.");
                }

               

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
                    
                    if (appUser.Statu == Statu.Active || appUser.Statu == Statu.Modified)
                    {
                        SignInResult result = await _signInManager.PasswordSignInAsync(appUser.UserName, dTO.Password, false, false);

                        if (result.Succeeded)
                        {
                            var roles = await _userManager.GetRolesAsync(appUser);

                            if (roles.Contains("Admın"))
                            {
                                return Redirect(dTO.ReturnUrl ?? "/admin/AppAdmin/index");
                            }
                            else
                            {
                                return Redirect(dTO.ReturnUrl ?? "/member/appuser/index");
                            }
                        }
                    }
                    else
                    {
                        
                        return View(dTO);
                    }

                   
                }

            }
            return View(dTO);

        }
    }
}
