using Bloe_web.Areas.Member.Models.DTOs;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bloe_web.Areas.Member.Models.VMs
{
    public class ArticleUpdateVM
    {


        //Article
        public int ID { get; set; }



        [Required(ErrorMessage = "Bu alan Boş bırakılamaz")]
        [MinLength(2, ErrorMessage = "En az 2 karakter yazmalısınız")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Bu alan Boş bırakılamaz")]
        [MinLength(200, ErrorMessage = "En az 200 karakter yazmalısınız")]
        public string Content { get; set; }


        [NotMapped]
        [Required(ErrorMessage = "Lütfen bir resim seçiniz")]
        public IFormFile Image { get; set; }


        public string ImagePath { get; set; }

        public string OldImagePath { get; set; }

        //catagory

        [Required(ErrorMessage = "Lütfen bir kategori seçiniz")]
        public int CategoryID { get; set; }

        public List<GetCategoryDTO> Categories { get; set; }


        //user 
        [Required(ErrorMessage = "Lütfen bir kullanıcı seçiniz")]
        public string AppUserID { get; set; }
    }
}
