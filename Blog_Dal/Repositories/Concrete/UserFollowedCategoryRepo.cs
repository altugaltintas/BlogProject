using Blog_Dal.Context;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blog_Dal.Repositories.Concrete
{
    public class UserFollowedCategoryRepo : IUserFollowedCategoryRepo
    {
        private readonly ProjectContext _context;
        private readonly DbSet<UserFollowedCategory> _table;

        public UserFollowedCategoryRepo(ProjectContext context)
        {
            _context = context;
            _table = _context.Set<UserFollowedCategory>();
        }

        public void Delete(UserFollowedCategory entity)
        {
            _table.Remove(entity);
            _context.SaveChanges();
        }

        public List<UserFollowedCategory> GetFollowedCategories(Expression<Func<UserFollowedCategory, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }

        public UserFollowedCategory GetUserFollowedCategory(Expression<Func<UserFollowedCategory, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }
    }
}
