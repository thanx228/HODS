<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="CheckPrice.aspx.vb" Inherits="MIS_HTI.CheckPrice" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<%@ Register src="../UserControl/GridviewShow.ascx" tagname="GridviewShow" tagprefix="uc2" %>
<%@ Register src="../UserControl/DateTextChange.ascx" tagname="DateTextChange" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table bgcolor="White" style="width: 95%;">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Cust Code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbCustCode" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Cust Name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbCustName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbItem" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Effect Date From"></asp:Label>
                    </td>
                    <td>
                        <uc1:Date ID="UcDateEffFrom" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Effect Date From"></asp:Label>
                    </td>
                    <td>
                        <uc1:Date ID="UcDateEffTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Expire Date From"></asp:Label>
                    </td>
                    <td>
                        <uc1:Date ID="UcDateExpireFrom" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Expire Date To"></asp:Label>
                    </td>
                    <td>
                        <uc1:Date ID="UcDateExpireTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="CbNotExpire" runat="server" Text="Not Expire" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table style="width: 95%;">
                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="BtShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="BtExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>
            <uc2:GridviewShow ID="UcGv" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
