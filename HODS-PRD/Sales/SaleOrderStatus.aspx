<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleOrderStatus.aspx.vb" Inherits="MIS_HTI.SaleOrderStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style8
        {
            width: 161px;
        }
        .style10
        {
            width: 148px;
        }
        .style11
        {
            width: 136px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 95%;">
                <tr>
                    <td style="background-color: #FFFFFF; vertical-align: top;" class="style8">
                        <asp:Label ID="Label2" runat="server" Text="Sale Order Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" colspan="3">
                        <asp:CheckBoxList ID="cblSaleType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style8">
                        <asp:Label ID="Label3" runat="server" Text="Sale Order Number"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style10">
                        <asp:TextBox ID="tbSaleOrderNo" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style11">
                        <asp:Label ID="Label4" runat="server" Text="Sale Order Seq"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSaleOrderSeq" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Customer"></asp:Label>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCust" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:Label ID="Label16" runat="server" Text="Plant"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="TbPlant" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label17" runat="server" Text="Cust W/O"></asp:Label>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:TextBox ID="TbCustWo" runat="server"></asp:TextBox>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:Label ID="Label18" runat="server" Text="Cust Line"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="TbCustLine" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label12" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style8">
                        <asp:Label ID="Label15" runat="server" Text="Begin Due Delivery Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style10">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF" class="style11">
                        <asp:Label ID="Label5" runat="server" Text="End Due Delivery Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF; vertical-align: top;">
                        <asp:Label ID="Label13" runat="server" Text="App Status"></asp:Label>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblApp" runat="server">
                            <asp:ListItem Value="Y">Y:Approved</asp:ListItem>
                            <asp:ListItem Value="N">N:Not Approved</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF; vertical-align: top;">
                        <asp:Label ID="Label6" runat="server" Text="SO Status"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblStatus" runat="server" RepeatColumns="2">
                            <asp:ListItem Value="N">N:Not Closed</asp:ListItem>
                            <asp:ListItem Value="Y">Y:Auto-Closed</asp:ListItem>
                            <asp:ListItem Value="y">y:Manual-Closed</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF; vertical-align: top;">
                        <asp:Label ID="Label19" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:CheckBox ID="CbOverDue" runat="server" Text="Over Due" />
                    </td>
                    <td class="style11" style="background-color: #FFFFFF; vertical-align: top;">&nbsp;</td>
                    <td style="background-color: #FFFFFF">&nbsp;</td>
                </tr>
            </table>
            <table style="width: 95%; background-image: url('../Images/btt.jpg'); background-repeat: repeat-x;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="ucCountRow" runat="server" />
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TC003" HeaderText="Date" />
                    <asp:BoundField DataField="TD001" HeaderText="SO" />
                    <asp:BoundField DataField="PLANT" HeaderText="Plant" />
                    <asp:BoundField DataField="TD004" HeaderText="Item" />
                    <asp:BoundField DataField="TD006" HeaderText="Spec" />
                    <asp:BoundField DataField="custPO" HeaderText="Cust P/O" />
                    <asp:BoundField DataField="CWO" HeaderText="Cust W/O" />
                    <asp:BoundField DataField="CLINE" HeaderText="Cust Line" />
                    <asp:BoundField DataField="TD008" DataFormatString="{0:N}" HeaderText="Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRICE" DataFormatString="{0:N3}" HeaderText="Price">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD009" DataFormatString="{0:N}" 
                        HeaderText="Delivery Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD0089" DataFormatString="{0:N}" 
                        HeaderText="Balance Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MC007" DataFormatString="{0:N}" 
                        HeaderText="Stock Qty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD013" HeaderText="Delivery Date" />
                    <asp:BoundField DataField="TD013_OVER" DataFormatString="{0:#,#}" HeaderText="Over Due(Days)">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TC004" HeaderText="Cust ID" />
                    <asp:BoundField DataField="MA002" HeaderText="Cust Name" />
                    <asp:BoundField DataField="TC012" HeaderText="Industry" />
                    <asp:BoundField DataField="MB025" HeaderText="Property" />
                    <asp:BoundField DataField="TD016" HeaderText="Closed" />
                    <asp:BoundField DataField="TC027" HeaderText="App" />
                    <asp:BoundField DataField="TD015" HeaderText="Sale Forcast" />
                    <asp:BoundField DataField="MO" HeaderText="MO" />
                    <asp:BoundField DataField="PR" HeaderText="PR" />
                    <asp:BoundField DataField="PO" HeaderText="PO" />
                    <asp:BoundField DataField="TA001" HeaderText="Plan Batch No." />
                    <asp:BoundField DataField="TA006" DataFormatString="{0:N0}" HeaderText="Plan Prod Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TA051" HeaderText="Release to MO" />
                    <asp:BoundField DataField="PROCESS_OUTS" HeaderText="Outsource" />
                    <asp:BoundField DataField="PROCESS_LIST" HeaderText="Process List" />
                    <asp:BoundField DataField="MAT_SPEC" HeaderText="Materail" />
                    <asp:BoundField DataField="MAT_BAL" DataFormatString="{0:N3}" HeaderText="Mat Bal Qty" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" 
                    Wrap="True" />
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
