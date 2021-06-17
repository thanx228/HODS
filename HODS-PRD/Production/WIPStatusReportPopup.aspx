<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS_SUB.Master" CodeBehind="WIPStatusReportPopup.aspx.vb" Inherits="MIS_HTI.WIPStatusReportPopup" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Scripts/gridviewScroll.css" rel="stylesheet" />
        <script type="text/javascript">
        function gvShowScrollbar() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 40,
                height: screen.availHeight * 0.62,
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
    <table style="width:75%;">
        <tr>
            <td style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x"
                align="center">

                <asp:Button ID="btShow" runat="server" Text="Show Again" />
&nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" />

            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <uc1:CountRow ID="CountRow1" runat="server" />

            <asp:GridView ID="gvShow" runat="server" BackColor="White" CellPadding="4" BorderColor="#3366CC">
                <HeaderStyle BackColor="#003399" ForeColor="#CCCCFF" HorizontalAlign="Center" Wrap="True" />
                <PagerStyle Wrap="True" />
                <RowStyle HorizontalAlign="Left" Wrap="False" />
            </asp:GridView>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
