<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="UpdatePurchaseRequest.aspx.vb" Inherits="MIS_HTI.UpdatePurchaseRequest" %>
<%@ Register src="../UserControl/GridviewShow.ascx" tagname="GridviewShow" tagprefix="uc1" %>
<%@ Register src="../UserControl/docTypeD.ascx" tagname="docTypeD" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="PR TYPE"></asp:Label>
                    </td>
                    <td>
                        <uc2:docTypeD ID="ucPrType" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="PR NO"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbPrNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">&nbsp;<asp:Button ID="btUpdate" runat="server" Text="Update" />
                    </td>
                </tr>
            </table>
            <uc1:GridviewShow ID="ucGv" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
