using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmergencyEntity.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Models;
using WebApi.Qiniu;

namespace WebApi.Controllers
{
    [Route("v0/qinius")]
    public class QiniuController : BaseApiController
    {

        private QiniuService QiniuService { get; set; }

        /// <summary>
        /// 初始化(autofac 已经注入)
        /// </summary>
        public QiniuController(IOptions<AppSettings> settings)
        {
            QiniuService = new QiniuService(settings);
        }

        /// <summary>
        /// 获得qiniutoken信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("token")]
        public ResponseModel GetQiniuToken()
        {
            return Success(QiniuService.GetToken());
        }

       
    }
}