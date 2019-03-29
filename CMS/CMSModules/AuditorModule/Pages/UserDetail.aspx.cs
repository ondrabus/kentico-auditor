using CMS.Helpers;
using CMS.Membership;
using CMS.SiteProvider;
using CMS.UIControls;
using System;
using System.Linq;

namespace Auditor.Pages
{
    public partial class UserDetail : CMSDeskPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = QueryHelper.GetInteger("objectid", 0);
            if (userId == 0)
                return;

            var user = UserInfoProvider.GetUsers()
                .Columns(nameof(UserInfo.UserGUID), nameof(UserInfo.UserName))
                .WhereEquals(nameof(UserInfo.UserID), userId)
                .TopN(1)
                .FirstOrDefault();

            if (user == null)
                return;

            ltlUserName.Text = user.UserName;
            filter.UserGuid = user.UserGUID;
        }
    }
}