﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaperNewsService.Application;
using PaperNewsService.Entity;
using PaperNewsService.Enum;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("v0/versions")]
    public class VersionsController : BaseApiController
    {
        public IVersionService VersionService { get; set; }

        //public VersionsController(IVersionService versionService)
        //{
        //    VersionService = versionService;
        //}

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
            if (string.IsNullOrEmpty(entityVersion.VersionId)) return Fail(ErrorCodeEnum.ParamIsNullArgument);
            if (!Enum.IsDefined(typeof(EnumVersionStatus), entityVersion.VersionStatus)) return Fail(ErrorCodeEnum.ParamsInvalid);
            await VersionService.UpdateVersionAsync(entityVersion);
            return Success("编辑成功");
        }

        /// <summary>
        /// 获得版本明细信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{versionId}")]
        public async Task<ResponseModel> GetVersionsAsync(string versionId)
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