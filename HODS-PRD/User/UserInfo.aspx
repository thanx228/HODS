<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="UserInfo.aspx.vb" Inherits="MIS_HTI.UserInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 235px;
            height: 24px;
        }
        .style7
        {
            
        }
        .style8
        {
            width: 235px;
        }
        .style9
        {            
            
        }
        .style10
        {
            width: 235px;
        }
        .style11
        {
            
        }
        .style13
        {
            
        }
        .style14
        {
            width: 235px;
        }
        .style15
        {
            
        }
        .style16
        {
            width: 63px;
        }
        .style17
        {
            width: 119px;
            height: 24px;
        }
        .style18
        {
            width: 119px;
        }
        .style19
        {
            width: 114px;
            height: 24px;
        }
        .style20
        {
            width: 114px;
        }
        .style21
        {
            width: 117px;
        }
        .style22
        {
            width: 117px;
            height: 24px;
        }
        .style23
        {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <asp:ScriptManager ID="ScriptManager1" runat="server" /> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
    <ContentTemplate> 
       <fieldset>
       <legend>Basic Data</legend>

       <table class="style3" >
            <tr>
                <td class="style17" style="background-color: #FFFFFF">
                    <asp:Label ID="Label2" runat="server" Text="User ID :"></asp:Label>
                </td>
                <td class="style21" style="background-color: #FFFFFF">
                    <asp:TextBox ID="txtuser" runat="server"></asp:TextBox>
                </td>
                <td class="style19" style="background-color: #FFFFFF">
                    <asp:Label ID="Label5" runat="server" Text="UserName :"></asp:Label>
                </td>
                <td class="style6" style="background-color: #FFFFFF">
                    <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                </td>
                <td class="style7">
                    </td>
                <td class="style7">
                    </td>
            </tr>
            <tr>
                <td class="style18" style="background-color: #FFFFFF">
                    <asp:Label ID="Label3" runat="server" Text="Password :"></asp:Label>
                </td>
                <td class="style21" style="background-color: #FFFFFF">
                    <asp:TextBox ID="txtpassword" runat="server"></asp:TextBox>
                </td>
                <td class="style20" style="background-color: #FFFFFF">
                    <asp:Label ID="Label7" runat="server" Text="Sex :"></asp:Label>
                </td>
                <td class="style10" style="background-color: #FFFFFF">
                    <asp:RadioButtonList ID="RBSex" runat="server" AutoPostBack="True" 
                        RepeatDirection="Horizontal" Width="117px">
                        <asp:ListItem Selected="True">M</asp:ListItem>
                        <asp:ListItem>F</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="style11">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style17" style="background-color: #FFFFFF">
                    <asp:Label ID="Label6" runat="server" Text="Department :"></asp:Label>
                </td>
                <td class="style22" style="background-color: #FFFFFF">
                    <asp:DropDownList ID="DDLdept" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="style19" style="background-color: #FFFFFF">
                    <asp:Label ID="Label4" runat="server" Text="User Group :"></asp:Label>
                </td>
                <td class="style6" style="background-color: #FFFFFF">
                    <asp:DropDownList ID="DDLGroup" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="style23">
                    </td>
                <td class="style23">
                    </td>
            </tr>
            <tr>
                <td class="style18" style="background-color: #FFFFFF">
                    &nbsp;</td>
                <td class="style21" style="background-color: #FFFFFF">
                    <asp:Button ID="BuSave" runat="server" Text="Save" Width="100px" />
                </td>
                <td class="style20" style="background-color: #FFFFFF">
                    &nbsp;</td>
                <td class="style8" style="background-color: #FFFFFF">
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                </td>
                <td class="style13">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
       </fieldset>

       <br />
         <br />
         <fieldset>
        <legend>Search Data</legend>

            <table class="style9">
                <tr>
                    <td class="style16" style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Search :"></asp:Label>
                    </td>
                    <td class="style15" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="DDLSearch" runat="server" AutoPostBack="True" 
                            Width="89px">
                            <asp:ListItem Value="UserName">User ID</asp:ListItem>
                            <asp:ListItem Value="NameSurname">User Name</asp:ListItem>
                            <asp:ListItem Value="Dept">Department</asp:ListItem>
                            <asp:ListItem Value="UserGroup">User Group</asp:ListItem>
                            <asp:ListItem>Sex</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style14" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
                    </td>
                    <td class="style15" style="background-color: #FFFFFF">
                        <asp:Button ID="Busearch" runat="server" Text="Search" />
                    </td>
                    <td class="style15">
                        </td>
                    <td class="style15">
                        </td>
                    <td class="style15">
                        </td>
                </tr>
            </table>

        </fieldset><br />
         &nbsp;<asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" 
            DataSourceID="SqlDataSource1" Width="649px" PageSize="20" 
            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
            CellPadding="4">
            <Columns>
                  <asp:TemplateField HeaderText="Dept.">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplShow3" runat="server" Target="_blank">OTDept</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Plan">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Plan</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Daily">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplShow2" runat="server" Target="_blank">Daily</asp:HyperLink>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" 
                    ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="UserName" HeaderText="UserID" 
                    SortExpression="UserName" />
                <asp:BoundField DataField="UserPassWord" HeaderText="PassWord" 
                    SortExpression="UserPassWord" />

                <asp:BoundField DataField="NameSurname" HeaderText="UserName" 
                    SortExpression="NameSurname" />

                     <asp:BoundField DataField="Sex" HeaderText="Sex" 
                    SortExpression="Sex" >

                     <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                     <asp:BoundField DataField="UserGroup" HeaderText="UserGroup" 
                    SortExpression="UserGroup" />

                <asp:BoundField DataField="Dept" HeaderText="Department" SortExpression="Dept" />
                <asp:ButtonField ButtonType="Image" CommandName="OnEdit" HeaderText="Edit" 
                    ImageUrl="~/Images/edit.gif" Text="Edit">
                <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>
               
               <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" 
                                ImageUrl="~/Images/delete.gif" 
                                onclientclick="return confirm('Are you sure delete it')" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
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

          <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
            SelectCommand="SELECT * FROM [UserInfo]" 
            DeleteCommand="DELETE FROM [UserInfo] WHERE [Id] = @Id">
            <DeleteParameters>
                <asp:Parameter Name="Id" Type="String" />
            </DeleteParameters>
        </asp:SqlDataSource>

</ContentTemplate> 
      </asp:UpdatePanel>
      </asp:Content>