<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS_SUB.Master" CodeBehind="CustPOReportPopup.aspx.vb" Inherits="MIS_HTI.CustPOReportPopup" %>
<%@ Register src="../UserControl/GridviewShow.ascx" tagname="GridviewShow" tagprefix="uc1" %>
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
                        <asp:Label ID="Label3" runat="server" Text="PO"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LbPo" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Cust"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LbCust" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Plant"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LbPlant" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LbItem" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LbSpec" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="Qty"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LbQty" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="Void Qty"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LbQtyVoid" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label16" runat="server" Text="Invoice Qty"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LbQtyInvoice" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server" Text="Balance"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LbQtyBal" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 95%;">
                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="BtShow" runat="server" Text="Show Again" />
                        &nbsp;<asp:Button ID="BtExport" runat="server" Text="Export Excel" />
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
