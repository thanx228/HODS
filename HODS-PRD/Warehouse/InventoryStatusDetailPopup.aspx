<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InventoryStatusDetailPopup.aspx.vb" Inherits="MIS_HTI.InvenStatusDetailPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">





<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td align="left" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Inventory Status Detail"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width:100%;">
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label19" runat="server" Text="Item"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label17" runat="server" Text="JP Spec."></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label3" runat="server" Text="SO Undelivery"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label8" runat="server" Text="2101 FG"></asp:Label>
                </td>
            </tr>
            <tr>
                
                 <td align="left" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbItem" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="left" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbSpec" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbSOUn" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lb2101" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
            </tr>
        </table>


        <br />


        <br />
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label2" runat="server" Text="จำนวนรายการ   Sale Order      "></asp:Label>
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
        <asp:GridView ID="gvIN" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="S01" HeaderText="SO Type-No." />
                    <asp:BoundField DataField="S02" HeaderText="SO Seq." />
                    <asp:BoundField HeaderText="Item" DataField="IN01">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Spec." DataField="IN02">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="SO Undelivery" DataField="IN03" 
                    DataFormatString="{0:#,##0.000}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IN05" HeaderText="Ware House">
                    </asp:BoundField>
                    <asp:BoundField DataField="IN06" HeaderText="Store Bin"/>
                    <asp:BoundField HeaderText="Due Date" DataField="IN07">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Cus. Code" DataField="IN08">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Cus. Name" DataField="IN09">
                    </asp:BoundField>
                </Columns>
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
