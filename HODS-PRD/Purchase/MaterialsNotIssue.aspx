<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MaterialsNotIssue.aspx.vb" Inherits="MIS_HTI.MaterialsNotIssue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 60%;">
                <tr>
                    <td align="left" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Materials Not Issue Status"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Materials Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlCodeType" runat="server">
                            <asp:ListItem Selected="True" Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1">Materials</asp:ListItem>
                            <asp:ListItem Value="4">Spare Part</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; vertical-align: top;">
                        <asp:Label ID="Label4" runat="server" Text="MO type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF; vertical-align: top;" colspan="3">
                        <asp:CheckBoxList ID="clWorkType" runat="server" ForeColor="Blue">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="MO No."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbWorkNo" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="Codition"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlCondition" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Not Issue</asp:ListItem>
                            <asp:ListItem Value="2">Issue &lt; Require</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Plan Start Date From"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="Plan Start Date To"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 60%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;
                        <asp:Button ID="btExport" runat="server" Text="Excel Export" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width: 60%;">
                <tr>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label12" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label13" runat="server" Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="TA001" HeaderText="MO" />
                    <asp:BoundField DataField="TA009" HeaderText="Plan Start Date" />
                    <asp:BoundField DataField="TA035" HeaderText="Item Spec" />
                    <asp:BoundField DataField="TB003" HeaderText="Mat Item" />
                    <asp:BoundField DataField="TB012" HeaderText="Mat Spec" />
                    <asp:BoundField DataField="TB004" DataFormatString="{0:n2}" 
                        HeaderText="Request Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TB005" DataFormatString="{0:n2}" 
                        HeaderText="Issue Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="balQty" DataFormatString="{0:n2}" 
                        HeaderText="Bal Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TB007" HeaderText="Unit" />
                    <asp:BoundField DataField="prQty" DataFormatString="{0:n2}" HeaderText="PR Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="poQty" DataFormatString="{0:n2}" HeaderText="PO Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="poRcpQty" DataFormatString="{0:n2}" 
                        HeaderText="PO Inspect Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="stockQty" DataFormatString="{0:n2}" 
                        HeaderText="Inventory Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" HorizontalAlign="Center" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="True" />
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
