using BlogDemo.Core.Entities;
using BlogDemo.Core.Interface;
using BlogDemo.Core.Request;
using BlogDemo.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Infrastructure.Imp
{
    public class NewsClassifyRepository : INewsClassifyRepository
    {
        private readonly MyDBContext _db;

        public NewsClassifyRepository(MyDBContext db)
        {
            this._db = db;
        }

        public void AddNewsClassify(AddNewsClassify newsClassify)
        {
           
            var newsClassifyModel = new NewsClassify() {  Name = newsClassify.Name,  Sort = newsClassify.Sort,  Remark = newsClassify.Remark };

            _db.NewsClassify.Add(newsClassifyModel);
        }

        public void DeleteNewsClassifyById(int id)
        {
            var delNewsClassify = _db.NewsClassify.Find(id);
            _db.NewsClassify.Remove(delNewsClassify);
        }

        public async Task<PaginatedList<NewsClassify>> GetPagingNewsClassify(NewsClassifyQueryParameters newsClassifyQueryParameters)
        {
            var qureyNewsClassify = _db.NewsClassify.AsQueryable();
            if (!string.IsNullOrEmpty(newsClassifyQueryParameters.Name))
            {
                qureyNewsClassify = qureyNewsClassify.Where(x => x.Name.ToLowerInvariant() == newsClassifyQueryParameters.Name.ToLowerInvariant());
            }
            qureyNewsClassify = qureyNewsClassify.OrderBy(x => x.Sort);

            var count = await qureyNewsClassify.CountAsync();

            var data = await qureyNewsClassify
                .Skip(newsClassifyQueryParameters.PageIndex * newsClassifyQueryParameters.PageSize)
                .Take(newsClassifyQueryParameters.PageSize)
                .ToListAsync();

            return new PaginatedList<NewsClassify>(newsClassifyQueryParameters.PageIndex, newsClassifyQueryParameters.PageSize, count, data);
        }

        public async Task<IEnumerable<NewsClassify>> GetAllNewsClassifys()
        {
            return await _db.NewsClassify.ToListAsync();
        }
        public async Task<NewsClassify> GetSearchNewsClassify(string name)
        {
            return await _db.NewsClassify.FirstOrDefaultAsync(x => x.Name == name);
        }
        public async Task<NewsClassify> GetNewsClassifyByIdAsync(int id)
        {
            return await _db.NewsClassify.FindAsync(id);
        }

       
    }
}
