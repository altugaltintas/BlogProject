using AutoMapper;
using Bloe_web.Areas.Admin.Models.DTOs;
using Bloe_web.Areas.Admin.Models.VMs;
using Bloe_web.Areas.Member.Models.DTOs;
using Bloe_web.Areas.Member.Models.VMs;
using Blog_Dal.Context;
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

namespace Bloe_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICategoryRepo _cRepo;
        private readonly IArticleRepo _articleRepo;
        private readonly IMapper _mapper;
        private readonly ProjectContext _project;
        private readonly ILikeRepo _likeRepo;
        private readonly ICommentRepo _commentRepo;

        public ArticleController(UserManager<AppUser> userManager, ICategoryRepo cRepo, IArticleRepo articleRepo, IMapper mapper, ProjectContext project, ILikeRepo likeRepo, ICommentRepo commentRepo)
        {
            _userManager = userManager;
            _cRepo = cRepo;
            _articleRepo = articleRepo;
            _mapper = mapper;
            _project = project;
            _likeRepo = likeRepo;
            _commentRepo = commentRepo;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Detail(int id)

        {
            AppUser appUser = await _userManager.GetUserAsync(User);

            var article = _articleRepo.GetByDefault
               (selector: a => new AdminArticleDetailVM()
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
                   AppUserID = appUser.Id,
                   OkunmaSayisi = a.OkunmaSayisi,
                   Categories = a.ArticleCategories.Select(ac => new AdminGetCategoryDTO
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


            ViewData["ArticleID"] = article.ArticleID;

            int karakterSayisi = article.Content.Length;
            double süre = 2.0 / 100; // 100 karakter için 2 dakika

            double okumaSuresi = karakterSayisi * süre;

            article.OkumaSuresi = okumaSuresi.ToString();
            return View(article);
        }
        public async Task<IActionResult> Like(int id)
        {
            Article article = _articleRepo.GetDefault(a => a.ID == id);

            AppUser appUser = await _userManager.GetUserAsync(User);

            Like like = new Like() { ArticleID = id, Article = article, AppUser = appUser, AppUserID = appUser.Id };

            _likeRepo.Create(like);

            return RedirectToAction("Detail", new { id = id }); // burada detail actionuna gönderdiğimz parametre değişkenin adı neyse ona elimizdeki MakaleID yi atıyoruz çünkü detail sayfası makaleID siz çalışmaz
        }
        public async Task<IActionResult> Unlike(int id) //makaleid
        {
            Article article = _articleRepo.GetDefault(a => a.ID == id);

            AppUser appUser = await _userManager.GetUserAsync(User);


            Like like = _likeRepo.GetDefault(a => a.AppUserID == appUser.Id && a.ArticleID == article.ID);

            _likeRepo.Delete(like);

            return RedirectToAction("Detail", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentVM vM)
        {

            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.GetUserAsync(User);


                var comment = new Comment
                {
                    Text = vM.Text,
                    AppUserID = appUser.Id,
                    ArticleID = vM.ArticleID
                };

                _commentRepo.Create(comment);

                return RedirectToAction("Detail", new { id = vM.ArticleID });
            }

            return RedirectToAction("Detail", new { id = vM.ArticleID });
        }
        public IActionResult CategoryTop(int id)
        {
            List<AdminGetArticleVM> list = _articleRepo.GetByDefaults(
                selector: a => new AdminGetArticleVM
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
            ).ToList();

            return View(list);
        }
    }
}
