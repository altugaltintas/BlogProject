using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bloe_web.Areas.Member.Views.Shared.Components.FollowedCategory
{
    public class FollowedCategoryViewComponent : ViewComponent
    {
        // kullanıcımın takip ettği kategoriler sunmak 


        private readonly ICategoryRepo _categoryRepo;
        private readonly UserManager<AppUser> _userManager;

        public FollowedCategoryViewComponent(ICategoryRepo categoryRepo, UserManager<AppUser> userManager)
        {
            _categoryRepo = categoryRepo;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser appUser = await _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);

            List<Category> list = _categoryRepo.GetCategoryWithID(appUser.Id);

            return View(list);
        }
    }
}
