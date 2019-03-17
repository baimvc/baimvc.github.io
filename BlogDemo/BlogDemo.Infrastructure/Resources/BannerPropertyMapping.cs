using BlogDemo.Core.Entities;
using BlogDemo.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Infrastructure.Resources
{
    public class BannerPropertyMapping:PropertyMapping<BannerResources,Banner>
    {
        public BannerPropertyMapping() : base(
            new Dictionary<string, List<MappedProperty>>(StringComparer.OrdinalIgnoreCase)
            {
                [nameof(BannerResources.Image)] = new List<MappedProperty>
                {
                    new MappedProperty{ Name = nameof(Banner.Image), Revert = false}
                },
                [nameof(BannerResources.Url)] = new List<MappedProperty>
                {
                    new MappedProperty{ Name = nameof(Banner.Url), Revert = false}
                }
                
                
            })
        {

        }
    }
}
