using BlogDemo.Core.Entities;
using BlogDemo.Core.Interface;
using BlogDemo.Core.Request;
using BlogDemo.Infrastructure.Database;
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
    public class NewsCommentRepository : INewsCommentRepository
    {
        private readonly MyDBContext _db;
        private readonly IPropertyMappingContainer _propertyMappingContainer;
        private readonly INewsRepository _newsRepository;

        public NewsCommentRepository(
            MyDBContext db,
            IPropertyMappingContainer propertyMappingContainer,
            INewsRepository newsRepository
        )
        {
            _db = db;
            _propertyMappingContainer = propertyMappingContainer;
            _newsRepository = newsRepository;
        }
        /// <summary>
        /// 添加评论信息
        /// </summary>
        /// <param name="addComment"></param>
        public void AddComment(AddComment addComment)
        {
            var news = _newsRepository.GetSearchOneNews(x => x.Id == addComment.NewsId);
            if (news != null)
            {
                var com = new NewsComment
                {
                    AddTime = DateTime.Now,
                    NewsId = addComment.NewsId,
                    Contents = addComment.Contents,
                    Remark = addComment.Remark

                };
                _db.NewsComment.Add(com);

            }
        }
        /// <summary>
        /// 返回评论内容
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public async Task<NewsCommentModel> GetCommentById(int newsId)
        {
            var comment = await _newsRepository.GetSearchOneNews(x => x.Id == newsId);
            NewsCommentModel commentResources = new NewsCommentModel();
            if (comment != null)
            {
                commentResources = new NewsCommentModel()
                {
                    Contents = comment.Contents,
                    Floor = "" + comment.NewsComment.Count + 1,
                    AddTime = DateTime.Now
                };
            }
            return commentResources;

        }
        /// <summary>
        /// 删除某条评论
        /// </summary>
        /// <param name="id"></param>
        public void DeleteComment(int id)
        {
            var comm = _db.NewsComment.Find(id);
            if (comm != null)
            {
                _db.NewsComment.Remove(comm);
            }
        }
        public  List<NewsCommentModel> GetCommentList(Expression<Func<NewsComment, bool>> where)
        {
            var comments =  _db.NewsComment.Include("News").Where(where).OrderByDescending(x => x.AddTime).ToList();
            List<NewsCommentModel> commentsList = new List<NewsCommentModel>();

            if (comments != null)
            {
                int floor = 1;
                foreach (var com in comments)
                {
                    commentsList.Add(new NewsCommentModel()
                    {
                        Id = com.Id,
                        NewsName = com.News.Title,
                        Contents = com.Contents,
                        AddTime = com.AddTime,
                        Remark = com.Remark,
                        Floor ="#"+floor
                    });
                    floor++;
                }

            }
            commentsList.Reverse();
            return  commentsList;
        }
    }
}
