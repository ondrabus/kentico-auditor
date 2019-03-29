using System;
using System.Collections.Generic;
using Auditor.Core.Models;
using CMS.Base;
using Auditor.Core.Helpers;
using CMS.SiteProvider;
using System.Linq;

namespace Auditor.Core.Actions.SettingsKey
{
    internal abstract class SettingsKeyInfoBaseAction : BaseAction
    {
        public override List<DataField> GetAuditData(CMSEventArgs e)
        {
            var args = ObjectHelper.GetObjectEventArgs(e);
            if (!ObjectHelper.HasChanged(args))
            {
                CancelAction = true;
                return null;
            }

            var settingsKey = args.Object as CMS.DataEngine.SettingsKeyInfo;

            if (settingsKey == null)
                throw new InvalidOperationException("SettingsKey action requires SettingsKeyInfo object.");

            AuditDataObjectName = settingsKey.KeyName;
            AuditDataObjectGuid = settingsKey.KeyGUID;

            if (settingsKey.SiteID == 0)
                AuditDataSiteGuid = Guid.Empty;
            else
            {
                var siteGuid = SiteInfoProvider.GetSites().Column(nameof(SiteInfo.SiteGUID)).WhereEquals(nameof(SiteInfo.SiteID), settingsKey.SiteID).TopN(1).Single()?.SiteGUID;
                if (!siteGuid.HasValue)
                    throw new InvalidOperationException($"Site {settingsKey.SiteID} not found!");

                AuditDataSiteGuid = siteGuid.Value;
            }

            var data = ObjectHelper.AppendChangedColumns(args);

            if (!data.Any())
            {
                CancelAction = true;
                return null;
            }

            return data;
        }
    }
}
