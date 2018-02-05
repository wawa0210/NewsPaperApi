using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaperNewsService.Entity
{
    public class EntityNews
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
        [Required(ErrorMessage ="新闻标题不能为空")]
        public string Title
        {
            get; set;
        }

        /// <summary>
        /// 新闻概要
        /// </summary>
        [Required(ErrorMessage = "新闻概要不能为空")]
        public string ShortContent
        {
            get; set;
        }

        /// <summary>
        /// 新闻内容
        /// </summary>
        [Required(ErrorMessage = "新闻内容不能为空")]
        public string Content
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
        /// 媒体信息
        /// </summary>
        public string Media
        {
            get;set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get; set;
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
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
    }
}
