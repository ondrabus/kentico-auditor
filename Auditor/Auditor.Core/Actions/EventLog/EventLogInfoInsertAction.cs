using CMS.EventLog;

namespace Auditor.Core.Actions.EventLog
{
    internal sealed class EventLogInfoInsertAction : EventLogInfoBaseAction
    {
        public override void Register()
        {
            EventLogEvents.LogEvent.After += CreateAuditLogItem;
        }
    }
}
