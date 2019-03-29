using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auditor.Core.Models;
using CMS.Base;
using CMS.WorkflowEngine;
using Auditor.Core.Helpers;

namespace Auditor.Core.Actions.Documents.Workflow
{
    internal sealed class WorkflowHistoryInfoInsertAction : WorkflowHistoryBaseAction
    {

        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var data = base.GetAuditData(e);

            var args = ObjectHelper.GetObjectEventArgs(e);
            var wfHistory = args.Object as WorkflowHistoryInfo;

            data.Add(new DataField { Name = "StepID", Value = wfHistory.StepID.ToString() });
            data.Add(new DataField { Name = "StepDisplayName", Value = wfHistory.StepDisplayName });
            data.Add(new DataField { Name = "TargetStepID", Value = wfHistory.TargetStepID.ToString() });
            data.Add(new DataField { Name = "TargetStepDisplayName", Value = wfHistory.TargetStepDisplayName });
            data.Add(new DataField { Name = "HistoryTransitionType", Value = wfHistory.HistoryTransitionType.ToString() });
            data.Add(new DataField { Name = "WasRejected", Value = wfHistory.WasRejected.ToString() });

            if (!string.IsNullOrEmpty(wfHistory.Comment))
                data.Add(new DataField { Name = "Comment", Value = wfHistory.Comment });

            return data;
        }

        public override void Register()
        {
            WorkflowHistoryInfo.TYPEINFO.Events.Insert.After += CreateAuditLogItem;
        }
    }
}
