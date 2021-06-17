<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleDeliveryForcast.aspx.vb" Inherits="MIS_HTI.SaleDeliveryForcast" %>
<%@ Register src="../UserControl/DateTextChange.ascx" tagname="DateTextChange" tagprefix="uc1" %>
<%@ Register src="../UserControl/DropDownListUserControl.ascx" tagname="DropDownListUserControl" tagprefix="uc2" %>
<%@ Register src="../UserControl/CheckBoxListUserControl.ascx" tagname="CheckBoxListUserControl" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .displayNone {
            display: none;
        }
         .auto-style3 {
             height: 43px;
         }
         .auto-style4 {
             width: 95%;
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
                        <asp:Label ID="Label3" runat="server" Text="Cust"></asp:Label>
                    </td>
                    <td>
                        <uc2:DropDownListUserControl ID="UcDdlCust" runat="server" />
                        <asp:Label ID="LbCust" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Plant"></asp:Label>
                    </td>
                    <td>
                        <uc2:DropDownListUserControl ID="UcDdlPlant" runat="server" />
                        <asp:Label ID="LbPlant" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Ship Date"></asp:Label>
                    </td>
                    <td>
                        <uc1:DateTextChange ID="UcDateShip" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Codition"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="cbStockFirst" runat="server" Text="Stock First" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Call In No"></asp:Label>
                    </td>
                    <td rowspan="2" style="vertical-align: top">
                        <uc3:CheckBoxListUserControl ID="UcCblCallinNo" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Sale Delivery Type"></asp:Label>
                    </td>
                    <td>
                        <uc2:DropDownListUserControl ID="UcDdlSaleDelType" runat="server" />
                        <asp:Label ID="LbDocType" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Sort By"></asp:Label>
                    </td>
                    <td>
                        <uc2:DropDownListUserControl ID="UcDdlSort" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="All Qty"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LbQtyAll" runat="server" ForeColor="Blue"></asp:Label>
                        <asp:Label ID="LbShipTime" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="All Amount"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LbAmtAll" runat="server" ForeColor="Blue"></asp:Label>
                        <asp:Label ID="LbDocNo" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table bgcolor="White" class="auto-style4">
                <tr>
                    <td style="width: 50%; border: thin dashed #808080; vertical-align: top;">
                        <asp:GridView ID="GvLeft" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sel">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CbSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MF001" HeaderText="No-Seq" />
                                <asp:BoundField DataField="MF005" HeaderText="Spec" />
                                <asp:BoundField DataField="CUST_WO" HeaderText="W/O" />
                                <asp:BoundField DataField="CUST_LINE" HeaderText="Line" />
                                <asp:BoundField DataField="MODEL" HeaderText="Model" />
                                <asp:BoundField DataField="MF008" HeaderText="Qty" DataFormatString="{0:#,#}" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MC007" HeaderText="Stock" DataFormatString="{0:#,#}" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SO_BAL" HeaderText="S/O Bal" DataFormatString="{0:#,#}" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TC036" HeaderText="Pending Stock" DataFormatString="{0:#,#}" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SHIP_TIME" HeaderText="Time" />
                                <asp:BoundField DataField="MF010" HeaderText="Unit" >
                                <HeaderStyle CssClass="displayNone" />
                                <ItemStyle CssClass="displayNone" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MF003" HeaderText="Item" >
                                <HeaderStyle CssClass="displayNone" />
                                <ItemStyle CssClass="displayNone" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MF004" HeaderText="Desc" >
                                <HeaderStyle CssClass="displayNone" />
                                <ItemStyle CssClass="displayNone" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <SortedAscendingCellStyle BackColor="#F4F4FD" />
                            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                            <SortedDescendingCellStyle BackColor="#D8D8F0" />
                            <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        </asp:GridView>
                    </td>
                    <td style="width: 50%; border: thin dashed #808080; vertical-align: top;">
                        <asp:GridView ID="GvRight" runat="server">
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table style="width: 95%;">
                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x" class="auto-style3">
                        <asp:Button ID="BtForcast" runat="server" Text="Call In" />
                        &nbsp;<asp:Button ID="BtSelect" runat="server" Text="Select &gt;&gt;" />
                        &nbsp;<asp:Button ID="BtSaleDelivery" runat="server" Text="Sale Delivery" />
                        &nbsp;<asp:Button ID="BtReset" runat="server" Text="&lt;&lt; Reset" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
