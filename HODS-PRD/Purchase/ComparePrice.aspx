<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ComparePrice.aspx.vb" Inherits="MIS_HTI.ComparePrice" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            height: 26px;
        }
    .style1
    {
        width: 282px;
    }
        .style6
        {
            width: 100px;
        }
        .style7
        {
            width: 300px;
        }
        .style8
        {
            width: 243px;
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
            gridView1 = $('#<%= gvShowPR.ClientID %>').gridviewScroll({
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
        <table>
            <tr>
                <td style="background-color: #FFFFFF">
                    <asp:Label ID="Label5" runat="server" Text="Item"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF" class="style7">
                    <asp:TextBox ID="tbItem" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="background-color: #FFFFFF" class="style6">
                    &nbsp;</td>
                <td style="background-color: #FFFFFF" class="style8">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="background-color: #FFFFFF">
                    <asp:Label ID="Label11" runat="server" Text="Vendor Code"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF" class="style7">
                    <asp:TextBox ID="tbSup" runat="server" Width="83px"></asp:TextBox>
                </td>
                <td style="background-color: #FFFFFF" class="style6">
                    &nbsp;</td>
                <td style="background-color: #FFFFFF" class="style8">
                    &nbsp;</td>
            </tr>
        </table>
        <table style="width: 69%;">
            <tr>
                <td align="center" 
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Button ID="btShow" runat="server" Text="Show Report" />
                    &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    &nbsp;<asp:Button ID="btPrint" runat="server" Text="Print Report" />
                </td>
            </tr>
        </table>
        <uc1:CountRow ID="CountRow1" runat="server" />
        <asp:GridView ID="gvShow" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            Width="190px">
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
        <asp:GridView ID="gvShowPR" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            Width="190px">
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
