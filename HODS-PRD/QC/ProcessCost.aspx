<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ProcessCost.aspx.vb" Inherits="MIS_HTI.ProcessCost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table style="width: 59%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Process Cost"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" DataKeyNames="wc" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="wc" HeaderText="Work Center No." ReadOnly="True" 
                        SortExpression="wc">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wcName" HeaderText="Work Center Name" ReadOnly="True"
                        SortExpression="wcName"></asp:BoundField>
                    <asp:BoundField DataField="DLCost" HeaderText="DL Cost / Min" 
                        SortExpression="DLCost">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MachineCost" HeaderText="Machine Cost / Min" 
                        SortExpression="MachineCost">
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
            <asp:Button ID="btnUpdate" runat="server" style="height: 26px" 
                Text="Update Process" />
            <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                DeleteCommand="DELETE FROM [ProcessCost] WHERE [wc] = @original_wc" 
                InsertCommand="INSERT INTO [ProcessCost] ([wc], [wcName], [DLCost],[MachineCost]) VALUES (@wc, @wcName, @DLCost,@MachineCost)" 
                OldValuesParameterFormatString="original_{0}" 
                SelectCommand="SELECT [wc], [wcName],[DLCost], [MachineCost] FROM [ProcessCost] ORDER BY [wc]" 
                UpdateCommand="UPDATE [ProcessCost] SET [DLCost] = @DLCost, [MachineCost] = @MachineCost WHERE [wc] = @original_wc">
                <DeleteParameters>
                    <asp:Parameter Name="original_wc" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="wc" Type="String" />
                    <asp:Parameter Name="wcName" />
                    <asp:Parameter Name="DLCost" />
                    <asp:Parameter Name="MachineCost" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="DLCost" />
                    <asp:Parameter Name="MachineCost" />
                    <asp:Parameter Name="original_wc" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <br />
            <br />
<br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
