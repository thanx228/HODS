<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="WorkDay.aspx.vb" Inherits="MIS_HTI.WorkDay" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            width: 129%;
        }
        .style4
        {
            width: 127px;
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
            <table style="width: 47%;">
                <tr>
                    <td align="left" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label4" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Work Hour Work Center"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 47%; background-color: #FFFFFF;">
                <tr>
                    <td class="style4">
                        <asp:Label ID="Label1" runat="server" Text="Work Center :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLWC" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        <asp:Label ID="Label2" runat="server" Text="Date Form :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtdate1_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtdate1">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        <asp:Label ID="Label3" runat="server" Text="Date To :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtdate2" runat="server" Width="80px"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtdate2_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtdate2">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 47%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="BuSearch" runat="server" Text="Search" Width="100px" />
                        &nbsp;<asp:Button ID="Bureport" runat="server" Text="View Report" Visible="False" 
                            Width="100px" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" CellPadding="4" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" Width="384px">
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
    </asp:UpdatePanel>
</asp:Content>
