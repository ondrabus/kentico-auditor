<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogItemDetail.aspx.cs" Inherits="Auditor.Pages.LogItemDetail"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Theme="Default" %>
<asp:Content ID="header" runat="server" ContentPlaceHolderID="plcHeaderTabs">

</asp:Content>

<asp:Content ID="content" runat="server" ContentPlaceHolderID="plcContent">
    <h3><asp:Label CssClass="form-control-text" ID="lblActionText" runat="server" EnableViewState="false" /></h3>

    <div class="form-horizontal">
        <div class="form-group">
            <div class="editing-form-label-cell">
                <cms:LocalizedLabel CssClass="control-label" ID="lblLogItemGuid" runat="server" ResourceString="Auditor.LogItemGuid" EnableViewState="false" />
            </div>
            <div class="editing-form-value-cell">
                <asp:Label CssClass="form-control-text" ID="lblLogItemGuidText" runat="server" EnableViewState="false" /> 
            </div>
        </div>

        <div class="form-group">
            <div class="editing-form-label-cell">
                <cms:LocalizedLabel CssClass="control-label" ID="lblSite" runat="server" ResourceString="Auditor.SiteInfo" EnableViewState="false" />
            </div>
            <div class="editing-form-value-cell">
                <asp:Label CssClass="form-control-text" ID="lblSiteText" runat="server" EnableViewState="false" /> 
            </div>
        </div>

        <div class="form-group">
            <div class="editing-form-label-cell">
                <cms:LocalizedLabel CssClass="control-label" ID="lblUser" runat="server" ResourceString="Auditor.UserInfo" EnableViewState="false" />
            </div>
            <div class="editing-form-value-cell">
                <asp:Label CssClass="form-control-text" ID="lblUserText" runat="server" EnableViewState="false" /> 
            </div>
        </div>


        <div class="form-group">
            <div class="editing-form-label-cell">
                <cms:LocalizedLabel CssClass="control-label" ID="lblDate" runat="server" ResourceString="Auditor.Date" EnableViewState="false" />
            </div>
            <div class="editing-form-value-cell">
                <asp:Label CssClass="form-control-text" ID="lblDateText" runat="server" EnableViewState="false" /> 
            </div>
        </div>

        <div class="form-group">
            <div class="editing-form-label-cell">
                <cms:LocalizedLabel CssClass="control-label" ID="lblObjectType" runat="server" ResourceString="Auditor.ObjectType" EnableViewState="false" />
            </div>
            <div class="editing-form-value-cell">
                <asp:Label CssClass="form-control-text" ID="lblObjectTypeText" runat="server" EnableViewState="false" /> 
            </div>
        </div>
        <div class="form-group">
            <div class="editing-form-label-cell">
                <cms:LocalizedLabel CssClass="control-label" ID="lblObjectName" runat="server" ResourceString="Auditor.ObjectName" EnableViewState="false" />
            </div>
            <div class="editing-form-value-cell">
                <asp:Label CssClass="form-control-text" ID="lblObjectNameText" runat="server" EnableViewState="false" /> 
            </div>
        </div>
        <asp:Panel ID="pnlObjectGuid" runat="server" Visible="false">
            <div class="form-group">
                <div class="editing-form-label-cell">
                    <cms:LocalizedLabel CssClass="control-label" ID="lblObjectGuid" runat="server" ResourceString="Auditor.ObjectGuid" EnableViewState="false" />
                </div>
                <div class="editing-form-value-cell">
                    <asp:Label CssClass="form-control-text" ID="lblObjectGuidText" runat="server" EnableViewState="false" /> 
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAssignmentType" runat="server" Visible="false">
            <div class="form-group">
                <div class="editing-form-label-cell">
                    <cms:LocalizedLabel CssClass="control-label" ID="lblFirstObjectType" runat="server" ResourceString="Auditor.FirstObjectType" EnableViewState="false" />
                </div>
                <div class="editing-form-value-cell">
                    <asp:Label CssClass="form-control-text" ID="lblFirstObjectTypeText" runat="server" EnableViewState="false" /> 
                </div>
            </div>
            <div class="form-group">
                <div class="editing-form-label-cell">
                    <cms:LocalizedLabel CssClass="control-label" ID="lblFirstObjectName" runat="server" ResourceString="Auditor.FirstObjectName" EnableViewState="false" />
                </div>
                <div class="editing-form-value-cell">
                    <asp:Label CssClass="form-control-text" ID="lblFirstObjectNameText" runat="server" EnableViewState="false" /> 
                </div>
            </div>
            <div class="form-group">
                <div class="editing-form-label-cell">
                    <cms:LocalizedLabel CssClass="control-label" ID="lblFirstObjectGuid" runat="server" ResourceString="Auditor.FirstObjectGuid" EnableViewState="false" />
                </div>
                <div class="editing-form-value-cell">
                    <asp:Label CssClass="form-control-text" ID="lblFirstObjectGuidText" runat="server" EnableViewState="false" /> 
                </div>
            </div>


            <div class="form-group">
                <div class="editing-form-label-cell">
                    <cms:LocalizedLabel CssClass="control-label" ID="lblSecondObjectType" runat="server" ResourceString="Auditor.SecondObjectType" EnableViewState="false" />
                </div>
                <div class="editing-form-value-cell">
                    <asp:Label CssClass="form-control-text" ID="lblSecondObjectTypeText" runat="server" EnableViewState="false" /> 
                </div>
            </div>
            <div class="form-group">
                <div class="editing-form-label-cell">
                    <cms:LocalizedLabel CssClass="control-label" ID="lblSecondObjectName" runat="server" ResourceString="Auditor.SecondObjectName" EnableViewState="false" />
                </div>
                <div class="editing-form-value-cell">
                    <asp:Label CssClass="form-control-text" ID="lblSecondObjectNameText" runat="server" EnableViewState="false" /> 
                </div>
            </div>
            <div class="form-group">
                <div class="editing-form-label-cell">
                    <cms:LocalizedLabel CssClass="control-label" ID="lblSecondObjectGuid" runat="server" ResourceString="Auditor.SecondObjectGuid" EnableViewState="false" />
                </div>
                <div class="editing-form-value-cell">
                    <asp:Label CssClass="form-control-text" ID="lblSecondObjectGuidText" runat="server" EnableViewState="false" /> 
                </div>
            </div>
        </asp:Panel>

        
        <h3>Data</h3>
        <cms:LocalizedLabel ID="lblNoData" runat="server" ResourceString="Auditor.NoAdditionalData" /> 
        <asp:Repeater ID="rptData" runat="server">
            <ItemTemplate>
                <div class="form-group">
                    <div class="editing-form-label-cell">
                        <asp:Label CssClass="control-label" ID="lbl" runat="server" EnableViewState="false" Text='<%# Eval("Name") %>' />
                    </div>
                    <div class="editing-form-value-cell">
                        <asp:Panel ID="pnlUpdatedValue" runat="server" Visible='<%# IsUpdateAction() %>'>
                            <asp:Label CssClass="form-control-text" ID="lblTextOld" runat="server" EnableViewState="false" Text='<%# System.Net.WebUtility.HtmlEncode(Eval("OldValue")?.ToString()) %>' Visible='<%# !string.IsNullOrEmpty(Eval("OldValue")?.ToString()) %>' />
                            <asp:Label CssClass="form-control-text" ID="lblTextOldEmpty" runat="server" EnableViewState="false" Text="(no value)" Visible='<%# Eval("OldValue") != null && string.IsNullOrEmpty(Eval("OldValue").ToString()) %>' />
                            <asp:Placeholder ID="pnlSeparator" runat="server" Visible='<%# Eval("OldValue") != null %>' EnableViewState="false">
                                -&gt; <br />
                            </asp:Placeholder>
                        
                            <strong>
                                <asp:Label CssClass="form-control-text" ID="lblTextNew" runat="server" EnableViewState="false" Text='<%# System.Net.WebUtility.HtmlEncode(Eval("Value")?.ToString()) %>' Visible='<%# !string.IsNullOrEmpty(Eval("Value")?.ToString()) %>' /> 
                                <asp:Label CssClass="form-control-text" ID="lblTextNewEmpty" runat="server" EnableViewState="false" Text="(no value)" Visible='<%# string.IsNullOrEmpty(Eval("Value")?.ToString()) %>' />
                            </strong>
                        </asp:Panel>
                        <asp:Panel ID="pnlSingleValue" runat="server" Visible='<%# !IsUpdateAction() %>'>
                            <asp:Label CssClass="form-control-text" ID="lblText" runat="server" EnableViewState="false" Text='<%# System.Net.WebUtility.HtmlEncode(Eval("Value")?.ToString()) %>' Visible='<%# !string.IsNullOrEmpty(Eval("Value")?.ToString()) %>' /> 
                            <asp:Label CssClass="form-control-text" ID="lblTextEmpty" runat="server" EnableViewState="false" Text="(no value)" Visible='<%# string.IsNullOrEmpty(Eval("Value")?.ToString()) %>' />
                        </asp:Panel>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
</asp:Content>