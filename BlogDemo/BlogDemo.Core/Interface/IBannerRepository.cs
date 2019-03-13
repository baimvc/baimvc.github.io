using BlogDemo.Core.Entities;
using BlogDemo.Core.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Core.Interface
{
    public interface IBannerRepository
    {
        Task<PaginatedList<Banner>> GetALLBanners(BannerQueryParameters bannerQueryParameters);
        void AddBanner(PostBanner banner);
        Task<Banner> GetPostByIdAsync(int id);
    }
}
