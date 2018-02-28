using EmergencyEntity.PageQuery;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaperNewsService.Entity
{
    public class EntityNewQuery : EntityBasePageQuery
    {
        public EntityNewQuery()
        {
            IsAll = false;
            IsEnable = true;
        }
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title
        {
            get; set;
        }
        /// <summary>
        /// 是否上线
        /// </summary>
        public bool IsEnable
        {
            get; set;
        }

        /// <summary>
        /// 筛选所有
        /// </summary>
        public bool IsAll
        {
            get; set;
        }

        /// <summary>
        /// 小程序版本号
        /// </summary>
        public string versionId
        {
            get; set;
        }
    }
}
