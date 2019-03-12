using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogDemo.Core.Entities;
using BlogDemo.Core.Interface;
using BlogDemo.Core.Request;
using BlogDemo.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;



namespace BlogDemo.Api.Controllers
{
    [Route("api/Banners")]
    public class BannerController : Controller
    {
        private readonly IBannerRepository  _bannerRepository;
        //工作单元模式
        private readonly IUnitOfWork _unitOfWork;

        public BannerController(IBannerRepository bannerRepository,IUnitOfWork unitOfWork)
        {
            _bannerRepository = bannerRepository;
            this._unitOfWork = unitOfWork;
        }

        // GET: /<controller>/
        public async Task<IActionResult>  GetAsync()
        {
            var banner = await _bannerRepository.GetALLBanners();
            return Ok(banner);
        }
        [HttpPost]
        public IActionResult AddBanner([FromBody] PostBanner postBanner)
        {
            var banner = new Banner {
                Image = "图片7",
                Url = "www.baidu.com",
                AddTime = DateTime.Now,
                Remark = "百度"
                 
            };
            _bannerRepository.AddBanner(postBanner);
            return Ok();
        }

    }
}
