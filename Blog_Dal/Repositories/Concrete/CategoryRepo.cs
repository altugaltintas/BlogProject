using Blog_Dal.Context;
using Blog_Dal.Repositories.Abstract;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog_Dal.Repositories.Concrete
{
    public class CategoryRepo : BaseRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(ProjectContext context) : base(context)
        {
        }


        public List<Category> GetCategories()
        {
             return _context.Categories.ToList();
        }
      

        public List<Category> GetCategoryWithID(string id)
        {
            return _context.FollowedCategories.Include(a => a.AppUser).Include(a => a.Category).Where(a => a.AppUserID == id).Select(a => a.Category).ToList();
        }
    }
}
