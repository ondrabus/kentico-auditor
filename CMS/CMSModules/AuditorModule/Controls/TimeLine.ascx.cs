using Auditor.Core.Helpers;
using Auditor.UI.Helpers;
using Auditor.WebApi.Models;
using CMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Auditor.Controls
{
    public partial class TimeLine : System.Web.UI.UserControl
    {
        public List<AuditDataRestItem> Data { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            RenderTimelineItems();
        }

        private void RenderTimelineItems()
        {
            if (Data == null || Data.Count == 0)
                return;

            var sb = new StringBuilder();
            sb.AppendLine(@"<script type=""text/javascript"">");
            sb.AppendLine(@"var items = new vis.DataSet([");

            foreach (var item in Data)
            {
                sb.AppendFormat("{id:'{0}', label:'{1}', title: '{2}', start: new Date('{3}'), type: 'point'},",
                    item.LogGuid,
                    ResHelper.GetString(ResxHelper.GetActionResxKey(item.Action)),
                    string.Format(ResHelper.GetString(ResxHelper.GetActionDetailResxKey(item.Action)), item.ObjectName),
                    item.DateCreated.ToString());
            }

        }
    }
}