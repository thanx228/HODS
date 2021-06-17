<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="updateSale.aspx.vb" Inherits="MIS_HTI.updateSale" %>
<%@ Register src="../UserControl/DropDownListUserControl.ascx" tagname="DropDownListUserControl" tagprefix="uc1" %>
<%@ Register src="../UserControl/GridviewShow.ascx" tagname="GridviewShow" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="95%">
                <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        Sale Order
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Type"></asp:Label>
                                </td>
                                <td>
                                    <uc1:DropDownListUserControl ID="UcDdlSaleOrderType" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Number"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TbSaleOrderNumber" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Seq"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TbSaleOrderSeq" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="BtCheck" runat="server" Text="Check" />
                                </td>
                                <td>
                                    <asp:Label ID="LbSaleType" runat="server"></asp:Label>
                                    <asp:Label ID="LbSaleOrder" runat="server"></asp:Label>
                                    <asp:Label ID="LbSaleSeq" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="Label17" runat="server" Text="Item"></asp:Label>
                                </td>
                                <td class="auto-style3">
                                    <asp:Label ID="LbItem" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                                <td class="auto-style3">
                                    <asp:Label ID="sss" runat="server" Text="Desc"></asp:Label>
                                </td>
                                <td class="auto-style3">
                                    <asp:Label ID="LbSpec" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label21" runat="server" Text="Qty"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LbQty" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="Label6" runat="server" Text="Cust WO"></asp:Label>
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TbWo" runat="server"></asp:TextBox>
                                    <asp:Label ID="LbOldWo" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style3">
                                    <asp:Label ID="Label8" runat="server" Text="Cust Line"></asp:Label>
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TbLine" runat="server"></asp:TextBox>
                                    <asp:Label ID="LbOldLine" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="Model"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TBModel" runat="server"></asp:TextBox>
                                    <asp:Label ID="LbOldModel" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    <HeaderTemplate>
                        Sale Delivery
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="Label15" runat="server" Text="Type"></asp:Label>
                                </td>
                                <td>
                                    <uc1:DropDownListUserControl ID="UcDdlSaleDelType" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="Label16" runat="server" Text="Number"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TbSaleDelNumber" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
            <table style="width: 95%;">
                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="BtUpdate" runat="server" Text="Update" />
                        &nbsp;<asp:Button ID="BtHistory" runat="server" Text="History" />
                    </td>
                </tr>
            </table>
            <uc2:GridviewShow ID="UcGv" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
