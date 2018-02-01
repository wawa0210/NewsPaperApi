using System.Threading;

namespace EmergencyEntity
{
    public class UserContext
    {
        public static AsyncLocal<AccountContext> Current { get; set; } = new AsyncLocal<AccountContext>();
    }

    public class AccountContext
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

    }
}
