<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="planIssueMatPopup.aspx.vb" Inherits="MIS_HTI.planIssueMatPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Plan Request Stock</title>
    <style type="text/css">
        .style1
        {
            width: 37px;
        }
        .style2
        {
            width: 189px;
        }
        .style3
        {
            width: 175px;
        }
        .style4
        {
            width: 134px;
        }
    </style>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:69%;">
            <tr>
                <td align="center" 
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" Text="Material Shortage Detail" 
                        Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width:69%;">
            <tr>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style4">
                    <asp:Label ID="Label2" runat="server" Text="Code"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style3">
                    <asp:Label ID="Label3" runat="server" Text="SPEC"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style2">
                    <asp:Label ID="Label4" runat="server" Text="On Materials Request"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label5" runat="server" Text="On Stock"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style4">
                    <asp:Label ID="lbPart" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="left" style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style3">
                    <asp:Label ID="lbSpec" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style2">
                    <asp:Label ID="lbNotDel" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbStock" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label10" runat="server" Text="จำนวนรายการ Mat Issue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountIssue" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" class="style1" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label12" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvIssue" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            Height="16px">
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
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label13" runat="server" Text="จำนวนรายการ MO"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountMO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" class="style1" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label14" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMO" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Height="16px">
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
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label15" runat="server" Text="จำนวนรายการ PO"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountPO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" class="style1" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label16" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPO" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Height="16px">
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
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label17" runat="server" Text="จำนวนรายการ PR"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountPR" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" class="style1" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label18" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPR" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Height="16px">
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
    
    </div>
    </form>
</body>
</html>
