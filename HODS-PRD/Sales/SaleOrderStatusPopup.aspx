<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SaleOrderStatusPopup.aspx.vb" Inherits="MIS_HTI.SaleOrderStatusPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" >
    <title>Detail Plan delivery stock</title>
    <style type="text/css">
        .style1
        {
            width: 201px;
        }
    </style>
</head>
<body style="background-image: url('../Images/bg.jpg'); background-repeat: repeat">
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 71%;">
            <tr>
                <td 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label18" runat="server" Font-Size="X-Large" ForeColor="Blue" 
                        Text="SO Status Detail"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 71%; margin-bottom: 0px;">
            <tr>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label26" runat="server" Text="SO"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    Item</td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style1">
                    Spec</td>
            </tr>
            <tr>
                <td style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbSO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbItem" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style1">
                    <asp:Label ID="lbSpec" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label15" runat="server" Text="จำนวนรายการ PR"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
        <asp:Label ID="lbCountPR" runat="server"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label17" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPR" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" 
            Width="697px">
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
        <table>
            <tr>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label27" runat="server" Text="จำนวนรายการ PO"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
        <asp:Label ID="lbCountPO" runat="server"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label28" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPO" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" 
            Width="697px">
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
    
        <table>
            <tr>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label29" runat="server" Text="จำนวนรายการ MO"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
        <asp:Label ID="lbCountMO" runat="server"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label30" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMO" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" 
            Width="697px">
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
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
