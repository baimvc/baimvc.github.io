using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogDemo.Core.Entities;
using BlogDemo.Core.Interface;
using BlogDemo.Core.Request;
using BlogDemo.Core.Response;
using BlogDemo.Infrastructure.Database;
using BlogDemo.Infrastructure.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlogDemo.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BannerController : Controller
    {
        private readonly IBannerRepository _bannerRepository;
        //工作单元模式
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _loggerFactory;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

        public BannerController(
            IBannerRepository bannerRepository,
            IUnitOfWork unitOfWork,
            ILoggerFactory loggerFactory,
            IMapper mapper,
            IUrlHelper urlHelper
        )
        {
            //注入这两个实例所用的都是同一个MyDBContext
            _bannerRepository = bannerRepository;
            _unitOfWork = unitOfWork;
            _loggerFactory = loggerFactory.CreateLogger("BlogDemo.Api.Controllers.BannerController");
            _mapper = mapper;
            _urlHelper = urlHelper;
        }
        /// <summary>
        /// 获取全部主题数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBanners()
        {
            var bannerList = await _bannerRepository.GetAllBanners();
            var bannerListRessources = _mapper.Map<IEnumerable<Banner>, IEnumerable<BannerResources>>(bannerList);
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "Success", Data = bannerListRessources }));
        }
        // GET: /<controller>/
        [HttpGet(Name = "GetBanner")]
        public async Task<IActionResult> GetAsync(BannerQueryParameters bannerQueryParameters)
        {
            var bannerList = await _bannerRepository.GetPagingBanners(bannerQueryParameters);

            var bannerResources = _mapper.Map<IEnumerable<Banner>, IEnumerable<BannerResources>>(bannerList);
            //前一页
            var previousPageLink = bannerList.HasPrevious ?
                CreatePageUrl(bannerQueryParameters, PaginationResourceUriType.PreviousPage) : null;
            //下一页
            var nextPageLink = bannerList.HasNext ?
                CreatePageUrl(bannerQueryParameters, PaginationResourceUriType.NextPage) : null;

            var meta = new {
                PageSize = bannerList.PageSize,
                PageIndex = bannerList.PageIndex,
                PageCount = bannerList.PageCount,
                TotalItemsCount = bannerList.TotalItemsCount,
                previousPageLink,
                nextPageLink
            };
            Response.Headers.Add("X-Pagintion", JsonConvert.SerializeObject(meta, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));

            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "Success", Data = bannerResources }));
        }
        /// <summary>
        /// 创建 前一页、后一页、当前页的url
        /// </summary>
        /// <param name="bannerQueryParameters">分页请求参数类</param>
        /// <param name="uriType">分页按钮类型</param>
        /// <returns></returns>
        private string CreatePageUrl(BannerQueryParameters bannerQueryParameters, PaginationResourceUriType uriType)
        {
            switch (uriType)
            {
                case PaginationResourceUriType.PreviousPage:
                    var perviousParameters = new
                    {
                        PageIndex = bannerQueryParameters.PageIndex - 1,
                        PageSize = bannerQueryParameters.PageSize,
                        OrderBy = bannerQueryParameters.OrderBy,
                        fields = bannerQueryParameters.Fields

                    };
                    return _urlHelper.Link("GetBanner", perviousParameters);
                case PaginationResourceUriType.NextPage:
                    var nextPageParameters = new
                    {
                        PageIndex = bannerQueryParameters.PageIndex + 1,
                        PageSize = bannerQueryParameters.PageSize,
                        OrderBy = bannerQueryParameters.OrderBy,
                        fields = bannerQueryParameters.Fields

                    };
                    return _urlHelper.Link("GetBanner", nextPageParameters);
                default:
                    var currentParameters = new {
                        PageIndex = bannerQueryParameters.PageIndex,
                        PageSize = bannerQueryParameters.PageSize,
                        OrderBy = bannerQueryParameters.OrderBy,
                        fields = bannerQueryParameters.Fields

                    };
                    return _urlHelper.Link("GetBanner", currentParameters);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var post = await _bannerRepository.GetBannerByIdAsync(id);
            if (post == null)
            {
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "改数据不存在！" }));

            }
            var bannerResources = _mapper.Map<Banner, BannerResources>(post);
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "Success", Data = bannerResources }));
        }
        [HttpPost]
        public async Task<IActionResult> AddBannerAsync([FromBody] PostBanner postBanner)
        {
            if (!ModelState.IsValid)
            {
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据验证不通过！" }));
            }
            //判断添加的主题是否重复
            var isBanner = await _bannerRepository.GetSearchOneBanner(x=>x.Image == postBanner.Image);
            if (isBanner != null)
            {
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "主题数据添加重复！" }));
            }
            _bannerRepository.AddBanner(postBanner);
            bool b = await _unitOfWork.SaveAsync();
            if (b)
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "数据添加成功！" }));
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据添加失败！" }));
        }
        [HttpPut]
        public async Task<IActionResult> EditBannerAsync([FromBody] EditBanner banner)
        {
            
            _bannerRepository.EditBanner(banner);
            bool b = await _unitOfWork.SaveAsync();
            if (b)
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "数据修改成功！" }));
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据修改失败！" }));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DelBannerAsync(int id)
        {
            _bannerRepository.DeleteBannerById(id);
            bool b = await _unitOfWork.SaveAsync();
            if (b)
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "删除数据成功！" }));
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "删除数据失败！" }));
        }

    }
}
