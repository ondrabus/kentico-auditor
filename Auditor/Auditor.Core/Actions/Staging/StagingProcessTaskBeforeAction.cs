using CMS.Synchronization;

namespace Auditor.Core.Actions.Staging
{
    internal sealed class StagingProcessTaskBeforeAction : StagingProcessTaskBaseAction
    {
        public override void Register()
        {
            StagingEvents.ProcessTask.Before += CreateAuditLogItem;
        }
    }
}
