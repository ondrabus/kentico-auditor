namespace Auditor.Core.Actions.SettingsKey
{
    internal sealed class SettingsKeyInfoDeleteAction : SettingsKeyInfoBaseAction
    {
        public override void Register()
        {
            CMS.DataEngine.SettingsKeyInfo.TYPEINFO.Events.Delete.After += CreateAuditLogItem;
        }
    }
}
