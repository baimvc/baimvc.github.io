using BlogDemo.Core.Entities;
using BlogDemo.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Core.Interface
{
    public interface  INewsRepository
    {
        Task<PaginatedList<News>> GetPagingNews(NewsQueryParameters newsQueryParameters);
        Task<IEnumerable<News>> GetAllNews();
        Task<News> GetSearchOneNews(Expression<Func<News, bool>> where);
        void AddNews(AddNews news);
        void DeleteNewsById(int id);
        void EditNews(EditNews editbanner);

    }
}
