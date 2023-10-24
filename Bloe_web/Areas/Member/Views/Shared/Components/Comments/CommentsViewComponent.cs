using Bloe_web.Areas.Member.Models.VMs;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bloe_web.Areas.Member.Views.Shared.Components.Comments
{
    public class CommentsViewComponent : ViewComponent
    {
        private readonly ICommentRepo _commentRepo;
        private readonly UserManager<AppUser> _userManager;

        public CommentsViewComponent(ICommentRepo commentRepo, UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepo;
            _userManager = userManager;
        }
        public IViewComponentResult Invoke()
        {
            int articleID = Convert.ToInt32(ViewData["ArticleID"]);
            var model = new CommentVM
            {
                ArticleID = articleID,
               
            };

            return View(model);
        }

        //[HttpPost]
        //public IActionResult AddComment(CommentVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Yeni yorum oluşturun ve CommentService'i kullanarak kaydedin
        //        var newComment = new Comment
        //        {
        //            Text = model.Text,
        //            ArticleID = model.ArticleID,
        //            AppUserID = model.AppUserID
        //        };

        //        _commentRepo.AddComment(newComment);
        //    }

        //    return (IActionResult)View("Detail", new { articleID = model.ArticleID });

        //}

    }
}
