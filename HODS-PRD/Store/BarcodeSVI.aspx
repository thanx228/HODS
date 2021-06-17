<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="BarcodeSVI.aspx.vb" Inherits="MIS_HTI.Barcode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        .style5
        {
            width: 88px;
        }
        .style6
        {
            width: 135px;
        }
        .style8
        {
            width: 59px;
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
                    <td colspan="3">
                        <asp:Label ID="Label13" runat="server" ForeColor="Blue" 
                            Text="Label BarCode SVI" BorderStyle="Solid" Font-Size="X-Large" 
                            Width="225px"></asp:Label>
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
                    <td class="style5">
                        <asp:Label ID="Label1" runat="server" Text="Search by :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="DDLSearch" runat="server" AutoPostBack="True" 
                            style="margin-left: 0px">
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
                    <td class="style5">
                        <asp:Label ID="Label12" runat="server" Text="Invoice Type :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Ltype" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Label5" runat="server" Text="Item :"></asp:Label>
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
                    <td class="style5">
                        <asp:Label ID="Label4" runat="server" Text="Invoice No :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Linvoice" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Label6" runat="server" Text="Desc :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Ldesc" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lake" runat="server"></asp:Label>
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
                <tr>
                    <td class="style5">
                        <asp:Label ID="Label3" runat="server" Text="PO :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Lpo" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Label7" runat="server" Text="Spec :"></asp:Label>
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
                    <td class="style5">
                        <asp:Label ID="Label2" runat="server" Text="Customer :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Lsup" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Label11" runat="server" Text="Qty :"></asp:Label>
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
                    <td class="style5">
                        <asp:Label ID="Label14" runat="server" Text="Qty /Box :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox ID="txtqbox" runat="server" AutoPostBack="true" ></asp:TextBox>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Label15" runat="server" Text="Full Box :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Lfbox" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="BuCal" runat="server" Text="Cal" Visible="False" />
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
                <tr>
                    <td class="style5">
                        <asp:Label ID="Label16" runat="server" Text="Last Qty :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Llqty" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Label17" runat="server" Text="Last Box :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Llbox" runat="server" ForeColor="Blue"></asp:Label>
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
                    <td class="style5">
                        <asp:Label ID="Label18" runat="server" Text="Start :" Visible="False"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox ID="TextBox3" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Label19" runat="server" Text="To :" Visible="False"></asp:Label>
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
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="TA001,TA002" DataSourceID="DataSourceInvoice" 
                AllowPaging="True" AllowSorting="True">
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
                  
                    <asp:BoundField DataField="TB022" HeaderText="Qty" SortExpression="TB022" DataFormatString={0:F3} />
                  
                    <asp:ButtonField ButtonType="Image" CommandName="OnPrint" 
                        ImageUrl="~/Images/icon_print.png" Visible="False" />
                  
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="DataSourceInvoice" runat="server" 
                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                
                SelectCommand="select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004 = 'S007'"></asp:SqlDataSource>
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
