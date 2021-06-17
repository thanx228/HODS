<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="TransOrderReport.aspx.vb" Inherits="MIS_HTI.TransOrderReport" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc3" %>
<%@ Register src="../UserControl/workCenterC.ascx" tagname="workCenterC" tagprefix="uc4" %>
<%@ Register src="../UserControl/docTypeC.ascx" tagname="docTypeC" tagprefix="uc5" %>
<%@ Register src="../UserControl/docTypeD.ascx" tagname="docTypeD" tagprefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .style10
        {
            width: 106px;
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
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
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
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <uc1:HeaderForm ID="HeaderForm1" runat="server" />
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td class="style10" style="vertical-align: top">
                        Trans.&amp;Rec. Type.</td>
                    <td colspan="3">
                        <uc5:docTypeC ID="ucTranType" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="style10" style="vertical-align: top">
                        <asp:DropDownList ID="ddWCType" runat="server">
                            <asp:ListItem Value="TB005">WC Issue</asp:ListItem>
                            <asp:ListItem Value="TB008">WC Reciept</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td colspan="3">
                        <uc4:workCenterC ID="ucWC" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="style10">
                        Trans.&amp;Rec. No.</td>
                    <td>
                        <asp:TextBox ID="tbTransNo" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        Show Data</td>
                    <td>
                        <asp:DropDownList ID="ddlShow" runat="server">
                            <asp:ListItem Selected="True" Value="0">Details</asp:ListItem>
                            <asp:ListItem Value="1">Summary</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style10">
                        MO Type.</td>
                    <td>
                        <uc6:docTypeD ID="ucMoType" runat="server" />
                    </td>
                    <td>
                        MO No.</td>
                    <td>
                        <asp:TextBox ID="tbMONo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style10">
                        <asp:Label ID="Label6" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        Spec.</td>
                    <td>
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style10">
                        Doc. Date From</td>
                    <td>
                        <uc3:Date ID="DateFrom" runat="server" />
                    </td>
                    <td>
                        Doc. Date To</td>
                    <td>
                        <uc3:Date ID="DateTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="style10">
                        Inputer</td>
                    <td>
                        <asp:TextBox ID="tbInputer" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        W/H</td>
                    <td>
                        <asp:DropDownList ID="ddlWH" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style10">
                        Group No.</td>
                    <td>
                        <asp:DropDownList ID="ddlGroup" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width: 75%; background-image: url('../Images/btt.jpg'); background-repeat: no-repeat;">
                <tr>
                    <td align="center" class="style7">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>
            <uc2:CountRow ID="CountRow1" runat="server" />
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
