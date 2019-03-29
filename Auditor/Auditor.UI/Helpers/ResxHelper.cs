
using Auditor.Core.Actions;
using Auditor.Core.Actions.Object;
using Auditor.Core.Helpers;
using System.Linq;

namespace Auditor.UI.Helpers
{
    public static class ResxHelper
    {
        public const string ResxPrefix = "Auditor.";
        public const string DetailSuffix = "Text";
        public const string ActionSuffix = "Action";

        public static string GetActionResxKey(string action)
        {
            return ResxPrefix + action + ActionSuffix;
        }
        public static string GetGenericActionResxKey(string action)
        {
            var objectType = ObjectHelper.GetObjectType(action);
            if (!string.IsNullOrEmpty(objectType))
                return GetActionResxKey(action.Replace(objectType, "ObjectInfo"));

            return GetActionResxKey(action);
        }
        public static string GetActionDetailResxKey(string action)
        {
            return ResxPrefix + action + ActionSuffix + DetailSuffix;
        }
        public static string GetGenericActionDetailResxKey(string action)
        {
            var objectType = ObjectHelper.GetObjectType(action);
            return GetActionDetailResxKey(!string.IsNullOrEmpty(objectType) ? action.Replace(objectType, "ObjectInfo") : action);
        }
        public static string GetObjectResxKey(string objectType)
        {
            return ResxPrefix + objectType;
        }
    }
}
