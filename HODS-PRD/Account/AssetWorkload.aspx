<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="AssetWorkload.aspx.vb" Inherits="MIS_HTI.AssetWorkload" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px;
        }
        .auto-style1 {
            width: 611px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:HeaderForm ID="ucHeader" runat="server" />
            <table style="width: 75%;">
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label3" runat="server" Text="Start Date"></asp:Label>
                    </td>
                    <td bgcolor="White" class="auto-style1">
                        <uc2:Date ID="ucDate" runat="server" />
                    </td>
                    <td bgcolor="White">
                        &nbsp;</td>
                    <td bgcolor="White">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" class="style6" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btCal" runat="server" Text="Calculate" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
