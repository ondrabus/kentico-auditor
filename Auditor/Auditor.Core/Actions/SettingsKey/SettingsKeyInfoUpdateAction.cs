namespace Auditor.Core.Actions.SettingsKey
{
    internal sealed class SettingsKeyInfoUpdateAction : SettingsKeyInfoBaseAction
    {
        public override void Register()
        {
            CMS.DataEngine.SettingsKeyInfo.TYPEINFO.Events.Update.Before += PrepareAuditedObjectData;
            CMS.DataEngine.SettingsKeyInfo.TYPEINFO.Events.Update.After += CreateAuditLogItem;
        }
    }
}
