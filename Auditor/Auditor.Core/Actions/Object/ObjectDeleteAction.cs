using Auditor.Core.Actions;
using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.Base;
using System.Collections.Generic;

namespace Auditor.Core.Actions.Object
{
    internal sealed class ObjectDeleteAction : ObjectBaseAction
    {
        public override ActionType Type => ActionType.Delete;
        public override void Register()
        {
            CMS.DataEngine.ObjectEvents.Delete.Before += PrepareAuditedObjectData;
            CMS.DataEngine.ObjectEvents.Delete.After += CreateAuditLogItem;
        }
    }
}