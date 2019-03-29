using Auditor.Core.Actions;
using CMS.Base;
using System.Collections.Generic;
using System.Linq;

namespace Auditor.Core.Helpers
{
    public static class ActionsHelper
    {
        public static List<string> Actions => InterfaceHelper
            .GetImplementingClassesInstances<IAuditableAction>()
            .Where(action => !action.IsGeneric)
            .Select(action => action.SettingsKey)
            .ToList();

        internal static List<KeyValuePair<string, object>> GetChangedData(CMSEventArgs e, ref string key)
        {
            var infoObject = ObjectHelper.GetBaseObject(e);

            List<KeyValuePair<string, object>> data;
            data = ObjectHelper.GetChangedColumns(infoObject);
            key = ObjectHelper.GetKey(infoObject);

            return data;
        }
    }
}