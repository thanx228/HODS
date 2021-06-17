<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MoActualDate.aspx.vb" Inherits="MIS.MoActualDate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 118%;">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Start Date From"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Start Date TO"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnReport" runat="server" Text="Report" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
