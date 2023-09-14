using Blog_Dal.Context;
using Blog_Dal.Repositories.Abstract;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_Dal.Repositories.Concrete
{
    public class CategoryRepo : BaseRepo<Category>,ICategoryRepo
    {
        public CategoryRepo(ProjectContext context) : base(context)
        {
        }
    }
}
