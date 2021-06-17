<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleInvoiceCustPO.aspx.vb" Inherits="MIS_HTI.SaleInvoiceCustPO" %>
<%@ Register src="../UserControl/DropDownListUserControl.ascx" tagname="DropDownListUserControl" tagprefix="uc1" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<%@ Register src="../UserControl/DateTextChange.ascx" tagname="DateTextChange" tagprefix="uc3" %>
<%@ Register src="../UserControl/CheckBoxListUserControl.ascx" tagname="CheckBoxListUserControl" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            height: 30px;
        }
         .displayNone {
            display: none;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table bgcolor="White" style="width: 95%;">
                <tr>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="Customer"></asp:Label>
                    </td>
                    <td>
                        <uc1:DropDownListUserControl ID="UcDdlCust" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Plant"></asp:Label>
                    </td>
                    <td>
                        <uc1:DropDownListUserControl ID="UcDdlPlant" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label16" runat="server" Text="Ship/Invoice Date"></asp:Label>
                    </td>
                    <td>
                        <uc3:DateTextChange ID="UcShipDate" runat="server" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3" style="vertical-align: top">
                        <asp:Label ID="Label15" runat="server" Text="Sale Delivery No"></asp:Label>
                    </td>
                    <td class="auto-style3" style="vertical-align: top">
                        <uc4:CheckBoxListUserControl ID="UcCblSaleDel" runat="server" />
                    </td>
                    <td class="auto-style3" style="vertical-align: top">
                        <asp:Label ID="Label5" runat="server" Text="Sale Invoice No"></asp:Label>
                    </td>
                    <td class="auto-style3" style="vertical-align: top">
                        <uc1:DropDownListUserControl ID="UcDdlSaleInvoice" runat="server" />
                        <asp:Label ID="LbSaleInvoiceType" runat="server"></asp:Label>
                        <asp:Label ID="LbSaleInvoiceNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="border: thin dotted #000000; vertical-align: top; text-align: left;width:50%">
                        <asp:GridView ID="GvLeft" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TH001" HeaderText="Type" />
                                <asp:BoundField DataField="TH002" HeaderText="No" />
                                <asp:BoundField HeaderText="Seq" DataField="TH003" />
                                <asp:BoundField HeaderText="Spec" DataField="TH006" />
                                <asp:BoundField HeaderText="Qty" DataField="TH008" DataFormatString="{0:N0}" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Unit" DataField="TH009" />
                                <asp:BoundField HeaderText="Price" DataField="TH012" DataFormatString=" {0:N5}" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="W/H" DataField="TH007" />
                                <asp:BoundField HeaderText="PO Bal" DataField="PO_BAL" DataFormatString="{0:N0}" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TH004" HeaderText="Item" >
                                <HeaderStyle CssClass="" />
                                <ItemStyle CssClass="" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TH005" HeaderText="Desc" >
                                <HeaderStyle CssClass="displayNone" />
                                <ItemStyle CssClass="displayNone" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MA005" HeaderText="AC Code" >
                                <HeaderStyle CssClass="displayNone" />
                                <ItemStyle CssClass="displayNone" />
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                    <td colspan="2" style="border: thin dotted #000000; vertical-align: top; text-align: left;width:50%">
                        <asp:GridView ID="GvRight" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </td>
                </tr>          
            </table>
            <table style="width: 95%;">
                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="BtSaleDel" runat="server" Text="Sale Delivery" />
                        &nbsp;<asp:Button ID="BtSelect" runat="server" Text="Select &gt;&gt;&gt;" />
                        &nbsp;<asp:Button ID="BtSave" runat="server" Text="Save Invoice" />
                        &nbsp;<asp:Button ID="BtReset" runat="server" Text="&lt;&lt;&lt; Reset" />
                        &nbsp;<asp:Button ID="BtACRLB" runat="server" Text="ACRLB" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
