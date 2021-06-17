<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="Label.aspx.vb" Inherits="MIS_HTI.Label1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .style6
    {
        width: 100%;
    }
    .style7
    {
    }
    .style8
    {
        width: 97px;
    }
    .style9
    {
        width: 136px;
    }
    .style10
    {
        width: 84px;
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
        <table class="style6">
            <tr>
                <td class="style7" colspan="2">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#CC0000" Text="Label Material Issue"></asp:Label>
                </td>
                <td class="style10">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style8">
                    <asp:Label ID="Label3" runat="server" Text=" Doc No :"></asp:Label>
                </td>
                <td class="style9">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td class="style10">
                    <asp:Label ID="Label6" runat="server" Text="Mat Code :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style8">
                    <asp:Label ID="Label5" runat="server" Text="Issue No :"></asp:Label>
                </td>
                <td class="style9">
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
                <td class="style10">
                    <asp:Label ID="Label8" runat="server" Text="Mat Name :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style8">
                    <asp:Label ID="Label7" runat="server" Text="Mfg Code :"></asp:Label>
                </td>
                <td class="style9">
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
                <td class="style10">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="BuSearch" runat="server" Text="Search" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1">
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
<br />
<br />
<br />
        <br />
        <br />
        <br />
        <br />
    </ContentTemplate>
</asp:UpdatePanel>
<p>
    <br />
</p>
</asp:Content>
