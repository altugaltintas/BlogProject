using Bloe_web.Areas.Member.Models.VMs;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bloe_web.Areas.Admins.Views.Shared.Components.AdminComments
{
    public class AdminCommentsViewComponent : ViewComponent
    {
        private readonly ICommentRepo _commentRepo;
        private readonly UserManager<AppUser> _userManager;

        public AdminCommentsViewComponent(ICommentRepo commentRepo, UserManager<AppUser> userManager)
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

    }
}
