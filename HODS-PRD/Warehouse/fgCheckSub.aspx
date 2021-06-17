<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="fgCheckSub.aspx.vb" Inherits="MIS_HTI.fgCheckSub" %>

<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FG Check Popup</title>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollgvShowIn() {
            gridView1 = $('#<%= gvShowIn.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 300,
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
        function gridviewScrollgvShowOut() {
            gridView1 = $('#<%= gvShowOut.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 300,
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
        function gridviewScrollgvShowSum() {
            gridView1 = $('#<%= gvShowSum.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 300,
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
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <uc1:HeaderForm ID="ucHeadIn" runat="server" />
        <uc2:CountRow ID="ucCountRowIn" runat="server" />
        <asp:GridView ID="gvShowIn" runat="server">
        </asp:GridView>
        <br />
        <asp:Button ID="btExcel1" runat="server" Text="Excel" />
        <br />
        <br />
        <uc1:HeaderForm ID="ucHeadOut" runat="server" />
        <uc2:CountRow ID="ucCountRowOut" runat="server" />
        <asp:GridView ID="gvShowOut" runat="server">
        </asp:GridView>
    
        <br />
        <asp:Button ID="btExcel2" runat="server" Text="Excel" />
    
        <br />
        <br />
    
        <uc1:HeaderForm ID="ucHeadSum" runat="server" />
        <uc2:CountRow ID="ucCountRowSum" runat="server" />
        <asp:GridView ID="gvShowSum" runat="server">
        </asp:GridView>
    
    </div>
        <br />
        <asp:Button ID="btExcel3" runat="server" Text="Excel" />
    </form>
</body>
</html>
