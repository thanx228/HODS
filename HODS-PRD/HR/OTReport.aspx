<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="OTReport.aspx.vb" Inherits="MIS_HTI.OTReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style type="text/css">

        .style8
        {
        }
        .style9
        {
            width: 237px;
        }
        .style10
        {
            width: 323px;
        }
        .style11
        {
            width: 321px;
        }
        .style12
        {
            width: 164px;
        }
            .auto-style4 {
                width: 215px;
            }
            .auto-style6 {
                width: 84px;
            }
            .auto-style7 {
                width: 97px;
            }
        </style>

     <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollShow();
            gridviewScrollSum();
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

        function gridviewScrollSum() {
            gridView1 = $('#<%= gvSum.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 450,
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
            <table style="width: 80%;">
                <tr>
                    <td class="auto-style6" style="background-color: #FFFFFF">
                        Dept.</td>
                    <td class="style10" colspan="3" style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblDept" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6" style="background-color: #FFFFFF">
                        <asp:Label ID="lbEmpNo" runat="server" Text="Emp. No."></asp:Label>
                    </td>
                    <td class="auto-style4" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbEmpNo" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style7" style="background-color: #FFFFFF">
                        Shift </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cbShift" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="07:00" Selected="True">Day</asp:ListItem>
                            <asp:ListItem Value="19:00" Selected="True">Night</asp:ListItem>
                        </asp:CheckBoxList>
                   
      
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6" style="background-color: #FFFFFF">
                        Date From</td>
                    <td class="auto-style4" style="background-color: #FFFFFF">
                           <asp:TextBox ID="ucDateFrom" runat="server" Width="80px" AutoPostBack="true" ></asp:TextBox>
                        <asp:CalendarExtender ID="ucDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="ucDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td class="auto-style7" style="background-color: #FFFFFF">
                        Date To</td>
                    <td class="style10" style="background-color: #FFFFFF">
                  <asp:TextBox ID="ucDateTo" runat="server" Width="80px" AutoPostBack="true" ></asp:TextBox>
                        <asp:CalendarExtender ID="ucDateTo_CalendarExtender1" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="ucDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btShowRe" runat="server" Text="Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Print Report" Visible="False" />
                        &nbsp;<asp:Button ID="btExcel" runat="server" Text="Export Excel" />
                        &nbsp;<asp:Button ID="btLocation" runat="server" Text="Sum Location" />
                        <br />
                    </td>
                </tr>
            </table>
            <uc2:CountRow ID="cntRowSum" runat="server" Visible="False" />
            <asp:GridView ID="gvSum" runat="server" BackColor="White" BorderColor="#3366CC" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4">
                              <FooterStyle BackColor="#99CCCC" ForeColor="#003399" Wrap="False" />
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
                <uc2:CountRow ID="cntRowShow" runat="server" Visible="False" />
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowFooter="True">
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
              <uc2:CountRow ID="cntRowPickup" runat="server" Visible="False" />
            <asp:GridView ID="gvSumPickup" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4"  ShowFooter="True"
                style="margin-bottom: 0px">
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
            <asp:PostBackTrigger ControlID="btExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
