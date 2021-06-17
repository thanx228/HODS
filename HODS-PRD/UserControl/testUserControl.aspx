<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="testUserControl.aspx.vb" Inherits="MIS_HTI.testUserControl" %>
<%@ Register src="Date.ascx" tagname="Date" tagprefix="uc2" %>
<%@ Register src="HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="CountRow.ascx" tagname="CountRow" tagprefix="uc6" %>
<%@ Register src="docTypeC.ascx" tagname="docTypeC" tagprefix="uc7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style7
        {
            width: 110px;
        }
        .style9
        {
        }
        .style10
        {
            width: 40px;
        }
        .style11
        {
            width: 110px;
            height: 30px;
        }
        .style12
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc3:HeaderForm ID="HeaderForm1" runat="server" />
            <table style="width: 75%;">
                <tr>
                    <td class="style7" bgcolor="White">
                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="style9" bgcolor="White">
                        <uc2:Date ID="Date1" runat="server" />
                    </td>
                    <td class="style10" bgcolor="White">
                        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <uc2:Date ID="Date2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="style11" bgcolor="White">
                        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="style12" colspan="3" bgcolor="White">
                        <uc7:docTypeC ID="docTypeC1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="style7" bgcolor="White">
                        <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="style9" bgcolor="White">
                        &nbsp;</td>
                    <td class="style10" bgcolor="White">
                        &nbsp;</td>
                    <td bgcolor="White">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="Button1" runat="server" Text="Button" />
                    </td>
                </tr>
            </table>
            <uc6:CountRow ID="CountRow1" runat="server" />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
