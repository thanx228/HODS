<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="Menu.aspx.vb" Inherits="MIS_HTI.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" /> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
    <ContentTemplate> 
        <table style="width:75%;">
            <tr>
                <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label2" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                        Text="Create Menu"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="MenuGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Id" 
            OnPageIndexChanging="MenuGridView_PageIndexChanging" 
            onrowcancelingedit="MenuGridView_RowCancelingEdit" 
            onrowcommand="MenuGridView_RowCommand" onrowdeleting="MenuGridView_RowDeleting" 
            onrowediting="MenuGridView_RowEditing" onrowupdating="MenuGridView_RowUpdating" 
            PageSize="20" ShowFooter="True" Width="408px" 
            >
            <Columns>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" 
                            Height="20px" ImageUrl="/Images/update.jpg" ToolTip="Update" Width="20px" />
                        <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" 
                            Height="20px" ImageUrl="/Images/cancel.png" ToolTip="Cancel" Width="20px" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <table style="width: 15%;">
                            <tr>
                                <td class="style3">
                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" 
                                        Height="20px" ImageUrl="/Images/edit.gif" ToolTip="Edit" Width="20px" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CausesValidation="false" 
                                        CommandName="Delete" Height="20px" ImageUrl="/Images/delete.gif" 
                                        OnClientClick="return confirm('Are you sure you want to delete this Menu?');" 
                                        Text="Edit" ToolTip="Delete" Width="20px" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="AddNew" 
                            Height="17px" ImageUrl="/Images/Addnew.gif" ToolTip="Add new Menu" 
                            ValidationGroup="validaiton" Width="17px" />
                        <asp:ImageButton ID="imgbtnSch" runat="server" CommandName="Search" 
                            Height="17px" ImageUrl="/Images/Search.jpg" ToolTip="Search" Width="18px" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID">
                    <EditItemTemplate>
                        <asp:Label ID="lblIdmenu" runat="server" Text='<%#Eval("Id") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblItemmenu" runat="server" Text='<%#Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Parent ID">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcode" runat="server" size="10" 
                            Text='<%#Eval("ParentId") %>' Width="60px" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcode" runat="server" Text='<%#Eval("ParentId") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtfcode" runat="server" size="10" Width="60px" />
                        <asp:RequiredFieldValidator ID="rfcode" runat="server" 
                            ControlToValidate="txtfcode" Text="*" ValidationGroup="validaiton" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Line">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtline" runat="server" size="5" Text='<%#Eval("Line") %>' 
                            Width="40px" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblline" runat="server" Text='<%#Eval("Line") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtfline" runat="server" size="5" Width="40px" />
                        <asp:RequiredFieldValidator ID="rfline" runat="server" 
                            ControlToValidate="txtfline" Text="*" ValidationGroup="validaiton" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtname" runat="server" size="50" Text='<%#Eval("Name") %>' 
                            Width="200px" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtfname" runat="server" size="50" Width="200px" />
                        <asp:RequiredFieldValidator ID="rfname" runat="server" 
                            ControlToValidate="txtfname" Text="*" ValidationGroup="validaiton" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Program">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtprog" runat="server" size="100" Text='<%#Eval("Prog") %>' 
                            Width="300px" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblprog" runat="server" Text='<%#Eval("Prog") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtfprog" runat="server" size="100" Width="300px" />
                        <asp:RequiredFieldValidator ID="rfprog" runat="server" 
                            ControlToValidate="txtfprog" Text="*" ValidationGroup="validaiton" />
                    </FooterTemplate>
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
    <asp:Label ID="lblresult" runat="server"></asp:Label>
        <table style="width:75%;">
            <tr>
                <td align="center" 
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    &nbsp;</td>
            </tr>
        </table>
</ContentTemplate> 
      </asp:UpdatePanel>
  
    </asp:Content>
  