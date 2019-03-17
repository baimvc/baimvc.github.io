using BlogDemo.Core.Entities;
using BlogDemo.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Infrastructure.Resources
{
    public class NewsPropertyMapping : PropertyMapping<NewsResources, News>
    {
        public NewsPropertyMapping() : base(
           new Dictionary<string, List<MappedProperty>>(StringComparer.OrdinalIgnoreCase)//StringComparer.OrdinalIgnoreCase 不区分大小写
            {
               [nameof(NewsResources.Title)] = new List<MappedProperty>
               {
                    new MappedProperty{ Name = nameof(News.Title), Revert = false}
               },
               [nameof(NewsResources.Image)] = new List<MappedProperty>
               {
                    new MappedProperty{ Name = nameof(News.Image), Revert = false}
               },
               [nameof(NewsResources.PublishDate)] = new List<MappedProperty>
               {
                    new MappedProperty{ Name = nameof(News.PublishDate), Revert = false}
               },
              

           })
        {

        }
    }
}
