using System;
using System.Collections.Generic;
using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.Base;
using CMS.Synchronization;

namespace Auditor.Core.Actions.Staging
{
    internal abstract class StagingProcessTaskBaseAction : BaseAction
    {
        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetEventArgs<StagingSynchronizationEventArgs>(e);
            var list = new List<DataField>
            {
                new DataField { Name = nameof(StagingSynchronizationEventArgs.TaskType), Value = args.TaskType.ToString() },
                new DataField { Name = nameof(StagingSynchronizationEventArgs.ObjectType), Value = args.ObjectType },
                new DataField { Name = nameof(StagingSynchronizationEventArgs.TaskHandled), Value = args.TaskHandled.ToString() }
            };

            AuditDataObjectName = args.TaskType.ToString() + " (" + args.ObjectType + ")";
            AuditDataObjectGuid = Guid.Empty;

            return list;
        }
    }
}
