<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PlanScheduleListCheck.aspx.vb" Inherits="MIS_HTI.PlanScheduleListCheck" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 524px;
        }
        .style5
        {
            
        }
        .style7
        {
            height: 19px;
        }
        .style8
        {
            width: 91px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 75%; background-color: #FFFFFF;">
                <tr>
                    <td class="style8" style="vertical-align: top">
                        <asp:Label ID="Label6" runat="server" Text="Date From"></asp:Label>
                    </td>
                    <td>
                        <uc1:Date ID="ucPlanDateFrom" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td>
                        <uc1:Date ID="ucPlanDateTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="vertical-align: top">
                        <asp:Label ID="Label10" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:CheckBoxList ID="cblWorkCenter" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        &nbsp;<asp:Button ID="btSearch" runat="server" Text="Show Report" />
                        &nbsp;</td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    HorizontalAlign="Center" />
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
