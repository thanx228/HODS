<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleNotPlan.aspx.vb" Inherits="MIS_HTI.SaleNotPlan" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 134px;
        }
        .style6
        {
        }
        .style7
        {
            width: 433px;
        }
        .style8
        {
            width: 130px;
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 75%;">
                <tr>
                    <td class="style7" align="left" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label10" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="Sale Not Aprove"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="Show Type"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlShowType" runat="server">
                            <asp:ListItem Selected="True" Value="0">Sale Not Plan</asp:ListItem>
                            <asp:ListItem Value="1">Over Due Date</asp:ListItem>
                            <asp:ListItem Value="2">MO Not Aprove</asp:ListItem>
                            <asp:ListItem Value="3">Not Print LOT</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="lblShowText" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="Code Type"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlCodeType" runat="server">
                            <asp:ListItem Selected="True" Value="0">NCT</asp:ListItem>
                            <asp:ListItem Value="1">MOLD</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Customer ID"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtCustCode" runat="server" Height="19px" MaxLength="4" 
                            Width="57px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label1" runat="server" Text="Sale Order Type"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF" colspan="3">
                        <asp:CheckBoxList ID="cblSaleType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Sale order No."></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSaleNo" runat="server" MaxLength="11"></asp:TextBox>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Date Type"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlDateType" runat="server">
                            <asp:ListItem Selected="True" Value="0">Sale Order Date</asp:ListItem>
                            <asp:ListItem Value="1">Sale Order Due Date</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Part No"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtPartNo" runat="server" MaxLength="16"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Sale Date From"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtDateFrom">
                        </cc1:CalendarExtender>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Sale Date To"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtDateTo">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show" Width="100px" />
                        <asp:Button ID="btExport" runat="server" Text="Export" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;" >
                <tr>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="lblShowText0" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="lblList" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="lblShowText1" runat="server" Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
