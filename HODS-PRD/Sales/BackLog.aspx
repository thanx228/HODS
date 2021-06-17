<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="BackLog.aspx.vb" Inherits="MIS_HTI.BackLog" %>
<%@ Register src="../UserControl/GridviewShow.ascx" tagname="GridviewShow" tagprefix="uc1" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
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
                        <asp:Label ID="Label3" runat="server" Text="Ship Date From"></asp:Label>
                    </td>
                    <td>
                        <uc2:Date ID="UcDateFrom" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Ship Date  To"></asp:Label>
                    </td>
                    <td>
                        <uc2:Date ID="UcDateTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbItem" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Cust Code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbCustCode" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Cust Name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbCustName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Plant"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbPlant" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="CbCallIn" runat="server" Checked="True" Text="Call In" />
                        <asp:CheckBox ID="CbBackLog" runat="server" Text="Back log" />
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width: 95%;">
                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg')">
                        <asp:Button ID="BtShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="BtExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>
            <uc1:GridviewShow ID="UcGv" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
