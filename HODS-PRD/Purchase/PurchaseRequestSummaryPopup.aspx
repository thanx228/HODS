<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PurchaseRequestSummaryPopup.aspx.vb" Inherits="MIS_HTI.PurchaseRequestSummaryPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Purcahse Summary</title>
    <style type="text/css">
        .style5
        {
            width: 166px;
        }
        .style7
        {
            width: 424px;
        }
    </style>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:99%;">
            <tr>
                <td align="center" 
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" ForeColor="Blue" 
                        Text="Detail Purchase Request Summary"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 771px;">
            <tr>
                <td align="center" class="style5" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label5" runat="server" Text="JP PART"></asp:Label>
                </td>
                <td align="center" class="style7" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label6" runat="server" Text="JP SPEC"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label7" runat="server" Text="PR NOT PO"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="style5" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbCode" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="left" class="style7" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbSpec" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbPR" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Label ID="Label2" runat="server" Text="จำนวนรายการ"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
&nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" Text="รายการ"></asp:Label>
        <asp:GridView ID="gvPr" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="365px">
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
