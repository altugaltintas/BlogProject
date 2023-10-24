using AutoMapper;
using Bloe_web.Areas.Admin.Models.DTOs;
using Bloe_web.Areas.Member.Models.DTOs;
using Blog_Dal.Repositories.Concrete;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Blog_model.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloe_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryAdmin : Controller
    {


        private readonly IMapper _mapper;
        private readonly ICategoryRepo _categoryRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserFollowedCategoryRepo _usercateRepo;

        public CategoryAdmin(IMapper mapper, ICategoryRepo categoryRepo, UserManager<AppUser> userManager, IUserFollowedCategoryRepo usercateRepo)
        {
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _userManager = userManager;
            _usercateRepo = usercateRepo;
        }

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]

        public IActionResult Create(AdminCreateCategoryDTO dTO)
        {
            if (ModelState.IsValid)
            {

                Category category = _mapper.Map<Category>(dTO);
                _categoryRepo.Create(category);

                return RedirectToAction("AktisList");
            }

            return View(dTO);
        }


        public async Task<IActionResult> AktisList()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);


            var list = _categoryRepo.GetCategories();
            return View(list);

        }
       


        [HttpGet]
        public IActionResult Update(int id)    // getirme işlemini yapyıro  ıd yakalayarak posta düşüyor 
        {
            Category category = _categoryRepo.GetDefault(a => a.ID == id);

            AdminUpdateCategoryDTO dTO = _mapper.Map<AdminUpdateCategoryDTO>(category);
            return View(dTO);

        }


        [HttpPost]
        public IActionResult Update(AdminUpdateCategoryDTO dTO)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(dTO);
                _categoryRepo.Update(category);
                return RedirectToAction("AktisList");
            }
            return View(dTO);

        }



        public IActionResult Delete(int id)
        {
            Category category = _categoryRepo.GetDefault(a => a.ID == id);

            _categoryRepo.Delete(category);

            return RedirectToAction("AktisList");

        }
        public IActionResult Active(int id)
        {
            Category category = _categoryRepo.GetDefault(a => a.ID == id);

            _categoryRepo.Active(category);

            return RedirectToAction("AktisList");

        }

    }
}
