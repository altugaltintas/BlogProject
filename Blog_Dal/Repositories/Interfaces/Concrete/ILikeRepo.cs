using Blog_model.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Dal.Repositories.Interfaces.Concrete
{
    public interface ILikeRepo
    {

        void Create(Like like);

        void Delete(Like like);

        Like GetDefault(Expression<Func<Like, bool>> expression);    

    }
}
