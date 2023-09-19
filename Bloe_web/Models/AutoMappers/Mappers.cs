using AutoMapper;
using Bloe_web.Areas.Member.Models.DTOs;
using Bloe_web.Areas.Member.Models.VMs;
using Bloe_web.Models.DTOs;
using Blog_model.Models.Concrete;

namespace Bloe_web.Models.AutoMappers
{
    public class Mappers : Profile
    {

        public Mappers()
        {
            // maplemeler
            CreateMap<RegisterDTO, AppUser>();    // registerDTO --- > appUser nesnesi teslim etmeslisin

            CreateMap<CreateCategoryDTO, Category>();

            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();

            CreateMap<ArticleCreateVM, Article>();

            CreateMap<Article,ArticleUpdateVM>().ReverseMap();
        }
    }
}
