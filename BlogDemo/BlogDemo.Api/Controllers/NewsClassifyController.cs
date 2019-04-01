using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogDemo.Core.Entities;
using BlogDemo.Core.Interface;
using BlogDemo.Core.Request;
using BlogDemo.Core.Response;
using BlogDemo.Infrastructure.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlogDemo.Api.Controllers
{
    [Route("api/[controller]/[action]")]
   [Authorize]
    public class NewsClassifyController : Controller
    {
        private readonly INewsClassifyRepository _newsClassifyRepository;
        //工作单元模式
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _loggerFactory;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

        public NewsClassifyController(
            INewsClassifyRepository newsClassifyRepository,
            IUnitOfWork unitOfWork,
            ILoggerFactory loggerFactory,
            IMapper mapper,
            IUrlHelper urlHelper
        )
        {
            //注入这两个实例所用的都是同一个MyDBContext
            _newsClassifyRepository = newsClassifyRepository;
            _unitOfWork = unitOfWork;
            _loggerFactory = loggerFactory.CreateLogger("BlogDemo.Api.Controllers.NewsClassifyController");
            _mapper = mapper;
            _urlHelper = urlHelper;
        }
        /// <summary>
        /// 获取全部主题数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllNewsClassify()
        {
            var newsClassifyList = await _newsClassifyRepository.GetAllNewsClassifys();
            var newsClassifyListRessources = _mapper.Map<IEnumerable<NewsClassify>, IEnumerable<NewsClassifyResources>>(newsClassifyList);
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "Success", Data = newsClassifyListRessources }));
        }
        // GET: /<controller>/
        [HttpGet(Name = "GetNewsClassify")]
        public async Task<IActionResult> GetAsync(NewsClassifyQueryParameters newsClassifyQueryParameters)
        {
            var newsClassifyList = await _newsClassifyRepository.GetPagingNewsClassify(newsClassifyQueryParameters);

            var newsClassifyResources = _mapper.Map<IEnumerable<NewsClassify>, IEnumerable<NewsClassifyResources>>(newsClassifyList);
            //前一页
            var previousPageLink = newsClassifyList.HasPrevious ?
                CreatePageUrl(newsClassifyQueryParameters, PaginationResourceUriType.PreviousPage) : null;
            //下一页
            var nextPageLink = newsClassifyList.HasNext ?
                CreatePageUrl(newsClassifyQueryParameters, PaginationResourceUriType.NextPage) : null;

            var meta = new
            {
                PageSize = newsClassifyList.PageSize,
                PageIndex = newsClassifyList.PageIndex,
                PageCount = newsClassifyList.PageCount,
                TotalItemsCount = newsClassifyList.TotalItemsCount,
                previousPageLink,
                nextPageLink
            };
            Response.Headers.Add("X-Pagintion", JsonConvert.SerializeObject(meta, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));

            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "Success", Data = newsClassifyResources }));
        }
        /// <summary>
        /// 创建 前一页、后一页、当前页的url
        /// </summary>
        /// <param name="newsClassifyQueryParameters">分页请求参数类</param>
        /// <param name="uriType">分页按钮类型</param>
        /// <returns></returns>
        private string CreatePageUrl(NewsClassifyQueryParameters newsClassifyQueryParameters, PaginationResourceUriType uriType)
        {
            switch (uriType)
            {
                case PaginationResourceUriType.PreviousPage:
                    var perviousParameters = new
                    {
                        PageIndex = newsClassifyQueryParameters.PageIndex - 1,
                        PageSize = newsClassifyQueryParameters.PageSize,
                        OrderBy = newsClassifyQueryParameters.OrderBy,
                        fields = newsClassifyQueryParameters.Fields

                    };
                    return _urlHelper.Link("GetNewsClassify", perviousParameters);
                case PaginationResourceUriType.NextPage:
                    var nextPageParameters = new
                    {
                        PageIndex = newsClassifyQueryParameters.PageIndex + 1,
                        PageSize = newsClassifyQueryParameters.PageSize,
                        OrderBy = newsClassifyQueryParameters.OrderBy,
                        fields = newsClassifyQueryParameters.Fields

                    };
                    return _urlHelper.Link("GetNewsClassify", nextPageParameters);
                default:
                    var currentParameters = new
                    {
                        PageIndex = newsClassifyQueryParameters.PageIndex,
                        PageSize = newsClassifyQueryParameters.PageSize,
                        OrderBy = newsClassifyQueryParameters.OrderBy,
                        fields = newsClassifyQueryParameters.Fields

                    };
                    return _urlHelper.Link("GetNewsClassify", currentParameters);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var post = await _newsClassifyRepository.GetSearchOneNewsClassify(x=>x.Id == id);
            if (post == null)
            {
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "该数据不存在！" }));

            }
            var newsClassifyResources = _mapper.Map<NewsClassify, NewsClassifyResources>(post);
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "Success", Data = newsClassifyResources }));
        }
        [HttpPost]
        public async Task<IActionResult> AddNewsClassifyAsync([FromBody] AddNewsClassify addNewsClassify)
        {
            if (!ModelState.IsValid)
            {
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据验证不通过！" }));
            }
            //判断添加的主题是否重复
            var isNewsClassify = await _newsClassifyRepository.GetSearchOneNewsClassify(x => x.Name == addNewsClassify.Name);
            if (isNewsClassify != null)
            {
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据添加重复！" }));
            }
            _newsClassifyRepository.AddNewsClassify(addNewsClassify);
            bool b = await _unitOfWork.SaveAsync();
            if (b)
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "数据添加成功！" }));
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据添加失败！" }));
        }
        [HttpPut]
        public async Task<IActionResult> EditNewsClassifyAsync([FromBody] EditNewsClassify newsClassify)
        {
            if (!ModelState.IsValid)
            {
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据验证不通过！" }));
            }
            _newsClassifyRepository.EditNewsClassify(newsClassify);
            bool b = await _unitOfWork.SaveAsync();
            if (b)
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "数据修改成功！" }));
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据修改失败！" }));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DelNewsClassifyAsync(int id)
        {
            _newsClassifyRepository.DeleteNewsClassifyById(id);
            bool b = await _unitOfWork.SaveAsync();
            if (b)
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "删除数据成功！" }));
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "删除数据失败！" }));
        }
    }
}
