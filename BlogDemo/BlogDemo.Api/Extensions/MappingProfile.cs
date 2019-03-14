using AutoMapper;
using BlogDemo.Core.Entities;
using BlogDemo.Infrastructure.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogDemo.Api.Extensions
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //新闻主题
            CreateMap<Banner, BannerResources>();
                //.ForMember(dest => dest.Updatetime, opt => opt.MapFrom(src => src.AddTime)); 
            CreateMap< BannerResources, Banner>();
            //新闻分类
            CreateMap<NewsClassifyResources, NewsClassify>();
            CreateMap< NewsClassify, NewsClassifyResources>();
        }
    }
}
