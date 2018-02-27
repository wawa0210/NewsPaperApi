using EmergencyEntity.PageQuery;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaperNewsService.Entity
{
    public class EntityVersionQuery : EntityBasePageQuery
    {
        /// <summary>
        /// 版本名称
        /// </summary>
        public string VersionName { get; set; }
    }
}
