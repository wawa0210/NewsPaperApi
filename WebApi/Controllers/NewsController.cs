using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaperNewsService.Application;
using PaperNewsService.Entity;
using WebApi.Models;
using WebApi.Qiniu;

namespace WebApi.Controllers
{
    [Route("v0/news")]
    public class NewsController : BaseApiController
    {
        private INewsService _iNewsService { get; set; }

        ///// <summary>
        ///// 初始化(autofac 已经注入)
        ///// </summary>
        //public NewsController(INewsService iNewsService)
        //{
        //    _iNewsService = iNewsService;
        //}

        /// <summary>
        /// 初始化(autofac 已经注入)
        /// </summary>
        public NewsController()
        {
            _iNewsService = new NewsService();
        }

        /// <summary>
        /// 获得新闻详细信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{newsId}")]
        public async Task<ResponseModel> GetNewsbyIdAsync(string newsId)
        {
            if (string.IsNullOrEmpty(newsId)) return Fail(ErrorCodeEnum.ParamIsNullArgument);
            return Success(await _iNewsService.GetNewsIntfByIdAsync(newsId));
        }

        /// <summary>
        /// 获得新闻分享图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{newsId}/shareimgs")]
        public async Task<ResponseModel> GetNewsShareImgByIdAsync(string newsId)
        {
            if (string.IsNullOrEmpty(newsId)) return Fail(ErrorCodeEnum.ParamIsNullArgument);
            var imgUrl = await _iNewsService.GetNewsShareImgAsync(newsId);
            if (string.IsNullOrWhiteSpace(imgUrl)) return Fail(ErrorCodeEnum.ServerError);
            return Success(imgUrl);
        }

        private string UploadQiNiu(byte[] byteImgs)
        {
            return "http://img.xiaozhang.info/" + new QiniuService().UploadImg(byteImgs);
        }

        private async Task UpdateNewsImgAsync(EntityNews entityNews)
        {
            var imgUrl = UploadQiNiu(_iNewsService.GetNewsShareImgAsync(entityNews.Title,entityNews.ShortContent));
            await _iNewsService.UpateNewsImgAsync(entityNews.NewsId, imgUrl);

        }

        /// <summary>
        /// 添加新闻信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<ResponseModel> AddNewsAsync([FromBody]EntityNews entityNews)
        {
            var newsInfo = await _iNewsService.AddNewsAsync(entityNews);
            await UpdateNewsImgAsync(newsInfo);
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
            await UpdateNewsImgAsync(entityNews);
            return Success("更新成功");
        }

        /// <summary>
        /// 改变新闻状态
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("status")]
        public async Task<ResponseModel> UpdateNewsStatus([FromBody]EntityNewStatus entityNewStatus)
        {
            await _iNewsService.UpdateNewStatusAsync(entityNewStatus);
            return Success("保存成功");
        }

        /// <summary>
        /// 分页获得新闻信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<ResponseModel> GetPageNewsAsync([FromQuery] EntityNewQuery entityNewQuery)
        {


            return Success(await _iNewsService.GetPageCompanyAsync(entityNewQuery));
        }
    }
}