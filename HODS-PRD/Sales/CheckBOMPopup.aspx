<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CheckBOMPopup.aspx.vb" Inherits="MIS_HTI.CheckBOMPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" >
    <title>Detail Plan delivery stock</title>
    <style type="text/css">
        .style1
        {
            width: 124px;
        }
        .style2
        {
            width: 214px;
        }
    </style>
</head>
<body style="background-image: url('../Images/bg.jpg'); background-repeat: repeat">
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 696px">
            <tr>
                <td colspan="4" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label16" runat="server" Font-Size="X-Large" ForeColor="Blue" 
                        Text="BOM Detail"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" 
                    style="border: thin solid #0099FF; background-color: #FFFFFF;" 
                    class="style1">
                    <asp:Label ID="Label21" runat="server" Text="Item"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #0099FF; background-color: #FFFFFF;">
                    <asp:Label ID="lbItem" runat="server"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #0099FF; background-color: #FFFFFF;" 
                    class="style2">
                    <asp:Label ID="Label22" runat="server" Text="Spec"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #0099FF; background-color: #FFFFFF;">
                    <asp:Label ID="lbSpec" runat="server"></asp:Label>
                </td>
            </tr>
            </table>
        <asp:Label ID="Label1" runat="server" Text="จำนวนรายการ BOM Detail"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lbCountSO" runat="server"></asp:Label>
&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Text="รายการ"></asp:Label>
        <asp:GridView ID="gvBOMDetail" runat="server" BackColor="White" BorderColor="#3366CC" 
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
        <asp:Label ID="Label12" runat="server" Text="จำนวนรายการ BOM Stock"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lbCountMO" runat="server"></asp:Label>
&nbsp;&nbsp;
        <asp:Label ID="Label13" runat="server" Text="รายการ"></asp:Label>
        <asp:GridView ID="gvBOMList" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" 
            Width="697px">
            <Columns>
                <asp:TemplateField HeaderText="Detail">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
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
