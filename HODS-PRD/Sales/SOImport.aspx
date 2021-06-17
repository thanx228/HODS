<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SOImport.aspx.vb" Inherits="MIS_HTI.SOImport" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollList() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 400,
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
            <table style="width: 75%;">
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label3" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlTypeSo" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label11" runat="server" Text="SO NO"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="lbSONumber" runat="server" ForeColor="Blue"></asp:Label>
                        <asp:Label ID="LbListSo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label6" runat="server" Text="Doc Date"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <uc2:Date ID="ucDocDate" runat="server" />
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label16" runat="server" Text="Sale Order Cut"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:CheckBox ID="CbCut" runat="server" Checked="True" Text="Cust W/O" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label4" runat="server" Text="Customer"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlCust" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label5" runat="server" Text="Plant"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlPlant" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label8" runat="server" Text="Remark"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:TextBox ID="tbRemark" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label10" runat="server" Text="Excel File"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:FileUpload ID="fuUpload" runat="server" />
                    </td>
                    <td bgcolor="White" colspan="2">
                        <asp:Label ID="Label12" runat="server" ForeColor="Red" 
                            Text="Spec(A),Qty(B),Request Date(C)(20190101),W/O(D),Model(E),Line(F),QPA Cust(G),Cust Order(H)"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btCheck" runat="server" Text="Check &amp; Load" />
                        &nbsp;<asp:Button ID="btSave" runat="server" Text="Save" />
                        &nbsp;<asp:Button ID="btReset" runat="server" Text="Reset" />
                    </td>
                </tr>
            </table>
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
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="All Qty"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbQty" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="All Amount"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbAmt" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btCheck" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
