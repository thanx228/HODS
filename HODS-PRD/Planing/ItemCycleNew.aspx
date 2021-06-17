<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ItemCycleNew.aspx.vb" Inherits="MIS_HTI.ItemCycleNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 71%;
        }
        .style6
        {
        }
        .style13
        {
            width: 149px;
        }
        .style14
        {
            width: 149px;
            height: 26px;
        }
        .style15
        {
            height: 26px;
        }
        .auto-style3 {
            width: 772px;
        }
        .auto-style4 {
            height: 26px;
            width: 772px;
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
            <table style="width: 70%; background-color: #FFFFFF;">
                <tr>
                    <td class="style13">
                        <asp:Label ID="Label3" runat="server" Text="Warehourse :"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:DropDownList ID="DDLWh" runat="server" AutoPostBack="True" 
                            DataSourceID="DataSourceWh" DataTextField="Column1" DataValueField="MC001">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style14">
                        <asp:Label ID="Label7" runat="server" Text="Period"></asp:Label>
                    </td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="ddlPreroid" runat="server" DataSourceID="SqlDataSource2" 
                            DataTextField="preriod" DataValueField="preriod">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style13">
                        <asp:Label ID="Label8" runat="server" Text="List For"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:DropDownList ID="ddlFor" runat="server">
                            <asp:ListItem Selected="True" Value="1">Check </asp:ListItem>
                            <asp:ListItem Value="2">Audit</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style13">
                        <asp:Label ID="Label9" runat="server" Text="Print Label Condition"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:CheckBox ID="cbQty" runat="server" Text="Qty &gt;0" />
                    </td>
                </tr>
            </table>
            <asp:SqlDataSource ID="DataSourceWh" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                SelectCommand=" select MC001,MC001 +'-'+ MC002 from [HOOTHAI].[dbo].[CMSMC] where MC005 = 'Y' order by MC001">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                
                SelectCommand="SELECT SUBSTRING(RunNo, 1, 6) AS preriod FROM ItemCycle GROUP BY SUBSTRING(RunNo, 1, 6) ORDER BY preriod DESC">
            </asp:SqlDataSource>
            <table style="width: 70%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="Busave" runat="server" Text="Generate " Width="100px" />
                        &nbsp;
                        <asp:Button ID="BuSearch" runat="server" Text="Search" Width="100px" />
                        &nbsp;
                        <asp:Button ID="BuPrint" runat="server" style="margin-left: 0px" Text="Print Label" />
                        &nbsp;
                        <asp:Button ID="BuExcel" runat="server" Text="Print List" />
                        &nbsp;<asp:Button ID="BuEx" runat="server" Text="Excel" />
                    </td>
                </tr>
            </table>
            <table style="width: 72%;">
                <tr>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Number Of Item"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="LCount" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Item"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
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
        
        </ContentTemplate>

         <Triggers>
            <asp:PostBackTrigger ControlID="BuEx" />
        </Triggers>

    </asp:UpdatePanel>
   
</asp:Content>
