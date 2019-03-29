namespace Auditor.Core.Actions
{
    public interface IAuditableAction
    {
        string SettingsKey { get; }
        bool IsEnabled { get; }
        bool IsGeneric { get; }
        void Register();
    }
}