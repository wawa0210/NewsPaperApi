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
using PaperNewsService.Entity;
using PaperNewsService.Model;

namespace PaperNewsService.Application
{
    public class NewsService : BaseAppService, INewsService
    {
        public async Task AddNewsAsync(EntityNews entityNews)
        {
            var NewsRep = GetRepositoryInstance<TableNews>();

            var model = new TableNews
            {
                NewsId = GuidExtens.GuidTo16String(),
                Title = entityNews.Title,
                ShortContent = entityNews.ShortContent,
                NewsContent = entityNews.Content,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                IsEnable = true,
                HrefUrl = entityNews.HrefUrl ?? ""
            };
            await NewsRep.InsertAsync(model);
        }

        public async Task DisableNewsAsync(string newsId)
        {
            var NewsRep = GetRepositoryInstance<TableNews>();
            var model = await NewsRep.FindAsync(x => x.NewsId == newsId);
            model.IsEnable = false;
            model.UpdateTime = DateTime.Now;
            NewsRep.Update<TableNews>(model, item => new
            {
                item.IsEnable
            });
        }

        public async Task<EntityNews> GetNewsIntfByIdAsync(string newsId)
        {
            var NewsRep = GetRepositoryInstance<TableNews>();
            var model = await NewsRep.FindAsync(x => x.NewsId == newsId);
            return Mapper.Map<TableNews, EntityNews>(model);
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
                            FROM    T_News where isEnable=@isEnable  ");

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
                               IsEnable FROM T_News where isEnable=@isEnable ");

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
            var NewsRep = GetRepositoryInstance<TableNews>();

            var sqlQuery = new SqlQuery(strSql.ToString(), paras);
            var listResult = await NewsRep.FindAllAsync(sqlQuery);
            result.Items = Mapper.Map<List<TableNews>, List<EntityListNews>>(listResult.AsList());
            result.TotalCounts = NewsRep.Connection.ExecuteScalar<int>(strTotalSql.ToString(), paras);
            result.TotalPages = Convert.ToInt32(Math.Ceiling(result.TotalCounts / (entityNewQuery.PageSize * 1.0)));
            return result;
        }

        public async Task UpateNewsAsync(EntityNews entityNews)
        {
            var NewsRep = GetRepositoryInstance<TableNews>();
            var model = await NewsRep.FindAsync(x => x.NewsId == entityNews.NewsId);
            if (model == null) return;

            model.Title = entityNews.Title ?? model.Title;
            model.ShortContent = entityNews.ShortContent ?? model.ShortContent;
            model.NewsContent = entityNews.Content ?? model.NewsContent;
            model.HrefUrl = entityNews.HrefUrl ?? model.HrefUrl;
            model.IsEnable = entityNews.IsEnable;
            model.UpdateTime = DateTime.Now;
            NewsRep.Update(model);
        }

        public async Task UpdateNewStatusAsync(EntityNewStatus entityNewStatus)
        {
            var NewsRep = GetRepositoryInstance<TableNews>();
            var model = await NewsRep.FindAsync(x => x.NewsId == entityNewStatus.NewsId);
            model.IsEnable = entityNewStatus.IsEnable;
            model.UpdateTime = DateTime.Now;
            NewsRep.Update<TableNews>(model, item => new
            {
                item.IsEnable
            });
        }
    }
}
