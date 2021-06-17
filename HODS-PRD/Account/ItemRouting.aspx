<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ItemRouting.aspx.vb" Inherits="MIS_HTI.ItemRouting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 240px;
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
                            Text="Check Item Routing"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label4" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label6" runat="server" Text="Desc"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbDesc" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label5" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label10" runat="server" Text="Routing"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbRout" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        &nbsp;</td>
                    <td bgcolor="White">
                        <asp:CheckBox ID="cbEmpty" runat="server" Text="Routing is empty" />
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label7" runat="server" Text="Routing status"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlRouting" runat="server">
                            <asp:ListItem Selected="True" Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('http://localhost:54341/Images/btt.jpg'); background-repeat: no-repeat" 
                        align="center">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White" align="center" class="style6">
                        <asp:Label ID="Label8" runat="server" Text="Amount of rows"></asp:Label>
                    </td>
                    <td bgcolor="White" align="center">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td bgcolor="White" align="center">
                        <asp:Label ID="Label9" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="MB001" HeaderText="Item" />
                    <asp:BoundField DataField="MB002" HeaderText="Desc" />
                    <asp:BoundField DataField="MB003" HeaderText="Spec" />
                    <asp:BoundField DataField="MB004" HeaderText="Unit" />
                    <asp:BoundField DataField="MB011" HeaderText="Routing" />
                    <asp:BoundField DataField="ME002" HeaderText="Status" />
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
