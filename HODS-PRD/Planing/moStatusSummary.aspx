<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="moStatusSummary.aspx.vb" Inherits="MIS_HTI.moStatusSummary" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            width: 142px;
        }
        .style4
        {
            width: 49px;
        }
        .style5
        {
            width: 78px;
        }
        .style6
        {
            width: 760px;
        }
        .style7
        {
            width: 496px;
        }
        .style8
        {
            width: 221px;
        }
        .style9
        {
            width: 496px;
            height: 24px;
        }
        .style10
        {
            width: 760px;
            height: 24px;
        }
        .style11
        {
            width: 221px;
            height: 24px;
        }
        .style12
        {
            width: 78px;
            height: 24px;
        }
        .style14
        {
            width: 78px;
        }
        .style15
        {
            width: 68px;
        }
        .style16
        {
            width: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:65%;">
                <tr>
                    <td align="left" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        class="style15">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="MO Status Summary"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 65%; background-color: #FFFFFF;">
                <tr>
                    <td class="style9">
                        <asp:Label ID="Label5" runat="server" Text="Show "></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:DropDownList ID="ddlShow" runat="server">
                            <asp:ListItem Selected="True" Value="1">MO Summary</asp:ListItem>
                            <asp:ListItem Value="2">MO Delay</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style11">
                        <asp:Label ID="Label6" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td class="style12">
                        <asp:DropDownList ID="ddlSoType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label8" runat="server" Text="Date Type"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="ddlDateType" runat="server">
                            <asp:ListItem Selected="True" Value="1">Plan Date</asp:ListItem>
                            <asp:ListItem Value="2">Actual Date</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style12">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label7" runat="server" Text="Date From"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox ID="tbDateFrom" runat="server" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px" Width="70px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateFrom" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Label9" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td class="style14">
                        <asp:TextBox ID="tbDateTo" runat="server" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px" Width="70px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateTo" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:65%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                    </td>
                </tr>
            </table>
            <table style="width:65%;">
                <tr>
                    <td align="center" 
                        style="background-color: #FFFFFF; border: thin solid #00CCFF">
                        <asp:Label ID="Label2" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" 
                        style="background-color: #FFFFFF; border: thin solid #00CCFF">
                        <asp:Label ID="lbCount" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" 
                        style="background-color: #FFFFFF; border: thin solid #00CCFF" 
                        class="style16">
                        <asp:Label ID="Label4" runat="server" Text="รายการ"></asp:Label>
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
