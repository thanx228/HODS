<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="planDelivery.aspx.vb" Inherits="MIS_HTI.planDelivery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:75%;">
                <tr>
                    <td align="left" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" Font-Size="Large" ForeColor="Blue" 
                            Text="Plan Delivery Stock"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="background-color: #FFFFFF">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Sale Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOrdType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Cust ID"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbCust" runat="server" BorderColor="#3399FF" 
                            BorderStyle="Solid"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="JP PART"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbPart" runat="server" BorderColor="#3399FF" 
                            BorderStyle="Solid"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="JP SPEC"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbSpec" runat="server" BorderColor="#3399FF" 
                            BorderStyle="Solid"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlConSel" runat="server">
                            <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Stock Over Delivry</asp:ListItem>
                            <asp:ListItem Value="2">Delivery Over Stock</asp:ListItem>
                            <asp:ListItem Value="3">Delivery=0,Stock&gt;0</asp:ListItem>
                            <asp:ListItem Value="4">Stock=0,Delivery&gt;0</asp:ListItem>
                            <asp:ListItem Value="5">Sale OK</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Search" Width="100px" />
                    </td>
                </tr>
            </table>
            <asp:Label ID="Label7" runat="server" Font-Size="Medium" Text="จำนวนรายการ"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbCount" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label9" runat="server" Font-Size="Medium" Text="รายการ"></asp:Label>
            <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="height: 173px" 
                Width="679px">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
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
