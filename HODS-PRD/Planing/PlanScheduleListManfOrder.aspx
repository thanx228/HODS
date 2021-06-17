<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master"
    EnableEventValidation="false" CodeBehind="PlanScheduleListManfOrder.aspx.vb" Inherits="MIS_HTI.PlanScheduleListManfOrder" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<%@ Register src="../UserControl/DropDownListUserControl.ascx" tagname="DropDownListUserControl" tagprefix="uc5" %>
<%@ Register src="../UserControl/CheckBoxListUserControl.ascx" tagname="CheckBoxListUserControl" tagprefix="uc1" %>
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
        .auto-style28 {
            width: 126px;
            height: 20px;
        }
        .auto-style29 {
            height: 20px;
        }
        .auto-style30 {
            width: 126px;
        }
        </style>
     <link href="cssGrid/cssGrid.css" rel="stylesheet" />
     <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">    
        
        function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 500,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 14
            });
        }
        function MouseEventsSearch(obj, evt) {
            var checkbox = obj.getElementsByTagName("input")[0];
            if (evt.type == "mouseover") {
                obj.style.backgroundColor = "#82E0AA";
            }
                else if (evt.type == "mouseout") {
                    if (obj.rowIndex % 2 == 0) {
                        obj.style.backgroundColor = "white";
                    }
                    else {
                        obj.style.backgroundColor = "white";
                    }
                }
        }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 75%; background-color: #FFFFFF;">
                <tr>
                    <td class="auto-style30" style="vertical-align: top">
                        <asp:Label ID="Label30" runat="server" Text="WC"></asp:Label>
                    </td>
                    <td colspan="3">
                        <uc1:CheckBoxListUserControl ID="UcCblWc" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style30" style="vertical-align: top">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="cbPlan" runat="server" Checked="True" Text="Available Plan" />
                    </td>
                    <td>
                        <asp:CheckBox ID="cbWip" runat="server" Text="WIP &gt;0" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style30" style="vertical-align: top">
                        <asp:Label ID="Label31" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td>
                        <uc5:DropDownListUserControl ID="UcDdlMoType" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="MO No-Seq"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbMO" runat="server"></asp:TextBox>
                        -<asp:TextBox ID="tbManfSeq" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style28" style="vertical-align: top">
                        <asp:Label ID="Label33" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td class="auto-style29">
                        <uc5:DropDownListUserControl ID="UcDdlSoType" runat="server" />
                    </td>
                    <td class="auto-style29">
                        <asp:Label ID="Label32" runat="server" Text="SO No-Seq"></asp:Label>
                    </td>
                    <td class="auto-style29">
                        <asp:TextBox ID="tbSaleOrder" runat="server"></asp:TextBox>
                        -<asp:TextBox ID="tbSaleSeq" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style28" style="vertical-align: top">
                        <asp:Label ID="Label34" runat="server" Text="Cust Code"></asp:Label>
                    </td>
                    <td class="auto-style29">
                        <asp:TextBox ID="tbCustCode" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style29">
                        <asp:Label ID="Label25" runat="server" Text="Cust Name"></asp:Label>
                    </td>
                    <td class="auto-style29">
                        <asp:TextBox ID="tbCustName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style30" style="vertical-align: top">
                        <asp:Label ID="Label26" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label27" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style30" style="vertical-align: top">
                        <asp:Label ID="Label11" runat="server" Text="Plan Date Start"></asp:Label>
                    </td>
                    <td>
                        <uc2:Date ID="ucDateFrom" runat="server" />
                    </td>
                    <td style="vertical-align: top;">
                        <asp:Label ID="Label12" runat="server" Text="Plan Date End"></asp:Label>
                    </td>
                    <td>
                        <uc2:Date ID="ucDateTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="vertical-align: top">
                        <asp:Label ID="LbNote" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        &nbsp;<asp:Button ID="btSearch" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btnExport" runat="server" Text="Export Excel" Width="200px" />
                    </td>
                </tr>
            </table>
<%--            <table style="width: 75%;">
                <tr>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Select Month"></asp:Label>
                    </td>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="lbMonth" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="Select Year"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="lbYear" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>--%>
            <asp:GridView ID="gvShow" runat="server" 
                BackColor="White" BorderColor="#c0c0c0" BorderStyle="Double" BorderWidth="2px" 
                CellPadding="2">
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
   <%--       <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnExport" EventName="click">
        </asp:AsyncPostBackTrigger>
            </Triggers>--%>
           <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        </asp:UpdatePanel>
</asp:Content>
