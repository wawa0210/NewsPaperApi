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

    public class QiNiuConfig
    {
        public string ImgUrl
        {
            get; set;
        }
    }
}
