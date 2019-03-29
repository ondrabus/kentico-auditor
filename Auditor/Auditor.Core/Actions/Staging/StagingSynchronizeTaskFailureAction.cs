using System.Collections.Generic;
using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.Base;
using CMS.Synchronization;

namespace Auditor.Core.Actions.Staging
{
    internal sealed class StagingSynchronizeTaskBeforeAction : StagingSynchronizeTaskBaseAction
    {
        public override void Register()
        {
            StagingEvents.SynchronizeTask.Before += CreateAuditLogItem;
        }

        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetEventArgs<StagingTaskEventArgs>(e);

            var list = base.GetAuditData(e);
            list.Add(new DataField { Name = nameof(StagingTaskInfo.TaskData), Value = args.Task.TaskData });

            return list;
        }
    }
}
