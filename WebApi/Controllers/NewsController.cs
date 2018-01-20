using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaperNewsService.Application;
using PaperNewsService.Entity;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("v0/news")]
    public class NewsController : BaseApiController
    {
        private INewsService _iNewsService { get; set; }

        /// <summary>
        /// 初始化(autofac 已经注入)
        /// </summary>
        public NewsController(INewsService iNewsService)
        {
            _iNewsService = iNewsService;
        }

        /// <summary>
        /// 获得新闻详细信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{newsId}")]
        public async Task<ResponseModel> GetNewsbyIdAsync(string newsId)
        {
            if(string.IsNullOrEmpty(newsId)) return Fail(ErrorCodeEnum.ParamIsNullArgument);
            return Success( await _iNewsService.GetNewsIntfByIdAsync(newsId));
        }

        /// <summary>
        /// 添加新闻信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<ResponseModel> AddNewsAsync([FromBody]EntityNews entityNews)
        {
            await _iNewsService.AddNewsAsync(entityNews);
            return Success("新增成功");
        }

        /// <summary>
        /// 更新新闻信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public async Task<ResponseModel> UpdateNewsAsync([FromBody]EntityNews entityNews)
        {
            if (string.IsNullOrEmpty(entityNews.NewsId)) return Fail(ErrorCodeEnum.ParamIsNullArgument);
            await _iNewsService.UpateNewsAsync(entityNews);
            return Success("更新成功");
        }

        /// <summary>
        /// 下线新闻
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("{newsId}")]
        public async Task<ResponseModel> GetPageNewsIdAsync([FromQuery] string newsId)
        {
            if (string.IsNullOrEmpty(newsId)) return Fail(ErrorCodeEnum.ParamIsNullArgument);
            await _iNewsService.DisableNewsAsync(newsId);
            return Success("下线成功");
        }

        /// <summary>
        /// 分页获得新闻信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<ResponseModel> GetPageNewsAsync([FromQuery] EntityNewQuery entityNewQuery)
        {
            return Success( await _iNewsService.GetPageCompanyAsync(entityNewQuery));
        }
    }
}