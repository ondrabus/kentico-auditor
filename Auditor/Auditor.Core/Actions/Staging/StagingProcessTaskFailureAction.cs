using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.Base;
using CMS.Synchronization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Auditor.Core.Actions.Staging
{
    internal sealed class StagingProcessTaskFailureAction : StagingProcessTaskBaseAction
    {
        public override void Register()
        {
            StagingEvents.ProcessTask.Failure += CreateAuditLogItem;
        }

        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetEventArgs<StagingSynchronizationEventArgs>(e);
            var list = base.GetAuditData(e);

            // serialize dataset
            var data = args.TaskData;
            var json = JsonConvert.SerializeObject(data);

            list.Add(new DataField { Name = nameof(StagingSynchronizationEventArgs.TaskData), Value = json });

            return list;
        }
    }
}
