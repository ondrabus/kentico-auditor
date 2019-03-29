using CMS.DataEngine;
using CMS.Helpers;
using CMS.Membership;
using CMS.SiteProvider;
using CMS.UIControls;
using System;
using System.Linq;

namespace Auditor.Pages
{
    public partial class CustomTableDetail : CMSDeskPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var customTableId = QueryHelper.GetInteger("objectid", 0);
            if (customTableId == 0)
                return;

            var customTable = DataClassInfoProvider.GetClasses()
                .Columns(
                    nameof(DataClassInfo.ClassGUID),
                    nameof(DataClassInfo.ClassName),
                    nameof(DataClassInfo.ClassDisplayName))
                .WhereEquals(nameof(DataClassInfo.ClassID), customTableId)
                .TopN(1)
                .FirstOrDefault();

            if (customTable == null)
                return;

            ltlTableName.Text = customTable.ClassDisplayName;
            filter.SiteFilterVisible = false;

            filter.ObjectGuid = customTable.ClassGUID;
            filter.DataSearch.Add("ClassName", customTable.ClassName);
        }
    }
}