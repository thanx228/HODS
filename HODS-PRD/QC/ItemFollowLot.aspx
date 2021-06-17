<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ItemFollowLot.aspx.vb" Inherits="MIS_HTI.ItemFollowLot" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:50%; background-image: url('../Images/btt.jpg'); background-repeat: no-repeat;">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="Item Follow Lot"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:50%;">
                <tr>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:Label ID="Label4" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                        <asp:Label ID="lbID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:Label ID="Label5" runat="server" Text="Statr Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDate" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:Label ID="Label6" runat="server" Text="Follow Lot"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbLot" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width:50%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btClear" runat="server" Text="Clear" Width="80px" />
                        &nbsp;<asp:Button ID="btSearch" runat="server" CssClass="menu" Text="Search" 
                            Width="80px" />
                        &nbsp;<asp:Button ID="btSave" runat="server" CssClass="menu" Text="Save" 
                            Width="80px" onclientclick="return confirm('Are you save  it?');" />
                        &nbsp;<asp:Button ID="btClose" runat="server" CssClass="menu" Text="Close" 
                            Width="80px" onclientclick="return confirm('Are you close it?');" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Id" 
                DataSourceID="SqlDataSource1" PageSize="15">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" 
                        ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="item" HeaderText="item" SortExpression="item" />
                    <asp:BoundField DataField="dateStart" HeaderText="dateStart" 
                        SortExpression="dateStart" />
                    <asp:BoundField DataField="LotCheck" HeaderText="LotCheck" 
                        SortExpression="LotCheck" />
                    <asp:BoundField DataField="status" HeaderText="status" 
                        SortExpression="status" />
                    <asp:BoundField DataField="CreateBy" HeaderText="CreateBy" 
                        SortExpression="CreateBy" />
                    <asp:ButtonField ButtonType="Image" CommandName="onEdit" HeaderText="Edit" 
                        ImageUrl="~/Images/edit.gif" Text="Button">
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    HorizontalAlign="Center" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                SelectCommand="SELECT [Id], [item], [dateStart], [LotCheck], [status], [CreateBy] FROM [ItemFollowLot] ORDER BY [Id] DESC">
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
