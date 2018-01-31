using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaperNewsService.Model
{
    [Table("T_News")]
    public class TableNews
    {
        /// <summary>
        /// 属性: 
        /// </summary>
        [Column("Id")]
        [Key]
        [Description("")]
        public long Id
        {
            get; set;
        }

        /// <summary>
        /// 新闻编号
        /// </summary>
        [Column("NewsId")]
        [Description("新闻编号")]
        public string NewsId
        {
            get; set;
        }

        /// <summary>
        /// 新闻标题
        /// </summary>
        [Column("Title")]
        [Description("新闻标题")]
        public string Title
        {
            get; set;
        }

        /// <summary>
        /// 新闻概要
        /// </summary>
        [Column("ShortContent")]
        [Description("新闻概要")]
        public string ShortContent
        {
            get; set;
        }

        /// <summary>
        /// 新闻内容
        /// </summary>
        [Column("NewsContent")]
        [Description("新闻内容")]
        public string NewsContent
        {
            get; set;
        }

        /// <summary>
        /// 外部链接
        /// </summary>
        [Column("HrefUrl")]
        [Description("外部链接")]
        public string HrefUrl
        {
            get; set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CreateTime")]
        [Description("创建时间")]
        public string CreateTime
        {
            get; set;
        }

        /// <summary>
        /// 图片新闻地址
        /// </summary>
        [Column("NewsImgUrl")]
        [Description("图片新闻地址")]
        public string NewsImgUrl
        {
            get; set;
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("UpdateTime")]
        [Description("更新时间")]
        public string UpdateTime
        {
            get; set;
        }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Column("IsEnable")]
        [Description("是否可用")]
        public bool IsEnable
        {
            get; set;
        }
    }
}
