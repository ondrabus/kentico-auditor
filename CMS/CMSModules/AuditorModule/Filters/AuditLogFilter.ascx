<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AuditLogFilter.ascx.cs" Inherits="Auditor.Filters.AuditLogFilter" %>

<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>

<div class="form-horizontal">
    <asp:Panel ID="pnlSiteFilter" CssClass="form-group" runat="server"> 
        <div class="editing-form-label-cell">
            <cms:LocalizedLabel CssClass="control-label" ID="lblSiteFilterName" runat="server" ResourceString="general.site" EnableViewState="false" DisplayColon="true" />
        </div>
        <div class="editing-form-value-cell">
            
            <cms:CMSUpdatePanel ID="upSite" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <cms:UniSelector ID="usSite" ShortID="ss" runat="server" ObjectType="cms.site" ReturnColumnName="SiteGUID" ResourcePrefix="siteselect" SelectionMode="SingleDropDownList" AllowEmpty="True" DisplayNameFormat="{%SiteDisplayName%}" OrderBy="SiteDisplayName" AllowAll="True" />
                </ContentTemplate>
            </cms:CMSUpdatePanel>

        </div>
    </asp:Panel>

    <asp:Panel CssClass="form-group" runat="server" ID="pnlUserFilter">
        <div class="editing-form-label-cell">
            <cms:LocalizedLabel CssClass="control-label" ID="lblUserFilterName" runat="server" ResourceString="general.user" EnableViewState="false" DisplayColon="true" />
        </div>
        <div class="editing-form-value-cell">
            <cms:CMSUpdatePanel ID="upUser" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                     <cms:UniSelector ID="usUser" ShortID="us" runat="server" ObjectType="cms.user" ReturnColumnName="UserGUID" SelectionMode="SingleTextBox" AllowEmpty="True" OrderBy="UserName" DisplayNameFormat="{%UserName%}" AllowAll="True" />
                </ContentTemplate>
            </cms:CMSUpdatePanel>
        </div>
    </asp:Panel>

    <div class="form-group">
        <div class="editing-form-label-cell">
            <cms:LocalizedLabel CssClass="control-label" ID="lblTimeBetween" runat="server" ResourceString="eventlog.timebetween"
                DisplayColon="true" />
        </div>
        <div class="editing-form-value-cell">
            <div class="control-group-inline">
                <cms:DateTimePicker ID="dtmTimeFrom" runat="server" />
                <cms:LocalizedLabel ID="lblTimeBetweenAnd" runat="server" ResourceString="eventlog.timebetweenand" CssClass="form-control-text" />
            </div>
        </div>
        <div class="editing-form-label-cell"></div>
        <div class="editing-form-value-cell">
            <div class="control-group-inline">
                <cms:DateTimePicker ID="dtmTimeTo" runat="server" />
            </div>
        </div>
    </div>
    
    <asp:Panel ID="pnlObjectName" CssClass="form-group" runat="server">
        <div class="editing-form-label-cell">
            <cms:LocalizedLabel CssClass="control-label" ID="lblObjectName" runat="server" ResourceString="Auditor.ObjectName" EnableViewState="false" DisplayColon="true" />
        </div>
        <div class="editing-form-value-cell">
             <asp:TextBox ID="txtObjectName" runat="server" CssClass="form-control" />
        </div>
    </asp:Panel>
    
    <asp:Panel ID="pnlSecondObjectName" CssClass="form-group" runat="server">
        <div class="editing-form-label-cell">
            <cms:LocalizedLabel CssClass="control-label" ID="lblSecondObjectName" runat="server" ResourceString="Auditor.SecondObjectName" EnableViewState="false" DisplayColon="true" />
        </div>
        <div class="editing-form-value-cell">
             <asp:TextBox ID="txtSecondObjectName" runat="server" CssClass="form-control" />
        </div>
    </asp:Panel>

    <div class="form-group">
        <div class="editing-form-label-cell">
            <cms:LocalizedLabel CssClass="control-label" ID="lblSearchText" runat="server" ResourceString="Auditor.SearchText" EnableViewState="false" DisplayColon="true" />
        </div>
        <div class="editing-form-value-cell">
             <asp:TextBox ID="txtSearchBox" runat="server" CssClass="form-control" />
        </div>
    </div>
    
    <div class="form-group form-group-buttons">
        <div class="editing-form-label-cell"></div>
        <div class="editing-form-buttons-cell-wide-with-link">
            <cms:LocalizedButton ID="btnReset" runat="server" ButtonStyle="Default" ResourceString="general.reset" EnableViewState="false" OnClick="btnReset_Click" />
            <cms:LocalizedButton ID="btnSearch" runat="server" ButtonStyle="Primary" ResourceString="general.filter" OnClick="btnSearch_Click" />
        </div>
    </div>
</div>

