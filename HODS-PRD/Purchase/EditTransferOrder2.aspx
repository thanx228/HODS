<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="EditTransferOrder2.aspx.vb" Inherits="MIS_HTI.EditTransferOrder2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        .style6
        {
            width: 86px;
        }
        .style7
        {
            width: 57px;
        }
        .style8
        {
            width: 135px;
        }
        .style15
        {
            width: 106px;
        }
        .style16
        {
            width: 113px;
        }
        .style17
        {
            width: 113px;
            height: 10px;
        }
        .style18
        {
            width: 57px;
            height: 10px;
        }
        .style19
        {
            width: 86px;
            height: 10px;
        }
        .style20
        {
            width: 135px;
            height: 10px;
        }
        .style21
        {
            width: 106px;
            height: 10px;
        }
        .style22
        {
            height: 10px;
        }
        .style23
        {
            width: 113px;
            height: 30px;
        }
        .style24
        {
            width: 57px;
            height: 30px;
        }
        .style25
        {
            width: 86px;
            height: 30px;
        }
        .style26
        {
            width: 135px;
            height: 30px;
        }
        .style27
        {
            width: 106px;
            height: 30px;
        }
        .style28
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 75%;">
                <tr>
                    <td align="left" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Large" 
                            ForeColor="Blue" Text="Edit Price Transfer Order"></asp:Label>
                    </td>
                </tr>
            </table>



            <table style="width:75%;">
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label1" runat="server" Text="Transfer Order Type :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="DDLType" runat="server" AutoPostBack="True" 
                            DataSourceID="DataSourceType" DataTextField="TC001" DataValueField="TC001">
                        </asp:DropDownList>
                        <asp:ListSearchExtender ID="DDLType_ListSearchExtender" runat="server" 
                            Enabled="True" TargetControlID="DDLType">
                        </asp:ListSearchExtender>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Transfer Order No :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtno" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Button ID="BuSearch" runat="server" Text="Search" Width="100px" />
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:SqlDataSource ID="DataSourceType" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:HOOTHAIConnectionString %>" 
                            
                            SelectCommand="select distinct TC001 from SFCTC where TC001 in('D203','D204','D205','D206','D209') order by TC001" 
                            ProviderName="<%$ ConnectionStrings:HOOTHAIConnectionString.ProviderName %>">
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>



            <table class="style4" style="background-color: #FFFFFF; width: 804px;">
                <tr>
                    <td class="style16">
                        <asp:Label ID="Label3" runat="server" Text="Transfer Type :"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Ltype" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Label6" runat="server" Text="Transfer No :"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Lno" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style15">
                        <asp:Label ID="Label4" runat="server" Text="Seq :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Lseq" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style16">
                        <asp:Label ID="Label15" runat="server" Text="Currency"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Lcurrency" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Label12" runat="server" Text="Exchange Rate"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Lrate" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style15">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style17">
                        <asp:Label ID="Label13" runat="server" Text="Tax Type"></asp:Label>
                    </td>
                    <td class="style18">
                        <asp:Label ID="Ltaxtype" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style19">
                        <asp:Label ID="Label14" runat="server" Text="Tax rate "></asp:Label>
                    </td>
                    <td class="style20">
                        <asp:Label ID="Ltaxrate" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style21">
                        &nbsp;</td>
                    <td class="style22">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style16">
                        <asp:Label ID="Label7" runat="server" Text="Item :"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Litem" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Label8" runat="server" Text="Description :"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Ldesc" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style15">
                        <asp:Label ID="Label10" runat="server" Text="Spec :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Lspec" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style23">
                        <asp:Label ID="Label5" runat="server" Text="Quantity :"></asp:Label>
                    </td>
                    <td class="style24">
                        <asp:Label ID="Lqty" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style25">
                        <asp:Label ID="Label9" runat="server" Text="Price :"></asp:Label>
                    </td>
                    <td class="style26">
                        <asp:TextBox ID="txtprice" runat="server" BorderStyle="Solid"></asp:TextBox>
                    </td>
                    <td class="style27">
                        </td>
                    <td class="style28">
                        <asp:Button ID="BuSave" runat="server" Text="Save" Width="100px" />
                    </td>
                </tr>
            </table>
            <br/>
            
            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" 
                DataKeyNames="TC001,TC002,TC003" DataSourceID="SqlDataSource2" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="TC001" HeaderText="Type" ReadOnly="True" 
                        SortExpression="TC001" />
                    <asp:BoundField DataField="TC002" HeaderText="No." ReadOnly="True" 
                        SortExpression="TC002" />
                    <asp:BoundField DataField="TC003" HeaderText="Seq" ReadOnly="True" 
                        SortExpression="TC003" />
                    <asp:BoundField DataField="TC004" HeaderText="MO Type" SortExpression="TC004" />
                    <asp:BoundField DataField="TC005" HeaderText="MO No." SortExpression="TC005" />
                    <asp:BoundField DataField="TC047" HeaderText="Item" SortExpression="TC047" />
                    <asp:BoundField DataField="TC048" HeaderText="Description" SortExpression="TC048" />
                    <asp:BoundField DataField="TC049" HeaderText="Spec" SortExpression="TC049" />
                    <asp:BoundField DataField="TC014" HeaderText="Quantity" SortExpression="TC014" DataFormatString = {0:F3} />
                    <asp:BoundField DataField="TC017" HeaderText="Price" SortExpression="TC017"  
                        DataFormatString = {0:F4}/>
                    <asp:BoundField DataField="TC018" HeaderText="Amount" SortExpression="TC018" DataFormatString = {0:F3} />
                    <asp:ButtonField ButtonType="Image" CommandName="OnChange" HeaderText="Change" 
                        ImageUrl="~/Images/edit.gif">
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
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:HOOTHAIConnectionString %>" 
                
                SelectCommand="select TC001,TC002,TC003,TC004,TC005,TC047,TC048,TC049,TC014,TC017,TC018 from SFCTC" 
                ProviderName="<%$ ConnectionStrings:HOOTHAIConnectionString.ProviderName %>">
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
