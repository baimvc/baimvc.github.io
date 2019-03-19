using BlogDemo.Core.Entities;
using BlogDemo.Core.Interface;
using BlogDemo.Core.Request;
using BlogDemo.Infrastructure.Database;
using BlogDemo.Infrastructure.Extensions;
using BlogDemo.Infrastructure.Resources;
using BlogDemo.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogDemo.Infrastructure.Imp
{
    public class NewsClassifyRepository : INewsClassifyRepository
    {
        private readonly MyDBContext _db;
        private readonly IPropertyMappingContainer _propertyMappingContainer;

        public NewsClassifyRepository(
            MyDBContext db,
            IPropertyMappingContainer propertyMappingContainer
        )
        {
            _db = db;
            _propertyMappingContainer = propertyMappingContainer;
        }

      
        public async Task<PaginatedList<NewsClassify>> GetPagingNewsClassify(NewsClassifyQueryParameters newsClassifyQueryParameters)
        {
            var qureyNewsClassify = _db.NewsClassify.Where(x=>x.State!=1).AsQueryable();
            if (!string.IsNullOrEmpty(newsClassifyQueryParameters.Name))
            {
                qureyNewsClassify = qureyNewsClassify.Where(x => x.Name.ToLowerInvariant() == newsClassifyQueryParameters.Name.ToLowerInvariant());
            }
            qureyNewsClassify = qureyNewsClassify.ApplySort(newsClassifyQueryParameters.OrderBy, _propertyMappingContainer.Resolve<NewsClassifyResources, NewsClassify>());

            var count = await qureyNewsClassify.CountAsync();

            var data = await qureyNewsClassify
                .Skip(newsClassifyQueryParameters.PageIndex * newsClassifyQueryParameters.PageSize)
                .Take(newsClassifyQueryParameters.PageSize)
                .ToListAsync();

            return new PaginatedList<NewsClassify>(newsClassifyQueryParameters.PageIndex, newsClassifyQueryParameters.PageSize, count, data);
        }

        public async Task<IEnumerable<NewsClassify>> GetAllNewsClassifys()
        {
            return await _db.NewsClassify.Where(x=>x.State!=1).OrderByDescending(x=>x.Sort).ToListAsync();
        }
        

        public async Task<NewsClassify> GetSearchOneNewsClassify(Expression<Func<NewsClassify, bool>> where)
        {
            return await _db.NewsClassify.Where(x=>x.State!=1).FirstOrDefaultAsync(where);
        }
        public void AddNewsClassify(AddNewsClassify newsClassify)
        {
            if (newsClassify != null)
            {
                var newsClassifyModel = new NewsClassify()
                {
                    Name = newsClassify.Name,
                    Sort = newsClassify.Sort,
                    Remark = newsClassify.Remark,
                     CreatorName = "admin",
                      CreatorTime = DateTime.Now
                };

                _db.NewsClassify.Add(newsClassifyModel);
            }
           
        }

        public void DeleteNewsClassifyById(int id)
        {
            var delNewsClassify = _db.NewsClassify.Find(id);
            if (delNewsClassify != null)
            {
                delNewsClassify.State = 1;
                _db.NewsClassify.Update(delNewsClassify);
            }
            
        }
       
        public void EditNewsClassify(EditNewsClassify newsClassify)
        {
            var editEditNewsClassify = _db.NewsClassify.Find(newsClassify.Id);
            if (editEditNewsClassify != null)
            {
                editEditNewsClassify.Name = newsClassify.Name;
                editEditNewsClassify.Sort = newsClassify.Sort;
                editEditNewsClassify.Remark = newsClassify.Remark;
                editEditNewsClassify.UpdateName = "admin";
                editEditNewsClassify.UpdateTime = DateTime.Now;

                _db.NewsClassify.Update(editEditNewsClassify);
            }
        }
    }
}
