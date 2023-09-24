using Blog_Dal.Repositories.Interfaces.Abstract;
using Blog_model.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_Dal.Repositories.Interfaces.Concrete
{
    public interface ICategoryRepo : IBaseRepo<Category>
    {
        List<Category> GetCategoryWithID(string id);
    }
}
