<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="fgInventoryStatus.aspx.vb" Inherits="MIS_HTI.fgInventoryStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 122px;
        }
        .style7
        {
            width: 82px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label6" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="FG Inventory Status"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-color: #FFFFFF; vertical-align: top;" class="style7">
                        <asp:Label ID="Label3" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" colspan="3">
                        <asp:CheckBoxList ID="cblSaleType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style7">
                        <asp:Label ID="Label4" runat="server" Text="SO No."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSaleNo" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:Label ID="Label7" runat="server" Text="SO Seq."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSaleSeq" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style7">
                        <asp:Label ID="Label9" runat="server" Text="Cust ID"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCust" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:Label ID="Label10" runat="server" Text="Due Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDate" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style7">
                        <asp:Label ID="Label5" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:Label ID="Label8" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style7">
                        <asp:Label ID="Label11" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlCon" runat="server">
                            <asp:ListItem Value="0">All </asp:ListItem>
                            <asp:ListItem Value="1">Undelivery=0</asp:ListItem>
                            <asp:ListItem Value="2">Stock&gt;=Delivery</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:Label ID="Label14" runat="server" Text="Inventory Aging"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">1-90 Days</asp:ListItem>
                            <asp:ListItem Value="2">91-180 Days</asp:ListItem>
                            <asp:ListItem Value="3">181-270 Days</asp:ListItem>
                            <asp:ListItem Value="4">271-360 Days</asp:ListItem>
                            <asp:ListItem Value="5">&gt;361 Day</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-color: #FFFFFF" align="center">
                        <asp:Label ID="Label12" runat="server" Text="Amount of rows"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" align="center">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" align="center">
                        <asp:Label ID="Label13" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="item" HeaderText="Item" />
                    <asp:BoundField DataField="MB003" HeaderText="Spec" />
                    <asp:BoundField DataField="closeQty" DataFormatString="{0:n2}" 
                        HeaderText="Manual Close Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="changeQty" DataFormatString="{0:n2}" 
                        HeaderText="SO Change Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="delQty" DataFormatString="{0:n2}" 
                        HeaderText="Undelivery Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="delAmt" DataFormatString="{0:n2}" 
                        HeaderText="Undelivery Amt" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="invQty" DataFormatString="{0:n2}" 
                        HeaderText="Stock Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="invAmt" DataFormatString="{0:n2}" 
                        HeaderText="Stock Amt" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="qtyA" DataFormatString="{0:n2}" 
                        HeaderText="1-90 Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="amtA" DataFormatString="{0:n2}" 
                        HeaderText="1-90 Amt" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="qtyB" DataFormatString="{0:n2}" 
                        HeaderText="91-180 Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="amtB" DataFormatString="{0:n2}" 
                        HeaderText="91-180 Amt" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="qtyC" DataFormatString="{0:n2}" 
                        HeaderText="181-270 Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="amtC" DataFormatString="{0:n2}" 
                        HeaderText="181-270 Amt" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="qtyD" DataFormatString="{0:n2}" 
                        HeaderText="271-360 Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="amtD" DataFormatString="{0:n2}" 
                        HeaderText="271-360 Amt" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="qtyE" DataFormatString="{0:n2}" 
                        HeaderText=">360 Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="amtE" DataFormatString="{0:n2}" 
                        HeaderText=">360 Amt" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
    </asp:Content>
