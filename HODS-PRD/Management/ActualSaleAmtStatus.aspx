<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ActualSaleAmtStatus.aspx.vb" Inherits="MIS_HTI.ActualSaleAmtStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            height: 22px;
        }
        .style6
        {
            width: 195px;
        }
        .style7
        {
            height: 22px;
            width: 195px;
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
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Actual Sale Amount Status"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Sale Section"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="ddlSaleType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        <asp:Label ID="Label3" runat="server" Text="Sale Date From"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Sale Date To"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox ID="tbDateTo" runat="server" style="margin-top: 0px" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 36%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
