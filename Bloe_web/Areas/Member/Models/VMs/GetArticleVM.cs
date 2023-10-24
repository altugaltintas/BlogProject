using System;
using System.Collections.Generic;

namespace Bloe_web.Areas.Member.Models.VMs
{
    public class GetArticleVM
    {

        //article

        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AppUserID { get; set; }
        public string Content { get; set; }

        //Categorty

        public List<string> CategoryName { get; set; }



        //user

        public string FullName { get; set; }
    }
}
