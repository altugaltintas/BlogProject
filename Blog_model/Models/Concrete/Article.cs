using Blog_model.Models.Abstract;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_model.Models.Concrete
{
    public class Article:BaseEntity
    {

        public Article()
        {
            Likes = new List<Like>();
            Comments = new List<Comment>();   // list yapısı olarak  navporp var ise çoklu ilişliklerde constructor olarak eklenmesi gerekiyor
            //Categories = new List<Category>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImagePath { get; set; }

        // okuma sayısı 
        public int OkunmaSayisi { get; set; }


        [NotMapped]
        public IFormFile Image { get; set; }

        //nav prop

        // 1 makalenin bir yazarı

        public string AppUserID { get; set; }   // idendtiy  küpüphanesinden olduğu için  int değil string aldık
        public AppUser AppUser{ get; set; }    //  1 yazası olduğu için appuserin app sueri var dedik list almadık  çoka tek ilişki örneği



        // 1 makalenin çokça beğenisi

        public  List<Like> Likes { get; set; }

        // 1 makalenin çokça yorumu

        public List<Comment> Comments { get; set; }






        // 1 makalenin 1 kategorisi


        //public int CategoryID { get; set; }

        //public Category Category { get; set; }

        public List<ArticleCategory> ArticleCategories { get; set; }

      


    }
}