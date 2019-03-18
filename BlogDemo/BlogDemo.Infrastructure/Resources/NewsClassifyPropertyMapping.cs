using BlogDemo.Core.Entities;
using BlogDemo.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Infrastructure.Resources
{
    public class NewsClassifyPropertyMapping:PropertyMapping<NewsClassifyResources, NewsClassify>
    {
        public NewsClassifyPropertyMapping() : base(
            new Dictionary<string, List<MappedProperty>>(StringComparer.OrdinalIgnoreCase)//StringComparer.OrdinalIgnoreCase 不区分大小写
            {
                [nameof(NewsClassifyResources.Id)] = new List<MappedProperty>
                {
                    new MappedProperty{ Name = nameof(NewsClassify.Id), Revert = false}
                },
                [nameof(NewsClassifyResources.Name)] = new List<MappedProperty>
                {
                    new MappedProperty{ Name = nameof(NewsClassify.Name), Revert = false}
                },
                [nameof(NewsClassifyResources.Sort)] = new List<MappedProperty>
                {
                    new MappedProperty{ Name = nameof(NewsClassify.Sort), Revert = false}
                }
               

            })
        {

        }
    }
}
