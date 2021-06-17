<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="InventoryPriceList.aspx.vb" Inherits="MIS_HTI.InventoryPriceList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 41%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="FG Inventory Price List"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="WH"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlWh" runat="server">
                            <asp:ListItem Selected="True" Value="0">ALl WH</asp:ListItem>
                            <asp:ListItem Value="2101">2101 FG Metal Part</asp:ListItem>
                            <asp:ListItem Value="2102">2102 FG BOI 1</asp:ListItem>
                            <asp:ListItem Value="2103">2103 FG BOI 2</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbCode" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbSpec" runat="server" style="margin-top: 0px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 41%;">
                <tr>
                    <td align="center" class="style4" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btExport" runat="server" Text="Export Excel" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width: 41%;">
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="USD"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbUSD" runat="server" Width="50px">1</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="EUR"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbEUR" runat="server" Width="50px">1</asp:TextBox>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td align="center" 
                        style="border: thin solid #0066CC; background-repeat: no-repeat; background-color: #FFFFFF;">
                        <asp:Label ID="Label7" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" 
                        style="border: thin solid #0066CC; background-repeat: no-repeat; background-color: #FFFFFF;">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" 
                        style="border: thin solid #0066CC; background-repeat: no-repeat; background-color: #FFFFFF;">
                        <asp:Label ID="Label9" runat="server" Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
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
