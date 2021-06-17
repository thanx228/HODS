<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="setMoUrgent.aspx.vb" Inherits="MIS_HTI.setMoUrgent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 50%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label2" runat="server" Text="Set MO Urgent" Font-Size="Medium" 
                            ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="MO Type."></asp:Label>
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
                </tr>
            </table>
            <table style="width: 50%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btSave" runat="server" Text="Add Urgent" Width="80px"  />
                        &nbsp;
                        <asp:Button ID="btSearch" runat="server" Text="Search" Width="80px" />
                        &nbsp;
                        <asp:Button ID="btClear" runat="server" Text="Clear MO Close" Width="108px" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False" DataKeyNames="TA001,TA002" 
                DataSourceID="SqlDataSourceMO" Width="322px">
                <Columns>
                   <asp:TemplateField>
                       <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Runat="server"  OnClientClick="return confirm('Are you sure you want to delete it?');"
                                CommandName="Delete">Delete MO</asp:LinkButton>
                        </ItemTemplate>
                       <HeaderStyle HorizontalAlign="Center" />
                       <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="TA001" HeaderText="TA001" ReadOnly="True" 
                        SortExpression="TA001" />
                    <asp:BoundField DataField="TA002" HeaderText="TA002" ReadOnly="True" 
                        SortExpression="TA002" />
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
            <asp:SqlDataSource ID="SqlDataSourceMO" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                DeleteCommand="DELETE FROM [MoUrgent] WHERE [TA001] = @TA001 AND [TA002] = @TA002" 
                InsertCommand="INSERT INTO [MoUrgent] ([TA001], [TA002]) VALUES (@TA001, @TA002)" 
                SelectCommand="SELECT * FROM [MoUrgent] ORDER BY [TA001], [TA002] DESC">
                <DeleteParameters>
                    <asp:Parameter Name="TA001" Type="String" />
                    <asp:Parameter Name="TA002" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="TA001" Type="String" />
                    <asp:Parameter Name="TA002" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
