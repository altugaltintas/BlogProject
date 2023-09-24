using Bloe_web.Models.VMs;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                   CategoryID = a.CategoryID,
                   CategoryName = a.Category.Name,
                   UserID = a.AppUserID,
                   UserCreatedDate = a.AppUser.CreatedDate,
                   UserFUllName = a.AppUser.FullName,
                   UserImage = a.AppUser.ImagePath
               },
               expression: a => a.Statu != Statu.Passive && a.ID == id,
               include: a => a.Include(a => a.AppUser).Include(a => a.Category)
               );
            return View(article);
        }
    }
}
