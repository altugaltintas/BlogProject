using Bloe_web.Areas.Member.Models.DTOs;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Blog_model.Models.Concrete;
using System;

namespace Bloe_web.Areas.Member.Models.VMs
{
    public class ArticleDetailVM
    {
        //Article
        public int ArticleID { get; set; }        
        public string Title { get; set; }       
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Like> Likes { get; set; }

        public string OkumaSuresi { get; set; }

        public int OkunmaSayisi { get; set; }

        //catagory

        public List<int> CategoryID { get; set; }
        public List<string> CategoryName { get; set; }

         public List<GetCategoryDTO> Categories { get; set; }
        //user 

        public string UserID { get; set; }
        public string UserFUllName { get; set; }
        public string UserImage { get; set; }


        public string AppUserID { get; set; } // claımleri onaylanmış içerdeki kişi illa makale yazarı olmak zorunda değil


        public DateTime UserCreatedDate { get; set; }

        // toDo : cometn???

       
    }
}
