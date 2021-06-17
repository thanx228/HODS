<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MaterailsCallInPopup.aspx.vb" Inherits="MIS_HTI.MaterailsCallInPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Materails Call In</title>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td align="left" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Detail Materials  Call In"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width:100%;">
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label19" runat="server" Text="Code"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label17" runat="server" Text="Spec"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label15" runat="server" Text="Mat Issue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label13" runat="server" Text="On Stock"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label11" runat="server" Text="Delivery"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label9" runat="server" Text="MO"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label28" runat="server" Text="PO Insp"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label7" runat="server" Text="PO"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label33" runat="server" Text="PO Forcast"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label32" runat="server" Text="PO MO"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label31" runat="server" Text="PO Manual"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label5" runat="server" Text="PR"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbPart" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="left" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbSpec" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbIssue" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbStock" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbDel" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbMO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbPoInsp" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbPO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbPOFor" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbPOMO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbPOMan" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbPR" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label2" runat="server" 
                        Text="จำนวนรายการ Sale Order             "></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountSO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label4" runat="server" Text="     รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSO" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="Medium" 
            Height="16px">
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
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label20" runat="server" Text="จำนวนรายการ  Mat Issue           "></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountIssue" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label21" runat="server" Text="     รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvIssue" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            Font-Size="Medium" Height="16px">
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
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label22" runat="server" Text="จำนวนรายการ   MO           "></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountMO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label23" runat="server" Text="     รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMO" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="Medium" 
            Height="16px">
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
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="Medium" 
            Height="16px">
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
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="Medium" 
            Height="16px">
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
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label29" runat="server" 
                        Text="จำนวนรายการ  Purchase Receipt Inspection            "></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountPoInsp" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label30" runat="server" Text="     รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPoInsp" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="Medium" 
            Height="16px">
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
