<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="productivity.aspx.vb" Inherits="MIS_HTI.productivity" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
        }
        .style7
        {
            height: 30px;
        }
        .style10
        {
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 33%;">
                <tr>
                    <td 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label10" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Produce Time Report"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="background-color: #FFFFFF">
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label11" runat="server" Text="Show Data"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:DropDownList ID="ddlShowType" runat="server">
                            <asp:ListItem Selected="True" Value="0">Detail</asp:ListItem>
                            <asp:ListItem Value="1">Summary</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label1" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:DropDownList ID="ddlWC" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label2" runat="server" Text="Transfer Date From"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:TextBox ID="tbDateFrom" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateFrom" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label3" runat="server" Text="Transfer Date To"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:TextBox ID="tbDateTo" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateTo" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="style7" colspan="2" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width: 33%;">
                <tr>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label12" runat="server" ForeColor="Blue" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="lbShow" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label13" runat="server" ForeColor="Blue" Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
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
