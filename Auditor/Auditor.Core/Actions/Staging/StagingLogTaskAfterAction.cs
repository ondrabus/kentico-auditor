using CMS.Synchronization;

namespace Auditor.Core.Actions.Staging
{
    internal sealed class StagingLogTaskAfterAction : StagingLogTaskBaseAction
    {
        public override void Register()
        {
            StagingEvents.LogTask.After += CreateAuditLogItem;
        }
    }
}
