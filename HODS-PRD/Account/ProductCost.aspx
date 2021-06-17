<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ProductCost.aspx.vb" Inherits="MIS_HTI.ProductCost" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label10" runat="server" Text="Product Cost" Font-Size="1.1em" 
                            ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label13" runat="server" Text="Inventory Catagory"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlItemType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Desc"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDesc" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Doc Type"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlDocType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Doc No"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDocNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="Date From"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('http://localhost:57736/Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" style="height: 26px" Text="Show Report" 
                            Width="100px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="Amount of rows"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label12" runat="server" Text="rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="LA004" HeaderText="Doc Date" />
                    <asp:BoundField DataField="LA001" HeaderText="Item" />
                    <asp:BoundField DataField="MB002" HeaderText="Item Desc" />
                    <asp:BoundField DataField="MB003" HeaderText="Spec." />
                    <asp:BoundField DataField="MB004" HeaderText="Unit" />
                    <asp:BoundField DataField="LA006" HeaderText="Order No" />
                    <asp:BoundField DataField="LA011" DataFormatString="{0:n2}" 
                        HeaderText="Reciept Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LA012" DataFormatString="{0:n2}" 
                        HeaderText="Unit Cost">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LA017" DataFormatString="{0:n2}" 
                        HeaderText="Materials Amt">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LA018" DataFormatString="{0:n2}" 
                        HeaderText="Labour Amt">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LA019" DataFormatString="{0:n2}" 
                        HeaderText="Factory Overhead Cost Amt">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LA020" DataFormatString="{0:n2}" 
                        HeaderText="Subcontract Amt">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LA0171" DataFormatString="{0:n2}" 
                        HeaderText="Materials Amt/Unit">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LA0181" DataFormatString="{0:n2}" 
                        HeaderText="Labour Amt/Unit">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LA0191" DataFormatString="{0:n2}" 
                        HeaderText="Factory Overhead Cost Amt /Unit">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LA0201" DataFormatString="{0:n2}" 
                        HeaderText="Subcontract Amt/Unit">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LA024" HeaderText="Partner" />
                    <asp:BoundField DataField="LA009" HeaderText="Warehouse" />
                    <asp:BoundField DataField="LA010" HeaderText="Remark" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    HorizontalAlign="Center" />
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
