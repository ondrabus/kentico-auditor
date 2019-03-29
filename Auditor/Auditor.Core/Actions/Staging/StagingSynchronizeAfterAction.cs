using CMS.Synchronization;

namespace Auditor.Core.Actions.Staging
{
    internal sealed class StagingSynchronizeAfterAction : StagingSynchronizeBaseAction
    {
        public override void Register()
        {
            StagingEvents.Synchronize.After += CreateAuditLogItem;
        }
    }
}
