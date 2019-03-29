<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AuditorLogViewer.ascx.cs" Inherits="Auditor.Controls.AuditorLogViewer" %>

<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>

<cms:UniGrid ID="uniGrid" runat="server" OnOnBeforeSorting="UniGrid_OnBeforeSorting">
</cms:UniGrid>

<asp:Label runat="server" ID="lblNoItems" Visible="false" />