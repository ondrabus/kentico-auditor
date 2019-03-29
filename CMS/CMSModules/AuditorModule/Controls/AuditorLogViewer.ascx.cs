using Auditor.WebApi.Extensions;
using Auditor.WebApi.Models;
using Auditor.WebApi.Providers;
using CMS.Base.Web.UI;
using CMS.Helpers;
using CMS.Membership;
using CMS.SiteProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Auditor.Controls
{
    public partial class AuditorLogViewer : System.Web.UI.UserControl, ICallbackEventHandler
    {
        protected CMS.UIControls.UniGrid uniGrid;
        public string GridName { get; set; }
        public Guid LogItemGuid { get; set; }

        private Filters.AuditLogFilter _filter;
        private Filters.AuditLogPager _pager;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblNoItems.Visible = false;

            _filter = Parent.FindControl("filter") as Filters.AuditLogFilter;
            _pager = Parent.FindControl("pager") as Filters.AuditLogPager;

            uniGrid.GridName = GridName;

            uniGrid.OnAction += OnAction;
            uniGrid.OnExternalDataBound += OnExternalDataBound;
            uniGrid.OrderBy = _filter.OrderBy;

            ScriptHelper.RegisterBootstrapScripts(Page);
            ScriptHelper.RegisterDialogScript(Page);

            var logItemDetail = @"function OpenLogItemDetail(logItemGuid) { modalDialog(" + ScriptHelper.GetString(ResolveUrl("~/CMSModules/AuditorModule/Pages/LogItemDetail.aspx")) + " + logItemGuid, 'logitemdetail', 1080, 700); }";
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "LogItemDetailOpenScript", ScriptHelper.GetScript(logItemDetail));
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "LogItemDetail_" + ClientID, ScriptHelper.GetScript("var logItemDetailParams_" + ClientID + " = '';"));
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (_filter != null)
                LoadData(_filter.Filter);
            else
                LoadData(null);
        }

        private void LoadData(AuditDataFilter filter)
        {
            var logs = LogProvider.GetLogs(filter);

            _pager.Update(logs.TotalCount, logs.PageSize, logs.Page);

            uniGrid.DataSource = logs.Items.ToDataSet();
            uniGrid.ReloadData();

            if (DataHelper.DataSourceIsEmpty(uniGrid.DataSource))
            {
                lblNoItems.Visible = true;
            }
        }

        private string GetActionRowStringValue(object source, string columnName)
        {
            return GetRowValue((source as GridViewRow)?.DataItem, columnName)?.ToString() ?? string.Empty;
        }

        private object GetRowValue(object row, string columnName)
        {
            return ((DataRowView)row)[columnName];
        }
        private string GetRowStringValue(object row, string columnName)
        {
            return GetRowValue(row, columnName)?.ToString() ?? string.Empty;
        }
        private T GetRowValue<T>(object row, string columnName)
        {
            return (T)Convert.ChangeType(GetRowValue(row, columnName), typeof(T));
        }
        private object OnExternalDataBound(object sender, string sourceName, object parameter)
        {
            switch (sourceName)
            {
                case "showDialog":
                    var btn = sender as CMSGridActionButton;
                    if (btn == null)
                        return sender;

                    var logGuid = GetActionRowStringValue(parameter, "LogGuid");
                    btn.ToolTip = ResHelper.GetString("General.View");
                    btn.OnClientClick = "logItemDetailParams_" + ClientID + " = '" + logGuid + "';" + Page.ClientScript.GetCallbackEventReference(this, "logItemDetailParams_" + ClientID, "OpenLogItemDetail", null) + ";return false;";
                    return sender;

                case "Action":
                    return GetActionDisplayName(parameter);

                case "ActionText":
                    return GetActionDetailText(parameter);

                case "SiteGuid":
                    return SiteInfoProvider.GetSiteInfoByGUID(ValidationHelper.GetGuid(parameter, Guid.Empty))?.SiteName ?? ResHelper.GetString("Auditor.NoSite");

                case "UserGuid":
                    var userName = UserInfoProvider.GetUserInfoByGUID(ValidationHelper.GetGuid(parameter, Guid.Empty)).UserName;
                    if (userName == "public")
                        userName = ResHelper.GetString("Auditor.NoUser");

                    return userName;

            }


            return parameter;
        }

        private void OnAction(string actionName, object actionArgument)
        {
        }

        private string GetActionDisplayName(object parameter)
        {
            var action = GetRowStringValue(parameter, "Action");

            return UI.Helpers.ObjectHelper.GetActionText(action);
        }

        private string GetActionDetailText(object parameter)
        {
            var action = GetRowStringValue(parameter, "Action");
            var objectName = GetRowStringValue(parameter, "ObjectName");
            var secondObjectName = GetRowStringValue(parameter, "SecondObjectName");
            var data = GetRowValue<List<DataFieldRest>>(parameter, "Data");

            return UI.Helpers.ObjectHelper.GetActionDetailText(action, objectName, secondObjectName, data);
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            LogItemGuid = ValidationHelper.GetGuid(eventArgument, Guid.Empty);
        }

        public string GetCallbackResult()
        {
            return "?logItemGuid=" + LogItemGuid.ToString();
        }
        
        protected void UniGrid_OnBeforeSorting(object sender, EventArgs e)
        {
            var args = e as GridViewSortEventArgs;
            if (args == null)
                return;

            var orderBy = args.SortExpression + " " + (_filter.Filter.OrderBy.EndsWith("ASC") ? "DESC" : "ASC");
            _filter.OrderBy = orderBy;
            uniGrid.OrderBy = string.Empty;
            args.Cancel = true;
        }
    }
}