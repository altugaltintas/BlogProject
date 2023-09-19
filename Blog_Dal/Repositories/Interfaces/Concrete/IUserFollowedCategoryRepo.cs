using Blog_model.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Blog_Dal.Repositories.Interfaces.Concrete
{
    public interface IUserFollowedCategoryRepo    // Baseden kalıtım almadığı için IbaseRepodan İmple olmaz
    {


        void Delete(UserFollowedCategory entity);

        List<UserFollowedCategory> GetFollowedCategories(Expression<Func<UserFollowedCategory, bool>> expression);

        UserFollowedCategory GetUserFollowedCategory(Expression<Func<UserFollowedCategory, bool>> expression);

    }
}
