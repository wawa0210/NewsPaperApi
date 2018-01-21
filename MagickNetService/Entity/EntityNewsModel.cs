using System;
using System.Collections.Generic;
using System.Text;

namespace MagickNetService.Entity
{
    public class EntityNewsModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// 二维码地址
        /// </summary>
        public string QcodeUrl
        {
            get;
            set;
        }
    }

}
