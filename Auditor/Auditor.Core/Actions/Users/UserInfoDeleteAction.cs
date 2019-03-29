using CMS.Membership;

namespace Auditor.Core.Actions.Users
{
    internal sealed class UserInfoDeleteAction : UserInfoBaseAction
    {
        public override void Register()
        {
            UserInfo.TYPEINFO.Events.Delete.After += CreateAuditLogItem;
        }
    }
}