using Bloe_web.Areas.Member.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bloe_web.Areas.Member.Models.VMs
{
    [Area("Member")]
    public class ArticleCreateVM
    {

      

        // article için proplaar ihtiyaç var

        [Required(ErrorMessage ="Bu alan Boş bırakılamaz")]
        [MinLength(2,ErrorMessage ="En az 2 karakter yazmalısınız")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Bu alan Boş bırakılamaz")]
        [MinLength(200, ErrorMessage = "En az 200 karakter yazmalısınız")]
        public string Content { get; set; }


        [NotMapped]
        [Required(ErrorMessage = "Lütfen bir resim seçiniz")]
        public IFormFile Image { get; set; }


        //CAtegory
        [Required(ErrorMessage = "Lütfen bir kategori seçiniz")]
        public int CategoryID { get; set; }



        public List<GetCategoryDTO> Categories { get; set; }

        //appuser
        [Required(ErrorMessage = "Lütfen bir kullanıcı seçiniz")]
        public string AppUserID { get; set; }


    }
}
