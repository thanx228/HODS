<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleOrderTest2.aspx.vb" Inherits="MIS_HTI.SaleOrderTest2" %>
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
                    <td 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="SO QTY &lt;&gt;MO QTY"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="Desc."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtDesc" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Spec."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        Customer ID</td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtCus" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        Sale Type</td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSOtype" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        Sale No.</td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSONo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        Condition</td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlCon" runat="server">
                            <asp:ListItem Value="0">All  SO &lt;&gt; MO</asp:ListItem>
                            <asp:ListItem Value="1">SO &lt; MO</asp:ListItem>
                            <asp:ListItem Value="2">SO &gt; MO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        &nbsp;<asp:DropDownList ID="ddlFldDate" runat="server">
                            <asp:ListItem Value="TD013">Delivery Date</asp:ListItem>
                            <asp:ListItem Value="TC003">Order Date</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        Date To
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:60%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" 
                            style="height: 26px" />
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
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Customer" DataField="S01">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="SO Type" DataField="S02">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="SO No." DataField="S10" />
                    <asp:BoundField HeaderText="SO Seq." DataField="S11" />
                    <asp:BoundField DataField="S03" HeaderText="Item" />
                    <asp:BoundField DataField="S04" HeaderText="Desc." />
                    <asp:BoundField DataField="S05" HeaderText="Spec." />
                    <asp:BoundField HeaderText="Order Date" DataField="S06">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Delivery Date" DataField="S07">
                    </asp:BoundField>
                    <asp:BoundField DataField="S08" HeaderText="SO Qty"
                    DataFormatString="{0:#,##0.000}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="S09" HeaderText="MO Qty"
                    DataFormatString="{0:#,##0.000}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
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

