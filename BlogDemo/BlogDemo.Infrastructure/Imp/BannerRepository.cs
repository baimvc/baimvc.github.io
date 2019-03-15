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
    public class BannerRepository : IBannerRepository
    {
        private readonly MyDBContext _db;
        private readonly IPropertyMappingContainer _propertyMappingContainer;

        public BannerRepository(
            MyDBContext db,
            IPropertyMappingContainer propertyMappingContainer
        )
        {
            _db = db;
            _propertyMappingContainer = propertyMappingContainer;
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
        /// 修改主题
        /// </summary>
        /// <param name="banner"></param>
        public void EditBanner(EditBanner banner)
        {
            var editBanner =  _db.Banner.Find(banner.Id);
            if (editBanner != null)
            {
                editBanner.Image = banner.Image;
                editBanner.Url = banner.Url;
                editBanner.AddTime = DateTime.Now;
                editBanner.Remark = banner.Remark;
                _db.Banner.Update(editBanner);
            }
           

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
            //OrderBy 根据字段动态排序
            qureyBanner = qureyBanner.ApplySort(bannerQueryParameters.OrderBy,_propertyMappingContainer.Resolve<BannerResources,Banner>());

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
            return await _db.Banner.OrderByDescending(x=>x.AddTime).ToListAsync();
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
            if (delBanner != null)
            {
                _db.Banner.Remove(delBanner);
            }
            
        }
        /// <summary>
        /// 根据主题条件返回数据
        /// </summary>
        /// <param name="banner"></param>
        /// <returns></returns>

        public async Task<Banner> GetSearchOneBanner(Expression<Func<Banner, bool>> where)
        {
            return await _db.Banner.FirstOrDefaultAsync(where);
        }
    }
}
