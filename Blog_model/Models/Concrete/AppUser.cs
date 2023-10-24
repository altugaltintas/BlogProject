using Blog_model.Models.Abstract;
using Blog_model.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Blog_model.Models.Concrete
{
    public class AppUser : IdentityUser
    {


        public AppUser()
        {
            Articles = new List<Article>();
            Comments = new List<Comment>();
            Likes = new List<Like>();
            UserFollowedCategories = new List<UserFollowedCategory>();
            
        }

        // buraya ıd eklemyioruz çünkü ıdentityuser kütüphanesinde var 

        private DateTime _createdDate = DateTime.Now;

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        private Statu _statu = Statu.Passive;

        public Statu Statu
        {
            get { return _statu; }
            set { _statu = value; }
        }



        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Password { get; set; }

        public string ImagePath { get; set; }   // foto kaynağı

        [NotMapped]   // veritabanı atmasın diye
        public IFormFile Image { get; set; }    // foto çekip almaya, okumaya , atmaya çalışacak ama dbde olmayacak


        //nav property

        // 1 kullanıcının çokça makalesi olabilri
        public List<Article> Articles { get; set; }

        // 1 kullanıcın takip ettiği çokça katagori olabilir
        public List<UserFollowedCategory> UserFollowedCategories { get; set; }

        // 1 kullanıcnın çokça yorumu olabilir
        public List<Comment> Comments { get; set; }

        // 1 kullanıcının çokça beğenisi olabilir
        public List<Like> Likes { get; set; }

    }
}
