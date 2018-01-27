using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Exceptionless.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UeditorService;
using UeditorService.Entity;
using WebApi.Models;

namespace WebApi.Controllers
{
    /// <summary>
    /// ueditor 相关配置信息
    /// </summary>
    [Route("v0/ueditors")]
    public class UeditorsController : BaseApiController
    {

        private IHostingEnvironment host = null;

        public UeditorsController(IHostingEnvironment host)
        {
            this.host = host;
        }

        /// <summary>
        /// ueditor 配置信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        public void UeditorFiles()
        {
            var action = HttpContext.Request.Query["action"].ToString();
            //上传图片信息
            if (action== "uploadimage")
            {
                var uploadConfig = new UploadConfig
                {
                    AllowExtensions = Config.GetStringList("imageAllowFiles"),
                    PathFormat = Config.GetString("imagePathFormat"),
                    SizeLimit = Config.GetInt("imageMaxSize"),
                    UploadFieldName = Config.GetString("imageFieldName")
                };
            }
        }

        /// <summary>
        /// 获得ueditor 配置信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        public void GetUeditorConfig()
        {
            var action = HttpContext.Request.Query["action"].ToString();
            //获取配置信息
            if (action == "config")
            {
                var configInfo = Config.ItemsStr;
                var jsonpCallback = HttpContext.Request.Query["callback"];
                Response.Headers.Add("Content-Type", "application/javascript");
                Response.WriteAsync(String.Format("{0}({1});", jsonpCallback, configInfo));
            }
        }
    }
}