using EmergencyEntity.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Models;
using WebApi.Qiniu;

namespace WebApi.Controllers
{
    [Route("v0/qinius")]
    public class QiniuController : BaseApiController
    {

        public QiniuService QiniuService { get; set; }

        /// <summary>
        /// 初始化(autofac 已经注入)
        /// </summary>
        //public QiniuController(IOptions<AppSettings> settings)
        //{
        //    QiniuService = new QiniuService(settings);
        //}

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