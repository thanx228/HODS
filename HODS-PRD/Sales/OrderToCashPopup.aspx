<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OrderToCashPopup.aspx.vb" Inherits="MIS_HTI.OrderToCashPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail MO </title>
    </head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td align="left" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Detail Order to Cash"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="btExport" runat="server" Text="Export Excel" />
            <br />
            <asp:Label ID="Label9" runat="server" ForeColor="Blue" Text="Sale Order Detail"></asp:Label>
            <table style="width: 44%;">
                <tr>
                    <td align="center" style="background-color: #FFFFFF; ">
                        <asp:Label ID="Label5" runat="server" Text="Number of Row"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF; ">
                        <asp:Label ID="lbCountSO" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF; ">
                        <asp:Label ID="Label6" runat="server" Text="Row"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvSO" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                >
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
            <asp:Label ID="Label10" runat="server" ForeColor="Blue" 
                Text="Manufactor Order Detail"></asp:Label>
            <table style="width: 44%;">
                <tr>
                    <td align="center" style="background-color: #FFFFFF; ">
                        <asp:Label ID="Label7" runat="server" Text="Number of Row"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF; ">
                        <asp:Label ID="lbCountMO" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF; ">
                        <asp:Label ID="Label8" runat="server" Text="Row"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvMO" runat="server" BackColor="White" BorderColor="#3366CC" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4">
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
            <asp:Label ID="Label11" runat="server" ForeColor="Blue" Text="Invoice Detail"></asp:Label>
            <table style="width: 44%;">
                <tr>
                    <td align="center" style="background-color: #FFFFFF; ">
                        <asp:Label ID="Label2" runat="server" Text="Number of Row"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF; ">
                        <asp:Label ID="lbCountInv" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF; ">
                        <asp:Label ID="Label4" runat="server" Text="Row"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvInv" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                >
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
        
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
