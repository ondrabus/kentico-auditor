using System.Collections.Generic;
using System.Linq;
using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.Base;
using CMS.CustomTables;

namespace Auditor.Core.Actions.CustomTables
{
    internal sealed class CustomTableItemUpdateAction : CustomTableItemsBaseAction
    {
        public override void Register()
        {
            CustomTableItemEvents.Update.Before += PrepareAuditedObjectData;
            CustomTableItemEvents.Update.After += CreateAuditLogItem;
        }
        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetEventArgs<CustomTableItemEventArgs>(e);
            if (!ObjectHelper.HasChanged(args))
            {
                CancelAction = true;
                return null;
            }

            var data = base.GetAuditData(e);
            data.AddDataFields(ObjectHelper.AppendChangedColumns(args));

            if (!data.Any())
            {
                CancelAction = true;
                return null;
            }

            return data;
        }
    }
}
