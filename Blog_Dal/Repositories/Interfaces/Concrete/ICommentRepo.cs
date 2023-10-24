using Blog_Dal.Repositories.Interfaces.Abstract;
using Blog_model.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Dal.Repositories.Interfaces.Concrete
{
    public interface ICommentRepo : IBaseRepo<Comment>
    {
        void AddComment(Comment comment);
        List<Comment> GetCommentsForArticle(int articleID);

    }
}
