<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SettingSaturday.aspx.vb" Inherits="MIS_HTI.SettingSaturday" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
  <uc3:HeaderForm ID="HeaderForm1" runat="server" />
            <table style="width: 75%;">
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="lbEmpNo" runat="server" Text="Setting Saturday Working"></asp:Label>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <uc1:Date ID="Date1" runat="server" />
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td class="style10" style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        &nbsp;<asp:Button ID="btSave" runat="server" Text="Save" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Id" 
                DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
                        SortExpression="Id" />
                    <asp:BoundField DataField="DateSat" HeaderText="DateSat" 
                        SortExpression="DateSat" />

                <asp:ButtonField ButtonType="Image" CommandName="OnDelete" HeaderText="Delete" 
                    ImageUrl="~/Images/delete.gif" Text="Delete">
                <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>

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
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                SelectCommand="SELECT * FROM [NormalSatOT] ORDER BY [Id] DESC">
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
