using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Threading.Tasks;
using Blog_model.Models.Enums;
using Bloe_web.Areas.Member.Models.VMs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace Bloe_web.Areas.Member.Views.Shared.Components.OldUser
{
    public class OldUserViewComponent :ViewComponent
    {


        //member areadeki anasayfada yani en eski ilk 4 kullanıcımızı ad,soyad,varsa full name, id ve bu zamana kadarki toplam makale sayısı aktif pasif herşey ve aktif makale sayısı   gösteren ufak car yapısını bu bilgileri basınız

        //vm oluştur userları getir , select seç  viawa gönder

        private readonly UserManager<AppUser> _userManager;
        

        public OldUserViewComponent(UserManager<AppUser> userManager)
        {
           _userManager = userManager;
            
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = await _userManager.Users.Where(a => a.Statu != Statu.Passive).OrderBy(a=> a.CreatedDate)
                                                    .Select(a => new TopFourUserVM()
                                                    {
                                                        FullNAme = a.FullName,
                                                        UserID = a.Id,
                                                        AllArticleCounnt = a.Articles.Count,
                                                        AllNotPassiveArticleCount = a.Articles.Count(a => a.Statu != Statu.Passive)

                                                    }).Take(4).ToListAsync();
            return View(list);
        }

       
    }
}
