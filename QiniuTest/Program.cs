using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiniuTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var mac = new Mac("gXBXI5GVEAZweMD7v9roKmRhcode8fFtnxjd-RkB", "lPqZgqyP3DM8ibLNwq7C5JAzj8Am9_Dhq3vx9IiM");

            // 上传文件名
            var key = "wawa0201.jpg";
            // 本地文件路径
            var filePath = "D:\\WW.xlsx";

            // 设置上传策略，详见：https://developer.qiniu.com/kodo/manual/1206/put-policy
            var putPolicy = new PutPolicy
            {
                Scope = "qcode"
            };
            putPolicy.SetExpires(3600);
            putPolicy.DeleteAfterDays = 1;
            var token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());

            Config config = new Config();
            // 设置上传区域
            config.Zone = Zone.ZONE_CN_East;
            // 设置 http 或者 https 上传
            config.UseHttps = true;
            config.UseCdnDomains = true;
            config.ChunkSize = ChunkUnit.U512K;
            // 表单上传
            FormUploader target = new FormUploader(config);
            var result = target.UploadFile(filePath, key, token, null);
            Console.WriteLine("form upload result: " + result.ToString());

            Console.ReadKey();
        }
    }
}
