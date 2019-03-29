using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.SiteProvider;
using CMS.UIControls;
using System;
using System.Linq;

namespace Auditor.Pages
{
    public partial class DocumentDetail : CMSDeskPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var nodeId = QueryHelper.GetInteger("nodeid", 0);
            var culture = QueryHelper.GetString("culture", string.Empty);
            if (nodeId <= 0 || string.IsNullOrEmpty(culture))
                return;

            var doc = DocumentHelper.GetDocuments()
                .Columns(
                    nameof(TreeNode.DocumentID),
                    nameof(TreeNode.DocumentGUID),
                    nameof(TreeNode.DocumentName),
                    nameof(TreeNode.NodeID),
                    nameof(TreeNode.NodeName))
                .WhereEquals(nameof(TreeNode.NodeID), nodeId)
                .WhereEquals(nameof(TreeNode.DocumentCulture), culture)
                .TopN(1)
                .FirstOrDefault();

            if (doc == null)
                return;

            var pageName = doc.DocumentName;
            if (string.IsNullOrEmpty(pageName))
                pageName = doc.NodeName;
            if (string.IsNullOrEmpty(pageName))
                pageName = SiteContext.CurrentSiteName;

            ltlDocumentName.Text = pageName;
            
            filter.ObjectGuid = doc.DocumentGUID;
            filter.SiteGuid = SiteContext.CurrentSite.SiteGUID;

            filter.DataSearch.Add("AliasNodeID", doc.NodeID.ToString());
            filter.DataSearch.Add("TaskDocumentID", doc.DocumentID.ToString());
            filter.DataSearch.Add("AttachmentDocumentID", doc.DocumentID.ToString());
        }
    }
}