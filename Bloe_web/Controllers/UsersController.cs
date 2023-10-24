using AutoMapper;
using Bloe_web.Models.VMs;
using Blog_Dal.Context;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Blog_model.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Bloe_web.Controllers
{
    public class UsersController : Controller
    {


        private readonly IArticleRepo _articleRepo;
        private readonly ProjectContext _project;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(IArticleRepo articleRepo, UserManager<AppUser> userManager, IMapper mapper, ProjectContext project)
        {
            _articleRepo = articleRepo;
            _project = project;
            _userManager = userManager;
        }


        public async Task<IActionResult> UserDetail(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);

            var activeArticleCount = _project.Articles
                        .Where(a => a.AppUserID == appUser.Id && a.Statu != Statu.Passive).Count();

            var user = new WritterVM
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
