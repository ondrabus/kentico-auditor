﻿<%@ Page Language="C#" AutoEventWireup="true"  Codebehind="CustomTableDetail.aspx.cs" Inherits="Auditor.Pages.CustomTableDetail" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" %>
<%@ Register TagPrefix="adt" TagName="AuditorLogViewer" Src="~/CMSModules/AuditorModule/Controls/AuditorLogViewer.ascx" %>
<%@ Register TagPrefix="adt" TagName="AuditLogFilter" Src="~/CMSModules/AuditorModule/Filters/AuditLogFilter.ascx" %>
<%@ Register TagPrefix="adt" TagName="AuditLogPager" Src="~/CMSModules/AuditorModule/Filters/AuditLogPager.ascx" %>

<asp:Content ID="cnt" ContentPlaceHolderID="plcContent" runat="server">

    <h4>Logs for custom table '<asp:Literal ID="ltlTableName" runat="server" />'</h4>
     
    <div class="cms-bootstrap">
        <asp:Label runat="server" ID="lblInfo" EnableViewState="false" Visible="false" />
        
        <adt:AuditLogFilter ID="filter" runat="server" />
        <adt:AuditorLogViewer ID="viewer" runat="server" GridName="~/CMSModules/AuditorModule/Pages/CustomTableDetail.xml" />
        <adt:AuditLogPager ID="pager" runat="server" />
    </div>

</asp:Content>