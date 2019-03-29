using Auditor.Core.Actions;
using Auditor.Core.Actions.Object;
using Auditor.Core.Helpers;
using Auditor.WebApi.Models;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Auditor.UI.Helpers
{
    public static class ObjectHelper
    {
        public static List<string> GetSupportedObjectActions()
        {
            var actions = new List<string>();
            foreach (var action in ObjectSettings.SupportedObjectActions)
            {
                foreach (var actionType in Enum.GetValues(typeof(ActionType))
                    .Cast<ActionType>()
                    .Where(actionType => action.Value.HasFlag(actionType)))
                {
                    actions.Add(Core.Helpers.ObjectHelper.GetActionName(action.Key, actionType));
                }
            }

            return actions;
        }

        public static string GetObjectType(string action)
        {
            var actionTypes = Enum.GetNames(typeof(ActionType)).ToList();
            var actionEvents = new[] { "Before", "After", "Failure" };
            var pattern = $"(?<objectType>[a-z]+)({string.Join("|", actionTypes) + "|" + string.Join("|", actionEvents)})";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            var match = regex.Match(action);
            if (match.Success)
            {
                return match.Groups["objectType"].Value;
            }

            return action;
        }

        public static string GetActionText(string action)
        {
            var rsKey = ResxHelper.GetActionResxKey(action);
            var actionText = ResHelper.GetString(rsKey);
            if (actionText == rsKey)
                actionText = ResHelper.GetString(ResxHelper.GetGenericActionResxKey(action));

            var objectType = GetObjectType(action);
            var objectDisplayName = ResHelper.GetString(ResxHelper.GetObjectResxKey(objectType));

            return string.Format(actionText, objectDisplayName);
        }

        public static string GetActionDetailText(string action, string objectName, string secondObjectName, List<DataFieldRest> data)
        {
            var rsKeyText = ResxHelper.GetActionDetailResxKey(action);
            var actionTextText = ResHelper.GetString(rsKeyText);
            if (actionTextText == rsKeyText)
                actionTextText = ResHelper.GetString(ResxHelper.GetGenericActionDetailResxKey(action));

            var objectType = GetObjectType(action);
            var objectDisplayName = ResHelper.GetString(ResxHelper.GetObjectResxKey(objectType));

            var objectDisplaySettings = ObjectDisplaySettings.Settings.SingleOrDefault(a => a.Type.Name == objectType);

            if (objectDisplaySettings != null)
            {
                if (data.Any(d => d.Name == objectDisplaySettings.ObjectNameAccessor) && data.Any(d => d.Name == objectDisplaySettings.SecondObjectNameAccessor))
                {
                    if (objectDisplaySettings.UseObjectNameInsteadOfType)
                        objectDisplayName = objectName;

                    if (!string.IsNullOrEmpty(objectDisplaySettings.ObjectNameAccessor))
                        objectName = data.Single(d => d.Name == objectDisplaySettings.ObjectNameAccessor).Value;

                    if (!string.IsNullOrEmpty(objectDisplaySettings.SecondObjectNameAccessor))
                        secondObjectName = data.Single(d => d.Name == objectDisplaySettings.SecondObjectNameAccessor).Value;
                }
                else
                {
                    actionTextText = ResHelper.GetString(ResxHelper.GetGenericActionDetailResxKey(action));
                }
            }


            return string.Format(actionTextText,
                objectDisplayName,
                objectName.TrimLength(50),
                secondObjectName.TrimLength(50));

        }

    }
}
