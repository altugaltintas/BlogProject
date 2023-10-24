using Blog_model.Models.Concrete;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bloe_web.Areas.Member.Models.VMs
{
    public class CommentVM
    {
        public int CommentID { get; set; }

        [Required(ErrorMessage = "Yorum metni gereklidir.")]
        [Display(Name = "Yorum")]
        public string Text { get; set; }

        public string AppUserID { get; set; }
        public int ArticleID { get; set; }

        public List<Comment> Comments { get; set; }

    }
}
