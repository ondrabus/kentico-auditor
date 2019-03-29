using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.Base;
using CMS.DocumentEngine;
using System.Collections.Generic;
using System.Linq;

namespace Auditor.Core.Actions.Documents
{
    internal sealed class DocumentUpdateAction : DocumentsBaseAction
    {
        public override void Register()
        {
            DocumentEvents.Update.Before += PrepareAuditedObjectData;
            DocumentEvents.Update.After += CreateAuditLogItem;
        }
        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetDocumentEventArgs(e);
            if (!ObjectHelper.HasChanged(args))
            {
                CancelAction = true;
                return null;
            }

            var data = base.GetAuditData(e);
            data.AddRange(ObjectHelper.AppendChangedColumns(args));

            if (!data.Any())
            {
                CancelAction = true;
                return null;
            }

            return data;
        }
    }
}