<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleUndeliveryPeriod.aspx.vb" Inherits="MIS_HTI.SaleUndeliveryPeriod" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/UserControl/CountRow.ascx" TagPrefix="uc1" TagName="CountRow" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<%@ Register src="../UserControl/docTypeC.ascx" tagname="docTypeC" tagprefix="uc3" %>
<%@ Register Src="~/UserControl/GridviewShow.ascx" TagPrefix="uc1" TagName="GridviewShow" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 10px;
        }

        .auto-style4 {
            width: 100px;
        }

        .auto-style5 {
            width: 10px;
            height: 21px;
        }

        .auto-style6 {
            width: 100px;
            height: 21px;
        }

        .auto-style7 {
            height: 21px;
        }

        .auto-style8 {
            width: 243px;
        }

        .auto-style9 {
            height: 21px;
            width: 243px;
        }

        .auto-style10 {
            width: 101px;
        }

        .auto-style11 {
            width: 101px;
            height: 21px;
        }
    </style>

    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollShow();
        });

        function gridviewScrollShow() {
            gridView1 = $('#<%= GvUndelPeriod.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 500,
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style4">So Type :</td>
                    <td>
                        <uc3:docTypeC ID="ChkSOType" runat="server" />
                    </td>
                </tr>
            </table>
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style10">Cust ID :</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtCustID" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style4">Condition :</td>
                    <td>
                        <asp:DropDownList ID="ddlCondition" runat="server">
                            <asp:ListItem Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1">Stock &lt; Undelivery</asp:ListItem>
                            <asp:ListItem Value="2">Supply &gt;= Undelivery</asp:ListItem>
                            <asp:ListItem Value="3">Supply &lt; Undelivery</asp:ListItem>
                            <asp:ListItem Value="4">Stock &gt;= Undelivery</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;<asp:Label ID="lbRemark" runat="server" ForeColor="Red" Text="Supply = (Stock Qty + MO Qty + PO Qty + PR Qty)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style10">SO No :</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtSONo" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style4">SO Seq&nbsp; :</td>
                    <td>
                        <asp:TextBox ID="txtSeq" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style10">Item No :</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtItemNo" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style4">Spec&nbsp; :</td>
                    <td>
                        <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5"></td>
                    <td class="auto-style11">From Due Date :</td>
                    <td class="auto-style9">
                        <uc2:Date ID="DateF" runat="server" />
                    </td>
                    <td class="auto-style6">End Due Date&nbsp; :</td>
                    <td class="auto-style7">
                        <uc2:Date ID="DateT" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style10">Summary By :</td>
                    <td class="auto-style8">
                        <asp:DropDownList ID="ddlSummary" runat="server">
                            <asp:ListItem Value="0">Day</asp:ListItem>
                            <asp:ListItem Value="1">Week</asp:ListItem>
                            <asp:ListItem Value="2">Month</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style4">Report&nbsp; :</td>
                    <td>
                        <asp:DropDownList ID="ddlReport" runat="server">
                            <asp:ListItem Value="0">Default</asp:ListItem>
                            <asp:ListItem Value="1">Detail</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;<asp:Label ID="lbReport" runat="server" ForeColor="#CC0000" Text="Detail : Currency THB < 7 and Other < 14 (Text Color RED)" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style10"></td>
                    <td class="auto-style8"></td>
                    <td class="auto-style4"></td>
                    <td></td>
                </tr>
            </table>
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td align="center"
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btSearch" runat="server" Text="Search" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>

            <uc1:CountRow ID="CountRow1" runat="server" />
            <asp:GridView ID="GvUndelPeriod" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="ShowDetail" runat="server">Detail</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" Wrap="False" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" Wrap="False" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>
