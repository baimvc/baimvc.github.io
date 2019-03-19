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
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Infrastructure.Imp
{
    public class NewsRepository : INewsRepository
    {
        private readonly MyDBContext _db;
        private readonly IPropertyMappingContainer _propertyMappingContainer;

        public NewsRepository(
            MyDBContext db,
            IPropertyMappingContainer propertyMappingContainer
        )
        {
            _db = db;
            _propertyMappingContainer = propertyMappingContainer;
        }

        public async Task<IEnumerable<News>> GetAllNews()
        {
            return await _db.News.Where(x=>x.State!=1).Include("NewsClassify").Include("NewsComment").OrderByDescending(x => x.PublishDate).ToListAsync();
        }

        public async Task<PaginatedList<News>> GetPagingNews(NewsQueryParameters newsQueryParameters)
        {
            var qureyNews = _db.News.Where(x => x.State != 1).Include("NewsClassify").Include("NewsComment").AsQueryable();
            if (!string.IsNullOrEmpty(newsQueryParameters.Title))
            {
                qureyNews = qureyNews.Where(x => x.Title.ToLowerInvariant() == newsQueryParameters.Title.ToLowerInvariant());
            }
            qureyNews = qureyNews.ApplySort(newsQueryParameters.OrderBy, _propertyMappingContainer.Resolve<NewsResources, News>());

            var count = await qureyNews.CountAsync();

            var data = await qureyNews
                .Skip(newsQueryParameters.PageIndex * newsQueryParameters.PageSize)
                .Take(newsQueryParameters.PageSize)
                .ToListAsync();

            return new PaginatedList<News>(newsQueryParameters.PageIndex, newsQueryParameters.PageSize, count, data);
        }

        public async Task<News> GetSearchOneNews(Expression<Func<News, bool>> where)
        {
            var news =  _db.News.Where(x => x.State != 1).Include("NewsClassify").Include("NewsComment").FirstOrDefaultAsync(where);

            return await news;
        }
        public void AddNews(AddNews news)
        {
           
            var newsModel = GetSearchOneNews(x => x.Id == news.NewsClassifyId);
            if (newsModel != null)
            {
                var n = new News
                {
                    NewsClassifyId = news.NewsClassifyId,
                    Title = news.Title,
                    Image = news.Image,
                    Contents = news.Contents,
                    PublishDate = DateTime.Now,
                    Remark = news.Remark,
                     CreatorName = "admin",
                      CreatorTime = DateTime.Now
                };
                _db.News.Add(n);
             

            }

        }

        public void DeleteNewsById(int id)
        {
            var delNews = _db.News.Find(id);
            if (delNews != null)
            {
                delNews.State = 1;
                _db.News.Update(delNews);
            }
        }

        public void EditNews(EditNews editNews)
        {
            var editNewsModel = _db.News.Find(editNews.Id);
            if (editNewsModel != null)
            {
                editNewsModel.Title = editNews.Title;
                editNewsModel.Image = editNews.Image;
                editNewsModel.NewsClassifyId = editNews.NewsClassifyId;
                editNewsModel.PublishDate = DateTime.Now;
                editNewsModel.Contents = editNews.Contents;
                editNewsModel.Remark = editNews.Remark;
                editNewsModel.UpdateName = "admin";
                editNewsModel.UpdateTime = DateTime.Now;

                _db.News.Update(editNewsModel);
            }
        }
        /// <summary>
        /// 获取最新评论的新闻集合
        /// </summary>
        /// <param name="where"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        public  List<NewsResources> GetNewsCommentNewsList(Expression<Func<News, bool>> where, int topCount)
        {
            var newsIds = _db.NewsComment.Where(x=>x.State!=1).OrderByDescending(x => x.AddTime).GroupBy(x => x.NewsId)
                .Select(x => x.Key)
                .Take(topCount);
            var newsListResources = new List<NewsResources>();
            if (newsIds != null)
            {
                var newsList = _db.News.Where(x => x.State != 1).Include("NewsClassify").Include("NewsComment").Where(x => newsIds.Contains(x.Id))
               .OrderByDescending(x => x.PublishDate);
               
                if (newsList != null)
                {
                    foreach (var news in newsList)
                    {
                        newsListResources.Add(new NewsResources
                        {
                            Id = news.Id,
                            NewsClassifyName = news.NewsClassify.Name,
                            Title = news.Title,
                            Image = news.Image,
                            Contents = news.Contents.Length > 50 ? news.Contents.Substring(0, 50) : news.Contents,
                            PublishDate = news.PublishDate,
                            CommentCount = news.NewsComment.Count(),
                            Remark = news.Remark

                        });
                    }
                }
                
            }
            return newsListResources;

        }
        /// <summary>
        /// 获取相关推荐
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public async Task<List<NewsResources>> GetNewsRepositoryList(int newsId)
        {
            var news = await _db.News.Where(x => x.State != 1).FirstOrDefaultAsync(x => x.Id == newsId);
            var newsListResources = new List<NewsResources>();
            if (news != null)
            {
                var newsList = await _db.News.Where(x => x.State != 1).Include("NewsComment")
                    .Where(x => x.NewsClassifyId == news.NewsClassifyId && x.Id != newsId)
                    .OrderByDescending(x => x.PublishDate).OrderByDescending(x => x.NewsComment.Count).Take(6).ToListAsync();
                if (newsList != null)
                {
                    foreach (var n in newsList)
                    {
                        newsListResources.Add(new NewsResources
                        {
                            Id = n.Id,
                            NewsClassifyName = n.NewsClassify.Name,
                            Title = n.Title,
                            Image = n.Image,
                            Contents = n.Contents.Length > 50 ? n.Contents.Substring(0, 50) : n.Contents,
                            PublishDate = n.PublishDate,
                            CommentCount = n.NewsComment.Count(),
                            Remark = n.Remark

                        });
                    }
                }

            }
            return newsListResources;


        }
    }
}
