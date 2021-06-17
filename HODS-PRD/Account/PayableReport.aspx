<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PayableReport.aspx.vb" Inherits="MIS_HTI.PayableReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 161px;
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
                        <asp:Label ID="Label3" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="Payable Report"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%; background-color: #FFFFFF;">
                <tr>
                    <td class="style6" style="vertical-align: top">
                        <asp:Label ID="Label9" runat="server" Text="Date From"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style6" style="vertical-align: top">
                        <asp:Label ID="Label4" runat="server" Text="Supplier/VendorType"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:CheckBoxList ID="cblType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="style6" style="vertical-align: top">
                        <asp:Label ID="Label5" runat="server" Text="Supplier/Vendor Code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbSup" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Report Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReport" runat="server">
                            <asp:ListItem Selected="True" Value="1">Status</asp:ListItem>
                            <asp:ListItem Value="2">Detail</asp:ListItem>
                            <asp:ListItem Value="3">Summary</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btReport" runat="server" Text="Print Report" Width="100px" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>
