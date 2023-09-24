using Bloe_web.Models.VMs;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bloe_web.Views.Shared.Components.ArticleWith
{
    public class ArticlesWithViewComponent : ViewComponent
    {
        private readonly IArticleRepo _articleRepo;


        // en güncel 10 makale getirelim


        public ArticlesWithViewComponent(IArticleRepo articleRepo)
        {
            _articleRepo = articleRepo;
        }

        public IViewComponentResult Invoke()
        {
            List<GetArticleVM> list = _articleRepo.GetByDefaults
                (
                selector: a => new GetArticleVM
                {
                    Title = a.Title,
                    Content = a.Content,
                    ImagePath = a.ImagePath,
                    CreatedDate = a.CreatedDate,
                    UserFullName = a.AppUser.FullName,
                    CategoryName = a.Category.Name,
                    AppUserID = a.AppUserID,
                    ArticleID = a.ID
                },
                expression: a=> a.Statu!=Statu.Passive,
                include: a=> a.Include(a=> a.Category).Include(a=> a.AppUser),
                orderBy: a=> a.OrderByDescending(a=> a.CreatedDate)
                ).Take(10).ToList();

            return View(list);
        }

    }
}
