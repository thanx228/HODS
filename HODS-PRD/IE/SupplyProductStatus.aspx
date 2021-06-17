<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SupplyProductStatus.aspx.vb" Inherits="MIS_HTI.SupplyProductStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 126px;
        }
        .style5
        {
            width: 105px;
        }
        .style6
        {
            width: 161px;
        }
        .style7
        {
            width: 691px;
        }
        .style8
        {
            width: 235px;
        }
        .style9
        {
            width: 161px;
        }
        .style10
        {
            width: 105px;
        }
        .style11
        {
            
        }
        .style14
        {
            width: 235px;
        }
        .style15
        {
            width: 105px;
        }
        .style16
        {
            width: 235px;
        }
        .style17
        {
            width: 105px;
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
                    <td 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label14" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Supply Product &amp; Other Status"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-color: #FFFFFF" class="style8">
                        <asp:Label ID="Label1" runat="server" Text="Show Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style9">
                        <asp:DropDownList ID="ddlShowType" runat="server">
                            <asp:ListItem Value="1">PR Not PO</asp:ListItem>
                            <asp:ListItem Value="2">PO Not Close</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF" class="style10">
                        <asp:Label ID="Label3" runat="server" Text="Code Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style11">
                        <asp:DropDownList ID="ddlCodeType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style8">
                        <asp:Label ID="Label5" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style10">
                        <asp:Label ID="Label6" runat="server" Text="PR/PO No."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDocNo" runat="server" Width="120px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style8">
                        <asp:Label ID="Label7" runat="server" Text="Desc"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:TextBox ID="tbDesc" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style10">
                        <asp:Label ID="Label9" runat="server" Text="Supplier ID"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSup" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style16">
                        <asp:Label ID="Label8" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style17">
                        <asp:Label ID="Label10" runat="server" Text="Delivery Due Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDueDate" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDueDate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDueDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style16" style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td class="style6" style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td class="style17" style="background-color: #FFFFFF">
                        <asp:Label ID="Label15" runat="server" Text="Over Due"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlOverDue" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="5">1-5 Days</asp:ListItem>
                            <asp:ListItem Value="10">6-10 Days</asp:ListItem>
                            <asp:ListItem Value="15">11-15 Days </asp:ListItem>
                            <asp:ListItem Value="16">&gt;15 Days</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        class="style7">
                        <asp:Button ID="btShowReport" runat="server" Text="Show Report" Width="100px" />
                        <asp:Button ID="btExport" runat="server" Text="Excel Export" Width="100px" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td align="center" 
                        style="border: thin solid #0000FF; background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" 
                        style="border: thin solid #0000FF; background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" 
                        style="border: thin solid #0000FF; background-color: #FFFFFF">
                        <asp:Label ID="Label13" runat="server" Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                Width="203px">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
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
