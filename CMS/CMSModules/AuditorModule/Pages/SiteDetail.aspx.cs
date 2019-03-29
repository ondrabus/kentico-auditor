using CMS.Helpers;
using CMS.SiteProvider;
using CMS.UIControls;
using System;
using System.Linq;

namespace Auditor.Pages
{
    public partial class SiteDetail : CMSDeskPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var siteId = QueryHelper.GetInteger("objectid", 0);
            
            var site = SiteInfoProvider.GetSites()
                .Columns(nameof(SiteInfo.SiteGUID), "SiteDisplayName")
                .WhereEquals(nameof(SiteInfo.SiteID), siteId)
                .TopN(1)
                .FirstOrDefault();

            if (site == null)
                return;

            ltlSiteName.Text = site.DisplayName;

            filter.SiteGuid = site.SiteGUID;
        }
    }
}