using CMS.DocumentEngine;

namespace Auditor.Core.Actions.Documents
{
    internal sealed class DocumentInsertAction : DocumentsBaseAction
    {
        public override void Register()
        {
            DocumentEvents.Insert.After += CreateAuditLogItem;
        }
    }
}