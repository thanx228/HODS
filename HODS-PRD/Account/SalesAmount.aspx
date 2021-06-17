<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SalesAmount.aspx.vb" Inherits="MIS_HTI.SalesAmount" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 684px;
        }
        .style6
        {
            width: 84px;
        }
        .style7
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
                    <td colspan="2">
                        <asp:Label ID="Label2" runat="server" ForeColor="Blue" Text="Sales Amount" 
                            Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style7">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label3" runat="server" Text="From Date :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtfrom" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyyMMdd" TargetControlID="txtfrom">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="To Date :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtto" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtto_CalendarExtender" runat="server" Enabled="True" Format="yyyyMMdd" 
                            TargetControlID="txtto">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style7">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label5" runat="server" Text="Area :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLArea" runat="server" AutoPostBack="True" 
                            DataSourceID="DataSourceArea" DataTextField="name" DataValueField="id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="DataSourceArea" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                            SelectCommand="SELECT * FROM [Area]"></asp:SqlDataSource>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Group :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLGroup" runat="server" AutoPostBack="True" 
                            DataSourceID="SqlDataSource2" DataTextField="name" DataValueField="id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                            SelectCommand="SELECT * FROM [GroupProduct] order by id desc"></asp:SqlDataSource>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style7">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label7" runat="server" Text="Credit Term :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLCredit" runat="server" AutoPostBack="True" 
                            DataSourceID="lDataSourceCredit" DataTextField="name" 
                            DataValueField="name">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="lDataSourceCredit" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                            SelectCommand="SELECT * FROM [CreditTerm]"></asp:SqlDataSource>
                    </td>
                    <td>
                        <asp:Button ID="BuSearch" runat="server" Text="Search" />
                    </td>
                    <td>
                        <asp:ImageButton ID="IBuExcel" runat="server" 
                            ImageUrl="~/Images/Excel_icon.gif" Visible="False" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style7">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lbCount" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style7">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" 
                GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
<br />
<br />
<br />
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger  ControlID="IBuExcel"  />
        </Triggers>
    </asp:UpdatePanel>
    <p>
        <br />
    </p>
</asp:Content>
