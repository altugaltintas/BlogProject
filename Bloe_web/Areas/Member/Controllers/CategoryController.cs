using Bloe_web.Areas.Member.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Blog_Dal.Repositories.Concrete;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Blog_model.Models.Enums;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bloe_web.Areas.Member.Controllers
{
    [Area("Member")]
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepo _categoryRepo;
        private readonly UserManager<AppUser> _userManager;

        public CategoryController(IMapper mapper, ICategoryRepo categoryRepo,UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _userManager = userManager;
        }


        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]

        public IActionResult Create(CreateCategoryDTO dTO)
        {
            if (ModelState.IsValid)
            {

                Category category = _mapper.Map<Category>(dTO);
                _categoryRepo.Create(category);

                return RedirectToAction("List");
            }

            return View(dTO);
        }


        public IActionResult List()
        {
            var list = _categoryRepo.GetDefaults(a => a.Statu != Statu.Passive);
            return View(list);

        }


        public IActionResult Update(int id)    // getirme işlemini yapyıro  ıd yakalayarak posta düşüyor 
        {
            Category category = _categoryRepo.GetDefault(a => a.ID == id);

            UpdateCategoryDTO dTO = _mapper.Map<UpdateCategoryDTO>(category);
            return View(dTO);

        }


        [HttpPost]
        public IActionResult Update(UpdateCategoryDTO dTO)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(dTO);
                _categoryRepo.Update(category);
                return RedirectToAction("List");
            }
            return View(dTO);

        }

        public async Task<IActionResult> Follow(int id)
        {
            Category category =_categoryRepo.GetDefault(a=> a.ID == id);

            AppUser appUser = await _userManager.GetUserAsync(User);

            category.UserFollowedCategories.Add
                (
                new UserFollowedCategory() { CategoryID=id,Category=category,AppUser=appUser,AppUserID=appUser.Id}
                
                );
            return RedirectToAction("List");
        }


        public IActionResult Delete(int id)
        {
            Category category = _categoryRepo.GetDefault(a => a.ID == id);

           _categoryRepo.Delete(category);

            return RedirectToAction("List");

        }
    }
}
