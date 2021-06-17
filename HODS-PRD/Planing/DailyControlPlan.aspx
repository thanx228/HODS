<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="DailyControlPlan.aspx.vb" Inherits="MIS_HTI.DailyControlPlan" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            height: 23px;
        }
        .style6
        {
            height: 23px;
            width: 104px;
        }
        .style7
        {
            height: 23px;
            width: 32px;
        }
        .style8
        {
            height: 31px;
            width: 544px;
        }
        .style9
        {
            height: 23px;
            width: 137px;
        }
        .style10
        {
            height: 30px;
        }
        .style11
        {
            width: 126px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:75%; ">
                <tr>
                    <td align="left" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Daily Control Plan"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlWC" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td class="style11" style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; vertical-align: top;">
                        <asp:Label ID="Label12" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td colspan="3" style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblMoType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label16" runat="server" Text="Cust Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCust" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px" Width="50px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label14" runat="server" Text="Sale Type"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlSaleType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCode" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlDate" runat="server">
                            <asp:ListItem Value="1">Plan Start Date From</asp:ListItem>
                            <asp:ListItem Value="2">Trn Date From</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px" BackColor="White" 
                            BorderColor="#00CCFF" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateFrom" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px" BackColor="White" 
                            BorderColor="#00CCFF" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateTo" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label18" runat="server" Text="Plan Date Start"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbPlanDate" runat="server" BackColor="White" 
                            BorderColor="#00CCFF" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbPlanDate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbPlanDate">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td class="style11" style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width:66%;">
                <tr>
                    <td align="center" class="style10" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;
                        <asp:Button ID="btExportExcel" runat="server" Height="26px" Text="Export Excel" 
                            Width="100px" />
                        &nbsp; &nbsp;
                    </td>
                </tr>
            </table>
            <table style="border: thin solid #00CCFF; background-color: #FFFFFF; " 
                class="style8">
                <tr>
                    <td align="center" class="style9">
                        <asp:Label ID="Label5" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" class="style6">
                        <asp:Label ID="lbCount" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" class="style7">
                        <asp:Label ID="Label7" runat="server" Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Label ID="Label17" runat="server" Text="Capacity="></asp:Label>
            <asp:Label ID="lbCapa" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" 
                    Wrap="False" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
    </asp:Content>
