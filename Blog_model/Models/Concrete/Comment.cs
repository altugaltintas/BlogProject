using Blog_model.Models.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace Blog_model.Models.Concrete
{
    public class Comment : BaseEntity
    {

        public string Text { get; set; }



        //nav prop


        // 1 yorum 1 kişinindir
        public string AppUserID { get; set; }
        public AppUser AppUser { get; set; }

        // 1 yorum 1 makeleye atittir

        public int ArticleID { get; set; }
        public Article Article { get; set; }

        
    }
}