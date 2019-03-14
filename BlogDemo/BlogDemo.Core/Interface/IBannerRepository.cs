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
        Task<PaginatedList<Banner>> GetPagingBanners(BannerQueryParameters bannerQueryParameters);
        Task<IEnumerable<Banner>> GetAllBanners();
        Task<Banner> GetSearchBanner(string image);
        void AddBanner(PostBanner banner);
        void DeleteBannerById(int id);
        Task<Banner> GetBannerByIdAsync(int id);
    }
}
