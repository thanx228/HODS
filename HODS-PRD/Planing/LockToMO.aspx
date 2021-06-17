<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="LockToMO.aspx.vb" Inherits="MIS_HTI.LockToMO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 76px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:50%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label3" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="Lock Plan To MO"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:50%;">
                <tr>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:Label ID="Label4" runat="server" Text="Src Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSrcType" runat="server" style="margin-bottom: 0px" 
                            Width="60px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Src No"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSrcNo" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Src Seq"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSrcSeq" runat="server" Width="60px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width:50%; background-image: url('../Images/btt.jpg'); background-repeat: no-repeat;">
                <tr>
                    <td align="center">
                        <asp:Button ID="btLock" runat="server" Text="Lock Plan" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
