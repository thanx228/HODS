<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="noBomItemBlockPop.aspx.vb" Inherits="MIS_HTI.noBomItemBlockPop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 124px;
        }
    </style>
</head>
<body background="../Images/bg.jpg">
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 49%; background-color: #FFFFFF;">
            <tr>
                <td class="style1" style="border: thin solid #0033CC">
                    <asp:Label ID="Label1" runat="server" Text="Code"></asp:Label>
                </td>
                <td style="border: thin solid #0033CC">
                    <asp:Label ID="lbCode" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1" style="border: thin solid #0033CC">
                    <asp:Label ID="Label2" runat="server" Text="Name"></asp:Label>
                </td>
                <td style="border: thin solid #0033CC">
                    <asp:Label ID="lbName" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 48%; height: 25px;">
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="จำนวนรายการ"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbCnt" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
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
    
    </div>
    </form>
</body>
</html>
