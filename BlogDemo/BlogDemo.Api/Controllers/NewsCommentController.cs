using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogDemo.Core.Interface;
using BlogDemo.Core.Request;
using BlogDemo.Core.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogDemo.Api.Controllers
{
    [Route("api/[controller]/[action]")]
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
        /// 添加新闻评论
        /// </summary>
        /// <param name="addComment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddNewsCommentAsync([FromBody] AddComment addComment)
        {
            if (!ModelState.IsValid)
            {
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据验证不通过！" }));
            }
            ////判断添加的主题是否重复
            //var isNewsComment = await _newsCommentRepository.GetCommentById(addComment.NewsId);
            //if (isNewsComment != null)
            //{
            //    return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据添加重复！" }));
            //}
            _newsCommentRepository.AddComment(addComment);
            bool b = await _unitOfWork.SaveAsync();
            if (b)
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "数据添加成功！" }));
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据添加失败！" }));
        }
        /// <summary>
        /// 返回添加评论数据
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetCommentByIdAsync(int newsId)
        {
            var newsCommentModel = await _newsCommentRepository.GetCommentById(newsId);
           if(newsCommentModel!=null)
                return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 200, Reslut = "数据返回成功！",Data = newsCommentModel }));
            return Ok(JsonNetHelper.SerializerToString(new ResponseModel { Code = 0, Reslut = "数据返回失败！", Data = newsCommentModel }));
        }
    }
}
