using CMS.DataEngine;
using System.Collections.Generic;

namespace Auditor.Core.Helpers
{
    internal sealed class SettingsHelper : Singleton<SettingsHelper>
    {
        private const string AuditorAllowedObjectsSettingsKey = "AuditorAllowedActions";

        public List<string> AllowedActions
        {
            get
            {
                var allowedActions = SerializationHelper.Unserialize<List<string>>(SettingsKeyInfoProvider.GetSettingsKeyInfo(AuditorAllowedObjectsSettingsKey)?.KeyValue ?? string.Empty);

                return allowedActions != null ? allowedActions : new List<string>();
            }
        }
    }
}