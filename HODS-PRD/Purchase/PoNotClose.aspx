<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PoNotClose.aspx.vb" Inherits="MIS_HTI.PoNotClose" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            width: 100%;
            margin-top: 0px;
        }
        .style4
        {
        }
        .style5
        {
            width: 111px;
        }
        .style8
        {
            width: 111px;
            height: 36px;
        }
        .style9
        {
            width: 111px;
            height: 55px;
        }
        .style13
        {
            height: 55px;
        }
        .style14
        {
            width: 111px;
            height: 24px;
        }
        .style15
        {
            width: 111px;
        }
        .style17
        {
            width: 111px;
            height: 28px;
        }
        .style20
        {
            height: 28px;
        }
        .style21
        {
            width: 113px;
        }
        .style22
        {
            width: 113px;
            height: 55px;
        }
        .style23
        {
            width: 113px;
            height: 28px;
        }
        .style24
        {
            width: 119px;
        }
        .style25
        {
            width: 119px;
            height: 55px;
        }
        .style26
        {
            width: 119px;
            height: 28px;
        }
        .style28
        {
            width: 119px;
            height: 36px;
        }
        .style29
        {
            width: 113px;
            height: 36px;
        }
        .style30
        {
            height: 36px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="style3">
                <tr>
                    <td class="style4" colspan="4" bgcolor="Maroon" align="center">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
                            ForeColor="White" Text="Purchase Order Not Close"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style24" style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Sales Order Type :"></asp:Label>
                    </td>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="DDLSType" runat="server" AutoPostBack="True">
                            <asp:ListItem>ALL</asp:ListItem>
                            <asp:ListItem Value="2201">2201 : Sec 1</asp:ListItem>
                            <asp:ListItem Value="2202">2202 : Sec 2</asp:ListItem>
                            <asp:ListItem Value="2203">2203 : Sec 3</asp:ListItem>
                            <asp:ListItem Value="2204">2204 : Sec 4</asp:ListItem>
                            <asp:ListItem Value="2205">2205 : PM</asp:ListItem>
                            <asp:ListItem Value="2206">2206 : Sec-1 Mold</asp:ListItem>
                            <asp:ListItem Value="2207">2207 : Sec-2 Mold</asp:ListItem>
                            <asp:ListItem Value="2208">2208 : Sec-3 Mold</asp:ListItem>
                            <asp:ListItem Value="2209">2209 : Sec-4 Mold</asp:ListItem>
                            <asp:ListItem Value="2210">2210 : PM Mold</asp:ListItem>
                            <asp:ListItem Value="2211">2211 : Mold Sup</asp:ListItem>
                            <asp:ListItem Value="2212">2212 : Wirecut</asp:ListItem>
                            <asp:ListItem Value="2213">2213 : FAI Sample</asp:ListItem>
                            <asp:ListItem Value="2214">2214 : Test RoHS</asp:ListItem>
                            <asp:ListItem Value="2215">2215 : Material</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style21" style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style25" style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Po Type :"></asp:Label>
                    </td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="DDLPoType" runat="server" DataSourceID="SqlDataSource2" 
                            DataTextField="ConcatField" DataValueField="MQ001">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                            SelectCommand="select MQ001,MQ001 + '  ' + MQ034 AS ConcatField from CMSMQ where MQ004 = '1' and MQ005 = '4' and MQ003 between '31' and '36' order by MQ001">
                        </asp:SqlDataSource>
                    </td>
                    <td class="style22" style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="Po No :"></asp:Label>
                    </td>
                    <td class="style13" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtpono" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style24" style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="Item :"></asp:Label>
                    </td>
                    <td class="style14" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtitem" runat="server"></asp:TextBox>
                    </td>
                    <td class="style21" style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Spec :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtspec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style24" style="background-color: #FFFFFF">
                        <asp:Label ID="Label12" runat="server" Text="Supp ID :"></asp:Label>
                    </td>
                    <td class="style15" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtsup" runat="server"></asp:TextBox>
                    </td>
                    <td class="style21" style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style24" style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Delivery From :"></asp:Label>
                    </td>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtfrom" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyyMMdd" TargetControlID="txtfrom">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style21" style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="Delivery To :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtto" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtto_CalendarExtender" runat="server" Enabled="True" 
                            Format="yyyyMMdd" TargetControlID="txtto">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style26">
                        <asp:Button ID="Button1" runat="server" EnableTheming="True" 
                            Text="PO Not Close Show" />
                    </td>
                    <td class="style17">
                        <asp:Button ID="Button2" runat="server" Text="PO Not Close Export" />
                    </td>
                    <td class="style23">
                        </td>
                    <td class="style20">
                        </td>
                </tr>
                <tr>
                    <td class="style28">
                        <asp:Label ID="Label5" runat="server" Text="Number of Item :" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="lblcount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style29">
                        <asp:Label ID="Label8" runat="server" ForeColor="Blue" Text="item"></asp:Label>
                    </td>
                    <td class="style30">
                        </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" DataSourceID="SqlDataSource1" Width="1313px">
                <Columns>
                    <asp:BoundField DataField="TD001" HeaderText="Po Type" SortExpression="TD001">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD002" HeaderText="Po No" SortExpression="TD002">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD004" HeaderText="Item" SortExpression="TD004">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD006" HeaderText="Spec" SortExpression="TD006">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD007" HeaderText="So Type" SortExpression="TD007">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD008" DataFormatString="{0:F}" 
                        HeaderText="Purchase Qty" SortExpression="TD008">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD015" DataFormatString="{0:F}" 
                        HeaderText="Delivery Qty" SortExpression="TD015">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD009" HeaderText="Unit" SortExpression="TD009">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD012" HeaderText="Comfirm Delivery Date" 
                        SortExpression="TD012">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Balance" DataFormatString="{0:F}" 
                        HeaderText="Balance Qty" ReadOnly="True" SortExpression="Balance">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TC004" HeaderText="Sup ID" SortExpression="TC004">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TH007" DataFormatString="{0:F}" 
                        HeaderText="Receipt Qty" SortExpression="TH007">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TH015" DataFormatString="{0:F}" 
                        HeaderText="Accepted Qty" SortExpression="TH015">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PDD" HeaderText="Plan Delivery Date" 
                        SortExpression="PDD">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RDate" HeaderText="Required Date" 
                        SortExpression="RDate">
                    <ItemStyle Width="10px" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                SelectCommand="select L.TD001,L.TD002,L.TD003,RL.TB003,L.TD026,L.TD027,L.TD004,L.TD006,L.TD016,L.TD007,L.TD008,L.TD015,L.TD009
,L.TD012,L.TD008 - L.TD015 as Balance,H.TC004,R.TH007,R.TH015,L.UDF01 as PDD,L.TD012 as CDD,L.TD007 as WH,RL.TB011 AS RDate 
from PURTD L left join PURTC H on(H.TC001 = L.TD001) and (H.TC002 = L.TD002) left join PURTH R on (L.TD001 = R.TH011) 
and (L.TD002 = R.TH012) and (L.TD004 = R.TH004) and (L.TD015 = R.TH007) left join PURTB RL ON(L.TD026 = RL.TB001) 
AND (L.TD027 = RL.TB002) and (L.TD004 = RL.TB004) and (L.TD003 = RL.TB003) where L.TD016 = 'N'">
            </asp:SqlDataSource>

        </ContentTemplate>
        <Triggers>

        <asp:PostBackTrigger ControlID = "Button2"  />

        </Triggers> 
    </asp:UpdatePanel>
    
</asp:Content>
