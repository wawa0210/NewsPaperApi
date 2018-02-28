using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaperNewsService.Model
{
    [Table("T_Version")]
    public class TableVersions
    {
        /// <summary>
        /// 属性: 
        /// </summary>
        [Column("Id")]
        [Key]
        [Description("")]
        public long Id
        {
            get;
        }

        /// <summary>
        /// 版本编号
        /// </summary>
        [Column("VersionId")]
        [Description("版本编号")]
        public string VersionId
        {
            get; set;
        }


        /// <summary>
        /// 版本编号
        /// </summary>
        [Column("VersionName")]
        [Description("版本名称")]
        public string VersionName
        {
            get; set;
        }

        /// <summary>
        /// 版本状态(1:审核中，2：审核通过)
        /// </summary>
        [Column("VersionStatus")]
        [Description("版本状态")]
        public int VersionStatus
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
        /// 备注
        /// </summary>
        [Column("Remark")]
        [Description("备注")]
        public string Remark
        {
            get; set;
        }
    }
}
