using Blog_Dal.Context;
using Blog_Dal.Repositories.Abstract;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Dal.Repositories.Concrete
{
    public class CommentRepo : BaseRepo<Comment>, ICommentRepo
    {
        public CommentRepo(ProjectContext context) : base(context)
        {
        }

        public void AddComment(Comment comment)
        {
           _context.Add(comment);
            _context.SaveChanges();
        }

        public List<Comment> GetCommentsForArticle(int articleID)
        {
            return _context.Comments
        .Where(c => c.ArticleID == articleID)
        .ToList();
        }
    }

   
}
