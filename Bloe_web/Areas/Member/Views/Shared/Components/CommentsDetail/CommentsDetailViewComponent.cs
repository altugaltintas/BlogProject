using Bloe_web.Areas.Member.Models.VMs;
using Blog_Dal.Repositories.Concrete;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bloe_web.Areas.Member.Views.Shared.Components.CommentsDetail
{
    public class CommentsDetailViewComponent : ViewComponent
    {

        private readonly ICommentRepo _commentRepo;
        private readonly UserManager<AppUser> _userManager;

        public CommentsDetailViewComponent(ICommentRepo commentRepo, UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepo;
            _userManager = userManager;
        }
        public IViewComponentResult Invoke()
        {
            int articleID = Convert.ToInt32(ViewData["ArticleID"]);
            var comments = _commentRepo.GetCommentsForArticle(articleID); // Veritabanından yorumları al
            return View(comments);
        }
    }   
}
