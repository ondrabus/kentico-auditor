using CMS.Synchronization;

namespace Auditor.Core.Actions.Staging
{
    internal sealed class StagingLogTaskBeforeAction : StagingLogTaskBaseAction
    {
        public override void Register()
        {
            StagingEvents.LogTask.Before += CreateAuditLogItem;
        }
    }
}
