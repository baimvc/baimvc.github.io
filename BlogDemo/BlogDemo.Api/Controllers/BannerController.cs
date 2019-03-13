using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogDemo.Core.Entities;
using BlogDemo.Core.Interface;
using BlogDemo.Core.Request;
using BlogDemo.Infrastructure.Database;
using BlogDemo.Infrastructure.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlogDemo.Api.Controllers
{
    [Route("api/Banners")]
    public class BannerController : Controller
    {
        private readonly IBannerRepository  _bannerRepository;
        //工作单元模式
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _loggerFactory;
        private readonly IMapper _mapper;

        public BannerController(
            IBannerRepository bannerRepository,
            IUnitOfWork unitOfWork,
            ILoggerFactory loggerFactory,
            IMapper mapper
            )
        {
            //注入这两个实例所用的都是同一个MyDBContext
            _bannerRepository = bannerRepository;
            _unitOfWork = unitOfWork;
            _loggerFactory = loggerFactory.CreateLogger("BlogDemo.Api.Controllers.BannerController");
            _mapper = mapper;
        }

        // GET: /<controller>/
        public async Task<IActionResult>  GetAsync(BannerQueryParameters bannerQueryParameters)
        {
            var bannerList = await _bannerRepository.GetALLBanners(bannerQueryParameters);

            var bannerResources = _mapper.Map<IEnumerable<Banner>,IEnumerable<BannerResources>>(bannerList);
            var meta = new {
                PageSize = bannerList.PageSize,
                PageIndex = bannerList.PageIndex,
                PageCount = bannerList.PageCount,
                TotalItemsCount = bannerList.TotalItemsCount
                
            };
            Response.Headers.Add("X-Pagintion", JsonConvert.SerializeObject(meta, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));

            return Ok(bannerResources);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var post =  await _bannerRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            var bannerResources = _mapper.Map<Banner, BannerResources>(post);
            return Ok(bannerResources);
        }
        [HttpPost]
        public async Task<IActionResult> AddBanner([FromBody] PostBanner postBanner)
        {
           
            _bannerRepository.AddBanner(postBanner);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

    }
}
