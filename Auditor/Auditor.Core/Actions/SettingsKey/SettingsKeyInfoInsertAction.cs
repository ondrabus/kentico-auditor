using System.Collections.Generic;
using Auditor.Core.Models;
using CMS.Base;
using Auditor.Core.Helpers;

namespace Auditor.Core.Actions.SettingsKey
{
    internal sealed class SettingsKeyInfoInsertAction : SettingsKeyInfoBaseAction
    {
        public override void Register()
        {
            CMS.DataEngine.SettingsKeyInfo.TYPEINFO.Events.Insert.After += CreateAuditLogItem;
        }

        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var data = base.GetAuditData(e);

            var settingsKey = ObjectHelper.GetObject<CMS.DataEngine.SettingsKeyInfo>(e);

            data.Add(new DataField { Name = nameof(CMS.DataEngine.SettingsKeyInfo.KeyValue), Value = settingsKey.KeyValue });
            data.Add(new DataField { Name = nameof(CMS.DataEngine.SettingsKeyInfo.KeyDefaultValue), Value = settingsKey.KeyDefaultValue });

            return data;
        }
    }
}
