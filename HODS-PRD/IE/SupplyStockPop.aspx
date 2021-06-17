<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SupplyStockPop.aspx.vb" Inherits="MIS_HTI.SupplyStockPop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Check Code Status</title>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td align="center" 
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Detail  Supply Product and Other Status"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width:61%;">
            </table>
        <table style="width: 56%;">
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label26" runat="server" 
                        Text="จำนวนรายการ  Purchase Request            "></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountPR" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label27" runat="server" Text="     รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPR" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" 
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
    
        <table style="width: 55%;">
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label24" runat="server" 
                        Text="จำนวนรายการ  Purchase Order            "></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountPO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label25" runat="server" Text="     รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPO" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" 
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
    
    </div>
    </form>
</body>
</html>
