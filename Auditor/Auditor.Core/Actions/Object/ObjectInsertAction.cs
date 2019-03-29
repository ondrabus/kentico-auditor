
namespace Auditor.Core.Actions.Object
{
    internal sealed class ObjectInsertAction : ObjectBaseAction
    {
        public override ActionType Type => ActionType.Insert;
        public override void Register()
        {
            CMS.DataEngine.ObjectEvents.Insert.After += CreateAuditLogItem;
        }
    }
}