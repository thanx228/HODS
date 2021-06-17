<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="MIS_HTI.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
        <br />
        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
            DataSourceID="SqlDataSource1" DataTextField="MX001" DataValueField="MX001">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
            SelectCommand="SELECT [MX001], [MX002] FROM [CMSMX] ORDER BY [MX001]">
        </asp:SqlDataSource>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button" />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    
    </div>
    </form>
</body>
</html>
