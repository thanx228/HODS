<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SumScrap.aspx.vb" Inherits="MIS_HTI.SumScrap" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 36%;">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" ForeColor="Blue" 
                            Text="Scrap Summary"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class="style4">
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Property :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="DDLProperty" runat="server" AutoPostBack="True">
                            <asp:ListItem>1:Work Center</asp:ListItem>
                            <asp:ListItem>2:Outsource Supplier</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
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
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Work Center :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="DDLWC" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Suppliers :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="TextBox5" runat="server" ReadOnly="True"></asp:TextBox>
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
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Status :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="DDLStatus" runat="server" AutoPostBack="True">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem Value="Y">Approved</asp:ListItem>
                            <asp:ListItem Value="N">Not Approved</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
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
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Document Date From :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtfrom" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyyMMdd" TargetControlID="txtfrom">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="To :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtto" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtto_CalendarExtender" runat="server" Enabled="True" 
                            Format="yyyyMMdd" TargetControlID="txtto">
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
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="lblcount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="รายการ"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Button ID="BuSearch" runat="server" Text="Search" />
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
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" 
                DataSourceID="DataSourceScrap" Width="531px">
                <Columns>
                    <asp:ButtonField CommandName="OnClick" HeaderText=" Detail " Text="Detail" />
                    <asp:BoundField DataField="TC001" HeaderText=" Transfer Type " SortExpression="TC001" />
                    <asp:BoundField DataField="TC002" HeaderText=" Transfer No " SortExpression="TC002" />
                    <asp:BoundField DataField="TC023" HeaderText=" Work Center " SortExpression="TC023" />
                    <asp:BoundField DataField="TC014" HeaderText=" Total MO Qty " SortExpression="TC014" DataFormatString ={0:F3} />
                    <asp:BoundField DataField="TC016" HeaderText=" Total Scrap Qty " SortExpression="TC016" DataFormatString = {0:F3} />
                    <asp:BoundField DataField="Scrap" HeaderText=" Scrap % " ReadOnly="True"  DataFormatString = {0:F3}
                        SortExpression="Scrap" />
                    <asp:BoundField DataField="ScrapAmt" HeaderText=" Scrap Amt. " ReadOnly="True"  DataFormatString = {0:F3}
                        SortExpression="ScrapAmt" />
                </Columns>
                <HeaderStyle Wrap="False" />
                <RowStyle Wrap="False" />
            </asp:GridView>
            <asp:SqlDataSource ID="DataSourceScrap" runat="server" 
                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" SelectCommand="select TC001,TC002,TC023,TC014,TC016,(TC014 * TC016)/100 as Scrap,SUM(TC016) as ScrapAmt  from SFCTC 
group by TC023,TC014,TC016,TC001,TC002 "></asp:SqlDataSource>
            <table class="style4">
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Detail Report"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txttype" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtno" runat="server" Visible="False"></asp:TextBox>
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
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                DataSourceID="DataSourceDetail">
                <Columns>
                    <asp:BoundField DataField="TC023" HeaderText="Work Center" 
                        SortExpression="TC023" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TC005" HeaderText=" MO No. " SortExpression="TC005" />
                    <asp:BoundField DataField="TC047" HeaderText=" Item " SortExpression="TC047" />
                    <asp:BoundField DataField="TC049" HeaderText=" Spec " SortExpression="TC049" />
                    <asp:BoundField DataField="TC014" HeaderText=" Mo Qty " SortExpression="TC014" DataFormatString = {0:F3} />
                    <asp:BoundField DataField="TC016" HeaderText=" Scrap Qty " SortExpression="TC016" DataFormatString = {0:F3} />
                    <asp:BoundField DataField="Scrap" HeaderText=" Scrap % " ReadOnly="True" DataFormatString = {0:F3} 
                        SortExpression="Scrap" />
                    <asp:BoundField DataField="ScrapAmt" HeaderText=" Scrap Amt. " ReadOnly="True" DataFormatString = {0:F3}
                        SortExpression="ScrapAmt" />
                </Columns>
                <HeaderStyle Wrap="False" />
                <RowStyle Wrap="False" />
            </asp:GridView>
            <asp:SqlDataSource ID="DataSourceDetail" runat="server" 
                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" SelectCommand="select TC023,TC005,TC047,TC049,TC014,TC016,(TC014 * TC016)/100 as Scrap,SUM(TC016) as ScrapAmt  from SFCTC 
where TC001 = @type and TC002 = @No
group by TC023,TC005,TC047,TC049,TC014,TC016
">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txttype" Name="type" PropertyName="Text" />
                    <asp:ControlParameter ControlID="txtno" Name="No" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
            <br />
           
        </ContentTemplate>
    </asp:UpdatePanel>
   <br />
   <br />

</asp:Content>
