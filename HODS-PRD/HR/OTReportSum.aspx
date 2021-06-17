<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="OTReportSum.aspx.vb" Inherits="MIS_HTI.OTReportSum" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">

        .style8
        {
            width: 161px;
        }
        .style9
        {
            width: 230px;
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
                freezesize: 3,
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
            <uc3:HeaderForm ID="HeaderForm1" runat="server" />
            <table style="width: 75%;">
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        Emp. No. (รหัสพนักงาน)</td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbUserID" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td class="style10" style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        Date From (จากวันที่)</td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateFrom" runat="server" AutoPostBack="true" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        Date To (ถึงวันที่)</td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" AutoPostBack="true" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        &nbsp;<asp:Button ID="btShowRe" runat="server" Text="Show Report" />
                        <asp:Button ID="btExcel" runat="server" Text="Export Excel" />
                        <br />
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
            <br />

            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
