using CMS.Synchronization;

namespace Auditor.Core.Actions.Staging
{
    internal sealed class StagingSynchronizeTaskFailureAction : StagingSynchronizeTaskBaseAction
    {
        public override void Register()
        {
            StagingEvents.SynchronizeTask.Failure += CreateAuditLogItem;
        }
    }
}
