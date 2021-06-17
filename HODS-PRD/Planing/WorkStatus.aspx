<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="WorkStatus.aspx.vb" Inherits="MIS_HTI.WorkStatus" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            height: 27px;
            width: 99px;
        }
        .style34
        {
            height: 27px;
            width: 78px;
        }
        .style38
        {
            height: 27px;
            width: 53px;
        }
        .style40
        {
            height: 21px;
            width: 99px;
        }
        .style41
        {
            height: 27px;
            width: 38px;
        }
        .style42
        {
            height: 21px;
            width: 38px;
        }
        .style43
        {
            height: 21px;
            width: 53px;
        }
        .style44
        {
            height: 21px;
            width: 78px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 95%;">
                <tr>
                    <td class="style34" style="background-color: #FFFFFF">
                        <asp:Label ID="Label1" runat="server" Text="Sale Type"></asp:Label>
                    </td>
                    <td class="style41" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlSaleType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="style38" style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Sale No"></asp:Label>
                    </td>
                    <td class="style4" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSaleNo" runat="server" BorderColor="Black" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style44" style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Sale Seq"></asp:Label>
                    </td>
                    <td class="style42" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSaleSeq" runat="server" BorderColor="Black" 
                            Width="34px"></asp:TextBox>
                    </td>
                    <td class="style43" style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="Customer"></asp:Label>
                    </td>
                    <td class="style40" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCust" runat="server" BorderColor="Black" 
                            Width="52px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style44" style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Work Type"></asp:Label>
                    </td>
                    <td class="style42" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlWorkType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="style43" style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Work No"></asp:Label>
                    </td>
                    <td class="style40" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbWorkNo" runat="server" BorderColor="Black" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style44" style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Part No"></asp:Label>
                    </td>
                    <td class="style42" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbPart" runat="server" BorderColor="Black"></asp:TextBox>
                    </td>
                    <td class="style43" style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td class="style40" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server" BorderColor="Black"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style44" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlDateType" runat="server">
                            <asp:ListItem Value="0">Delivery Date</asp:ListItem>
                            <asp:ListItem Value="1">Sale Date</asp:ListItem>
                            <asp:ListItem Selected="True" Value="2">MO Date</asp:ListItem>
                            <asp:ListItem Value="3">Plan Start Date</asp:ListItem>
                            <asp:ListItem Value="4">Plan Finish Date</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style42" style="background-color: #FFFFFF">
                        <uc1:Date ID="UcDateFrom" runat="server" />
                    </td>
                    <td class="style43" style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="To"></asp:Label>
                    </td>
                    <td class="style40" style="background-color: #FFFFFF">
                        <uc1:Date ID="UcDateTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="style44" style="background-color: #FFFFFF">
                        <asp:Label ID="Label13" runat="server" Text="Status Code"></asp:Label>
                    </td>
                    <td class="style42" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlStatusCode" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Close</asp:ListItem>
                            <asp:ListItem Selected="True" Value="2">Manufacturing</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style43" style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td class="style40" style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width: 95%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btnReport" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="Label11" runat="server" ForeColor="Blue" 
                            Text="จำนวนรายการที่สั่งผลิต"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="lbShow" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="Label12" runat="server" ForeColor="Blue" Text="รายการ"></asp:Label>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
    </asp:Content>
