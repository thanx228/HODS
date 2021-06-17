<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="planDeliveryPopup.aspx.vb" Inherits="MIS_HTI.planDeliveryPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" >
    <title>Detail Plan delivery stock</title>
</head>
<body style="background-image: url('../Images/bg.jpg'); background-repeat: repeat">
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 696px">
            <tr>
                <td align="center" colspan="4" 
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label16" runat="server" Font-Size="X-Large" ForeColor="Blue" 
                        Text="Detail Plan delivery stock"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" 
                    style="border: thin solid #0099FF; background-color: #FFFFFF;">
                    <asp:Label ID="Label4" runat="server" Text="JP Part"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #0099FF; background-color: #FFFFFF;">
                    <asp:Label ID="Label5" runat="server" Text="JP SPEC"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #0099FF; background-color: #FFFFFF;">
                    <asp:Label ID="Label6" runat="server" Text="Over All Delivery"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #0099FF; background-color: #FFFFFF;">
                    <asp:Label ID="Label7" runat="server" Text="On Stock"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="border: thin solid #0099FF; background-color: #FFFFFF;">
                    <asp:Label ID="lbPart" runat="server"></asp:Label>
                </td>
                <td align="left" style="border: thin solid #0099FF; background-color: #FFFFFF;">
                    <asp:Label ID="lbSpec" runat="server"></asp:Label>
                </td>
                <td align="right" 
                    style="border: thin solid #0099FF; background-color: #FFFFFF;">
                    <asp:Label ID="lbNotDel" runat="server"></asp:Label>
                </td>
                <td align="right" 
                    style="border: thin solid #0099FF; background-color: #FFFFFF;">
                    <asp:Label ID="lbStock" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Label ID="Label1" runat="server" Text="จำนวนรายการ Sale Order"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lbCountSO" runat="server"></asp:Label>
&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Text="รายการ"></asp:Label>
        <asp:GridView ID="gvSO" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" 
            Font-Size="Medium" Height="16px" Width="697px">
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
        <asp:Label ID="Label12" runat="server" Text="จำนวนรายการ MO"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lbCountMO" runat="server"></asp:Label>
&nbsp;&nbsp;
        <asp:Label ID="Label13" runat="server" Text="รายการ"></asp:Label>
        <asp:GridView ID="gvMO" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" 
            Font-Size="Medium" Height="16px" Width="697px">
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
        <asp:Label ID="Label14" runat="server" Text="จำนวนรายการ PO"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lbCountPO" runat="server"></asp:Label>
&nbsp;&nbsp;
        <asp:Label ID="Label15" runat="server" Text="รายการ"></asp:Label>
        <asp:GridView ID="gvPO" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" 
            Font-Size="Medium" Height="16px" Width="697px">
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
        <asp:Label ID="Label17" runat="server" Text="จำนวนรายการ PR"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lbCountPR" runat="server"></asp:Label>
&nbsp;&nbsp;
        <asp:Label ID="Label18" runat="server" Text="รายการ"></asp:Label>
        <asp:GridView ID="gvPR" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" 
            Font-Size="Medium" Height="16px" Width="697px">
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
        <asp:Label ID="Label19" runat="server" Text="จำนวนรายการ Mat Issue"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lbCountIssue" runat="server"></asp:Label>
&nbsp;&nbsp;
        <asp:Label ID="Label20" runat="server" Text="รายการ"></asp:Label>
        <asp:GridView ID="gvIssue" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            Font-Size="Medium" Height="16px" Width="697px">
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
        <br />
    
    </div>
    </form>
</body>
</html>
