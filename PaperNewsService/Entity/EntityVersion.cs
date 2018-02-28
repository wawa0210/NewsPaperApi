using PaperNewsService.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaperNewsService.Entity
{
    public class EntityVersion
    {
        public EntityVersion()
        {
            VersionStatus = EnumVersionStatus.Examining;
        }
        /// <summary>
        /// 属性: 
        /// </summary>
        public long Id
        {
            get; set;
        }
        /// <summary>
        /// 版本编号
        /// </summary>
        public string VersionId
        {
            get; set;
        }
        /// <summary>
        /// 版本编号
        /// </summary>
        [Required(ErrorMessage = "版本名称不能为空")]
        public string VersionName
        {
            get; set;
        }

        /// <summary>
        /// 版本状态(1:审核中，2：审核通过)
        /// </summary>
        [Required(ErrorMessage = "版本状态不能为空")]

        public EnumVersionStatus VersionStatus
        {
            get; set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get; set;
        }
    }
}
