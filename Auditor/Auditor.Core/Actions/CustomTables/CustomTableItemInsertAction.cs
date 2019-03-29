using CMS.CustomTables;

namespace Auditor.Core.Actions.CustomTables
{
    internal sealed class CustomTableItemInsertAction : CustomTableItemsBaseAction
    {
        public override void Register()
        {
            CustomTableItemEvents.Insert.After += CreateAuditLogItem;
        }
    }
}
