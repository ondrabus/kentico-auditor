using CMS.DocumentEngine;

namespace Auditor.Core.Actions.Documents
{
    internal sealed class DocumentDeleteAction : DocumentsBaseAction
    {
        public override void Register()
        {
            DocumentEvents.Delete.After += CreateAuditLogItem;
        }
    }
}