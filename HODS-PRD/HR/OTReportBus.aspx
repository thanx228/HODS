<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OTReportBus.aspx.vb" Inherits="MIS_HTI.OTReportBus" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">

    .style1
    {
        height: 23px;
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table style="width:100%;">
            <tr>
                <td align="left" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Bus Location Detail"></asp:Label>
                </td>
            </tr>
        </table>
            <uc2:CountRow ID="CountRow1" runat="server" />
        <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
            CellPadding="4">
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

<SortedAscendingCellStyle BackColor="#EDF6F6"></SortedAscendingCellStyle>

<SortedAscendingHeaderStyle BackColor="#0D4AC4"></SortedAscendingHeaderStyle>

<SortedDescendingCellStyle BackColor="#D6DFDF"></SortedDescendingCellStyle>

<SortedDescendingHeaderStyle BackColor="#002876"></SortedDescendingHeaderStyle>
            </asp:GridView>
  
    </div>
    </form>
</body>
</html>
