﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CommonLib;
using Dapper;
using EmergencyBaseService;
using EmergencyData.MicroOrm.SqlGenerator;
using EmergencyEntity.PageQuery;
using PaperNewsService.Entity;
using PaperNewsService.Enum;
using PaperNewsService.Model;

namespace PaperNewsService.Application
{
    public class VersionService : BaseAppService, IVersionService
    {
        public async Task<EntityVersion> AddVersionAsync(EntityVersion entityVersion)
        {
            var versionRep = GetRepositoryInstance<TableVersions>();
            var model = new TableVersions
            {
                VersionId = Utils.GetCheckCode(6),
                VersionName = entityVersion.VersionName,
                VersionStatus = (int)entityVersion.VersionStatus,
                CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Remark = ""
            };
            await versionRep.InsertAsync(model);
            entityVersion.Id = model.Id;
            return entityVersion;
        }

        public async Task<PageBase<EntityVersion>> GetPageVersionAsync(EntityVersionQuery entityVersionQuery)
        {
            var result = new PageBase<EntityVersion>
            {
                CurrentPage = entityVersionQuery.CurrentPage,
                PageSize = entityVersionQuery.PageSize
            };

            var strTotalSql = new StringBuilder();
            var strSql = new StringBuilder();

            //计算总数
            strTotalSql.Append(@"        
                            SELECT  COUNT(1)
                            FROM    T_Version where 1=1  ");

            if (!string.IsNullOrEmpty(entityVersionQuery.VersionName))
            {
                strTotalSql.Append(" and  VersionName like @versionName ");
            }
            //分页信息
            strSql.Append(@";  SELECT 
                               Id ,
                               VersionId ,
                               VersionName ,
                               VersionStatus ,
                               CreateTime ,
                               Remark 
                               FROM T_Version where 1=1 ");

            if (!string.IsNullOrEmpty(entityVersionQuery.VersionName))
            {
                strSql.Append(" and  VersionName like @versionName ");
            }
            strSql.Append(@"
                            order by createTime desc
                        ");

            strSql.Append(@" limit @startIndex,@pageSize ");

            var paras = new DynamicParameters(new
            {
                versionName = "%" + entityVersionQuery.VersionName + "%",
                startIndex = (entityVersionQuery.CurrentPage - 1) * entityVersionQuery.PageSize,
                pageSize = entityVersionQuery.PageSize
            });
            var versionRep = GetRepositoryInstance<TableVersions>();

            var sqlQuery = new SqlQuery(strSql.ToString(), paras);
            var listResult = await versionRep.FindAllAsync(sqlQuery);
            result.Items = Mapper.Map<List<TableVersions>, List<EntityVersion>>(listResult.AsList());
            result.TotalCounts = versionRep.Connection.ExecuteScalar<int>(strTotalSql.ToString(), paras);
            result.TotalPages = Convert.ToInt32(Math.Ceiling(result.TotalCounts / (entityVersionQuery.PageSize * 1.0)));
            return result;
        }

        public async Task<EntityVersion> GetVersionAsync(string versionId)
        {
            var versionRep = GetRepositoryInstance<TableVersions>();
            var model = await versionRep.FindAsync(x => x.VersionId == versionId);
            return Mapper.Map<TableVersions, EntityVersion>(model);
        }

        public async Task UpdateVersionAsync(EntityVersion entityVersion)
        {
            var versionRep = GetRepositoryInstance<TableVersions>();
            var model = await versionRep.FindAsync(x => x.VersionId == entityVersion.VersionId);
            if (model == null) return;
            model.VersionStatus = (int)entityVersion.VersionStatus;
            model.Remark = entityVersion.Remark ?? model.Remark;
            versionRep.Update(model);
        }

        /// <summary>
        /// 根据新闻编号获得新闻最新状态
        /// </summary>
        /// <param name="versionId"></param>
        /// <returns></returns>
        public async Task<bool?> GetVersionStatus(string versionId)
        {
            if (string.IsNullOrEmpty(versionId)) return null;
            var versionRep = GetRepositoryInstance<TableVersions>();
            var model = await versionRep.FindAllAsync(x => x.VersionId == versionId);
            if (!model.Any()) return null;

            return model.ToList().OrderByDescending(x => x.CreateTime).First().VersionStatus == (int)EnumVersionStatus.Published;
        }
    }
}
