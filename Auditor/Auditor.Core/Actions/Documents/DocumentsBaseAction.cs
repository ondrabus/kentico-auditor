using Auditor.Core.Helpers;
using Auditor.Core.Models;
using CMS.Base;
using CMS.DocumentEngine;
using CMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auditor.Core.Actions.Documents
{
    internal abstract class DocumentsBaseAction : BaseAction
    {
        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetDocumentEventArgs(e);

            AuditDataObjectName = args.Node.DocumentName;
            AuditDataObjectGuid = args.Node.DocumentGUID;
            AuditDataSiteGuid = args.Node.Site.SiteGUID;

            var data = ObjectHelper.GetBaseInfoDefaultData(args.Node);
            return data;
        }
    }
}