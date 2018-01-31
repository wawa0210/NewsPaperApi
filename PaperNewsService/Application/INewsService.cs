using EmergencyEntity.PageQuery;
using PaperNewsService.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PaperNewsService.Model;

namespace PaperNewsService.Application
{
    public interface INewsService
    {
        /// <summary>
        /// 分页获得新闻信息
        /// </summary>
        /// <param name="entityNewQuery"></param>
        /// <returns></returns>
        Task<PageBase<EntityListNews>> GetPageCompanyAsync(EntityNewQuery entityNewQuery);

        /// <summary>
        /// 新增新闻信息
        /// </summary>
        /// <param name="entityNews"></param>
        /// <returns></returns>
        Task<EntityNews> AddNewsAsync(EntityNews entityNews);

        /// <summary>
        /// 编辑新闻信息
        /// </summary>
        /// <param name="entityNews"></param>
        /// <returns></returns>

        Task UpateNewsAsync(EntityNews entityNews);

        /// <summary>
        /// 获得新闻详细信息
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        Task<EntityNews> GetNewsIntfByIdAsync(string newsId);

        /// <summary>
        /// 获得新闻分享图片信息
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        Task<string> GetNewsShareImgAsync(string newsId);

        /// <summary>
        /// 获得新闻分享图片信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="shortContent"></param>
        /// <returns></returns>
        byte[] GetNewsShareImgAsync(string title, string shortContent);

        /// <summary>
        /// 下线新闻
        /// </summary>
        /// <param name="entityNewStatus"></param>
        /// <returns></returns>
        Task UpdateNewStatusAsync(EntityNewStatus entityNewStatus);

        /// <summary>
        /// 更新商品图片信息
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="newsImgUrl"></param>
        /// <returns></returns>
        Task UpateNewsImgAsync(string newsId, string newsImgUrl);

    }
}
