<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PONotCloseNew.aspx.vb" Inherits="MIS_HTI.PONotCloseNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 177px;
        }
        .style1
    {
        width: 282px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc3:HeaderForm ID="HeaderForm1" runat="server" />
            <table style="width:75%;" bgcolor="White">
                <tr>
                    <td class="style6" style="vertical-align: top">
                        <asp:Label ID="Label3" runat="server" Text="PO Type"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:CheckBoxList ID="cblPurType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label4" runat="server" Text="PO NO"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbPoNo" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Supplier"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbSup" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style6" style="vertical-align: top">
                        <asp:Label ID="Label2" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:CheckBoxList ID="cblSaleType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label5" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:DropDownList ID="ddlDate" runat="server">
                            <asp:ListItem Value="TD014">Confirm Del Date</asp:ListItem>
                            <asp:ListItem Value="TC003">PO Date</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;<asp:Label ID="Label8" runat="server" Text="From"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label10" runat="server" Text="Buyer"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbBuyer" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width:75%; background-image: url('../Images/btt.jpg'); background-repeat: no-repeat;">
                <tr>
                    <td align="center">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="CountRow1" runat="server" />
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
