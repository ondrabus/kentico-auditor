using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.Base;
using CMS.SiteProvider;
using CMS.Synchronization;
using System;
using System.Collections.Generic;

namespace Auditor.Core.Actions.Staging
{
    internal abstract class StagingSynchronizeBaseAction : BaseAction
    {
        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var list = new List<DataField>();

            AuditDataObjectName = string.Empty;
            AuditDataObjectGuid = Guid.Empty;

            return list;
        }
    }
}
