<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="WorkNotActualDate.aspx.vb" Inherits="MIS_HTI.WorkNotActualDate" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:58%;">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Date From"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtFrom">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtTo_CalendarExtender" runat="server" Enabled="True" 
                            TargetControlID="txtTo">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Button ID="btnReport" runat="server" Text="Report" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
