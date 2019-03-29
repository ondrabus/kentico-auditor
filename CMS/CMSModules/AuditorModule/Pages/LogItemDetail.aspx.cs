using Auditor.Core.Actions.Object;
using Auditor.UI.Helpers;
using Auditor.WebApi.Models;
using Auditor.WebApi.Providers;
using CMS.Helpers;
using CMS.Membership;
using CMS.SiteProvider;
using CMS.UIControls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auditor.Pages
{
    public partial class LogItemDetail : CMSModalPage
    {
        private bool _isUpdateAction = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            var guid = ValidationHelper.GetGuid(Request.QueryString["logItemGuid"], Guid.Empty);
            if (guid == Guid.Empty)
                return;

            var logItem = LogProvider.GetLog(guid);

            _isUpdateAction = logItem.Action.Contains("Update");

            lblLogItemGuidText.Text = logItem.LogGuid.ToString();
            lblSiteText.Text = SiteInfoProvider.GetSiteInfoByGUID(logItem.SiteGuid)?.DisplayName ?? ((logItem.SiteGuid == Guid.Empty) ? ResHelper.GetString("Auditor.NoSite") : ResHelper.GetStringFormat("Auditor.SiteInfoDeleted", logItem.SiteGuid));
            lblUserText.Text = UserInfoProvider.GetUserInfoByGUID(logItem.UserGuid)?.UserName ?? ((logItem.UserGuid == Guid.Empty) ? ResHelper.GetString("Auditor.NoUser") : ResHelper.GetStringFormat("Auditor.UserInfoDeleted", logItem.UserGuid));
            lblDateText.Text = logItem.DateCreated.ToString();
            lblActionText.Text = UI.Helpers.ObjectHelper.GetActionText(logItem.Action);

            var objectType = UI.Helpers.ObjectHelper.GetObjectType(logItem.Action);
            var objectTypeTitle = ResHelper.GetString(ResxHelper.GetObjectResxKey(objectType));
            lblObjectTypeText.Text = objectTypeTitle;

            lblObjectNameText.Text = logItem.ObjectName;

            if (logItem.ObjectGuid != null && logItem.ObjectGuid != Guid.Empty)
            {
                pnlObjectGuid.Visible = true;
                lblObjectGuidText.Text = logItem.ObjectGuid.ToString();
            }

            var assignmentSettings = ObjectSettings.AssignmentTypes.SingleOrDefault(a => a.Type.Name == objectType);
            if (assignmentSettings != null)
            {
                pnlObjectGuid.Visible = false;

                var firstObjectType = assignmentSettings.ObjectType;
                var secondObjectType = assignmentSettings.SecondObjectType;
                
                pnlAssignmentType.Visible = true;

                lblFirstObjectTypeText.Text = firstObjectType;
                lblFirstObjectNameText.Text = logItem.ObjectName;
                lblFirstObjectGuidText.Text = logItem.ObjectGuid.ToString();

                lblSecondObjectTypeText.Text = secondObjectType;
                lblSecondObjectNameText.Text = logItem.SecondObjectName;
                lblSecondObjectGuidText.Text = logItem.SecondObjectGuid.ToString();
            }

            var data = ExcludeAssignmentData(logItem.Data);
            if (data.Any())
            {
                lblNoData.Visible = false;
                rptData.DataSource = data;
                rptData.DataBind();
            }

            PageTitle.TitleText = GetString("Auditor.LogItemDetailTitle");
            Page.Title = PageTitle.TitleText;
        }

        private IEnumerable<DataFieldRest> ExcludeAssignmentData(List<DataFieldRest> data)
        {
            return data.Where(i => i.Name != "ObjectDisplayName" && i.Name != "SecondObjectDisplayName");
        }

        protected bool IsUpdateAction()
        {
            return _isUpdateAction;
        }
    }
}