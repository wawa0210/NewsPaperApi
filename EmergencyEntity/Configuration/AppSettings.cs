using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyEntity.Configuration
{
    public class AppSettings
    {
        public QiNiuConfig QiNiuConfig
        {
            get; set;
        }
    }

    /// <summary>
    /// 七牛配置信息
    /// </summary>
    public class QiNiuConfig
    {
        public string ImgUrl
        {
            get; set;
        }

        public string ApplicationKey
        {
            get; set;
        }
        public string SecretKey
        {
            get; set;
        }
        public string Bucket
        {
            get; set;
        }
    }
}
