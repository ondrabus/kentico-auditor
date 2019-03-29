using System.Collections.Generic;
using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.Base;
using CMS.Synchronization;

namespace Auditor.Core.Actions.Staging
{
    internal sealed class StagingLogTaskFailureAction : StagingLogTaskBaseAction
    {
        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetEventArgs<StagingLogTaskEventArgs>(e);
            var list = base.GetAuditData(e);

            list.Add(new DataField { Name = nameof(StagingTaskInfo.TaskData), Value = args.Task.TaskData });

            return list;
        }
        public override void Register()
        {
            StagingEvents.LogTask.Failure += CreateAuditLogItem;
        }
    }
}
