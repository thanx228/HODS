<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleDeliveryStatusDN.aspx.vb" Inherits="MIS_HTI.SaleDeliveryStatusDN" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:59%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Sale Delivery Status (DN)"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="background-color: #FFFFFF">
                        SO Type</td>
                    <td style="background-color: #FFFFFF" colspan="3">
                        <asp:CheckBoxList ID="cblSaleType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        SO No.</td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSONo" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Cus. ID"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtCusCode" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="Due Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        Spec.</td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width:60%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" 
                            style="height: 26px" />
                        <asp:Button ID="Button1" runat="server" Text=" Print Report" />
                        <asp:Button ID="btExport" runat="server" Text="Excel Export" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width:60%;">
                <tr>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField HeaderText="SO Type -No." DataField="S01">
                    </asp:BoundField>
                    <asp:BoundField DataField="S12" HeaderText="Delv. Type-No." />
                    <asp:BoundField HeaderText="Delv. Date" DataField="S03">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Item" DataField="S04">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Spec." DataField="S05">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="FG Store" DataField="S06"
                    DataFormatString="{0:#,##0.000}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Delv. Qt'y" DataField="S07" 
                    DataFormatString="{0:#,##0.000}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="FG Bal." DataField="S11" 
                    DataFormatString="{0:#,##0.000}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="WH" DataField="S08">
                    </asp:BoundField>
                    <asp:BoundField DataField="S09" HeaderText="Store Bin"/>
                    <asp:BoundField HeaderText="Cus. Name" DataField="S10">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Actual Qt'y" />
                    <asp:BoundField HeaderText="Check By" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

