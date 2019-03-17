using BlogDemo.Core.Entities;
using BlogDemo.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogDemo.Core.Interface
{
    public interface INewsClassifyRepository
    {
        Task<PaginatedList<NewsClassify>> GetPagingNewsClassify(NewsClassifyQueryParameters newsClassifyQueryParameters);
        Task<IEnumerable<NewsClassify>> GetAllNewsClassifys();
        Task<NewsClassify> GetSearchOneNewsClassify(Expression<Func<NewsClassify, bool>> where);
        void AddNewsClassify(AddNewsClassify newsClassify);
        void DeleteNewsClassifyById(int id);
        void EditNewsClassify(EditNewsClassify banner);

    }
}
