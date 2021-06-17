<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleQuotation.aspx.vb" Inherits="MIS_HTI.SaleQuotation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 265px;
        }
        .style7
        {
            width: 149px;
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
                        <asp:Label ID="Label3" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="Sale Order check Quotation"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White" class="style7" style="vertical-align: top">
                        <asp:Label ID="Label4" runat="server" Text="Sale Order Type"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3" style="vertical-align: top">
                        <asp:CheckBoxList ID="cblType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label9" runat="server" Text="Sale No."></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSaleNo" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label13" runat="server" Text="Status"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Value="ALL">ALL</asp:ListItem>
                            <asp:ListItem Selected="True" Value="N">Not Close</asp:ListItem>
                            <asp:ListItem Value="Y">Auto Close</asp:ListItem>
                            <asp:ListItem Value="y">Close Manual</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label14" runat="server" Text="Cust Code"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbCust" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label11" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label10" runat="server" Text="Desc"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbDesc" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label12" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label5" runat="server" Text="Date From"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label6" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbEnd" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbEnd_CalendarExtender" runat="server" Enabled="True" 
                            Format="dd/MM/yyyy" TargetControlID="tbEnd">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label15" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:CheckBox ID="cbNotQuon" runat="server" Text="SO Not Quotation" />
                        <asp:CheckBox ID="cbPrice" runat="server" 
                            Text="SO Price less then Qout. Price" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" bgcolor="White" class="style6">
                        <asp:Label ID="Label7" runat="server" Text="Amount of Rows"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="Label8" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="TD001" HeaderText="SO Detail" />
                    <asp:BoundField DataField="TC003" HeaderText="SO Date" />
                    <asp:BoundField DataField="TC004" HeaderText="Cust." />
                    <asp:BoundField DataField="TD004" HeaderText="Item" />
                    <asp:BoundField DataField="TD006" HeaderText="Spec" />
                    <asp:BoundField DataField="TD008" DataFormatString="{0:N2}" HeaderText="SO Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD011" DataFormatString="{0:N5}" 
                        HeaderText="SO Price">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="quPrice" DataFormatString="{0:N5}" 
                        HeaderText="Quantation Price">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="quDate" HeaderText="Quantation Date" />
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
    </asp:UpdatePanel>
</asp:Content>
