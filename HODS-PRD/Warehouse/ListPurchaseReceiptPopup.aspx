<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ListPurchaseReceiptPopup.aspx.vb" Inherits="MIS_HTI.ListPurchaseReceiptPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Sale Undelivery Status</title>
    <style type="text/css">
        .style1
        {
            height: 28px;
        }
        .style2
        {
            height: 28px;
            width: 271px;
        }
        .style3
        {
            width: 217px;
        }
        .style4
        {
            width: 175px;
        }
    </style>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 50%;">
            <tr>
                <td align="center" 
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label18" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Detail FG Inventory  Status"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width:50%;">
            <tr>
                <td align="left" bgcolor="White" class="style4">
                    <asp:Label ID="Label19" runat="server" Text="Purchase Receipt Type"></asp:Label>
                </td>
                <td align="left" bgcolor="White">
                    <asp:Label ID="lbType" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="left" bgcolor="White" class="style3">
                    <asp:Label ID="Label23" runat="server" Text="Purchase Receipt Number"></asp:Label>
                </td>
                <td align="left" bgcolor="White">
                    <asp:Label ID="lbNumber" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            </table>
        <table style="width: 50%;">
            <tr>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style2">
                    <asp:Label ID="Label15" runat="server" Text="จำนวนรายการ Purchase Receipt"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style1">
                    <asp:Label ID="lbCountSO" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style1">
                    <asp:Label ID="Label17" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPur" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            AutoGenerateColumns="False" Font-Size="Small">
            <Columns>
                <asp:BoundField DataField="TH001" HeaderText="Purchase Receipt" />
                <asp:BoundField DataField="TH003" HeaderText="Seq" />
                <asp:BoundField DataField="TH004" HeaderText="Item" />
                <asp:BoundField DataField="TH005" HeaderText="Item Desc" />
                <asp:BoundField DataField="TH006" HeaderText="Spec" />
                <asp:BoundField DataField="TH007" DataFormatString="{0:N}" 
                    HeaderText="Receipt Qty" />
                <asp:BoundField DataField="TH015" DataFormatString="{0:N}" 
                    HeaderText="Accept Qty" />
                <asp:BoundField DataField="TH017" DataFormatString="{0:N}" 
                    HeaderText="Insp.Return Qty" />
                <asp:BoundField DataField="TH008" HeaderText="Unit" />
                <asp:BoundField DataField="TH011" HeaderText="PO Type" />
                <asp:BoundField DataField="TH012" HeaderText="PO No." />
                <asp:BoundField DataField="TH013" HeaderText="PO Seq" />
                <asp:BoundField DataField="TH014" HeaderText="Inspect Date" />
                <asp:BoundField DataField="TH028" HeaderText="Insp Status" />
                <asp:BoundField DataField="TH033" HeaderText="Remark" />
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                Wrap="True" />
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
