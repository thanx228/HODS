<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MaterialsReview.aspx.vb" Inherits="MIS_HTI.MaterialsReview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/DropDownListUserControl.ascx" TagName="DropDownListUserControl" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/CountRow.ascx" TagName="CountRow" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/CheckBoxListUserControl.ascx" TagName="CheckBoxListUserControl" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/Date.ascx" TagName="Date" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6 {
            width: 248px;
        }

        .auto-style3 {
            width: 139px;
        }

        .auto-style4 {
            width: 205px;
        }

        .auto-style5 {
            width: 113px;
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
                    <td style="background-color: #FFFFFF; width: 10%; vertical-align: top;">
                        <asp:Label ID="Label17" runat="server" Text="W/H"></asp:Label>
                    </td>
                    <td colspan="3" style="background-color: #FFFFFF;">
                        <uc3:CheckBoxListUserControl ID="UcCblWh" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; width: 10%;">
                        <asp:Label ID="Label12" runat="server" Text="Mat Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF; width: 40%;">
                        <asp:TextBox ID="tbMatItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF; width: 10%;">
                        <asp:Label ID="Label4" runat="server" Text="FG Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF; width: 40%;">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style3">
                        <asp:Label ID="Label13" runat="server" Text="Mat Desc"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style4">
                        <asp:TextBox ID="tbMatDesc" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style5">
                        <asp:Label ID="Label7" runat="server" Text="FG Spec"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style3">
                        <asp:Label ID="Label14" runat="server" Text="Mat Spec"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style4">
                        <asp:TextBox ID="tbMatSpec" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style5">
                        <asp:Label ID="Label6" runat="server" Text="Show Mat Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc1:DropDownListUserControl ID="UcDdlMatType" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style3">
                        <asp:Label ID="Label5" runat="server" Text="Date From"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style4">
                        <uc4:Date ID="UcDateFrom" runat="server" />
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style5">
                        <asp:Label ID="Label8" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc4:Date ID="UcDateTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style3">
                        <asp:Label ID="Label16" runat="server" Text="Report Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc1:DropDownListUserControl ID="UcDdlReportType" runat="server" />
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label15" runat="server" Text="Include Condition"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:CheckBox ID="cbSum" runat="server" Text="Sum &lt;0" Checked="True" />
                        <asp:CheckBox ID="cbPR" runat="server" Text="PR" />
                        <asp:CheckBox ID="cbPO" runat="server" Text="PO" />
                    </td>
                </tr>
            </table>
            <table style="width: 95%;">
                <tr>
                    <td align="center"
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                        &nbsp;<asp:Button ID="BtBOM" runat="server" Text="BOM" />
                    </td>
                </tr>
            </table>
            <uc2:CountRow ID="CountRow1" runat="server" />
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