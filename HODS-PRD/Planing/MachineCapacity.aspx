<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MachineCapacity.aspx.vb" Inherits="MIS_HTI.MachineCapacity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="Label1" runat="server" Font-Size="X-Large" ForeColor="Blue" 
        Text="Capacity"></asp:Label>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" DataKeyNames="wc" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="wc" HeaderText="Work Center" ReadOnly="True" 
                        SortExpression="wc" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="capacity" HeaderText="Manchine" 
                        SortExpression="capacity" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="mancapacity" HeaderText="Man Work" 
                        SortExpression="mancapacity">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:CommandField ShowEditButton="True" />
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
            <br />
            <asp:Button ID="btnUpdateMch" runat="server" style="height: 26px" 
                Text="Update Machine" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
        DeleteCommand="DELETE FROM [MachineCapacity] WHERE [wc] = @original_wc" 
        InsertCommand="INSERT INTO [MachineCapacity] ([wc], [capacity], [mancapacity]) VALUES (@wc, @capacity, @mancapacity)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT [wc], [capacity], [mancapacity] FROM [MachineCapacity] ORDER BY [wc]" 
        
        UpdateCommand="UPDATE [MachineCapacity] SET [capacity] = @capacity, [mancapacity] = @mancapacity WHERE [wc] = @original_wc">
        <DeleteParameters>
            <asp:Parameter Name="original_wc" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="wc" Type="String" />
            <asp:Parameter Name="capacity" Type="Decimal" />
            <asp:Parameter Name="mancapacity" Type="Decimal" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="capacity" Type="Decimal" />
            <asp:Parameter Name="mancapacity" Type="Decimal" />
            <asp:Parameter Name="original_wc" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
