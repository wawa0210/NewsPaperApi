using System.ComponentModel.DataAnnotations;

namespace EmergencyAccount.Entity
{
    public class EntityAccountPwd
    {
        [Required(ErrorMessage = "用户编号不能为空")]
        public string Id { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        public string UserPwd { get; set; }
    }
}
