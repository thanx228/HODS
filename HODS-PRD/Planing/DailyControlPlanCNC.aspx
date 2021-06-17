<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="DailyControlPlanCNC.aspx.vb" Inherits="MIS_HTI.DailyControlPlanCNC" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
        }
        .style6
        {
          
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
           
        }
        .style10
        {
            height: 30px;
        }
        .style11
        {
            width: 126px;
        }
        .auto-style9 {
            width: 13%;
        }
        .auto-style16 {
            width: 135px;
        }
        .auto-style17 {
            width: 56%;
        }
        .auto-style18 {
            width: 27%;
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
                    <td style="background-color: #FFFFFF" class="auto-style18">
                        <asp:Label ID="Label2" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style16">
                        <asp:DropDownList ID="ddlWC" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style17">
                        &nbsp;</td>
                    <td class="auto-style9" style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; vertical-align: top;" class="auto-style18">
                        <asp:Label ID="Label12" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td colspan="3" style="background-color: #FFFFFF; vertical-align: top;">
                        <asp:CheckBoxList ID="cblMoType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style18">
                        <asp:Label ID="Label16" runat="server" Text="Cust Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style16">
                        <asp:TextBox ID="tbCust" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px" Width="50px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style17">
                        <asp:Label ID="Label14" runat="server" Text="Sale Type"></asp:Label>
                    </td>
                    <td class="auto-style9" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlSaleType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style18">
                        <asp:Label ID="Label3" runat="server" Text="Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style16">
                        <asp:TextBox ID="tbCode" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style17">
                        <asp:Label ID="Label4" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td class="auto-style9" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style18">
                        <asp:DropDownList ID="ddlDate" runat="server">
                            <asp:ListItem Value="1">Plan Start Date From</asp:ListItem>
                            <asp:ListItem Value="2">Trn Date From</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style16">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px" BackColor="White" 
                            BorderColor="#00CCFF" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateFrom" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style17">
                        <asp:Label ID="Label11" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td class="auto-style9" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px" BackColor="White" 
                            BorderColor="#00CCFF" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateTo" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style18">
                        <asp:Label ID="Label18" runat="server" Text="Plan Date Start"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style16">
                        <asp:TextBox ID="tbPlanDate" runat="server" BackColor="White" 
                            BorderColor="#00CCFF" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbPlanDate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbPlanDate">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style17">
                        &nbsp;</td>
                    <td class="auto-style9" style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width:75%; background-image: url('../Images/btt.jpg'); background-repeat: repeat-x;">
                <tr>
                    <td align="center" class="style10" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;
                        <asp:Button ID="btExportExcel" runat="server" Text="Export Excel" />
                        &nbsp; &nbsp;
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="ucRowCount" runat="server" />
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
