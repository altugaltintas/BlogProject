using Blog_model.Models.Concrete;
using System.Collections.Generic;
using System;

namespace Bloe_web.Models.VMs
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


        //catagory

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }


        //user 

        public string UserID { get; set; }
        public string UserFUllName { get; set; }
        public string UserImage { get; set; }
        public DateTime UserCreatedDate { get; set; }

       

    }
}
