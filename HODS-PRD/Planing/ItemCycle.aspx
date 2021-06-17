<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ItemCycle.aspx.vb" Inherits="MIS_HTI.ItemCycle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        .style6
        {
        }
        .style7
        {
            width: 46px;
        }
        .style8
        {
        }
        .style9
        {
            width: 3px;
        }
        .style10
        {
            width: 62px;
        }
        .style11
        {
            width: 4px;
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
            <table class="style4">
                <tr>
                    <td class="style6" colspan="3">
                        <asp:Label ID="Label2" runat="server" ForeColor="#CC0000" 
                            Text="Item Cycle Count Card" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    </td>
                    <td class="style11">
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
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style10">
                        <asp:Label ID="Label3" runat="server" Text="Warehourse :"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:DropDownList ID="DDLWh" runat="server" AutoPostBack="True" 
                            DataSourceID="DataSourceWh" DataTextField="Column1" DataValueField="MC001">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="DataSourceWh" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                            SelectCommand=" select MC001,MC001 ++ MC002 from [JINPAO80].[dbo].[CMSMC] where MC005 = 'Y'">
                        </asp:SqlDataSource>
                    </td>
                    <td class="style9">
                        <asp:Button ID="BuSearch" runat="server" Text="Search" />
                    </td>
                    <td class="style11">
                        <asp:Button ID="Busave" runat="server" Text="Save" />
                    </td>
                    <td>
                        <asp:Button ID="BuExcel" runat="server" Text="Excel" />
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
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style8" colspan="2">
                        <asp:Label ID="Label4" runat="server" Text="Number Of Item"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:Label ID="LCount" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                    </td>
                    <td class="style11">
                        <asp:Label ID="Label6" runat="server" Text="Item"></asp:Label>
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
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" 
                DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" 
                GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="MB001" HeaderText="Item" 
                        SortExpression="MB001" />
                    <asp:BoundField DataField="MB002" HeaderText="Desc" SortExpression="MB002" />
                    <asp:BoundField DataField="MB003" HeaderText="Spec" SortExpression="MB003" />
                    <asp:BoundField DataField="MC007" HeaderText="Inventory Qty" SortExpression="MC007" DataFormatString="{0:F3}" />
                    <asp:BoundField DataField="MB004" HeaderText="Unit" SortExpression="MB004" />
                    <asp:BoundField DataField="MC002" HeaderText="Wh" SortExpression="MC002" />

                    <asp:BoundField HeaderText="Count Qt'y" />
                    <asp:BoundField HeaderText="Count By" />

                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                SelectCommand="select MB001,MB002,MB003,MB004,MC007,MC002 from INVMC C 
left join INVMB B on(B.MB001 =C.MC001) and MC007 > 0 ">
            </asp:SqlDataSource>
<br />
        
        </ContentTemplate>
    </asp:UpdatePanel>
   
</asp:Content>
