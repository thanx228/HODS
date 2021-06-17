<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SaleOrderProductStatusPopup.aspx.vb" Inherits="MIS_HTI.SaleOrderProductStatusPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Sale Order Product Status</title>
    </head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 94%;">
            <tr>
                <td align="center" 
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label18" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Detail Sale Order Product  Status"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 94%;">
            <tr>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label23" runat="server" Text="Cust ID"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label1" runat="server" Text="Item"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label2" runat="server" Text="SPEC"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label9" runat="server" Text="Stock Qty"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label24" runat="server" Text="Undelivery Qty"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label8" runat="server" Text="MO Qty"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbCust" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
                <td style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbCode" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
                <td style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbSpec" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbStock" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbUndelivery" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbMO" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label15" runat="server" Text="จำนวนรายการ SO"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbCountSO" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label17" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSO" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4">
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
        <table>
            <tr>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label19" runat="server" Text="จำนวนรายการ MO"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbCountMO" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label20" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMO" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4">
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
    </div>
    </form>
</body>
</html>
