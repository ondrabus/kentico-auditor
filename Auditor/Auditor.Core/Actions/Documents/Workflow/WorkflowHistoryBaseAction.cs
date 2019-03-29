using System;
using System.Collections.Generic;
using System.Linq;
using Auditor.Core.Models;
using CMS.Base;
using Auditor.Core.Helpers;
using CMS.WorkflowEngine;
using CMS.DocumentEngine;

namespace Auditor.Core.Actions.Documents.Workflow
{
    internal abstract class WorkflowHistoryBaseAction : BaseAction
    {
        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetObjectEventArgs(e);
            var wfHistory = args.Object as WorkflowHistoryInfo;

            if (wfHistory == null)
                throw new InvalidOperationException("WorkflowHistory action requires WorkflowHistoryInfo object.");

            var doc = DocumentHelper.GetDocuments().WhereEquals(nameof(TreeNode.DocumentID), wfHistory.HistoryObjectID).TopN(1).Single();

            AuditDataObjectName = doc.DocumentName;
            AuditDataObjectGuid = doc.DocumentGUID;
            
            return new List<DataField>();
        }
    }
}
