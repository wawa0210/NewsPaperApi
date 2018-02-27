using System;
using System.Collections.Generic;
using System.Text;

namespace PaperNewsService.Entity
{
    public class EntityListNews
    {
        /// <summary>
        /// 新闻编号
        /// </summary>
        public string NewsId
        {
            get; set;
        }

        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title
        {
            get; set;
        }

        /// <summary>
        /// 新闻概要
        /// </summary>
        public string ShortContent
        {
            get; set;
        }

        /// <summary>
        /// 外部链接
        /// </summary>
        public string HrefUrl
        {
            get; set;
        }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable
        {
            get; set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get; set;
        }

        public string Media
        {
            get; set;
        }

        public int NewsType
        {
            get;set;
        }
    }
}
