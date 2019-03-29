using Auditor.UI.Models;
using CMS.DataEngine;
using CMS.WorkflowEngine;
using System.Collections.Generic;

namespace Auditor.UI.Helpers
{
    public class ObjectDisplaySettings
    {
        public static readonly List<ObjectDisplayConfiguration> Settings = new List<ObjectDisplayConfiguration>
        {
            new ObjectDisplayConfiguration { Type = typeof(WorkflowHistoryInfo), UseObjectNameInsteadOfType = true, ObjectNameAccessor = nameof(WorkflowHistoryInfo.StepDisplayName), SecondObjectNameAccessor = nameof(WorkflowHistoryInfo.TargetStepDisplayName) },
            new ObjectDisplayConfiguration { Type = typeof(SettingsKeyInfo), SecondObjectNameAccessor = nameof(SettingsKeyInfo.KeyValue) }
        };
    }
}
