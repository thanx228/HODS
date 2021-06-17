<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="DailyMachinePlan.aspx.vb" Inherits="MIS_HTI.DailyMachinePlan" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            
        }
        .style6
        {
            width: 104px;
            height: 25px;
        }
        .style7
        {
            width: 32px;
            height: 25px;
        }
        .style8
        {
            
        }
        .style9
        {
            width: 137px;
            height: 25px;
        }
        .style10
        {
 
        }
        .style11
        {
            width: 126px;
        }
        .style12
        {
            width: 822px;
            height: 23px;
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
                    <td align="center" class="style12" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Daily Machine Plan"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Property"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlProperty" runat="server">
                            <asp:ListItem Selected="True" Value="1">Internal(Work Center)</asp:ListItem>
                            <asp:ListItem Value="2">Outsource(Supplier)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="Suplier"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbWC" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px" MaxLength="5" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlWC" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label13" runat="server" Text="Sale Type"></asp:Label>
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
                        <asp:Label ID="Label10" runat="server" Text="Date From"></asp:Label>
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
                    <td align="justify" style="background-color: #FFFFFF;">
                        <asp:Label ID="Label12" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td colspan="3" style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblMoType" runat="server" ForeColor="#0033CC" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow" RepeatColumns="7">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" class="style10" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Search" Width="100px" />
                        &nbsp;
                        <asp:Button ID="btExportGrid" runat="server" Height="26px" Text="Export Excel" 
                            Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="border: thin solid #00CCFF; background-color: #FFFFFF; width:75%;"  >
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
            <asp:PostBackTrigger ControlID="btExportGrid" />
        </Triggers>
    </asp:UpdatePanel>
    </asp:Content>
