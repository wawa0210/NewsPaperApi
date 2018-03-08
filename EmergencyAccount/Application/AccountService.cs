using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CommonLib;
using CommonLib.Security;
using EmergencyAccount.Entity;
using EmergencyAccount.Model;
using EmergencyBaseService;

namespace EmergencyAccount.Application
{
    public class AccountService : BaseAppService,IAccountService
    {
        public EntityAccountManager GetAccountManager(string userName)
        {
            var accountRep = GetRepositoryInstance<TableAccountManager>();
            var restult = accountRep.Find(x => x.UserName == userName);
            return Mapper.Map<TableAccountManager, EntityAccountManager>(restult);
        }

        public async Task<EntityAccountManager> GetAccountManagerInfo(string useId)
        {
            throw new NotImplementedException();
        }

        public bool CheckLoginInfo(string inputPwd, string salt, string dbPwd)
        {
            var userPwd = DesEncrypt.Encrypt(inputPwd, salt);
            return userPwd == dbPwd;
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="entityAccountPwd"></param>
        public async Task UpdateAccountPwd(EntityAccountPwd entityAccountPwd)
        {
            var userSalt = Utils.GetCheckCode(16);
            var model = new TableAccountManager()
            {
                Id = entityAccountPwd.Id,
                UserPwd = DesEncrypt.Encrypt(entityAccountPwd.UserPwd.Trim().Trim(), userSalt),
                UserSalt = userSalt
            };
            var accountRep = GetRepositoryInstance<TableAccountManager>();

            accountRep.Update<TableAccountManager>(model, managerInfo => new
            {
                managerInfo.UserPwd,
                managerInfo.UserSalt

            });
        }
    }
}
