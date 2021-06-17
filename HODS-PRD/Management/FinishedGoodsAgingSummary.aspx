<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="FinishedGoodsAgingSummary.aspx.vb" Inherits="MIS_HTI.FinishedGoodsAgingSummary" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table style="width: 65%;">
            <tr>
                <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Finished Goods Aging Inventory"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 65%;">
            <tr>
                <td style="background-color: #FFFFFF">
                    <asp:Label ID="Label3" runat="server" Text="Sale"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF">
                    <asp:DropDownList ID="ddlSale" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="background-color: #FFFFFF">
                    <asp:Label ID="Label4" runat="server" Text="Inv Date"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF">
                    <asp:TextBox ID="tbDate" runat="server" Width="80px"></asp:TextBox>
                    <asp:CalendarExtender ID="tbDate_CalendarExtender" runat="server" 
                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDate">
                    </asp:CalendarExtender>
                </td>
            </tr>
        </table>
        <table style="width: 65%;">
            <tr>
                <td align="center" 
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                    &nbsp;
                    <asp:Button ID="btExport" runat="server" Text="Excel Export" Width="100px" />
                </td>
            </tr>
        </table>
        <table style="width: 64%;">
            <tr>
                <td align="center" style="background-color: #FFFFFF">
                    <asp:Label ID="Label5" runat="server" Text="Number of Row"></asp:Label>
                </td>
                <td align="center" style="background-color: #FFFFFF">
                    <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" style="background-color: #FFFFFF">
                    <asp:Label ID="Label8" runat="server" Text="Row"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvShow" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btExport" />
    </Triggers>
</asp:UpdatePanel>
</asp:Content>
