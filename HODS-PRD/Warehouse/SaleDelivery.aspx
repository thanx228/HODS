<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleDelivery.aspx.vb" Inherits="MIS_HTI.SaleDelivery" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc2" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                width: screen.availWidth ,
                height: 600,
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
    <style type="text/css">
        .style9
        {
            width: 201px;
        }
        .style11
        {
            width: 137px;
            height: 25px;
        }
        .style12
        {
            width: 201px;
            height: 25px;
        }
        .style14
        {
            height: 25px;
        }
        .style15
        {
            width: 68px;
            height: 25px;
        }
        .style16
        {
            width: 68px;
        }
        .style18
        {
            width: 137px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           <table style="width:95%;">
        <tr>
            <td class="style18" style="background-color: #FFFFFF">
                Sale Delivery Type</td>
            <td class="style9" colspan = "3" style="background-color: #FFFFFF">
            <asp:CheckBoxList ID="cblSaleType" runat="server">
                        </asp:CheckBoxList>
                </td>
        </tr>
        <tr>
            <td class="style11" style="background-color: #FFFFFF">
                Sale Delivery&nbsp; Number</td>
            <td class="style12" style="background-color: #FFFFFF">
                <asp:TextBox ID="tbNo" runat="server"></asp:TextBox>
            </td>
            <td class="style15" style="background-color: #FFFFFF">
                Plant</td>
            <td class="style14" style="background-color: #FFFFFF">
                <asp:DropDownList ID="ddlPlant" runat="server" Width="100px">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
           <td class="style18" style="background-color: #FFFFFF">
                Delivery Date</td>
            <td class="style9" style="background-color: #FFFFFF">
                <uc3:Date ID="UcDate" runat="server" />
            </td>
            <td class="style16" style="background-color: #FFFFFF">
                Ship Time</td>
            <td style="background-color: #FFFFFF; color: #FF3300;">
                <asp:TextBox ID="tbShTime" runat="server" Width="70px"></asp:TextBox>
              
                </td>
        </tr>
        
               <tr>
                   <td class="style18" style="background-color: #FFFFFF">
                       <asp:Label ID="Label3" runat="server" Text="Target"></asp:Label>
                   </td>
                   <td class="style9" style="background-color: #FFFFFF">
                       <asp:CheckBox ID="CbSaleInvoice" runat="server" Checked="True" Text="Sale Invoice" />
                   </td>
                   <td class="style16" style="background-color: #FFFFFF">&nbsp;</td>
                   <td style="background-color: #FFFFFF; color: #FF3300;">&nbsp;</td>
               </tr>
        
    </table>
            <table style="width:95%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                        <br />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="ucCntRow" runat="server" />
              <%-- 
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                DataSourceID="SqlDataSource1">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
          
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:HOOTHAIConnectionString1 %>" 
                SelectCommand="">
            </asp:SqlDataSource>--%>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
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
