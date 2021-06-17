<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PlanScheduleAddPop.aspx.vb" Inherits="MIS.PlanScheduleAddPop" %>

<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>

<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            
        }
        .style3
        {
            width: 199px;
        }
        .style4
        {
            width: 137px;
        }
        .style5
        {
            width: 178px;
        }
        </style>
</head>
<script src="../Scripts/jsScript.js" type="text/javascript"></script>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <uc1:HeaderForm ID="ucHeaderForm" runat="server" />
        <table style="width:63%;">
            <tr>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style4">
                    <asp:Label ID="Label2" runat="server" Text="MO"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style3">
                    <asp:Label ID="Label3" runat="server" Text="SPEC"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style5">
                    <asp:Label ID="Label4" runat="server" Text="MO Qty"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style5">
                    <asp:Label ID="Label5" runat="server" Text="Customer"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style4">
                    <asp:Label ID="lbMO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="left" style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style3">
                    <asp:Label ID="lbSpec" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style5">
                    <asp:Label ID="lbMoQty" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" style="border: thin solid #00CCFF; background-color: #FFFFFF" 
                    class="style5">
                    <asp:Label ID="lbCust" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
        <uc2:CountRow ID="ucCountRow1" runat="server" />
        <asp:GridView ID="gvMO" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            Height="16px" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Detail">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TB003" HeaderText="Item" />
                <asp:BoundField DataField="TB012" HeaderText="Desc" />
                <asp:BoundField DataField="TB013" HeaderText="Spec" />
                <asp:BoundField DataField="TB004" DataFormatString="{0:N3}" 
                    HeaderText="Require Qty">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="TB005" DataFormatString="{0:N3}" 
                    HeaderText="Issued Qty">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="TB0045" DataFormatString="{0:N3}" 
                    HeaderText="Not Issue Qty">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="TB007" HeaderText="Unit" />
                <asp:BoundField DataField="TB009" HeaderText="Warehouse" />
                <asp:BoundField DataField="STOCK" DataFormatString="{0:N3}" 
                    HeaderText="Stock Qty">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="ISSUE" DataFormatString="{0:N3}" 
                    HeaderText="Plan Issue Qty">
                <ItemStyle HorizontalAlign="Right" />
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
    
        <uc2:CountRow ID="ucCountRow2" runat="server" />
    
        <asp:GridView ID="gvOperation" runat="server" 
            Height="16px" AutoGenerateColumns="False">
        </asp:GridView>
    
        <uc2:CountRow ID="ucCountRow3" runat="server" />
        <asp:GridView ID="gvStatus" runat="server" 
            Height="16px" AutoGenerateColumns="False">
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
