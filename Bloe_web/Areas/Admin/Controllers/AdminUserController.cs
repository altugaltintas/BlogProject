﻿using Bloe_web.Areas.Admin.Models.VMs;
using Bloe_web.Areas.Member.Models.DTOs;
using Blog_Dal.Context;
using Blog_Dal.Repositories.Abstract;
using Blog_Dal.Repositories.Interfaces.Abstract;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Blog_model.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloe_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserRepo _userRepo;
        private readonly ProjectContext _project;

        public AdminUserController(UserManager<AppUser> userManager, IAppUserRepo userRepo, ProjectContext project)
        {
            _userManager = userManager;
            _userRepo = userRepo;
            _project = project;
        }

       
        public IActionResult List()
        {
            // Tüm kullanıcıları al
            List<AppUser> users = _userManager.Users.ToList();

            return View(users);
        }


        public IActionResult Update(string id)    // getirme işlemini yapyıro  ıd yakalayarak posta düşüyor 
        {
            AppUser appUser = _userManager.FindByIdAsync(id).Result;

            
            return View(appUser);

        }


        [HttpPost]
        public async Task<IActionResult> Update(AppUser app)
        {
            if (ModelState.IsValid)
            {
                //AppUser appUser = _userManager.FindByIdAsync(app.Id).Result;
                AppUser appUser = await _userManager.FindByIdAsync(app.Id);

                if (appUser != null)
                {
                    // Kullanıcı bilgilerini güncelleme
                    appUser.FirstName = app.FirstName;
                    appUser.LastName = app.LastName;
                    //appUser.Password = app.Password;
                    //appUser.PasswordHash = app.g.;
                    if (!string.IsNullOrEmpty(app.Password))
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                        var resetPasswordResult = await _userManager.ResetPasswordAsync(appUser, token, app.Password);
                        appUser.Password = app.Password;
                    }


                    // Kullanıcıyı güncelleme işlemi
                    //var result = _userManager.UpdateAsync(appUser).Result;
                    var updateResult = await _userManager.UpdateAsync(appUser);

                    if (updateResult.Succeeded)
                    {
                        
                        return RedirectToAction("List");
                    }
                    
                }
                else
                {
                    
                    return RedirectToAction("List");
                }

            }
            return View("List");

        }

        public async Task< IActionResult> Delete(string id)
        {
            AppUser appUser = _userManager.FindByIdAsync(id).Result;

            await _userRepo.Delete(appUser);

            var users = await _userManager.Users.ToListAsync();
            return View("List", users);

        }
        public async Task< IActionResult> Active(string id)
        {
            AppUser appUser = _userManager.FindByIdAsync(id).Result;

            await _userRepo.Active(appUser);

            var users = await _userManager.Users.ToListAsync();
            return View("List", users);

        }
        public async Task<IActionResult> UserDetail(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);

            var activeArticleCount = _project.Articles
                        .Where(a => a.AppUserID == appUser.Id && a.Statu != Statu.Passive).Count();

            var user = new AdminWritterVM
            {


                UserFUllName = appUser.FullName,
                UserCreatedDate = appUser.CreatedDate,
                UserImage = appUser.ImagePath,
                AritcleCount = activeArticleCount.ToString()

            };


            return View(user);
        }
    }
}
