<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="VoucherList.aspx.vb" Inherits="MIS_HTI.VoucherList" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                            Text="Voucher List"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label4" runat="server" Text="Voucher Type"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label6" runat="server" Text="Account Code"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbAcCode" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label5" runat="server" Text="Voucher Date From"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label7" runat="server" Text="Voucher Date To"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        &nbsp;<asp:Button ID="btPrintDet" runat="server" Text="Print Detail Report" />
                        <asp:Button ID="btPrintSum" runat="server" Text="Print Summary Report" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
