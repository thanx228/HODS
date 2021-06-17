<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MaterialsOverIssue.aspx.vb" Inherits="MIS_HTI.MaterialsOverIssue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style5
        {
            width: 78px;
        }
        .style7
        {
            width: 78px;
        }
        .style9
        {
            width: 78px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:70%;">
                <tr>
                    <td align="left" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label6" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Materials Over Issues"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 70%;">
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label1" runat="server" Text="MO Status"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlMoStatus" runat="server">
                            <asp:ListItem Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1">Closed</asp:ListItem>
                            <asp:ListItem Selected="True" Value="2">Manufacturing</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Over Issue"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlOverIssue" runat="server">
                            <asp:ListItem Value="0">ALL Over &gt; 0 %</asp:ListItem>
                            <asp:ListItem Value="1">&lt;5 %</asp:ListItem>
                            <asp:ListItem Value="2">6-10 %</asp:ListItem>
                            <asp:ListItem Value="3">11-15 %</asp:ListItem>
                            <asp:ListItem Value="4">16-20 %</asp:ListItem>
                            <asp:ListItem Value="5">21-25 %</asp:ListItem>
                            <asp:ListItem Value="6">26-30 %</asp:ListItem>
                            <asp:ListItem Value="7">&gt;30%</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Code TYpe"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlCodeType" runat="server">
                            <asp:ListItem Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1">Raw Materials</asp:ListItem>
                            <asp:ListItem Value="3">Semi Part</asp:ListItem>
                            <asp:ListItem Value="4">Spare Part</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlWc" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="MO Date From"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="TO"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 70%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btSave" runat="server" Text="Show Report" Width="100px" />
                        <asp:Button ID="btExcel" runat="server" Text="Export Excel" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width: 544px;">
                <tr>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" ForeColor="Blue" 
                            Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" ForeColor="Blue" 
                            Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Font-Size="Medium" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
