using CommonLib;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using Qiniu.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Qiniu
{
    public class QiniuService
    {
        public string UploadImg(byte[] imgdata)
        {
            var saveKey = GuidExtens.GuidTo16String() + ".jpg";
            var token = GetToken();
            var formUploader = new FormUploader();
            var result = formUploader.UploadData(imgdata, saveKey, token);
            return saveKey;
        }

        public string GetToken()
        {
            var applicationKey = "y3Fil8mtyT_45kQjKeSE6H7rNHr1ke1hUGq0sV6R";
            var mac = new Mac(applicationKey, "ghVAWRTHaxR21x3qClTabEwnjAQiW4wdVi0dTnap");
            const string bucket = "blockcomet-news";
            var putPolicy = new PutPolicy { Scope = bucket };
            Config.AutoZone(applicationKey, bucket, useHTTPS: true);

            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);

            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            //putPolicy.DeleteAfterDays = 1;

            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token            
            var jstr = putPolicy.ToJsonString();
            var token = Auth.CreateUploadToken(mac, jstr);

            return token;
        }
    }
}
