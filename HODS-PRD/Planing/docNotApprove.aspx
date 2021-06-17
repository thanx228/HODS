<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="docNotApprove.aspx.vb" Inherits="MIS_HTI.docNotApprove" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            width: 58px;
        }
        .style4
        {
            width: 339px;
        }
        .style5
        {
            width: 207px;
        }
        .style6
        {
            
        }
        .style7
        {
            width: 822px;
        }
        .style8
        {
           
        }
        .style16
        {
            width: 63px;
        }
        .style17
        {
            width: 102px;
        }
        .auto-style3 {
            width: 289px;
        }
        .auto-style4 {
            width: 426px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 75%;">
                <tr>
                    <td class="style17" style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Record Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Selected="True" Value="D1">Production Input</asp:ListItem>
                            <asp:ListItem Value="D2">Transfer</asp:ListItem>
                            <asp:ListItem Value="D3">MO Recieve</asp:ListItem>
                            <asp:ListItem Value="D4">Materials Issue</asp:ListItem>
                            <asp:ListItem Value="D5">Inventory Transaction Order</asp:ListItem>
                            <asp:ListItem Value="D6">Purchase Receipt Inspection</asp:ListItem>
                            <asp:ListItem Value="D7">MO Receipt Inspection</asp:ListItem>
                            <asp:ListItem Value="D8">Subcontract Receipt Inspection</asp:ListItem>
                            <asp:ListItem Value="D9">Transfer Inspection</asp:ListItem>
                            <asp:ListItem Value="D10">Sale Return Inspection</asp:ListItem>
                            <asp:ListItem Value="D11">Sales Delivery</asp:ListItem>
                            <asp:ListItem Value="D12">Purchase Receipt</asp:ListItem>
                            <asp:ListItem Value="D13">Loan/Borrow Order</asp:ListItem>
                            <asp:ListItem Value="D14">Loan/Borrow</asp:ListItem>
                            <asp:ListItem Value="D15">Asset PR</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style17" style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="End Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbEndDate" runat="server" BorderColor="#00CCFF" 
                            BorderStyle="Solid"></asp:TextBox>
                        <asp:CalendarExtender ID="tbEndDate_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbEndDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 75%; background-image: url('../Images/btt.jpg'); background-repeat: no-repeat;">
                <tr>
                    <td align="center" class="style8" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btSearch" runat="server" Text="Show Report" />
                    </td>
                </tr>
            </table>
            <table style="width: 75%" class="auto-style3">
                <tr>
                    <td align="center" class="style4">
                        <asp:Label ID="Label4" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" class="auto-style4">
                        <asp:Label ID="lbCnt" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" class="style7">
                        <asp:Label ID="Label6" runat="server" Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
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
