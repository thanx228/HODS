<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MenuNew.aspx.vb" Inherits="MIS_HTI.MenuNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 102px;
        }
        .style7
        {
            width: 102px;
            height: 25px;
        }
        .style8
        {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                Height="50%" Width="75%">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1"><HeaderTemplate>
 Main Menu
 </HeaderTemplate>
 <ContentTemplate>
 <table style="width:50%;"><tr><td class="style6"><asp:Label ID="Label4" runat="server" Text="ID"></asp:Label></td><td><asp:Label ID="lbID" runat="server" ForeColor="Blue"></asp:Label></td></tr><tr><td class="style7"><asp:Label ID="Label5" runat="server" Text="Line"></asp:Label></td><td class="style8"><asp:TextBox ID="tbLine" runat="server" Width="50px"></asp:TextBox></td></tr><tr><td class="style6"><asp:Label ID="Label7" runat="server" Text="Name"></asp:Label></td>
 
                            
 <td><asp:TextBox ID="tbName" runat="server"></asp:TextBox></td></tr></table><table style="width:50%;"><tr><td align="center" 
                                    
                                    style="background-image: url('http://localhost:54341/Images/btt.jpg'); background-repeat: no-repeat"><asp:Button ID="btSave" runat="server" Text="Save" Width="100px" /></td></tr></table><asp:GridView ID="gvMenu" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="4" DataKeyNames="Id" DataSourceID="SqlDataSourceMenu"><Columns><asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="Id" /><asp:BoundField DataField="Line" HeaderText="Line" SortExpression="Line" /><asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" /><asp:ButtonField ButtonType="Image" CommandName="onEdit" HeaderText="Edit" 
                                    ImageUrl="~/Images/icon-checkbox-tick.gif" Text="Button"><HeaderStyle HorizontalAlign="Center" /><ItemStyle HorizontalAlign="Center" /></asp:ButtonField></Columns><FooterStyle BackColor="#99CCCC" ForeColor="#003399" /><HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" /><PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" /><RowStyle BackColor="White" ForeColor="#003399" /><SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" /><SortedAscendingCellStyle BackColor="#EDF6F6" /><SortedAscendingHeaderStyle BackColor="#0D4AC4" /><SortedDescendingCellStyle BackColor="#D6DFDF" /><SortedDescendingHeaderStyle BackColor="#002876" /></asp:GridView>
 
                        
 <asp:SqlDataSource ID="SqlDataSourceMenu" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                            SelectCommand="SELECT [Id], [Line], [Name] FROM [Menu] WHERE ([ParentId] = @ParentId) ORDER BY [Line], [Id]"><SelectParameters><asp:Parameter DefaultValue="0" Name="ParentId" Type="Int32" /></SelectParameters></asp:SqlDataSource><br /><br /><br /><br />
 </ContentTemplate>
 </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2"><HeaderTemplate>
 Sub Menu
 </HeaderTemplate>
 <ContentTemplate>
 <table style="width:50%;"><tr><td><asp:Label ID="Label8" runat="server" Text="ID"></asp:Label>
 </td><td><asp:Label ID="lbIDSub" runat="server" ForeColor="Blue"></asp:Label>
 </td></tr><tr><td><asp:Label ID="Label10" runat="server" Text="Parent Menu"></asp:Label>
 </td><td><asp:DropDownList ID="ddlParent" runat="server"></asp:DropDownList>
 </td></tr><tr><td><asp:Label ID="Label11" runat="server" Text="Line"></asp:Label>
 </td><td>
 
                                    
 <asp:TextBox ID="tbLineSub" runat="server" Width="50px"></asp:TextBox>
 </td></tr><tr><td><asp:Label ID="Label12" runat="server" Text="Name"></asp:Label>
 </td><td><asp:TextBox ID="tbNameSub" runat="server"></asp:TextBox>
 </td></tr><tr><td><asp:Label ID="Label13" runat="server" Text="Path"></asp:Label>
 </td><td><asp:TextBox ID="tbProg" runat="server" Width="200px"></asp:TextBox>
 </td></tr><tr><td>Status</td><td><asp:DropDownList ID="ddlStatus" runat="server"><asp:ListItem Value="1">Show Menu</asp:ListItem>
 <asp:ListItem Value="0">Hide Menu</asp:ListItem>
 </asp:DropDownList>
 </td></tr><tr>
 
                                
 <td><asp:Label ID="Label14" runat="server" Text="Is Parent"></asp:Label>
 </td><td><asp:DropDownList ID="ddlIsParent" runat="server"><asp:ListItem Value="N">No</asp:ListItem>
 <asp:ListItem Value="Y">Yes</asp:ListItem>
 </asp:DropDownList>
 </td></tr></table><table style="width: 50%;"><tr><td align="center" 
                                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat"><asp:Button ID="btShow" runat="server" Text="Search" Width="80px" />
 <asp:Button ID="btSubSave" runat="server" Text="Save" Width="80px" />
 </td></tr></table><asp:GridView ID="gvMenuSub" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="4"><Columns>
                                
 

                                <asp:BoundField DataField="Id" DataFormatString="{0:N0}" HeaderText="ID">
                                    
 

                                    <ItemStyle HorizontalAlign="Right" />
 </asp:BoundField>
                                
 

                                <asp:BoundField DataField="ParentId" DataFormatString="{0:N0}" 
                                    HeaderText="Parent ID">
 <ItemStyle HorizontalAlign="Right" />

                                

 


                                </asp:BoundField>
 <asp:BoundField DataField="Line" DataFormatString="{0:N0}" HeaderText="Line">
                                    
 

                                    <ItemStyle HorizontalAlign="Right" />
                                
 

                                </asp:BoundField>
                                
 

                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                
 

                                <asp:BoundField DataField="Prog" HeaderText="Path Link" />
                                
 

                                <asp:BoundField DataField="isParent" HeaderText="Is Parent" />
                                
 

                                <asp:BoundField DataField="MenuStatus" HeaderText="Status" />
                                
 

                                <asp:ButtonField ButtonType="Image" CommandName="onEditSub" HeaderText="Edit" 
                                    ImageUrl="~/Images/icon-checkbox-tick.gif" Text="Button" />
                                
 

                                <asp:ButtonField ButtonType="Image" CommandName="onDeleteSub" HeaderText="Delete" 
                                    ImageUrl="~/Images/delete.gif" Text="Button">
                                    
 

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
 <br /><br /><br /><br />
 </ContentTemplate>
 </asp:TabPanel>
            </asp:TabContainer>
<br />
<br />
<br />
<br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
