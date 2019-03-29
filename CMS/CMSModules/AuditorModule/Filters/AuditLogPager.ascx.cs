using CMS.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Auditor.Filters
{
    public partial class AuditLogPager : System.Web.UI.UserControl
    {
        private AuditLogFilter _filter;
        private bool _displayLeftDots = false;
        private bool _displayRightDots = false;

        public int PageSize
        {
            get
            {
                return ValidationHelper.GetInteger(drpPageSize.SelectedValue, 0);
            }
        }
        public int CurrentPage
        {
            get; set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _filter = Parent.FindControl("filter") as AuditLogFilter;
        }

        protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void Update(int totalItems, int pageSize, int currentPage)
        {
            drpPageSize.SelectedValue = _filter.Filter.PageSize.ToString();
            RenderPager(totalItems, pageSize, currentPage);
        }
        private void RenderPager(int totalItems, int pageSize, int currentPage)
        {
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var pages = new List<ListItem>();

            var startingIndex = Math.Max(CurrentPage - 4, 0);
            var endingIndex = Math.Min(CurrentPage + 5, totalPages);

            for (var i = startingIndex; i < endingIndex; i++)
            {
                pages.Add(new ListItem((i + 1).ToString(), i.ToString()));
            }

            if (startingIndex > 0)
                _displayLeftDots = true;

            if (endingIndex < totalPages)
                _displayRightDots = true;

            rptPager.DataSource = pages;
            rptPager.DataBind();
        }

        protected string DisplayLeftDots()
        {
            if (_displayLeftDots)
                return "<li><a href=\"javascript:;\">...</a></li>";

            return string.Empty;
        }
        protected string DisplayRightDots()
        {
            if (_displayRightDots)
                return "<li><a href=\"javascript:;\">...</a></li>";

            return string.Empty;
        }
        
        protected void lnkPage_Click(object sender, EventArgs e)
        {
            CurrentPage = ValidationHelper.GetInteger(((LinkButton)sender).CommandArgument, 0);
        }
    }
}