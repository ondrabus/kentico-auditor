using System;
using Auditor.Core.Helpers;
using CMS.Scheduler;
using System.Web;

namespace Auditor.Core.Tasks
{
    public class ActionsProcessingTask : BaseTask
    {
        public override TaskInterval TaskInterval => new TaskInterval
        {
            Period = SchedulingHelper.PERIOD_SECOND,
            StartTime = DateTime.Now,
            Every = 30
        };

        public override string Execute(TaskInfo task)
        {
            var addedItems = StorageHelper.Instance.Process();
            return $"Added {addedItems} audit log items.";
        }
    }
}