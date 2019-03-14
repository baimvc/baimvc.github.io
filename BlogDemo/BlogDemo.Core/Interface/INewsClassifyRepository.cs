using BlogDemo.Core.Entities;
using BlogDemo.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Core.Interface
{
    public interface INewsClassifyRepository
    {
        Task<PaginatedList<NewsClassify>> GetPagingNewsClassify(NewsClassifyQueryParameters newsClassifyQueryParameters);
        void AddNewsClassify(AddNewsClassify newsClassify);
        Task<IEnumerable<NewsClassify>> GetAllNewsClassifys();
        Task<NewsClassify> GetSearchNewsClassify(string name);
        void DeleteNewsClassifyById(int id);
        Task<NewsClassify> GetNewsClassifyByIdAsync(int id);
    }
}
