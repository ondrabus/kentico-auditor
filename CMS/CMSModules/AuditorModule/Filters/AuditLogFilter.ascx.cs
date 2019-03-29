using System;
using CMS.Helpers;
using Auditor.WebApi.Models;
using System.Collections.Generic;

namespace Auditor.Filters
{
    public partial class AuditLogFilter : System.Web.UI.UserControl
    {
        protected CMS.UIControls.UniSelector usSite;
        protected CMS.UIControls.UniSelector usUser;

        private Guid _userGuid;
        private Guid _siteGuid;
        private Guid _objectGuid;
        private string _orderBy;
        private Dictionary<string, string> _dataSearch = new Dictionary<string, string>();

        private AuditLogPager _pager;

        public AuditDataFilter Filter
        {
            get
            {
                var filter = new AuditDataFilter
                {
                    PageSize = _pager.PageSize,
                    Page = _pager.CurrentPage,
                    OrderBy = OrderBy,
                    DataSearch = new System.Collections.Generic.Dictionary<string, string>()
                };

                if (usSite.Value != null)
                {
                    var siteGuid = ValidationHelper.GetGuid(usSite.Value, Guid.Empty);

                    if (usSite.Value.ToString() == "0")
                        siteGuid = CMS.SiteProvider.SiteContext.CurrentSite.SiteGUID;

                    if (siteGuid != Guid.Empty)
                    {
                        filter.SiteGuid = siteGuid;
                    }
                }

                // user selector
                if (_userGuid != Guid.Empty)
                {
                    filter.UserGuid = _userGuid;
                }
                else
                {
                    if (usUser.Value != null)
                    {
                        var userGuid = ValidationHelper.GetGuid(usUser.Value, Guid.Empty);
                        if (userGuid != Guid.Empty)
                        {
                            filter.UserGuid = userGuid;
                        }
                    }
                }

                if (dtmTimeFrom.SelectedDateTime != DateTime.MinValue)
                    filter.DateStart = dtmTimeFrom.SelectedDateTime;

                if (dtmTimeTo.SelectedDateTime != DateTime.MinValue)
                    filter.DateEnd = dtmTimeTo.SelectedDateTime;

                if (!string.IsNullOrEmpty(txtObjectName.Text))
                    filter.ObjectName = txtObjectName.Text;

                if (!string.IsNullOrEmpty(txtSecondObjectName.Text))
                    filter.SecondObjectName = txtSecondObjectName.Text;

                if (!string.IsNullOrEmpty(txtSearchBox.Text))
                    filter.SearchText = txtSearchBox.Text;

                if (_objectGuid != Guid.Empty)
                {
                    filter.ObjectGuid = _objectGuid;
                }

                filter.DataSearch = _dataSearch;

                return filter;
            }
            set
            {
                if (value == null)
                    return;

                if (!value.SiteGuid.HasValue)
                    usSite.Value = -1;
                else
                    usSite.Value = value.SiteGuid.Value;

                if (!value.UserGuid.HasValue)
                    usUser.Value = null;
                else
                    usUser.Value = value.UserGuid.Value;

                if (!value.DateStart.HasValue)
                    dtmTimeFrom.SelectedDateTime = DateTimeHelper.ZERO_TIME;
                else
                    dtmTimeFrom.SelectedDateTime = value.DateStart.Value;

                if (!value.DateEnd.HasValue)
                    dtmTimeTo.SelectedDateTime = DateTimeHelper.ZERO_TIME;
                else
                    dtmTimeTo.SelectedDateTime = value.DateEnd.Value;

                if (string.IsNullOrEmpty(value.ObjectName))
                    txtObjectName.Text = string.Empty;
                else
                    txtObjectName.Text = value.ObjectName;

                if (string.IsNullOrEmpty(value.SecondObjectName))
                    txtSecondObjectName.Text = string.Empty;
                else
                    txtSecondObjectName.Text = value.SecondObjectName;

                if (string.IsNullOrEmpty(value.SearchText))
                    txtSearchBox.Text = string.Empty;
                else
                    txtSearchBox.Text = value.SearchText;

                if (value.DataSearch != null)
                {
                    _dataSearch = value.DataSearch;
                }
            }
        }

        public Guid UserGuid
        {
            set
            {
                _userGuid = value;
                pnlUserFilter.Visible = false;
            }
        }

        public Guid SiteGuid
        {
            set
            {
                _siteGuid = value;
                pnlSiteFilter.Visible = false;
            }
        }
        public bool SiteFilterVisible
        {
            set
            {
                pnlSiteFilter.Visible = value;
            }
        }
        public Guid ObjectGuid
        {
            set
            {
                _objectGuid = value;
                pnlObjectName.Visible = false;
            }
        }

        public string OrderBy
        {
            get
            {
                if (!string.IsNullOrEmpty(_orderBy))
                    return _orderBy;

                var storedOrderBy = ViewState["OrderBy"] as string;
                if (!string.IsNullOrEmpty(storedOrderBy))
                    return storedOrderBy;

                return nameof(AuditDataRestItem.DateCreated) + " DESC";
            }
            set
            {
                _orderBy = value;
                ViewState["OrderBy"] = value;
            }
        }

        public Dictionary<string, string> DataSearch
        {
            get
            {
                return _dataSearch;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Filter = new AuditDataFilter();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _pager = Parent.FindControl("pager") as AuditLogPager;

            pnlObjectName.Visible = false;
            pnlSecondObjectName.Visible = false;
        }
    }
}