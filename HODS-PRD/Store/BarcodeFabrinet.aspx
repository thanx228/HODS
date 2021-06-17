<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="BarcodeFabrinet.aspx.vb" Inherits="MIS_HTI.BarcodeFabrinet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        .style5
        {
        }
        .style6
        {
            width: 66px;
        }
        .style7
        {
            width: 89px;
        }
        .style8
        {
            width: 50px;
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
                    <td class="style5" colspan="3">
                        <asp:Label ID="Label1" runat="server" BorderStyle="Solid" Font-Size="X-Large" 
                            ForeColor="Blue" Text="Label FABRINET"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label2" runat="server" Text="Search By :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="DDLSearch" runat="server" AutoPostBack="True">
                         <asp:ListItem>Invoice No.</asp:ListItem>
                            <asp:ListItem>Po</asp:ListItem>
                            <asp:ListItem>Description</asp:ListItem>
                            <asp:ListItem>Spec</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style8">
                        <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="Busearch" runat="server" Text="Search" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label3" runat="server" Text="Invoice Type :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Ltype" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Label11" runat="server" Text="Item :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Litem" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label4" runat="server" Text="Invoice No :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Lno" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Label12" runat="server" Text="Desc :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Ldesc" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label5" runat="server" Text="Po :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Lpo" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Label13" runat="server" Text="Spec :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Lspec" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label6" runat="server" Text="Customer :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Lcust" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Label14" runat="server" Text="Qty :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Lqty" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label19" runat="server" Text="Qty/Box :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox ID="txtqbox" runat="server" Width="97px" AutoPostBack ="true" ></asp:TextBox>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Label20" runat="server" Text="Full Box :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lfbox" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label21" runat="server" Text="Last Qty :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="llqty" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Label22" runat="server" Text="Last Box :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="llbox" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="Buprint" runat="server" Text="Print" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="TA001,TA002" 
                DataSourceID="DataSourceInvoice">
                <Columns>
                    <asp:ButtonField CommandName="OnClick" Text="Select" />
                    <asp:BoundField DataField="TA001" HeaderText="Type" ReadOnly="True" 
                        SortExpression="TA001" />
                    <asp:BoundField DataField="TA002" HeaderText="No" ReadOnly="True" 
                        SortExpression="TA002" />
                    <asp:BoundField DataField="TA004" HeaderText="Cust ID" SortExpression="TA004" />
                    <asp:BoundField DataField="TA008" HeaderText="Cust Name" SortExpression="TA008" />
                    <asp:BoundField DataField="TB039" HeaderText="Item" SortExpression="TB039" />
                    <asp:BoundField DataField="TB040" HeaderText="Desc" SortExpression="TB040" />
                    <asp:BoundField DataField="TB041" HeaderText="Spec" SortExpression="TB041" />
                    <asp:BoundField DataField="TB048" HeaderText="Po No" SortExpression="TB048" />
                    <asp:BoundField DataField="TB022" HeaderText="Qty" SortExpression="TB022" DataFormatString ={0:F3} />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="DataSourceInvoice" runat="server" 
                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                SelectCommand="SELECT H.TA001, H.TA002, H.TA004, H.TA008, L.TB039, L.TB040, L.TB041, L.TB048, L.TB022 FROM ACRTA AS H LEFT OUTER JOIN ACRTB AS L ON H.TA001 = L.TB001 AND H.TA002 = L.TB002 WHERE (H.TA004 = 'F005')">
            </asp:SqlDataSource>
<br />
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

