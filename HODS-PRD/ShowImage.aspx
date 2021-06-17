<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ShowImage.aspx.vb" Inherits="MIS_HTI.ShowImage" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-image: url('../Images/bg2.jpg'); background-repeat: repeat">
    <form id="form1" runat="server" >
     <asp:Image ID="ImgShow" runat="server" EnableViewState="False" Height="500px" 
         Width="500px"  />
     </form>
</body>
</html>
