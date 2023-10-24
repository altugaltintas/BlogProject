using Blog_model.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Bloe_web.Areas.Admin.Models.DTOs
{
    public class AdminUpdateCategoryDTO
    {



        [Required(ErrorMessage = "Boş Bırakılamaz")]
        [MinLength(3, ErrorMessage = "En az 3 karakter olmalı")]
        public string Name { get; set; }



        [Required(ErrorMessage = "Boş Bırakılamaz")]
        [MinLength(3, ErrorMessage = "En az 3 karakter olmalı")]
        [MaxLength(250, ErrorMessage = "En fazla 250 karakter olmalı")]
        public string Desciription { get; set; }


        public Statu Statu { get; set; }



        public int ID { get; set; }
    }
}
