<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="moStatusNotClosed.aspx.vb" Inherits="MIS_HTI.moStatusNotClosed" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc3" %>
<%@ Register src="../UserControl/workCenterC.ascx" tagname="workCenterC" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 20px;
        }
        .style7
        {
            height: 20px;
            width: 145px;
        }
        .style8
        {
            width: 145px;
        }
    </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollgvDetail() {
            gridView1 = $('#<%= gvDetail.ClientID %>').gridviewScroll({
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
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label3" runat="server" Text="Plan Comp Date"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <uc3:Date ID="ucDate" runat="server" />
                    </td>
                    <td bgcolor="White" class="style6">
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style8">
                        <asp:Label ID="Label4" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="2">
                        <uc4:workCenterC ID="ucWc" runat="server" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>
            <uc2:CountRow ID="ucCountRowSum" runat="server" />
            <asp:GridView ID="gvSum" runat="server">
            </asp:GridView>
            <uc2:CountRow ID="ucCountRowDetail" runat="server" />
            <asp:GridView ID="gvDetail" runat="server">
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
