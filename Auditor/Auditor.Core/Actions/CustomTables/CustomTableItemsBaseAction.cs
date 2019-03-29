using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.Base;
using CMS.CustomTables;
using System;
using System.Collections.Generic;

namespace Auditor.Core.Actions.CustomTables
{
    internal abstract class CustomTableItemsBaseAction : BaseAction
    {
        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetEventArgs<CustomTableItemEventArgs>(e);

            AuditDataObjectName = args.Item.CustomTableClassName + "." + args.Item.ItemID;
            AuditDataObjectGuid = args.Item.ItemGUID;
            AuditDataSiteGuid = Guid.Empty;

            var data = ObjectHelper.GetBaseCustomTableItemData(args.Item);
            return data;
        }
    }
}
