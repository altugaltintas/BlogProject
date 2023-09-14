using Blog_Dal.Context;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Blog_model.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_Dal.Repositories.Concrete
{
    public class LikeRepo : ILikeRepo
    {
        private readonly ProjectContext _context;
        private readonly DbSet<Like> _table;

        public LikeRepo(ProjectContext context)
        {
            _context = context;
            _table = _context.Set<Like>();
        }


        public void Create(Like like)
        {
            _table.Add(like);
            _context.SaveChanges();
        }

        public void Delete(Like like)
        {
            
             _table.Remove(like);   // veritabanından siler.
            _context.SaveChanges();
        }
    }
}
