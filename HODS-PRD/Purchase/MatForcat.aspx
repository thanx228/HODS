<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MatForcat.aspx.vb" Inherits="MIS_HTI.MatForcat" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px;
        }
        .style7
        {
            width: 141px;
        }
        .style8
        {
            height: 21px;
            width: 141px;
        }
        .style9
        {
            width: 220px;
        }
        .style10
        {
            height: 21px;
            width: 220px;
        }
        .style11
        {
            width: 272px;
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
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Customer Code"></asp:Label>
                    </td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCust" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="FG Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label13" runat="server" Text="Date From"></asp:Label>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <asp:TabContainer ID="tcShow" runat="server" ActiveTabIndex="0" Height="150px" 
                Width="75%">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        Mat,SP and Other
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Mat Type"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTypeMat" runat="server">
                                        <asp:ListItem Value="0">All Mat</asp:ListItem>
                                        <asp:ListItem Value="1">Materials</asp:ListItem>
                                        <asp:ListItem Value="4">SP and Other</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Text="Include"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="cbAero" runat="server" Text="None Aero" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label15" runat="server" Text="Mat Item"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbMatItem" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label16" runat="server" Text="Mat Desc"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbMatDesc" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Text="Mat Spec"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbMatSpec" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label18" runat="server" Text="Report Type"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportType" runat="server">
                                        <asp:ListItem Selected="True" Value="1">Internal</asp:ListItem>
                                        <asp:ListItem Value="2">External</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    <HeaderTemplate>
                        Outsource
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td class="style12">
                                    <asp:Label ID="Label19" runat="server" Text="Vendor Code"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbVenCode" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        align="center">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" 
                            style="height: 26px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Report"  />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" class="style11" style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Amount of Rows"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label12" runat="server" Text="Rows"></asp:Label>
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
</asp:Content>
