using EmergencyAccount.Entity;
using System.Threading.Tasks;

namespace EmergencyAccount.Application
{
    public interface IAccountService
    {

        EntityAccountManager GetAccountManager(string userName);

        Task<EntityAccountManager> GetAccountManagerInfo(string useId);

        /// <summary>
        /// 校验登录密码是否正确
        /// </summary>
        /// <param name="inputPwd"></param>
        /// <param name="salt"></param>
        /// <param name="dbPwd"></param>
        /// <returns></returns>
        bool CheckLoginInfo(string inputPwd, string salt, string dbPwd);

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="entityAccountPwd"></param>
        /// <returns></returns>
        Task UpdateAccountPwd(EntityAccountPwd entityAccountPwd);
    }
}
