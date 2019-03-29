using CMS.Membership;
using System.Collections.Generic;
using Auditor.Core.Models;
using CMS.Base;
using Auditor.Core.Helpers;

namespace Auditor.Core.Actions.Users
{
    internal sealed class UserLoginAction : UserInfoBaseAction
    {
        public override void Register()
        {
            SecurityEvents.Authenticate.Execute += CreateAuditLogItem;
        }

        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetEventArgs<AuthenticationEventArgs>(e);

            // wont handle unsuccessful login attempts
            if (args.User == null)
            {
                CancelAction = true;
                return null;
            }

            AuditDataUserGUID = args.User.UserGUID;
            AuditDataObjectName = args.User.UserName;

            return null;
        }
    }
}