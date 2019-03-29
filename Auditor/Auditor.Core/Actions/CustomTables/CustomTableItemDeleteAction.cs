using CMS.CustomTables;

namespace Auditor.Core.Actions.CustomTables
{
    internal sealed class CustomTableItemDeleteAction : CustomTableItemsBaseAction
    {
        public override void Register()
        {
            CustomTableItemEvents.Delete.After += CreateAuditLogItem;
        }
    }
}
