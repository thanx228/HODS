<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PR.aspx.vb" Inherits="MIS_HTI.PR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        .style5
        {
            width: 69px;
        }
        .style6
        {
            width: 79px;
        }
        .style7
        {
            width: 141px;
        }
        .style8
        {
            width: 59px;
        }
        .style9
        {
            width: 69px;
        }
        .style10
        {
            width: 79px;
        }
        .style11
        {
            width: 141px;
        }
        .style12
        {
            width: 59px;
        }
        .style14
        {
            height: 50px;
        }
        .style15
        {
            width: 69px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="style4">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" BorderStyle="Solid" Font-Size="X-Large" 
                            ForeColor="Blue" Text="Purchase Requests"></asp:Label>
                    </td>
                </tr>
            </table>

            <fieldset>
            <legend>Search Data</legend> 
             <table class="style4">
                <tr>
                    <td class="style9">
                        <asp:Label ID="Label1" runat="server" Text="Search By :"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:DropDownList ID="DDLSearch" runat="server" AutoPostBack="True">
                            <asp:ListItem Value="TA001">P/R Type</asp:ListItem>
                            <asp:ListItem Value="TA002">P/R No.</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style11">
                        <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
                    </td>
                    <td class="style12">
                        <asp:Button ID="BuSearch" runat="server" Text="Search" />
                    </td>
                    <td class="style14">
                        <asp:ImageButton ID="ImageButton1" runat="server" 
                            ImageUrl="~/Images/imagesPrint.jpg" />
                    </td>
                    <td class="style14">
                        <asp:TextBox ID="TXTHT" runat="server" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style15">
                        <asp:TextBox ID="Dept" runat="server" Width="62px" Visible="False"></asp:TextBox>
                    </td>
                    <td class="style6">
                        <asp:TextBox ID="Issue" runat="server" Width="80px" Visible="False"></asp:TextBox>
                    </td>
                    <td class="style7">
                        <asp:TextBox ID="Remark" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td class="style8">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:TextBox ID="TXTHNO" runat="server" Visible="False"></asp:TextBox>
                    </td>
                </tr>
            </table>
            </fieldset> 

            <asp:GridView ID="GridPRHead" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="TA001,TA002" 
                DataSourceID="DataSourcePRH" BackColor="White" BorderColor="#3366CC" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    
                    <asp:ButtonField CommandName="OnClick" HeaderText="Select" Text="Select">
                    </asp:ButtonField>
                    
                    <asp:BoundField DataField="TA001" HeaderText="Type" ReadOnly="True" 
                        SortExpression="TA001" />
                    <asp:BoundField DataField="TA002" HeaderText="No." ReadOnly="True" 
                        SortExpression="TA002" />
                    <asp:BoundField DataField="TA004" HeaderText="Request Dep" 
                        SortExpression="TA004" >
                     <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:BoundField DataField="TA013" HeaderText="Date Issue" SortExpression="TA013" />
                    <asp:BoundField DataField="TA006" HeaderText="Remark" SortExpression="TA006" />
                   
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
            <asp:SqlDataSource ID="DataSourcePRH" runat="server" 
                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                SelectCommand="SELECT * FROM [PURTA]"></asp:SqlDataSource>

            <asp:GridView ID="GridPRL" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" 
                DataSourceID="DataSourcePRL" BackColor="White" BorderColor="#3366CC" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="TB001" HeaderText="Type" SortExpression="TB001" />
                    <asp:BoundField DataField="TB002" HeaderText="No." SortExpression="TB002" />
                    <asp:BoundField DataField="TB004" HeaderText="Item" SortExpression="TB004" />
                    <asp:BoundField DataField="TB005" HeaderText="Description" SortExpression="TB005" />
                    <asp:BoundField DataField="TB006" HeaderText="Spec" SortExpression="TB006" />
                    <asp:BoundField DataField="TB014" HeaderText="Quantity" SortExpression="TB014" DataFormatString = {0:F3} />
                    <asp:BoundField DataField="TB015" HeaderText="Unit" SortExpression="TB015" >
                    </asp:BoundField>
                    <asp:BoundField DataField="TB029" HeaderText="Reference Type" SortExpression="TB029" />
                    <asp:BoundField DataField="TB030" HeaderText="Reference No" SortExpression="TB030" />
                    <asp:BoundField DataField="TB010" HeaderText="Supp" 
                        SortExpression="TB010" >
                    </asp:BoundField>
                    <asp:BoundField DataField="TB032" HeaderText="Urgent" SortExpression="TB032" >
                    </asp:BoundField>
                    <asp:BoundField DataField="TB012" HeaderText="Remark" SortExpression="TB012" />
                     <asp:BoundField DataField="MB017" HeaderText="WH" SortExpression="MB017" />
                      <asp:BoundField DataField="MB064" HeaderText="Inv Qty" SortExpression="MB064" DataFormatString= {0:F3} />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle Wrap="False" BackColor="#003399" Font-Bold="True" 
                    ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle Wrap="False" BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:SqlDataSource ID="DataSourcePRL" runat="server" 
                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                
                SelectCommand="SELECT [TB001], [TB002], [TB004], [TB005], [TB006], [TB014], [TB015], [TB029], [TB030], [TB010], [TB032], [TB012], [MB017], [MB064] FROM [PURTB] L left join [INVMB] I on (L.TB004 = I.MB001) WHERE (([TB001] = @TB001) AND ([TB002] = @TB002))">
                <SelectParameters>
                    <asp:ControlParameter ControlID="TXTHT" Name="TB001" PropertyName="Text" 
                        Type="String" />
                    <asp:ControlParameter ControlID="TXTHNO" Name="TB002" PropertyName="Text" 
                        Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
 
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
