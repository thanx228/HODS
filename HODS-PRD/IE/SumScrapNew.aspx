<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SumScrapNew.aspx.vb" Inherits="MIS_HTI.SumScrapNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .style7
        {
            width: 141px;
        }
        .style9
        {
            width: 220px;
        }
        .style8
        {
            height: 21px;
            width: 141px;
        }
        .style10
        {
            height: 21px;
            }
        .style6
        {
            height: 21px;
        }
        .style11
        {
            width: 272px;
        }
        .style12
        {
            width: 137px;
        }
        .style13
        {
            height: 21px;
            width: 137px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table style="width: 75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label15" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="Scrap Summary"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Property :"></asp:Label>
                    </td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlProp" runat="server" AutoPostBack="true">
                            <asp:ListItem Value="1">1: Work Center</asp:ListItem>
                            <asp:ListItem Value="2">2: Outsource Supplier</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style12" style="background-color: #FFFFFF">
                        <asp:Label ID="Label20" runat="server" Text="Suppliers :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSupp" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label17" runat="server" Text="Work Center :"></asp:Label>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF" colspan="3">
                        <asp:CheckBoxList ID="cblWC" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="style7" style="background-color: #FFFFFF">
                        Document Date From :</td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtfrom" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtfrom">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style12" style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="To :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtto" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtto_CalendarExtender" runat="server" Enabled="True" 
                            TargetControlID="txtto">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Report" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
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
<br />
<br />
<br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
