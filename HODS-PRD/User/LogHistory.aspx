<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="LogHistory.aspx.vb" Inherits="MIS_HTI.LogHistory" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label10" runat="server" ForeColor="Blue" Text="LOG History"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Show Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Value="1">Log In History</asp:ListItem>
                            <asp:ListItem Value="2">Menu History</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="Computer Name"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCom" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="User Name"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbUser" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Menu Name"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbMenu" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="IP Address"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbAdd" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDate" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Amount of Row"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="Rows"></asp:Label>
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
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
    <br />
</asp:Content>
