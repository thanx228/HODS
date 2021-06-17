<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleUndeliveryPeriodPopup.aspx.vb" Inherits="MIS_HTI.SaleUndeliveryPeriodPopup" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/UserControl/CountRow.ascx" TagPrefix="uc1" TagName="CountRow" %>
<%@ Register Src="~/UserControl/GridviewShow.ascx" TagPrefix="uc1" TagName="GridviewShow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 10px;
        }

        .auto-style10 {
            width: 160px;
        }

        .auto-style11 {
            width: 110px;
        }

        .auto-style13 {
            width: 75%;
        }
    </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Scripts/gridviewScroll.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            GvSOScrollbar();
            GvMOScrollbar();
            GvPOScrollbar();

        });

        function GvSOScrollbar() {
            gridView1 = $('#<%= GvSO.ClientID %>').gridviewScroll({
                //width: screen.availWidth - 10,
                width: 1075,
                height: 800,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 0,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize: 8
            });
        }

        function GvMOScrollbar() {
            gridView1 = $('#<%= GvMO.ClientID %>').gridviewScroll({
                //width: screen.availWidth - 10,
                width: 1075,
                height: 800,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 0,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize: 8
            });
        }

        function GvPOScrollbar() {
            gridView1 = $('#<%= GvPO.ClientID %>').gridviewScroll({
                //width: screen.availWidth - 10,
                width: 1075,
                height: 800,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 0,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize: 8
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 75%; background-color: white">
                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Label ID="Label1" runat="server" Text="Detail Sale Undelivery Status"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 75%; background-color: white">
                <tr>
                    <td class="auto-style3"></td>
                    <td align="center" class="auto-style10">
                        <asp:Label ID="Label4" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td align="center" class="auto-style10">
                        <asp:Label ID="Label5" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td align="center" class="auto-style11">
                        <asp:Label ID="Label3" runat="server" Text="Undelivery Qty"></asp:Label>
                    </td>
                    <td align="center" class="auto-style11">
                        <asp:Label ID="Label6" runat="server" Text="MO Qty"></asp:Label>
                    </td>
                    <td align="center" class="auto-style11">
                        <asp:Label ID="Label7" runat="server" Text="PO Qty"></asp:Label>
                    </td>
                    <td align="center" class="auto-style11">
                        <asp:Label ID="Label8" runat="server" Text="PR Qty"></asp:Label>
                    </td>
                    <td align="center" class="auto-style11">
                        <asp:Label ID="Label9" runat="server" Text="Stock Qty"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                    <td align="center" class="auto-style10">&nbsp;<asp:HyperLink ID="Linktem" runat="server" Font-Bold="True" ForeColor="Blue" Font-Size="12">[hlItem]</asp:HyperLink>
                    </td>
                    <td align="center" class="auto-style10">
                        <asp:Label ID="lbSpec" runat="server" ForeColor="Blue" Font-Size="12"></asp:Label>
                    </td>
                    <td align="center" class="auto-style11">
                        <asp:Label ID="lbUndeliveryQty" runat="server" ForeColor="Blue" Font-Size="12"></asp:Label>
                    </td>
                    <td align="center" class="auto-style11">
                        <asp:Label ID="lbMOQty" runat="server" ForeColor="Blue" Font-Size="12"></asp:Label>
                    </td>
                    <td align="center" class="auto-style11">
                        <asp:Label ID="lbPOQty" runat="server" ForeColor="Blue" Font-Size="12"></asp:Label>
                    </td>
                    <td align="center" class="auto-style11">
                        <asp:Label ID="lbPRQty" runat="server" ForeColor="Blue" Font-Size="12"></asp:Label>
                    </td>
                    <td align="center" class="auto-style11">
                        <asp:Label ID="lbStockQty" runat="server" ForeColor="Blue" Font-Size="12"></asp:Label>
                    </td>
                </tr>
            </table>

            <table style="background-color: white" class="auto-style13">
                <tr>
                    <td align="center"
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btRefresh" runat="server" Text="Refresh" BorderStyle="None" />
                    </td>
                </tr>
            </table>

            <div style="width: 75%; background-color: #FFFFFF;">
                <div style="width: 75%; background-color: white;">
                    <asp:Label runat="server" Text="Stock List" Font-Size="Medium" ForeColor="#CC0000" Font-Bold="True"></asp:Label>
                    &nbsp;
                </div>
                <uc1:GridviewShow ID="GvStock" runat="server" />
                <br />
            </div>

            <div style="width: 75%; background-color: #FFFFFF;">
                <div style="width: 75%; background-color: white;">
                    <asp:Label runat="server" Text="SO List" Font-Size="Medium" ForeColor="#CC0000" Font-Bold="True"></asp:Label>
                    &nbsp;
                </div>

                &nbsp;<uc1:GridviewShow ID="GvSO" runat="server" />
                <br />
            </div>

            <div style="width: 75%; background-color: #FFFFFF;">
                <div style="width: 75%; background-color: white;">
                    <asp:Label runat="server" Text="MO List" Font-Size="Medium" ForeColor="#CC0000" Font-Bold="True"></asp:Label>
                    &nbsp;
                </div>
                &nbsp;<uc1:GridviewShow ID="GvMO" runat="server" />
                <br />
            </div>

            <div style="width: 75%; background-color: #FFFFFF;">
                <div style="width: 75%; background-color: white;">
                    <asp:Label runat="server" Text="PO List" Font-Size="Medium" ForeColor="#CC0000" Font-Bold="True"></asp:Label>
                    &nbsp;
                </div>
                <uc1:GridviewShow ID="GvPO" runat="server" />
                <br />
            </div>

            <div style="width: 75%; background-color: #FFFFFF;">
                <div style="width: 75%; background-color: white;">
                    <asp:Label runat="server" Text="PR List" Font-Size="Medium" ForeColor="#CC0000" Font-Bold="True"></asp:Label>
                    &nbsp;
                </div>
                <uc1:GridviewShow ID="GvPR" runat="server" />
                <br />
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>