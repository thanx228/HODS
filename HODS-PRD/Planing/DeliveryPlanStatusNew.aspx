<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="DeliveryPlanStatusNew.aspx.vb" Inherits="MIS_HTI.DeliveryPlanStatusNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<%@ Register src="../UserControl/GridviewShow.ascx" tagname="GridviewShow" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 155px;
        }
        .style7
        {
            width: 219px;
        }
    .style1
    {
        width: 282px;
    }
    </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollgvShow() {
            gridView1 = $('#<%= ucGvShow.getGridview.ClientID %>').gridviewScroll({
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 95%;">
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label7" runat="server" Text="Del Date From"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <uc2:Date ID="ucDateFrom" runat="server" />
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label11" runat="server" Text="Del Date To"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <uc2:Date ID="ucDateTo" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="95%">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        Delivery Sum
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table bgcolor="White" style="width:100%;">
                            <tr>
                                <td class="style6" style="vertical-align: top">
                                    <asp:Label ID="Label12" runat="server" Text="SO Type"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:CheckBoxList ID="cblSaleType" runat="server">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style6">
                                    <asp:Label ID="Label4" runat="server" Text="SO No."></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbSaleNo" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="SO Seq"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbSaleSeq" runat="server" Width="60px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style6">
                                    <asp:Label ID="Label5" runat="server" Text="Cust ID"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbCust" runat="server" Width="60px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="DN No"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbDelNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style6">
                                    <asp:Label ID="Label16" runat="server" Text="Cust PO"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbCustPO" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Text="Cust WO"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbCustWo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style6">
                                    <asp:Label ID="Label6" runat="server" Text="Item"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text="Spec"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style6">
                                    <asp:Label ID="Label18" runat="server" Text="Sum"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CbSumDelDate" runat="server" Checked="True" Text="Delivery Date" />
                                    &nbsp; </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    <HeaderTemplate>
                        Days Sum
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="Label19" runat="server" Text="Sum"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CbDaysum" runat="server" Text="Date" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <table style="width:95%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x"
                        align="center">
                        &nbsp;<asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <uc4:GridviewShow ID="ucGvShow" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
