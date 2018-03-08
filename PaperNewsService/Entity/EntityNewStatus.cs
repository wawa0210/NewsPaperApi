using System.ComponentModel.DataAnnotations;

namespace PaperNewsService.Entity
{
    public class EntityNewStatus
    {
        /// <summary>
        /// 新闻编号
        /// </summary>
        [Required(ErrorMessage = "新闻编号不能为空")]
        public string NewsId
        {
            get; set;
        }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Required(ErrorMessage = "状态值必填")]
        public bool IsEnable
        {
            get; set;
        }
    }
}
