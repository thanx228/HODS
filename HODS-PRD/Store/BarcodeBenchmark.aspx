<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="BarcodeBenchmark.aspx.vb" Inherits="MIS_HTI.BarcodeBenchmark" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        .style6
        {
            width: 91px;
        }
        .style8
        {
            width: 82px;
        }
        .style9
        {
            width: 33px;
        }
        .style10
        {
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
                        <asp:Label ID="Label2" runat="server" Text="Label Banchmark" 
                            BorderStyle="Solid" Font-Size="X-Large" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style10">
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
                    <td class="style8">
                        <asp:Label ID="Label1" runat="server" Text="Search By :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="DDLSearch" runat="server" AutoPostBack="True">
                            <asp:ListItem>Invoice No.</asp:ListItem>
                            <asp:ListItem>Po</asp:ListItem>
                            <asp:ListItem>Description</asp:ListItem>
                            <asp:ListItem>Spec</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style9">
                        <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
                    </td>
                    <td class="style10">
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
                    <td class="style8">
                        <asp:Label ID="Label6" runat="server" Text="Invoice Type :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Litype" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:Label ID="Label11" runat="server" Text="Item :"></asp:Label>
                    </td>
                    <td class="style10" colspan="2">
                        <asp:Label ID="Litem" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label19" runat="server" Text="Full Box :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Lfbox" runat="server" ForeColor="Blue"></asp:Label>
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
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style8">
                        <asp:Label ID="Label7" runat="server" Text="Invoice No :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Lino" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:Label ID="Label9" runat="server" Text="Desc :"></asp:Label>
                    </td>
                    <td class="style10" colspan="2">
                        <asp:Label ID="Ldesc" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label20" runat="server" Text="Last Box :"></asp:Label>
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
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style8">
                        <asp:Label ID="Label17" runat="server" Text="Cust ID :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Lcust" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:Label ID="Label10" runat="server" Text="Spec :"></asp:Label>
                    </td>
                    <td class="style10" colspan="2">
                        <asp:Label ID="Lspec" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label21" runat="server" Text="Last Qty :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Llqty" runat="server" ForeColor="Blue"></asp:Label>
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
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style8">
                        <asp:Label ID="Label8" runat="server" Text="Po No :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Lpo" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:Label ID="Label15" runat="server" Text="Qty :"></asp:Label>
                    </td>
                    <td class="style10" colspan="2">
                        <asp:Label ID="Lqty" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label22" runat="server" Text="Qty/Box:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtqbox" runat="server" Width="90px" AutoPostBack="true"></asp:TextBox>
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
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style8">
                        <asp:Label ID="Label3" runat="server" Text="Lot No :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox ID="txtlotno" runat="server"></asp:TextBox>
                    </td>
                    <td class="style9">
                        <asp:Label ID="Label4" runat="server" Text="Date Code :"></asp:Label>
                    </td>
                    <td class="style10" colspan="2">
                        <asp:TextBox ID="txtdatecode" runat="server" Width="93px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtdatecode_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd-MM-yyyy" TargetControlID="txtdatecode">
                        </asp:CalendarExtender>
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
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style8">
                        <asp:Label ID="Label18" runat="server" Text="Part Code :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox ID="txtPartCode" runat="server"></asp:TextBox>
                    </td>
                    <td class="style9">
                        &nbsp;</td>
                    <td class="style10" colspan="2">
                        <asp:Button ID="BuPrint" runat="server" Text="Print" />
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
                SelectCommand="select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) and (H.TA002 = L.TB002) where TA004 = 'B004'">
            </asp:SqlDataSource>
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
