<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS_SUB.Master" CodeBehind="WorkCenterInfoPop.aspx.vb" Inherits="MIS_HTI.WorkCenterInfoPop" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<%@ Register src="../UserControl/shiftD.ascx" tagname="shiftD" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 99px;
        }
        .auto-style7 {
            width: 73px;
        }
        .auto-style11 {
            width: 73px;
            height: 21px;
        }
        .auto-style12 {
            height: 21px;
        }
        .auto-style13 {
            width: 99px;
            height: 21px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table style="width: 75%;">
            <tr>
                <td bgcolor="White" class="auto-style11">
                    <asp:Label ID="Label3" runat="server" Text="Work Center"></asp:Label>
                </td>
                <td bgcolor="White" class="auto-style12">
                    <asp:Label ID="lbWc" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td bgcolor="White" class="auto-style13">
                    <asp:Label ID="Label5" runat="server" Text="W/C Name"></asp:Label>
                </td>
                <td bgcolor="White" class="auto-style12">
                    <asp:Label ID="lbWcName" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td bgcolor="White" class="auto-style7">
                    <asp:Label ID="Label4" runat="server" Text="Mach/Line"></asp:Label>
                </td>
                <td bgcolor="White">
                    <asp:Label ID="lbMach" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td bgcolor="White" class="auto-style5">
                    <asp:Label ID="Label6" runat="server" Text="Mach\Line Name"></asp:Label>
                </td>
                <td bgcolor="White">
                    <asp:Label ID="lbMachName" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td bgcolor="White" class="auto-style7">
                    <asp:Label ID="Label20" runat="server" Text="W/C"></asp:Label>
                </td>
                <td bgcolor="White">
                    <asp:DropDownList ID="ddlWC" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td bgcolor="White" class="auto-style5">&nbsp;</td>
                <td bgcolor="White">&nbsp;</td>
            </tr>
        </table>
        <table bgcolor="White" style="width: 75%;">
            <tr>
                <td class="auto-style7">
                    <asp:Label ID="Label19" runat="server" Text="Operation"></asp:Label>
                </td>
                <td>
                    <asp:CheckBoxList ID="cblOper" runat="server">
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>
        <table style="width: 75%;">
            <tr>
                <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                    <asp:Button ID="btSaveOperation" runat="server" Text="Save" />
                    &nbsp;&nbsp;<asp:Button ID="btShow" runat="server" Text="Show Again" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvShow" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
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
</asp:UpdatePanel>
</asp:Content>