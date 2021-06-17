<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="moStatusNotClosedPop.aspx.vb" Inherits="MIS_HTI.moStatusNotClosedPop" %>

<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail MO </title>
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
    </style>
     <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: screen.availHeight -200,
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
    
        <table style="width:100%;">
            <tr>
                <td align="left" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="lbHeader" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:75%; background-image: url('../Images/btt.jpg'); background-repeat: repeat-x;">
                <tr>
                    <td align="center">
                        <asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="ucCountRow" runat="server" />
            <asp:GridView ID="gvShow" runat="server" 
                >
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
