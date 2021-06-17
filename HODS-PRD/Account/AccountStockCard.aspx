<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="AccountStockCard.aspx.vb" Inherits="MIS_HTI.AccountStockCard" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 104px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label3" runat="server" Text="Account Stock Card" 
                            Font-Size="1.1em" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:Label ID="Label4" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; vertical-align: top;" class="style6">
                        <asp:Label ID="Label9" runat="server" Text="Code Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" colspan="3">
                        <asp:CheckBoxList ID="cblCodeType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="style6" style="background-color: #FFFFFF; vertical-align: top;">
                        <asp:Label ID="Label6" runat="server" Text="Warehouse"></asp:Label>
                    </td>
                    <td colspan="3" style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblWh" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:Label ID="Label7" runat="server" Text="Date From"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Group By "></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlGrpBy" runat="server">
                            <asp:ListItem Value="1">Warehouse</asp:ListItem>
                            <asp:ListItem Value="2">Account Code</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        align="center">
                        <asp:Button ID="btDetail" runat="server" Text="Detail" Width="100px" />
                        &nbsp;<asp:Button ID="btSum" runat="server" Text="Summary" Width="100px" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
