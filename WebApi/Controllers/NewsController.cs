using System.Threading.Tasks;
using EmergencyEntity.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaperNewsService.Application;
using PaperNewsService.Entity;
using WebApi.Models;
using WebApi.Qiniu;

namespace WebApi.Controllers
{
    [Route("v0/news")]
    public class NewsController : BaseApiController
    {
        private INewsService NewsService { get; set; }
        private AppSettings AppSettings { get; set; }
      
        private QiniuService QiniuService { get; set; }

        /// <summary>
        /// 初始化(autofac 已经注入)
        /// </summary>
        public NewsController(IOptions<AppSettings> settings, INewsService newsService)
        {
            AppSettings = settings.Value;
            NewsService = newsService;
            QiniuService = new QiniuService(settings);
        }
        /// <summary>
        /// 获得新闻详细信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize]
        [Route("{newsId}")]
        public async Task<ResponseModel> GetNewsbyIdAsync(string newsId)
        {
            if (string.IsNullOrEmpty(newsId)) return Fail(ErrorCodeEnum.ParamIsNullArgument);
            return Success(await NewsService.GetNewsIntfByIdAsync(newsId));
        }
        
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("{newsId}")]
        public async Task<ResponseModel> DeleteNewsInfo(string newsId)
        {
            if (string.IsNullOrEmpty(newsId)) return Fail(ErrorCodeEnum.ParamIsNullArgument);
            await NewsService.DeleteNewsAsync(newsId);
            return Success("保存成功");
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{newsId}/restore")]
        public async Task<ResponseModel> RestoreNewsInfo(string newsId)
        {
            if (string.IsNullOrEmpty(newsId)) return Fail(ErrorCodeEnum.ParamIsNullArgument);
            await NewsService.RestoreNewsAsync(newsId);
            return Success("保存成功");
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
            var imgUrl = await NewsService.GetNewsShareImgAsync(newsId);
            if (string.IsNullOrWhiteSpace(imgUrl)) return Fail(ErrorCodeEnum.ServerError);
            return Success(imgUrl);
        }

        private string UploadQiNiu(byte[] byteImgs)
        {
            return AppSettings.QiNiuConfig.ImgUrl + QiniuService.UploadImg(byteImgs);
        }

        private async Task UpdateNewsImgAsync(EntityNews entityNews)
        {
            var imgUrl = UploadQiNiu(NewsService.GetNewsShareImgAsync(entityNews.Title,entityNews.ShortContent));
            await NewsService.UpateNewsImgAsync(entityNews.NewsId, imgUrl);

        }

        /// <summary>
        /// 添加新闻信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<ResponseModel> AddNewsAsync([FromBody]EntityNews entityNews)
        {
            var newsInfo = await NewsService.AddNewsAsync(entityNews);
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
            await NewsService.UpateNewsAsync(entityNews);
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
            await NewsService.UpdateNewStatusAsync(entityNewStatus);
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
            return Success(await NewsService.GetPageNewsInfoAsync(entityNewQuery));
        }
    }
}