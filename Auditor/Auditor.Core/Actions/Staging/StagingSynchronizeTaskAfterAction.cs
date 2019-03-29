using CMS.Synchronization;

namespace Auditor.Core.Actions.Staging
{
    internal sealed class StagingSynchronizeTaskAfterAction : StagingSynchronizeTaskBaseAction
    {
        public override void Register()
        {
            StagingEvents.SynchronizeTask.After += CreateAuditLogItem;
        }
    }
}
