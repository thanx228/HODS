<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopSpec.aspx.vb" Inherits="MIS_HTI.PopSpec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 82px;
        }
        .style3
        {
            width: 81px;
        }
        .style4
        {
            width: 133px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <script language="javascript">
        function GetRowValue(val,val1,val2) {
            //  สร้างฟังก์ชั่นการคืนค่ากลับ
            //  อย่าลืม ContentPlaceHolder1 และ TextBox1 ต้องให้ตรงกันนะครับ      
            window.opener.document.getElementById("ctl00_MainContent_txtitem").value = val;
            window.opener.document.getElementById("ctl00_MainContent_txtdesc").value = val1;
            window.opener.document.getElementById("ctl00_MainContent_txtspec").value = val2;
            window.close();
        }
  </script>
    <div>
    
        <table class="style1">
            <tr>
                <td class="style2">
                    <asp:Label ID="Label1" runat="server" Text="Search By :"></asp:Label>
                </td>
                <td class="style3">
                    <asp:DropDownList ID="DDLSearch" runat="server" AutoPostBack="True">
                        <asp:ListItem>Item</asp:ListItem>
                        <asp:ListItem>Desc</asp:ListItem>
                        <asp:ListItem>Spec</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style4">
                    <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Busearch" runat="server" Text="Search" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <asp:GridView ID="GridSpec" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
            DataKeyNames="MB001" DataSourceID="SqlDataSource1" ForeColor="#333333" 
            GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" Text="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="MB001" HeaderText="Item" ReadOnly="True" 
                    SortExpression="MB001" />
                <asp:BoundField DataField="MB002" HeaderText="DESC" SortExpression="MB002" />
                <asp:BoundField DataField="MB003" HeaderText="SPEC" SortExpression="MB003" />
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
            SelectCommand="select MB001,MB002,MB003 from INVMB where MB109 = 'Y'">
        </asp:SqlDataSource>
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
