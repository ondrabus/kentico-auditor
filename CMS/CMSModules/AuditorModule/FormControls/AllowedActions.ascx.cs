using Auditor.Core.Helpers;
using CMS.FormEngine.Web.UI;
using CMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Auditor.FormControls
{
    public partial class AllowedActions : FormEngineUserControl
    {
        public override object Value
        {
            get
            {
                var settings = list.Items
                    .Cast<ListItem>()
                    .Where(item => item.Selected)
                    .Select(item => item.Value);

                return SerializationHelper.Serialize(settings);
            }

            set
            {
                EnsureItems();

                if (value == null)
                    return;

                var fields = SerializationHelper.Unserialize<List<string>>(value.ToString());
                if (fields == null)
                    return;

                foreach (ListItem item in list.Items)
                {
                    if (fields.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }

            }
        }

        private void EnsureItems()
        {
            if (list.Items.Count == 0)
            {
                var dataSource = new List<string>();
                dataSource.AddRange(ActionsHelper.Actions);
                dataSource.AddRange(UI.Helpers.ObjectHelper.GetSupportedObjectActions());
                var source = dataSource.Select(action => new KeyValuePair<string, string>(action, UI.Helpers.ObjectHelper.GetActionText(action))).OrderBy(a => a.Value);
                
                list.DataSource = source;
                list.DataValueField = "Key";
                list.DataTextField = "Value";
                list.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            EnsureItems();
        }
    }
}