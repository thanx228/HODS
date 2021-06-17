<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ERPUserPopup.aspx.vb" Inherits="MIS_HTI.ERPUserPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Sale Order Product Status</title>
    <style type="text/css">
        .style1
        {
            width: 147px;
        }
    </style>
    </head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 94%;">
            <tr>
                <td 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label18" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Detail Group"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 26%;">
            <tr>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style1">
                    <asp:Label ID="Label23" runat="server" Text="Group Code"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbGrp" runat="server"></asp:Label>
                </td>
            </tr>
            </table>
        <table>
            <tr>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label24" runat="server" Text="จำนวนรายการ"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbCountUser" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label25" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvUser" runat="server" BackColor="White" BorderColor="#3366CC" 
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
                    <asp:Label ID="Label15" runat="server" Text="จำนวนรายการ"></asp:Label>
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
        <asp:GridView ID="gvShow" runat="server" BackColor="White" BorderColor="#3366CC" 
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
            </table>
    </div>
    </form>
</body>
</html>
