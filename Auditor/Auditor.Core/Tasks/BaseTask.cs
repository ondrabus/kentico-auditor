using CMS.Scheduler;

namespace Auditor.Core.Tasks
{
    public abstract class BaseTask : IAuditorTask
    {
        public const string TaskPrefix = "AuditorTask.";
        public abstract TaskInterval TaskInterval { get; }
        public string TaskName => TaskPrefix + GetType().Name;
        public abstract string Execute(TaskInfo task);
    }
}