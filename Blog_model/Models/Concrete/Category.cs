using Blog_model.Models.Abstract;
using System.Collections.Generic;

namespace Blog_model.Models.Concrete
{
    public class Category:BaseEntity
    {

        public Category()
        {
            Articles = new List<Article>();
            UserFollowedCategories = new List<UserFollowedCategory>();
        }

        public string Name { get; set; }

        public string Desciription { get; set; }

        // nav prop

        // 1 kategorinin çokça makalsei olabilir  list olarak getir
        public List<Article> Articles { get; set; }


        // 1kategoriyi takip eden çokça kişi olabilir
        public List<UserFollowedCategory> UserFollowedCategories { get; set;}

    }
}