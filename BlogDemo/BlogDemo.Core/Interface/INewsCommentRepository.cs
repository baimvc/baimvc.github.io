using BlogDemo.Core.Entities;
using BlogDemo.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Core.Interface
{
    public interface INewsCommentRepository
    {
        void AddComment(AddComment addComment);
        Task<NewsCommentModel> GetCommentById(int newsId);
        void DeleteComment(int id);
        List<NewsCommentModel> GetCommentList(Expression<Func<NewsComment, bool>> where);
    }
}
