<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HttpErrorPage.aspx.vb" Inherits="MIS_HTI.HttpErrorPage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-image: url('Images/bg.jpg')">
    <form id="form1" runat="server">
<h2> Page Error Url ASPX: <span style="color:maroon;"><% Response.Write(Session("Path"))%></span></h2>
<h2> Page Error Url VB : <span style="color:maroon;"><% Response.Write(Session("PathVB"))%></span></h2>
<p> Function GetData Error : <span style="color:maroon;"><% Response.Write(Session("FC"))%></span></p>
    <div>
  Message Error : <span style="color:maroon;"> <%Response.Write(Session("Error"))%></span>
    </div>
<p> String Sql Error  :  <span style="color:maroon;"> <%Response.Write(Session("SQL"))%></span></p>
<br />
<div>
    <asp:Button ID="btBack" runat="server" Text="Back Page" Visible="False" />
&nbsp;<asp:Button ID="btSend" runat="server" Text="Send Error" Visible="False" />
</div>
    </form>
</body>
</html>