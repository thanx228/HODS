<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PrNotOpenPo.aspx.vb" Inherits="MIS_HTI.PrNotOpenPo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            width: 114%;
        }
        .style12
        {
            width: 179px;
            height: 38px;
        }
        .style13
        {
            height: 38px;
        }
        .style15
        {
            width: 99px;
            height: 38px;
        }
        .style16
        {
            width: 67px;
            height: 38px;
        }
        .style18
        {
            width: 184px;
            height: 38px;
        }
        .style19
        {
            width: 67px;
            height: 28px;
        }
        .style20
        {
            width: 184px;
            height: 28px;
        }
        .style21
        {
            width: 99px;
            height: 28px;
        }
        .style22
        {
            width: 179px;
            height: 28px;
        }
        .style23
        {
            height: 28px;
        }
        .style24
        {
            width: 67px;
            height: 29px;
        }
        .style25
        {
            width: 184px;
            height: 29px;
        }
        .style26
        {
            width: 99px;
            height: 29px;
        }
        .style27
        {
            width: 179px;
            height: 29px;
        }
        .style28
        {
            height: 29px;
        }
        .style29
        {
            width: 67px;
            height: 30px;
        }
        .style30
        {
            width: 184px;
            height: 30px;
        }
        .style31
        {
            width: 99px;
            height: 30px;
        }
        .style32
        {
            width: 179px;
            height: 30px;
        }
        .style33
        {
            height: 30px;
        }
        .style34
        {
            width: 67px;
            height: 25px;
        }
        .style35
        {
            width: 184px;
            height: 25px;
        }
        .style36
        {
            width: 99px;
            height: 25px;
        }
        .style37
        {
            width: 179px;
            height: 25px;
        }
        .style38
        {
            height: 25px;
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
                    <td bgcolor="#990000" align="center">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
                            ForeColor="White" Text="Purchase Request Not Open Purchase Order"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class="style3">
                <tr>
                    <td class="style29">
                        <asp:Label ID="Label2" runat="server" Text="SO Type :"></asp:Label>
                    </td>
                    <td class="style30">
                        <asp:DropDownList ID="DDLType" runat="server" AutoPostBack="True">
                            <asp:ListItem Value="0">All</asp:ListItem>
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
                              <asp:ListItem Value="3112">3112 : Supply Product</asp:ListItem>
                                <asp:ListItem Value="3113">3113 : Supply Other</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style31">
                        </td>
                    <td class="style32">
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                            SelectCommand="select MQ001,MQ001 + '  ' + MQ002 AS ConcatField from CMSMQ where MQ004 = '1' and MQ005 = '4' and MQ003 between '21' and '25' order by MQ001">
                        </asp:SqlDataSource>
                    </td>
                    <td class="style33">
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:JPDEMO80ConnectionString %>" 
                            SelectCommand="select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002">
                        </asp:SqlDataSource>
                        </td>
                </tr>
                <tr>
                    <td class="style24">
                        <asp:Label ID="Label10" runat="server" Text="PR Type :"></asp:Label>
                    </td>
                    <td class="style25">
                        <asp:TextBox ID="txttype" runat="server"></asp:TextBox>
                    </td>
                    <td class="style26">
                        <asp:Label ID="Label11" runat="server" Text="PR No :"></asp:Label>
                    </td>
                    <td class="style27">
                        <asp:TextBox ID="txtno" runat="server"></asp:TextBox>
                    </td>
                    <td class="style28">
                        </td>
                </tr>
                <tr>
                    <td class="style34">
                        <asp:Label ID="Label12" runat="server" Text="Item :"></asp:Label>
                    </td>
                    <td class="style35">
                        <asp:TextBox ID="txtitem" runat="server"></asp:TextBox>
                    </td>
                    <td class="style36">
                        <asp:Label ID="Label13" runat="server" Text="Spec :"></asp:Label>
                    </td>
                    <td class="style37">
                        <asp:TextBox ID="txtspec" runat="server"></asp:TextBox>
                    </td>
                    <td class="style38">
                        </td>
                </tr>
                <tr>
                    <td class="style19">
                        <asp:Label ID="Label14" runat="server" Text="Date Issue :"></asp:Label>
                    </td>
                    <td class="style20">
                        <asp:TextBox ID="txtdateissue" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtdateissue_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtdateissue">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style21">
                        <asp:Label ID="Label15" runat="server" Text="Date Required :"></asp:Label>
                    </td>
                    <td class="style22">
                        <asp:TextBox ID="txtrequest" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtrequest_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtrequest">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style23">
                        </td>
                </tr>
                <tr>
                    <td class="style29">
                        <asp:Label ID="Label9" runat="server" Text="Approve :"></asp:Label>
                    </td>
                    <td class="style30">
                        <asp:DropDownList ID="DDLApp" runat="server" AutoPostBack="True">
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CheckBoxList ID="CblApp" runat="server">
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    <td class="style31">
                        <asp:Button ID="BuView" runat="server" Text="View Data" />
                    </td>
                    <td class="style32">
                        <asp:Button ID="BuExport" runat="server" Text="Export Data" />
                    </td>
                    <td class="style33">
                        </td>
                </tr>
                <tr>
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style18">
                        <asp:Label ID="Label6" runat="server" ForeColor="#0000CC" 
                            Text="Number of items."></asp:Label>
                    </td>
                    <td class="style15">
                        <asp:Label ID="lblcount" runat="server" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                    </td>
                    <td class="style12">
                        <asp:Label ID="Label7" runat="server" ForeColor="#0000CC" Text="Item"></asp:Label>
                    </td>
                    <td class="style13">
                        &nbsp;</td>
                </tr>
            </table>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                DataSourceID="SqlDataSource2" 
                PageSize="20" Width="816px" BackColor="White" BorderColor="#3366CC" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>

                    <asp:BoundField DataField="TB001" HeaderText="PR Type" 
                        SortExpression="TB001" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TB002" HeaderText="PR No." 
                        SortExpression="TB002" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TB004" HeaderText="Item" SortExpression="TB004" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TB005" HeaderText="Description" 
                        SortExpression="TB005" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TB006" HeaderText="Spec" SortExpression="TB006" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TB007" HeaderText="Unit" SortExpression="TB007" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TB008" HeaderText="SO Type." SortExpression="TB008" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TB009" HeaderText="PR Quantity" 
                        SortExpression="TB009" DataFormatString="{0:F}" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TB010" HeaderText="Cust ID." SortExpression="TB010" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TB011" HeaderText="Required Date" 
                        SortExpression="TB011" >
                    <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TA013" HeaderText="Issue Date" 
                        SortExpression="TA013" >

                    <ItemStyle Width="20px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="TA007" HeaderText="Approve Status" 
                        SortExpression="TA007" />

                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle Wrap="True" BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                
                
                SelectCommand="select L.TB001,L.TB002,L.TB004,L.TB005,L.TB006,L.TB007,L.TB008,L.TB009,L.TB010,H.TA007,(SUBSTRING(L.TB011,7,2)+'/'+SUBSTRING(L.TB011,5,2)+'/'+SUBSTRING(L.TB011,1,4)) as TB011,(SUBSTRING(H.TA013,7,2)+'/'+SUBSTRING(H.TA013,5,2)+'/'+SUBSTRING(H.TA013,1,4)) as TA013 from PURTB L left join PURTA H on(L.TB001 = H.TA001) and (L.TB002 = H.TA002) where L.TB039 = 'N' and L.TB008 in ('2201') or L.TB008 in ('3112','3113')">
            </asp:SqlDataSource>

        </ContentTemplate>

      <Triggers>
      
      <asp:PostBackTrigger ControlID = "BuExport" />
      </Triggers>

    </asp:UpdatePanel>
    </asp:Content>
