using CMS.Synchronization;

namespace Auditor.Core.Actions.Staging
{
    internal sealed class StagingSynchronizeFailureAction : StagingSynchronizeBaseAction
    {
        public override void Register()
        {
            StagingEvents.Synchronize.Failure += CreateAuditLogItem;
        }
    }
}
