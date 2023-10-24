using Bloe_web.Areas.Member.Models.DTOs;
using Bloe_web.Areas.Member.Models.VMs;
using Bloe_web.Models.VMs;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ArticleDetailVM = Bloe_web.Models.VMs.ArticleDetailVM;
using GetArticleVM = Bloe_web.Models.VMs.GetArticleVM;

namespace Bloe_web.Controllers
{
    public class ArticleController : Controller
    {
        public IArticleRepo _articleRepo { get; }

        public ArticleController(IArticleRepo articleRepo)
        {
            _articleRepo = articleRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)

        {
            var article = _articleRepo.GetByDefault
               (selector: a => new ArticleDetailVM()
               {
                   ArticleID = a.ID,
                   Title = a.Title,
                   CreatedDate = a.CreatedDate,
                   Image = a.ImagePath,
                   Content = a.Content,
                   Likes = a.Likes,
                   CategoryID = a.ArticleCategories.Select(ac => ac.Category.ID).ToList(),
                   CategoryName = a.ArticleCategories.Select(ac => ac.Category.Name).ToList(),
                   UserID = a.AppUserID,
                   UserCreatedDate = a.AppUser.CreatedDate,
                   UserFUllName = a.AppUser.FullName,
                   UserImage = a.AppUser.ImagePath,
                   OkunmaSayisi = a.OkunmaSayisi,
                   Categories = a.ArticleCategories.Select(ac => new GetCategoryDTO
                   {
                       ID = ac.Category.ID,
                       Name = ac.Category.Name
                   }).ToList()
               },
               expression: a => a.Statu != Statu.Passive && a.ID == id,
               include: a => a.Include(a => a.AppUser).Include(a => a.ArticleCategories).ThenInclude(ac => ac.Category)
               );

            var article1 = _articleRepo.GetDefault(a => a.ID == id);
            if (article1 != null)
            {
                article1.OkunmaSayisi++;
                _articleRepo.Update(article1);
            }

            int karakterSayisi = article.Content.Length;
            double süre = 2.0 / 100; // 100 karakter için 2 dakika

            double okumaSuresi = karakterSayisi * süre;

            article.OkumaSuresi = okumaSuresi.ToString();
            return View(article);
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
