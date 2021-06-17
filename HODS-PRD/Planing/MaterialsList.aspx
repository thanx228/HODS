<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MaterialsList.aspx.vb" Inherits="MIS_HTI.MaterialsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 233px;
        }
        .style5
        {
            width: 120px;
        }
        .style6
        {
            width: 120px;
        }
        .style8
        {
            height: 36px;
        }
        .style11
        {
            width: 120px;
        }
        .style12
        {
            width: 79px;
        }
        .style13
        {
            height: 36px;
            width: 88px;
        }
        .style14
        {
            width: 88px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 75%;">
                <tr>
                    <td align="left" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        class="style12">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="BOM Materials List"></asp:Label>
                    </td>
                </tr>
            </table>
            <table  style="width: 75%;">
                <tr>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Search By"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style8">
                        <asp:DropDownList ID="ddlSearchBy" runat="server">
                            <asp:ListItem Value="1">Item</asp:ListItem>
                            <asp:ListItem Value="2">Sale Order</asp:ListItem>
                            <asp:ListItem Value="3">Manufactor Order</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF" class="style8">
                        <asp:Label ID="Label13" runat="server" Text="Cust "></asp:Label>
                        </td>
                    <td style="background-color: #FFFFFF" class="style8">
                        <asp:TextBox ID="tbCust" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    <td style="background-color: #FFFFFF" class="style8">
                        </td>
                    <td style="background-color: #FFFFFF" class="style13">
                        </td>
                </tr>
                <tr>
                    <td class="style6" style="background-color: #FFFFFF">
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
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Order Qty"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style14">
                        <asp:TextBox ID="tbOrdQty" runat="server" Width="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="Sale Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlSaleType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Sale No"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSaleNo" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="Sale Seq"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style14">
                        <asp:TextBox ID="tbSaleSeq" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    </tr>
                    <tr>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlWorkType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="MO No."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbWorkNo" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td class="style14" style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                        <asp:Button ID="btExport" runat="server" Text="Excel Export" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label12" runat="server" Text="รายการ"></asp:Label>
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
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
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
