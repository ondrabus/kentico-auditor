using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.Base;
using System.Collections.Generic;
using System.Linq;

namespace Auditor.Core.Actions.Object
{
    internal sealed class ObjectUpdateAction : ObjectBaseAction
    {
        public override ActionType Type => ActionType.Update;
        public override void Register()
        {
            CMS.DataEngine.ObjectEvents.Update.Before += PrepareAuditedObjectData;
            CMS.DataEngine.ObjectEvents.Update.After += CreateAuditLogItem;
        }
        
        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetObjectEventArgs(e);
            if (!ObjectHelper.HasChanged(args))
            {
                CancelAction = true;
                return null;
            }

            var data = base.GetAuditData(e);

            data.AddRange(ObjectHelper.AppendChangedColumns(args));

            if (!data.Any())
            {
                CancelAction = true;
                return null;
            }

            return data;
        }

        protected override List<KeyValuePair<string, object>> PrepareData(CMSEventArgs e, ref string key)
        {
            if (!ObjectHelper.ActionEnabled(e, Type))
                return null;

            return ActionsHelper.GetChangedData(e, ref key);
        }
    }
}