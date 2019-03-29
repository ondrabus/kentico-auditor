using System;
using System.Collections.Generic;
using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.Base;
using CMS.Membership;

namespace Auditor.Core.Actions.Users
{
    internal sealed class UserInfoInsertAction : UserInfoBaseAction
    {
        public override void Register()
        {
            UserInfo.TYPEINFO.Events.Insert.After += CreateAuditLogItem;
        }

        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var data = base.GetAuditData(e);

            var args = ObjectHelper.GetObjectEventArgs(e);
            var user = args.Object as UserInfo;
            if (user == null)
            {
                throw new InvalidOperationException("UserBaseAction must have UserInfo as e.Object parameter.");
            }

            data.AddRange(new List<DataField>
            {
                new DataField {Name = nameof(UserInfo.Email), Value = user.Email},
                new DataField {Name = nameof(UserInfo.SiteIndependentPrivilegeLevel), Value = user.SiteIndependentPrivilegeLevel.ToString()}
            });

            return data;
        }
    }
}