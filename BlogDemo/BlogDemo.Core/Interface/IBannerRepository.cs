using BlogDemo.Core.Entities;
using BlogDemo.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogDemo.Core.Interface
{
    public interface IBannerRepository
    {
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="bannerQueryParameters"></param>
        /// <returns></returns>
        Task<PaginatedList<Banner>> GetPagingBanners(BannerQueryParameters bannerQueryParameters);
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Banner>> GetAllBanners();
        /// <summary>
        /// 根据条件获取单条数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<Banner> GetSearchOneBanner(Expression<Func<Banner,bool>> where);
        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Banner> GetBannerByIdAsync(int id);
        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="banner"></param>
        void AddBanner(PostBanner banner);
        /// <summary>
        /// 修改主题
        /// </summary>
        /// <param name="banner"></param>
        void EditBanner(EditBanner banner);
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="id"></param>
        void DeleteBannerById(int id);
       
    }
}
