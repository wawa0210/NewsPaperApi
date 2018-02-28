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
    [Route("v0/versions")]
    public class VersionsController : BaseApiController
    {
        private IVersionService VersionService { get; set; }

        public VersionsController(IVersionService versionService)
        {
            VersionService = versionService;
        }

        /// <summary>
        /// 添加版本信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<ResponseModel> AddVersionsAsync([FromBody]EntityVersion entityVersion)
        {
            var newsInfo = await VersionService.AddVersionAsync(entityVersion);
            return Success("新增成功");
        }
        /// <summary>
        /// 编辑版本信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public async Task<ResponseModel> UpdateVersionsAsync([FromBody]EntityVersion entityVersion)
        {
            await VersionService.UpdateVersionAsync(entityVersion);
            return Success("编辑成功");
        }

        /// <summary>
        /// 获得版本明细信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{versionId}")]
        public async Task<ResponseModel> GetVersionsAsync([FromQuery]string versionId)
        {
            if (string.IsNullOrEmpty(versionId)) return Fail(ErrorCodeEnum.ParamIsNullArgument);
            return Success(await VersionService.GetVersionAsync(versionId));
        }

        /// <summary>
        /// 分页获得版本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<ResponseModel> GetPageVersionsAsync([FromQuery] EntityVersionQuery entityVersionQuery)
        {
            return Success(await VersionService.GetPageVersionAsync(entityVersionQuery));
        }
    }
}