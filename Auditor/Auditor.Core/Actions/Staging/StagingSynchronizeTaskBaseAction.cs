using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.Base;
using CMS.SiteProvider;
using CMS.Synchronization;
using System;
using System.Collections.Generic;

namespace Auditor.Core.Actions.Staging
{
    internal abstract class StagingSynchronizeTaskBaseAction : BaseAction
    {
        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetEventArgs<StagingTaskEventArgs>(e);

            var list = ObjectHelper.GetBaseInfoDefaultData(args.Task);

            AuditDataObjectName = args.Task.TaskTitle;
            AuditDataObjectGuid = Guid.Empty;
            AuditDataSiteGuid = SiteInfoProvider.GetSiteInfo(args.Task.Generalized.ObjectSiteID).SiteGUID;

            return list;
        }
    }
}
