using CMS.Synchronization;

namespace Auditor.Core.Actions.Staging
{
    internal sealed class StagingProcessTaskAfterAction : StagingProcessTaskBaseAction
    {
        public override void Register()
        {
            StagingEvents.ProcessTask.After += CreateAuditLogItem;
        }
    }
}
