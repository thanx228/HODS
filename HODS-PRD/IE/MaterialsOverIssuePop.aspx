<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MaterialsOverIssuePop.aspx.vb" Inherits="MIS_HTI.MaterialsOverIssuePop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Materials Over Issue</title>
</head>
<body style="background-image: url('../Images/bg.jpg')">
     <form id="form1" runat="server">
    <div>
    
        <table style="width: 52%;">
            <tr>
                <td align="center" 
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label14" runat="server" ForeColor="Blue" 
                        Text="Detail Materilas Issue Over"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="background-color: #FFFFFF; border: thin solid #0099FF">
                    <asp:Label ID="Label10" runat="server" Text="MO Type"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF; border: thin solid #0099FF">
                    <asp:Label ID="lbMoType" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF; border: thin solid #0099FF">
                    <asp:Label ID="Label11" runat="server" Text="MO No"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF; border: thin solid #0099FF">
                    <asp:Label ID="lbMoNo" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="background-color: #FFFFFF; border: thin solid #0099FF">
                    <asp:Label ID="Label16" runat="server" Text="Issue Qty"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF; border: thin solid #0099FF" align="right">
                    <asp:Label ID="lbIssueQty" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF; border: thin solid #0099FF">
                    <asp:Label ID="Label17" runat="server" Text="Issue Qty Over"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF; border: thin solid #0099FF" align="right">
                    <asp:Label ID="lbIssueOver" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="background-color: #FFFFFF; border: thin solid #0099FF">
                    <asp:Label ID="Label15" runat="server" Text="RW Code"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF; border: thin solid #0099FF">
                    <asp:Label ID="lbRwCode" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF; border: thin solid #0099FF">
                    &nbsp;</td>
                <td style="background-color: #FFFFFF; border: thin solid #0099FF">
                    &nbsp;</td>
            </tr>
        </table>
        <table style="width: 33%;">
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #0099FF">
                    <asp:Label ID="Label7" runat="server" Text="จำนวนรายการ"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #0099FF">
                    <asp:Label ID="lbCnt" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #0099FF">
                    <asp:Label ID="Label9" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvShow" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            Font-Size="Medium" Height="16px">
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
