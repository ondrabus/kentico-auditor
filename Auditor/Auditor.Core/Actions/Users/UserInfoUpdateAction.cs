using Auditor.Core.Helpers;
using CMS.Base;
using CMS.Membership;
using System.Collections.Generic;
using Auditor.Core.Models;
using System;
using System.Linq;

namespace Auditor.Core.Actions.Users
{
    internal sealed class UserInfoUpdateAction : UserInfoBaseAction
    {
        public override void Register()
        {
            UserInfo.TYPEINFO.Events.Update.Before += PrepareAuditedObjectData;
            UserInfo.TYPEINFO.Events.Update.After += CreateAuditLogItem;
        }

        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var data = base.GetAuditData(e);
            var args = ObjectHelper.GetObjectEventArgs(e);
            if (!ObjectHelper.HasChanged(args))
            {
                CancelAction = true;
                return null;
            }

            var changedCols = ObjectHelper.AppendChangedColumns(args);

            data.AddRange(changedCols);

            return data;
        }
    }
}