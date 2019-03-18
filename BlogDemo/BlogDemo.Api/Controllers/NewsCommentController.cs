using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogDemo.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogDemo.Api.Controllers
{
    [Route("api/NewsComments")]
    public class NewsCommentController : Controller
    {
        private readonly INewsCommentRepository _newsCommentRepository;
        //工作单元模式
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _loggerFactory;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

        public NewsCommentController(
            INewsCommentRepository newsCommentRepository,
            IUnitOfWork unitOfWork,
            ILoggerFactory loggerFactory,
            IMapper mapper,
            IUrlHelper urlHelper
        )
        {
            //注入这两个实例所用的都是同一个MyDBContext
            _newsCommentRepository = newsCommentRepository;
            _unitOfWork = unitOfWork;
            _loggerFactory = loggerFactory.CreateLogger("BlogDemo.Api.Controllers.NewsController");
            _mapper = mapper;
            _urlHelper = urlHelper;
        }
        /// <summary>
        /// 获取全部评论数据
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllNews()
        {
            var NewsList = await _newsCommentRepository.GetCommentList();
            var NewsListRessources = _mapper.Map<IEnumerable<News>, IEnumerable<NewsResources>>(NewsList);
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "Success", Data = NewsListRessources }));
        }
        // GET: /<controller>/
        [HttpGet(Name = "GetNews")]
        public async Task<IActionResult> GetAsync(NewsQueryParameters NewsQueryParameters)
        {
            var NewsList = await _newsCommentRepository.GetPagingNews(NewsQueryParameters);

            var NewsResources = _mapper.Map<IEnumerable<News>, IEnumerable<NewsResources>>(NewsList);
            //前一页
            var previousPageLink = NewsList.HasPrevious ?
                CreatePageUrl(NewsQueryParameters, PaginationResourceUriType.PreviousPage) : null;
            //下一页
            var nextPageLink = NewsList.HasNext ?
                CreatePageUrl(NewsQueryParameters, PaginationResourceUriType.NextPage) : null;

            var meta = new
            {
                PageSize = NewsList.PageSize,
                PageIndex = NewsList.PageIndex,
                PageCount = NewsList.PageCount,
                TotalItemsCount = NewsList.TotalItemsCount,
                previousPageLink,
                nextPageLink
            };
            Response.Headers.Add("X-Pagintion", JsonConvert.SerializeObject(meta, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));

            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "Success", Data = NewsResources }));
        }
        /// <summary>
        /// 创建 前一页、后一页、当前页的url
        /// </summary>
        /// <param name="NewsQueryParameters">分页请求参数类</param>
        /// <param name="uriType">分页按钮类型</param>
        /// <returns></returns>
        private string CreatePageUrl(NewsQueryParameters NewsQueryParameters, PaginationResourceUriType uriType)
        {
            switch (uriType)
            {
                case PaginationResourceUriType.PreviousPage:
                    var perviousParameters = new
                    {
                        PageIndex = NewsQueryParameters.PageIndex - 1,
                        PageSize = NewsQueryParameters.PageSize,
                        OrderBy = NewsQueryParameters.OrderBy,
                        fields = NewsQueryParameters.Fields

                    };
                    return _urlHelper.Link("GetNews", perviousParameters);
                case PaginationResourceUriType.NextPage:
                    var nextPageParameters = new
                    {
                        PageIndex = NewsQueryParameters.PageIndex + 1,
                        PageSize = NewsQueryParameters.PageSize,
                        OrderBy = NewsQueryParameters.OrderBy,
                        fields = NewsQueryParameters.Fields

                    };
                    return _urlHelper.Link("GetNews", nextPageParameters);
                default:
                    var currentParameters = new
                    {
                        PageIndex = NewsQueryParameters.PageIndex,
                        PageSize = NewsQueryParameters.PageSize,
                        OrderBy = NewsQueryParameters.OrderBy,
                        fields = NewsQueryParameters.Fields

                    };
                    return _urlHelper.Link("GetNews", currentParameters);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var post = await _newsCommentRepository.GetSearchOneNews(x => x.Id == id);
            if (post == null)
            {
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "该数据不存在！" }));

            }
            var NewsResources = _mapper.Map<News, NewsResources>(post);
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "Success", Data = NewsResources }));
        }
        [HttpPost]
        public async Task<IActionResult> AddNewsAsync([FromBody] AddNews addNews)
        {
            if (!ModelState.IsValid)
            {
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据验证不通过！" }));
            }
            _newsCommentRepository.AddNews(addNews);
            bool b = await _unitOfWork.SaveAsync();
            if (b)
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "数据添加成功！" }));
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据添加失败！" }));
        }
        [HttpPut]
        public async Task<IActionResult> EditNewsAsync([FromBody] EditNews News)
        {
            if (!ModelState.IsValid)
            {
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据验证不通过！" }));
            }
            _newsCommentRepository.EditNews(News);
            bool b = await _unitOfWork.SaveAsync();
            if (b)
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "数据修改成功！" }));
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据修改失败！" }));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DelNewsAsync(int id)
        {
            _newsCommentRepository.DeleteNewsById(id);
            bool b = await _unitOfWork.SaveAsync();
            if (b)
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "删除数据成功！" }));
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "删除数据失败！" }));
        }
    }
}
