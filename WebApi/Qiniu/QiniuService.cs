using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Qiniu
{
    public class QiniuService
    {
        public string UploadImg()
        {
            var mac = new Mac("gXBXI5GVEAZweMD7v9roKmRhcode8fFtnxjd-RkB", "lPqZgqyP3DM8ibLNwq7C5JAzj8Am9_Dhq3vx9IiM");
            var bucket = "qcode";
            var saveKey = "wawa021011.jpg";
            var localFile = "D:\\wawa.jpg";

            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            var putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = bucket;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            putPolicy.DeleteAfterDays = 1;
            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token            
            var jstr = putPolicy.ToJsonString();
            var token = Auth.CreateUploadToken(mac, jstr);
            var um = new UploadManager();
            var result = um.UploadFile(localFile, saveKey, token);
            return saveKey;
        }
    }
}
