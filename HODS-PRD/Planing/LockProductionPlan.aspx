<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="LockProductionPlan.aspx.vb" Inherits="MIS_HTI.LockProductionPlan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 170px;
        }
        .style7
        {
            width: 277px;
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
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label4" runat="server" Text="Batch No."></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbBatchNo" runat="server" Width="250px"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label7" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSaleType" runat="server" Width="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label5" runat="server" Text="SO No"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSaleNo" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label8" runat="server" Text="SO Seq"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSaleSeq" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label14" runat="server" Text="SO Item"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbItemSO" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label15" runat="server" Text="SO Spec"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSpecSO" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label6" runat="server" Text="PRD Item"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label9" runat="server" Text="PRD Spec"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSpec" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label13" runat="server" Text="W/C First"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="TbWC" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">&nbsp;</td>
                    <td bgcolor="White">&nbsp;</td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        align="center">
                        <asp:Button ID="btSearch" runat="server" Text="Search" Width="100px" />
                        &nbsp;<asp:Button ID="btLock" runat="server" Text="Lock" Width="100px" />
                        &nbsp;<asp:Button ID="btClear" runat="server" Text="Clear" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr align="center">
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label10" runat="server" Text="Amount of Rows"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label12" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="Lock">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
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
