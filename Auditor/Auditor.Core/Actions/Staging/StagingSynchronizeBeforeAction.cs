using CMS.Synchronization;

namespace Auditor.Core.Actions.Staging
{
    internal sealed class StagingSynchronizeBeforeAction : StagingSynchronizeBaseAction
    {
        public override void Register()
        {
            StagingEvents.Synchronize.Before += CreateAuditLogItem;
        }
    }
}
