using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public static class UserContext
    {
        private static AsyncLocal<AccountContext> current = new AsyncLocal<AccountContext>();

        public static AsyncLocal<AccountContext> Current
        {
            get { return current; }
            private set { current = value; }
        }
    }

    public class AccountContext
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

    }
}
