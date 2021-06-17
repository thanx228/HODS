<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="TransferReport.aspx.vb" Inherits="MIS_HTI.TransferReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            height: 21px;
        }
        .style5
        {
            height: 24px;
        }
        .style6
        {
            height: 24px;
            width: 81px;
        }
        .style7
        {
            width: 81px;
        }
        .style8
        {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 70%;">
                <tr>
                    <td class="style4" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label5" runat="server" ForeColor="Blue" 
                            Text="Transfer Outsource Report" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 497px">
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label1" runat="server" Text="Type"></asp:Label>
                    </td>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlTrnType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Vendor"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbVendor" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Transfer Date From"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbTrnDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbTrnDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbTrnDateFrom" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Transfer Date To"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style7">
                        <asp:TextBox ID="tbTrnDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbTrnDateTo_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbTrnDateTo"  Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Record Per Page"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style7">
                        <asp:DropDownList ID="ddlRecordPerPage" runat="server">
                            <asp:ListItem Selected="True" Value="5"></asp:ListItem>
                            <asp:ListItem Value="10"></asp:ListItem>
                            <asp:ListItem Value="13"></asp:ListItem>
                            <asp:ListItem Value="15"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table style="width: 70%;">
                <tr>
                    <td align="center" class="style4" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        <asp:Button ID="btPrint" runat="server" Text="Print Report" />
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
