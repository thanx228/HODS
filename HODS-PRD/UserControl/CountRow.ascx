<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CountRow.ascx.vb" Inherits="MIS_HTI.CountRow" %>
<style type="text/css">
    .style1
    {
        width: 282px;
    }
</style>
<table style="width:95%;">
    <tr>
        <td align="center" bgcolor="White" class="style1">
            <asp:Label ID="lbName" runat="server" Text="Amout of Rows"></asp:Label>
        </td>
        <td align="center" bgcolor="White">
            <asp:Label ID="lbCount" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
        </td>
        <td align="center" bgcolor="White">
            <asp:Label ID="Label3" runat="server" Text="Rows"></asp:Label>
        </td>
    </tr>
</table>

