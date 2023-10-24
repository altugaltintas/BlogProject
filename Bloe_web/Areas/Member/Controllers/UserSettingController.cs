﻿using AutoMapper;
using Bloe_web.Areas.Member.Models.DTOs;
using Blog_Dal.Context;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors;
using System.Collections.Generic;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Processing;
using System;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Bloe_web.Areas.Member.Models.VMs;

namespace Bloe_web.Areas.Member.Controllers
{

    [Area("Member")]
    public class UserSettingController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICategoryRepo _cRepo;
        private readonly IArticleRepo _articleRepo;
        private readonly IMapper _mapper;
        private readonly ProjectContext _project;
        private readonly ILikeRepo _likeRepo;
        private readonly IAppUserRepo _userRepo;
        private readonly SignInManager<AppUser> _signInManager;

        public UserSettingController(UserManager<AppUser> userManager, ICategoryRepo cRepo, IArticleRepo articleRepo, IMapper mapper, ProjectContext project, ILikeRepo likeRepo, IAppUserRepo userRepo, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _cRepo = cRepo;
            _articleRepo = articleRepo;
            _mapper = mapper;
            _project = project;
            _likeRepo = likeRepo;
            _userRepo = userRepo;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Setting(int id)
        {
            AppUser appUser = await _userManager.GetUserAsync(User);

            var updateuser = _mapper.Map<UserUpdateDTO>(appUser);


            return View(updateuser);


        }

        [HttpPost]
        public async Task<IActionResult> Setting(UserUpdateDTO dTO)
        {
            if (ModelState.IsValid && dTO.Image != null)
            {

                AppUser appUser = await _userManager.GetUserAsync(User);  //kullanıcı bilgileri

                string currentUserEmail = appUser.Email;
                string currentUSer = appUser.UserName;
                if (!_userRepo.IsEmailUniqueHaric(dTO.Email, currentUserEmail))
                {
                    ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılıyor.");
                    return View(dTO);
                }
                if (!_userRepo.IsUserlUniqueHaric(dTO.UserName, currentUSer))
                {
                    ModelState.AddModelError("UserName", "Bu kullanıcı adı zaten mevcut.");
                    return View(dTO);
                }

                appUser.FirstName = dTO.FirstName;
                appUser.LastName = dTO.LastName;
                appUser.Email = dTO.Email;
                appUser.UserName = dTO.UserName;

                if (!string.IsNullOrEmpty(dTO.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                    var result = await _userManager.ResetPasswordAsync(appUser, token, dTO.Password);
                    appUser.Password = dTO.Password;
                }

                string deger = appUser.ImagePath;
                System.IO.File.Delete($"wwwroot{deger}");

                // Resmi yeniden boyutlandır ve kaydet
                using var image = Image.Load(dTO.Image.OpenReadStream());
                image.Mutate(a => a.Resize(70, 70));

                Guid guid = Guid.NewGuid();

                image.Save($"wwwroot/Resimler/{guid}.jpeg");
                appUser.ImagePath = $"/Resimler/{guid}.jpeg";

                var updateResult = await _userManager.UpdateAsync(appUser);


                return RedirectToAction("Setting");

            }

            else
            {
                AppUser appUser = await _userManager.GetUserAsync(User);

                string currentUserEmail = appUser.Email;
                string currentUSer = appUser.UserName;
                if (!_userRepo.IsEmailUniqueHaric(dTO.Email, currentUserEmail))
                {
                    dTO.ImagePath = appUser.ImagePath;
                    ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılıyor.");
                    return View(dTO);
                }
                if (!_userRepo.IsUserlUniqueHaric(dTO.UserName, currentUSer))
                {
                    dTO.ImagePath = appUser.ImagePath;
                    ModelState.AddModelError("UserName", "Bu kullanıcı adı zaten mevcut.");
                    return View(dTO);
                }

                appUser.FirstName = dTO.FirstName;
                appUser.LastName = dTO.LastName;
                appUser.Email = dTO.Email;
                appUser.UserName = dTO.UserName;

                dTO.ImagePath = appUser.ImagePath;

                var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                var result = await _userManager.ResetPasswordAsync(appUser, token, dTO.Password);
                appUser.Password = dTO.Password;


                var updateResult = await _userManager.UpdateAsync(appUser);

                return RedirectToAction("Setting");

            }
        }

        public async Task<IActionResult> Delete()
        {
            var appUser = await _userManager.GetUserAsync(User);

            await _userRepo.Delete(appUser);

            return RedirectToAction("LogOut");

        }

        public async Task<IActionResult> LogOut()
        {

            await _signInManager.SignOutAsync();

            return Redirect("~/");   // redirectionAction("index","home"); yerine 

        }
        public async Task<IActionResult> ChangePassword(ChangePasswordVM vM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var passwordHistoryLimit = 3; // Son 3 şifreyi kontrol et
                    var isPasswordHistoryViolated = await _userRepo.IsPasswordHistoryViolatedAsync(user.Id, vM.NewPassword, passwordHistoryLimit);

                    if (isPasswordHistoryViolated)
                    {
                        ModelState.AddModelError("NewPassword", "Yeni şifre geçmiş şifrelerden birini içeremez.");
                        return View(vM);
                    }

                    // Şifre değiştirme işlemi
                    var result = await _userManager.ChangePasswordAsync(user, vM.OldPassword, vM.NewPassword);
                    user.Password = vM.NewPassword;
                    if (result.Succeeded)
                    {
                        // Şifre değiştirme işlemi başarılı oldu, eski şifre bilgisini OldPasswordHistory tablosuna ekleyin
                        var oldPasswordHistory = new OldPasswordHistory
                        {
                            UserId = user.Id,
                            PasswordHash = user.PasswordHash, // Eski şifre bilgisini kullanın
                            CreatedDate = DateTime.Now
                        };
                        _project.oldPasswordHistories.Add(oldPasswordHistory);
                        _project.SaveChanges();

                        // Oturumu kapatma işlemi
                        await _signInManager.SignOutAsync();

                        // Başarılı işlem
                        return RedirectToAction("LogOut");
                    }
                    else
                    {
                        // Şifre değiştirme işlemi başarısız
                        // Hata işleme kodları
                    }
                }
            }

            return View(vM);
        }

        //public async Task<IActionResult> ChangePassword(ChangePasswordVM vM)
        //{ 
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.GetUserAsync(User);
        //        if (user != null)
        //        {
        //            var passwordHistoryLimit = 3; // Son 3 şifreyi kontrol et
        //            var isPasswordHistoryViolated = await _userRepo.IsPasswordHistoryViolatedAsync(user.Id, vM.NewPassword, passwordHistoryLimit);

        //            if (isPasswordHistoryViolated)
        //            {
        //                ModelState.AddModelError(string.Empty, "Yeni şifre geçmiş şifrelerden birini içeremez.");
        //                return View(vM);
        //            }

        //            // Şifre değiştirme işlemi
        //            var result = await _userManager.ChangePasswordAsync(user, vM.OldPassword, vM.NewPassword);
        //            user.Password = vM.NewPassword;

        //            if (result.Succeeded)
        //            {
        //                // Başarılı işlem
        //                return RedirectToAction("LogOut");
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }

        //    return View(vM);
        //}

    }
}
