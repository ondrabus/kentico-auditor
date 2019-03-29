using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.EventLog;
using System.Collections.Generic;
using CMS.Base;
using CMS.Membership;

namespace Auditor.Core.Actions.EventLog
{
    internal abstract class EventLogInfoBaseAction : BaseAction
    {
        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetEventArgs<LogEventArgs>(e);

            if (!EventLogSettings.AllowedActions.Contains(args.Event.EventCode))
            {
                CancelAction = true;
                return null;
            }

            AuditDataUserGUID = UserInfoProvider.GetUserInfo(args.Event.UserID).UserGUID;

            var data = new List<DataField>
            {
                new DataField { Name = nameof(EventLogInfo.EventDescription), Value = args.Event.EventDescription }
            };

            return data;
        }
    }
}
