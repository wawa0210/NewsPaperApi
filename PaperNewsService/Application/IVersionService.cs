using EmergencyEntity.PageQuery;
using PaperNewsService.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaperNewsService.Application
{
    public interface IVersionService
    {
        /// <summary>
        /// 新增版本信息
        /// </summary>
        /// <param name="entityNews"></param>
        /// <returns></returns>
        Task<EntityVersion> AddVersionAsync(EntityVersion entityVersion);

        /// <summary>
        /// 分页获得版本信息
        /// </summary>
        /// <param name="entityNews"></param>
        /// <returns></returns>
        Task<PageBase<EntityVersion>> GetPageVersionAsync(EntityVersionQuery entityVersionQuery);

        /// <summary>
        /// 编辑版本信息
        /// </summary>
        /// <param name="entityNews"></param>
        /// <returns></returns>
        Task UpdateVersionAsync(EntityVersion entityVersion);

        /// <summary>
        /// 获得版本信息
        /// </summary>
        /// <param name="entityNews"></param>
        /// <returns></returns>
        Task<EntityVersion> GetVersionAsync(string versionId);

        Task<bool?> GetVersionStatus(string versionId);
    }
}
