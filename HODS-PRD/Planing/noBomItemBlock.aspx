<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="noBomItemBlock.aspx.vb" Inherits="MIS_HTI.noBomItemBlock" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

*{padding: 0; margin: 0;}

    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width:47%;">
        <tr>
            <td align="left" 
                style="background-image: url('../Images/btt.jpg'); ">
                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Large" 
                            ForeColor="Blue" Text="Not Appove"></asp:Label>
                    </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="height: 17px;">
                <tr>
                    <td align="center">
                        <asp:Label ID="Label1" runat="server" Text="Record Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" Height="20px" Width="125px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label6" runat="server" Text="Sale Order Type"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlSaleType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btSearch" runat="server" Text="Search" />
                    </td>
                </tr>
            </table>
            <asp:Label ID="Label2" runat="server" Text="จำนวนรายการ"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbCnt" runat="server" ForeColor="Blue"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label4" runat="server" Text="รายการ"></asp:Label>
            <asp:GridView ID="gvShowData" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target=_blank>Detail</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
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
