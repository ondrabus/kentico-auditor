using CMS.Scheduler;

namespace Auditor.Core.Tasks
{
    public interface IAuditorTask : ITask
    {
        string TaskName { get; }
        TaskInterval TaskInterval { get; }
    }
}