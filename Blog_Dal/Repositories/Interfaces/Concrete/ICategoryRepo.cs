using Blog_Dal.Repositories.Interfaces.Abstract;
using Blog_model.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Dal.Repositories.Interfaces.Concrete
{
    public interface ICategoryRepo : IBaseRepo<Category>
    {
        

        List<Category> GetCategoryWithID(string id);
        List<Category> GetCategories();

     
    }
}
