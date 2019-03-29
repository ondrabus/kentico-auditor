<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AuditLogPager.ascx.cs" Inherits="Auditor.Filters.AuditLogPager" %>

<div class="pagination">
    <ul class="pagination-list">

        <asp:Repeater ID="rptPager" runat="server">
            <HeaderTemplate>
                <li>
                    <asp:LinkButton ID="lnkLeft" runat="server" CommandArgument='<%# (CurrentPage - 1).ToString() %>' OnClick="lnkPage_Click">
                        <i class="icon-chevron-left" aria-hidden="true"></i>
                    </asp:LinkButton>
                </li>
                <%# DisplayLeftDots() %>
            </HeaderTemplate>

            <ItemTemplate>
                <li class='<%# (Eval("Value") != null && Eval("Value").ToString() == CurrentPage.ToString() ? "active" : "") %>'>
                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%# Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' OnClick="lnkPage_Click" />
                </li>            
            </ItemTemplate>

            <FooterTemplate>
                <%# DisplayRightDots() %>
                <li>
                    <asp:LinkButton ID="lnkRight" runat="server" CommandArgument='<%# (CurrentPage + 1).ToString() %>' OnClick="lnkPage_Click">
                        <i class="icon-chevron-right" aria-hidden="true"></i>
                    </asp:LinkButton>
                </li>
            </FooterTemplate>
        </asp:Repeater>
        
    </ul>

    <div class="pagination-items-per-page">
        <label for="drpPageSize">Items per page</label>

        <asp:DropDownList CssClass="form-control" ID="drpPageSize" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="drpPageSize_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Value="25" Text="25" Selected="True" />
        </asp:DropDownList>
    </div>
</div>