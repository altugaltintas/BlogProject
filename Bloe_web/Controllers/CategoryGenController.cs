using AutoMapper;
using Bloe_web.Models.VMs;
using Blog_Dal.Repositories.Concrete;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Blog_model.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloe_web.Controllers
{
    public class CategoryGenController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepo _categoryRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IArticleRepo _articleRepo;

        public CategoryGenController(IMapper mapper, ICategoryRepo categoryRepo, UserManager<AppUser> userManager, IArticleRepo articleRepo)
        {
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _userManager = userManager;
            _articleRepo = articleRepo;
        }
        public IActionResult List()
        {

            var categories = _categoryRepo.GetDefaults(a => a.Statu != Statu.Passive);
            var list = categories.Select(a => new GetCategoryVM
            {
                ID = a.ID,
                Name = a.Name
            }).ToList();

            return View(list);
        }
        public IActionResult CategoryTop5(int id)
        {
            List<GetArticleVM> list = _articleRepo.GetByDefaults(
                selector: a => new GetArticleVM
                {
                    Title = a.Title,
                    Content = a.Content,
                    ImagePath = a.ImagePath,
                    CreatedDate = a.CreatedDate,
                    UserFullName = a.AppUser.FullName,
                    CategoryName = a.ArticleCategories.Select(ac => ac.Category.Name).ToList(),
                    AppUserID = a.AppUserID,
                    ArticleID = a.ID
                },
                expression: a => a.Statu != Statu.Passive && a.ArticleCategories.Any(ac => ac.CategoryID == id),
                include: a => a.Include(a => a.ArticleCategories).ThenInclude(ac => ac.Category),
                orderBy: a => a.OrderByDescending(a => a.CreatedDate)
            ).Take(6).ToList();

            return View(list);
        }

    }
}
