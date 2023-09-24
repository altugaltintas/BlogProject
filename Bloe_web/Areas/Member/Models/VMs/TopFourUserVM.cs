using Blog_model.Models.Concrete;
using System.Collections.Generic;
using System;

namespace Bloe_web.Areas.Member.Models.VMs
{
    public class TopFourUserVM
    {
       

        //user 

        public string UserID { get; set; }      
        public string FullNAme { get; set; }

        public int AllArticleCounnt { get; set; }
        public int AllNotPassiveArticleCount { get; set; }
       
    }
}
