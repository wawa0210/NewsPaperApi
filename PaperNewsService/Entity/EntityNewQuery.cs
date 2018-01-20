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
    }
}
