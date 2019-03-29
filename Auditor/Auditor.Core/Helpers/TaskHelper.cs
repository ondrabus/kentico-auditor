using Auditor.Core.Tasks;
using CMS.Scheduler;
using System;
using System.Linq;

namespace Auditor.Core.Helpers
{
    public static class TaskHelper
    {
        public static void RegisterTasks()
        {
            var existingTasks = TaskInfoProvider
                .GetTasks()
                .WhereStartsWith(nameof(TaskInfo.TaskName), BaseTask.TaskPrefix)
                .Column("TaskName")
                .ToList()
                .Select(task => task.TaskName);

            var tasksToAdd = InterfaceHelper.GetImplementingClassesInstances<IAuditorTask>().Where(task => !existingTasks.Contains(task.TaskName));
            if (tasksToAdd.Count() > 0)
            {
                foreach (var task in tasksToAdd)
                {
                    var taskInfo = new TaskInfo
                    {
                        TaskAssemblyName = task.GetType().Assembly.GetName().Name,
                        TaskClass = task.GetType().FullName,
                        TaskEnabled = true,
                        TaskLastResult = string.Empty,
                        TaskDisplayName = task.TaskName,
                        TaskName = task.TaskName,
                        TaskType = ScheduledTaskTypeEnum.System,
                        TaskInterval = SchedulingHelper.EncodeInterval(task.TaskInterval),
                        TaskNextRunTime = DateTime.Now,
                        TaskRunInSeparateThread = true,
                        TaskGUID = Guid.NewGuid(),
                        TaskData = string.Empty
                    };

                    TaskInfoProvider.SetTaskInfo(taskInfo);
                }
            }
        }
    }
}