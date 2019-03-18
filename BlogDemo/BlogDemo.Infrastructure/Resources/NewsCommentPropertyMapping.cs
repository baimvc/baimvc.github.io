using BlogDemo.Core.Entities;
using BlogDemo.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Infrastructure.Resources
{
    public class NewsCommentPropertyMapping : PropertyMapping<NewsCommentResources, NewsComment>
    {
        public NewsCommentPropertyMapping() : base(
          new Dictionary<string, List<MappedProperty>>(StringComparer.OrdinalIgnoreCase)//StringComparer.OrdinalIgnoreCase 不区分大小写
           {
              [nameof(NewsCommentResources.Id)] = new List<MappedProperty>
              {
                    new MappedProperty{ Name = nameof(NewsComment.Id), Revert = false}
              },
              [nameof(NewsCommentResources.NewsName)] = new List<MappedProperty>
              {
                    new MappedProperty{ Name = nameof(NewsComment.News.Title), Revert = false}
              },
              
              [nameof(NewsCommentResources.AddTime)] = new List<MappedProperty>
              {
                    new MappedProperty{ Name = nameof(NewsComment.AddTime), Revert = false}
              }


          })
        {

        }

    }
}
