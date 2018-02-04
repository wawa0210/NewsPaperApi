using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CommonLib;
using Dapper;
using EmergencyBaseService;
using EmergencyData.MicroOrm.SqlGenerator;
using EmergencyEntity.PageQuery;
using MagickNetService;
using MagickNetService.Entity;
using PaperNewsService.Entity;
using PaperNewsService.Model;

namespace PaperNewsService.Application
{
    public class NewsService : BaseAppService, INewsService
    {
        public async Task<EntityNews> AddNewsAsync(EntityNews entityNews)
        {
            var newsRep = GetRepositoryInstance<TableNews>();

            var model = new TableNews
            {
                NewsId = GuidExtens.GuidTo16String(),
                Title = entityNews.Title,
                ShortContent = entityNews.ShortContent,
                NewsContent = entityNews.Content,
                CreateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                UpdateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                IsEnable = true,
                HrefUrl = entityNews.HrefUrl ?? "",
                NewsImgUrl = ""
            };
            await newsRep.InsertAsync(model);
            entityNews.NewsId = model.NewsId;
            return entityNews;
        }

        /// <summary>
        ///删除新闻
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteNewsAsync(string newsId)
        {
            var NewsRep = GetRepositoryInstance<TableNews>();
            var model = await NewsRep.FindAsync(x => x.NewsId == newsId);
            if (model == null) return false;
            if (model.IsEnable) return false;
            NewsRep.Delete(model);
            return true;
        }

        public async Task DisableNewsAsync(string newsId)
        {
            var newsRep = GetRepositoryInstance<TableNews>();
            var model = await newsRep.FindAsync(x => x.NewsId == newsId);
            model.IsEnable = false;
            model.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            newsRep.Update<TableNews>(model, item => new
            {
                item.IsEnable
            });
        }

        public async Task<EntityNews> GetNewsIntfByIdAsync(string newsId)
        {
            var newsRep = GetRepositoryInstance<TableNews>();
            var model = await newsRep.FindAsync(x => x.NewsId == newsId);
            return Mapper.Map<TableNews, EntityNews>(model);
        }

        public async Task<string> GetNewsShareImgAsync(string newsId)
        {
            var newsRep = GetRepositoryInstance<TableNews>();
            var model = await newsRep.FindAsync(x => x.NewsId == newsId);
            return model.NewsImgUrl;
        }

        //public async Task<byte[]> GetNewsShareImgAsync(string newsId)
        //{
        //    var news = await GetNewsIntfByIdAsync(newsId);
        //    if (news == null) return null;
        //    return new MagickService().GenerateNewImg(new EntityNewsModel
        //    {
        //        Title = news.Title,
        //        Content = news.ShortContent
        //    });
        //}

        public byte[] GetNewsShareImgAsync(string title, string shortContent)
        {
            return new MagickService().GenerateNewImg(new EntityNewsModel
            {
                Title = title,
                Content = shortContent
            });
        }

        public async Task<PageBase<EntityListNews>> GetPageCompanyAsync(EntityNewQuery entityNewQuery)
        {
            var result = new PageBase<EntityListNews>
            {
                CurrentPage = entityNewQuery.CurrentPage,
                PageSize = entityNewQuery.PageSize
            };

            var strTotalSql = new StringBuilder();
            var strSql = new StringBuilder();

            //计算总数
            strTotalSql.Append(@"        
                            SELECT  COUNT(1)
                            FROM    T_News where 1=1  ");

            if (!entityNewQuery.IsAll)
            {
                strTotalSql.Append(" and isEnable = @isEnable ");
            }

            if (!string.IsNullOrEmpty(entityNewQuery.Title))
            {
                strTotalSql.Append(" and  Title like @title ");
            }

            //分页信息
            strSql.Append(@";  SELECT 
                               Id ,
                               NewsId ,
                               Title ,
                               ShortContent ,
                               NewsContent ,
                               HrefUrl ,
                               CreateTime ,
                               UpdateTime ,
                               IsEnable FROM T_News where 1=1 ");

            if (!entityNewQuery.IsAll)
            {
                strSql.Append(" and isEnable=@isEnable ");
            }

            if (!string.IsNullOrEmpty(entityNewQuery.Title))
            {
                strSql.Append(" and  Title like @title ");
            }
            strSql.Append(@"
                            order by createTime desc
                        ");
            strSql.Append(@" limit @startIndex,@endIndex ");

            var paras = new DynamicParameters(new
            {
                isEnable = entityNewQuery.IsEnable,
                title = "%" + entityNewQuery.Title + "%",
                startIndex = (entityNewQuery.CurrentPage - 1) * entityNewQuery.PageSize,
                endIndex = entityNewQuery.CurrentPage * entityNewQuery.PageSize
            });
            var newsRep = GetRepositoryInstance<TableNews>();

            var sqlQuery = new SqlQuery(strSql.ToString(), paras);
            var listResult = await newsRep.FindAllAsync(sqlQuery);
            result.Items = Mapper.Map<List<TableNews>, List<EntityListNews>>(listResult.AsList());
            result.TotalCounts = newsRep.Connection.ExecuteScalar<int>(strTotalSql.ToString(), paras);
            result.TotalPages = Convert.ToInt32(Math.Ceiling(result.TotalCounts / (entityNewQuery.PageSize * 1.0)));
            return result;
        }

        /// <summary>
        /// 重新上线新闻
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public async Task<bool> RestoreNewsAsync(string newsId)
        {
            var NewsRep = GetRepositoryInstance<TableNews>();
            var model = await NewsRep.FindAsync(x => x.NewsId == newsId);
            if (model == null) return false;
            model.IsEnable = true;

            NewsRep.Update<TableNews>(model, item => new
            {
                item.IsEnable
            });

            return true;
        }

        public async Task UpateNewsAsync(EntityNews entityNews)
        {
            var newsRep = GetRepositoryInstance<TableNews>();
            var model = await newsRep.FindAsync(x => x.NewsId == entityNews.NewsId);
            if (model == null) return;

            model.Title = entityNews.Title ?? model.Title;
            model.ShortContent = entityNews.ShortContent ?? model.ShortContent;
            model.NewsContent = entityNews.Content ?? model.NewsContent;
            model.HrefUrl = entityNews.HrefUrl ?? model.HrefUrl;
            model.IsEnable = entityNews.IsEnable;
            model.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            newsRep.Update(model);
        }

        /// <summary>
        /// 更新商品分析图片
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="newsImgUrl"></param>
        /// <returns></returns>
        public async Task UpateNewsImgAsync(string newsId, string newsImgUrl)
        {
            var newsRep = GetRepositoryInstance<TableNews>();
            var model = await newsRep.FindAsync(x => x.NewsId == newsId);
            if (model == null) return;
            model.NewsImgUrl = newsImgUrl;
            newsRep.Update(model);
        }

        public async Task UpdateNewStatusAsync(EntityNewStatus entityNewStatus)
        {
            var newsRep = GetRepositoryInstance<TableNews>();
            var model = await newsRep.FindAsync(x => x.NewsId == entityNewStatus.NewsId);
            model.IsEnable = entityNewStatus.IsEnable;
            model.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            newsRep.Update<TableNews>(model, item => new
            {
                item.IsEnable
            });
        }
    }
}
