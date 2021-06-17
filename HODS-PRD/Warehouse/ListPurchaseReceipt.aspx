<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ListPurchaseReceipt.aspx.vb" Inherits="MIS_HTI.ListPurchaseReceipt" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 20px;
        }
        .style7
        {
            height: 20px;
            width: 136px;
        }
        .style9
        {
            width: 180px;
        }
        .style10
        {
            width: 177px;
        }
        .style11
        {
            width: 136px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label3" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="List Purchase Receipt"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td class="style7" style="background-color: #FFFFFF; vertical-align: top;">
                        <asp:Label ID="Label9" runat="server" Text="PO Type"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF" colspan="3">
                        <asp:CheckBoxList ID="cblPOType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style11">
                        <asp:Label ID="lbVendor" runat="server" Text="Vendor ID"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbVendor" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="Approval Indicator"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblApp" runat="server" RepeatColumns="4">
                            <asp:ListItem Value="Y">Y:Approved</asp:ListItem>
                            <asp:ListItem Value="N">N:Not Approved</asp:ListItem>
                            <asp:ListItem Value="U">U:Approved Failed</asp:ListItem>
                            <asp:ListItem Value="V">V:Canceled</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Receipt Date From"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Receipt Date To"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btPrint" runat="server" Text="Print Report" />
                    </td>
                </tr>
            </table>
            <table style="width:75%; background-color: #FFFFFF;">
                <tr>
                    <td align="center" class="style9">
                        <asp:Label ID="Label6" runat="server" Text="Amount Of Rows"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label8" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TG001" HeaderText="Receipt No" />
                    <asp:BoundField DataField="TG014" HeaderText="Date" />
                    <asp:BoundField DataField="TG005" HeaderText="Vendor" />
                    <asp:BoundField DataField="TG016" HeaderText="Invoice No." />
                    <asp:BoundField DataField="TH011" HeaderText="PO No." />
                    <asp:BoundField DataField="TG013" HeaderText="App Status" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    HorizontalAlign="Center" />
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
