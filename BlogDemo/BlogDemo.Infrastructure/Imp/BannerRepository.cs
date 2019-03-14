using BlogDemo.Core.Entities;
using BlogDemo.Core.Interface;
using BlogDemo.Core.Request;
using BlogDemo.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Infrastructure.Imp
{
    public class BannerRepository : IBannerRepository
    {
        private readonly MyDBContext _db;

        public BannerRepository(MyDBContext db)
        {
            this._db = db;
        }
        /// <summary>
        /// 添加主题
        /// </summary>
        /// <param name="banner"></param>
     
        public void AddBanner(PostBanner banner)
        {
            var bannerModel = new Banner() {  Image = banner.Image, Url = banner.Url, AddTime = banner.AddTime, Remark = banner.Remark};
             _db.Banner.Add(bannerModel);
        }

        /// <summary>
        /// 获取所有主题
        /// </summary>
        /// <returns></returns>
        public async Task<PaginatedList<Banner>> GetPagingBanners(BannerQueryParameters bannerQueryParameters)
        {
            var qureyBanner = _db.Banner.AsQueryable();
            if (!string.IsNullOrEmpty(bannerQueryParameters.Image))
            {
                qureyBanner =  qureyBanner.Where(x => x.Image.ToLowerInvariant() == bannerQueryParameters.Image.ToLowerInvariant());
            }
            qureyBanner = qureyBanner.OrderByDescending(x => x.AddTime);

            var count = await qureyBanner.CountAsync();

            var data = await qureyBanner
                .Skip(bannerQueryParameters.PageIndex * bannerQueryParameters.PageSize)
                .Take(bannerQueryParameters.PageSize)
                .ToListAsync();

            return new PaginatedList<Banner>(bannerQueryParameters.PageIndex, bannerQueryParameters.PageSize, count, data);
        }
        /// <summary>
        /// 返回所有的主题数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Banner>> GetAllBanners()
        {
            return await _db.Banner.ToListAsync();
        }
        /// <summary>
        /// 根据主题条件返回数据
        /// </summary>
        /// <param name="banner"></param>
        /// <returns></returns>
        public async Task<Banner> GetSearchBanner(string image)
        {
            return await _db.Banner.FirstOrDefaultAsync(x => x.Image == image);
        }
        /// <summary>
        /// 根据ID获取主题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Banner> GetBannerByIdAsync(int id)
        {
            return await _db.Banner.FindAsync(id);

        }
        /// <summary>
        /// 根据ID删除主题
        /// </summary>
        /// <returns></returns>
        public void DeleteBannerById(int id)
        {
            var delBanner = _db.Banner.Find(id);
            _db.Banner.Remove(delBanner);
        }
    }
}
