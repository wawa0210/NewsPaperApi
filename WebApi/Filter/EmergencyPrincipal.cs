using EmergencyAccount.Entity;
using System;
using System.Security.Principal;

namespace WebApi.Filter
{
    public class EmergencyPrincipal : IPrincipal
    {
        public IIdentity Identity => throw new NotImplementedException();

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public EntityAccountManager UserContext { get; set; }
    }
}
