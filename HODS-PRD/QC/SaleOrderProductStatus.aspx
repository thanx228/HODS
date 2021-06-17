<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleOrderProductStatus.aspx.vb" Inherits="MIS_HTI.SaleOrderProductStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 244px;
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
                            Text="Product Status"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White" style="vertical-align: top">
                        <asp:Label ID="Label4" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:CheckBoxList ID="cblSaleType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label5" runat="server" Text="Cust ID"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbCust" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label8" runat="server" Text="Code Type"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlCodeType" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="2">FG</asp:ListItem>
                            <asp:ListItem Value="3">WIP</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label6" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label9" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label7" runat="server" Text="Due Date From"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label10" runat="server" Text="Due Date To"></asp:Label>
                    </td>
                    <td bgcolor="White">
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
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" bgcolor="White" class="style6">
                        <asp:Label ID="Label11" runat="server" Text="Amount of Rows"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="Label12" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
