using BlogDemo.Core.Entities;
using BlogDemo.Core.Interface;
using BlogDemo.Core.Request;
using BlogDemo.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
             _db.Add(banner);
        }

        /// <summary>
        /// 获取所有主题
        /// </summary>
        /// <returns></returns>
        public async Task<List<Banner>> GetALLBanners()
        {
            return await _db.Banner.ToListAsync();
        }

    }
}
