using AutoMapper;
using Bloe_web.Areas.Member.Models.DTOs;
using Bloe_web.Areas.Member.Models.VMs;
using Blog_Dal.Context;
using Blog_Dal.Repositories.Concrete;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Blog_model.Models.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bloe_web.Areas.Member.Controllers
{

    [Area("Member")]

    public class ArticleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICategoryRepo _cRepo;
        private readonly IArticleRepo _articleRepo;
        private readonly IMapper _mapper;
        private readonly ProjectContext _project;

        public ArticleController(UserManager<AppUser> userManager, ICategoryRepo cRepo, IArticleRepo articleRepo, IMapper mapper, ProjectContext project)
        {
            _userManager = userManager;
            _cRepo = cRepo;
            _articleRepo = articleRepo;
            _mapper = mapper;
            _project = project;
        }


        public async Task<IActionResult> Create()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);

            ArticleCreateVM vM = new ArticleCreateVM()
            {

                AppUserID = appUser.Id,
                Categories = _cRepo.GetByDefaults
                    (selector: a => new GetCategoryDTO { ID = a.ID, Name = a.Name },
                      expression: a => a.Statu != Statu.Passive
                    )

            };

            return View(vM);
        }

        [HttpPost]
        public IActionResult Create(ArticleCreateVM vM)
        {


            if (ModelState.IsValid)
            {
                var article = _mapper.Map<Article>(vM);

                Guid guid = Guid.NewGuid();
                var image = Image.Load(vM.Image.OpenReadStream());  //using SixLabors.ImageSharp;kütüphansei 

                //Mutate şekil vermek demek
                image.Mutate(a => a.Resize(70, 70));
                image.Save($"wwwroot/Resimler/{guid}.jpg");


                article.ImagePath = $"/Resimler/{guid}.jpg";

                _articleRepo.Create(article);
                return RedirectToAction("List");

            }

            vM.Categories = _cRepo.GetByDefaults
                    (selector: a => new GetCategoryDTO { ID = a.ID, Name = a.Name },
                      expression: a => a.Statu != Statu.Passive
                    );



            return View(vM);
        }


        public async Task<IActionResult> List()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);  // kullnıcıyı aldık

            var list = _articleRepo.GetByDefaults  // daha sonrada _article repodan getdefauls altık list olduğu için getirdi
                (
                selector: a => new GetArticleVM()
                {
                    ArticleID = a.ID,
                    CategoryName = a.Category.Name,
                    FullName = a.AppUser.FullName,
                    Title = a.Title,
                    Image = a.ImagePath
                },
                expression: a => a.Statu != Statu.Passive && a.AppUserID == appUser.Id,
                include: a => a.Include(a => a.AppUser).Include(a => a.Category)

                );

            return View(list);
        }


        public IActionResult Update(int id)
        {
            Article article = _articleRepo.GetDefault(a => a.ID == id);

            var updatedArticle = _mapper.Map<ArticleUpdateVM>(article);


            updatedArticle.Categories = _cRepo.GetByDefaults
                (
                    selector: a => new GetCategoryDTO() { ID = a.ID, Name = a.Name },
                    expression: a => a.Statu != Statu.Passive
                );

            return View(updatedArticle);
        }
        [HttpPost]
        public IActionResult Update(ArticleUpdateVM vM)
        {


            if (ModelState.IsValid && vM.Image != null)
            {

                var article1 = _articleRepo.GetDefault(a => a.ID == vM.ID);

                if (article1 != null)
                {
                    string deger = article1.ImagePath;
                    System.IO.File.Delete($"wwwroot{deger}");

                    var article = _mapper.Map<Article>(vM);

                    using var image = Image.Load(vM.Image.OpenReadStream());
                    image.Mutate(a => a.Resize(70, 70));

                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/Resimler/{guid}.jpeg");

                    
                    article1.ImagePath = $"/Resimler/{guid}.jpeg";
                    
                    _project.Entry(article1).State = EntityState.Detached;

                    _articleRepo.Update(article1);
                    return RedirectToAction("List");
                }
                else
                {
                    return RedirectToAction("List");

                }
                //var article = _mapper.Map<Article>(vM);

                //using var image = Image.Load(vM.Image.OpenReadStream());
                //image.Mutate(a => a.Resize(70, 70));

                //Guid guid = Guid.NewGuid();
                //image.Save($"wwwroot/Resimler/{guid}.jpeg");

                //article.ImagePath = $"/Resimler/{guid}.jpeg";


                //_articleRepo.Update(article);
                //return RedirectToAction("List");

            }
            else
            {
                // Mevcut makaleyi DbContext üzerinden al
                var existingArticle = _project.Articles.FirstOrDefault(a => a.ID == vM.ID);

                if (existingArticle != null)
                {                    
                    vM.ImagePath = existingArticle.ImagePath;
                   
                    var article = _mapper.Map<Article>(vM);
                    
                    vM.Categories = _cRepo.GetByDefaults(
                        selector: a => new GetCategoryDTO { ID = a.ID, Name = a.Name },
                        expression: a => a.Statu != Statu.Passive);
                    
                    _project.Entry(existingArticle).CurrentValues.SetValues(article);
                    _project.SaveChanges();

                    return RedirectToAction("List");
                }

                return RedirectToAction("List");

                //vM.ImagePath = _articleRepo.GetDefault(a => a.ID == vM.ID).ImagePath;
                //var article = _mapper.Map<Article>(vM);


                //vM.Categories = _cRepo.GetByDefaults(
                // selector: a => new GetCategoryDTO { ID = a.ID, Name = a.Name },
                // expression: a => a.Statu != Statu.Passive);

                //_articleRepo.Update(article);
                //return RedirectToAction("List");
            }


            // toDo : kayıtlı kullanıcı , login olduktan sonra içeridideki bir başka kişinin makalesini günceleyemememli. """""""""" bu daha sonra yapılacak
            // toDo : negatif senaryoda yani validasyon kontrolleri gerçekleşmezse categories cshtmlde sorun olur  // yapıldı 

            // toDo : Foto güncellenmezse eski foto kullanıcıya sunulmalı isterse günecllenemeli   // yapıldı

            // toDo : Foto günecllerse bu maakaleye ait eski fotorağ wwwrootan silinmeli yerine yeni foto  eklenmeli   """"""""""""""" bir tek bu kaldı


        }

        public IActionResult Delete(int id)

        {

            // toDo Başkası tarafından silinmemeli
            Article article = _articleRepo.GetDefault(a => a.ID == id);

            _articleRepo.Delete(article);
            return RedirectToAction("List");
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
