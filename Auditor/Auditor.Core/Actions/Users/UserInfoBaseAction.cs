using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.Base;
using CMS.Membership;
using System;
using System.Collections.Generic;

namespace Auditor.Core.Actions.Users
{
    internal abstract class UserInfoBaseAction : BaseAction
    {
        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetObjectEventArgs(e);
            var user = args.Object as UserInfo;

            if (user == null)
            {
                throw new InvalidOperationException("UserBaseAction must have UserInfo as e.Object parameter.");
            }

            AuditDataObjectName = user.UserName;
            AuditDataObjectGuid = user.UserGUID;

            return new List<DataField>();
        }
    }
}