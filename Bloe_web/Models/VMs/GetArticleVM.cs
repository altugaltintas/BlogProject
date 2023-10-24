using System;
using System.Collections.Generic;

namespace Bloe_web.Models.VMs
{
    public class GetArticleVM
    {

        public string Title { get; set; }
        public string Content { get; set; }
        public int ArticleID { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AppUserID { get; set; }
        //public string CategoryName { get; set; }
        public List<string> CategoryName { get; set; }
        public string UserFullName { get; set; }



    }
}
