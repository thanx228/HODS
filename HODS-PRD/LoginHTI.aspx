<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoginHTI.aspx.vb" Inherits="MIS_HTI.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <title>User Login</title>
    
    <style type="text/css">
        .style32
        {
            height: 93px;
        }
        .style33
        {
            height: 20px;
        }
        .style34
        {
            width: 633px;
        }
        .style35
        {
            height: 20px;
            width: 633px;
        }
        .style36
        {
            width: 371px;
        }
        .style37
        {
            height: 20px;
            width: 274px;
        }
        .style38
        {
            width: 274px;
        }
    </style>
    
</head>
<body  background="/Images/bg.jpg">
    <form id="lgMember" runat="server">
    <div align="center">
    <asp:ScriptManager ID="ScriptManager1" runat="server" /> 
        
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
    <ContentTemplate> 
  <table style="width:92%; height: 100px;" >
            <tr>
                <td style="background-image: url('Images/hd.jpg'); background-repeat: no-repeat;" 
                    class="style32">
                    </td>
            </tr>
            <tr>
                <td class="style32">
                    <table>
                        <tr>
                            <td class="style38">
                                &nbsp;</td>
                            <td align="right" style="color: #0000FF">
                                User Name</td>
                            <td align="left" class="style34">
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style37">
                            </td>
                            <td align="right" class="style33" style="color: #0000FF; font-size: small">
                                Password</td>
                            <td align="left" class="style35">
                                <asp:TextBox ID="PassWord" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                            <td class="style33">
                            </td>
                        </tr>
                        <tr>
                            <td class="style38">
                                &nbsp;</td>
                            <td align="right">
                                &nbsp;</td>
                            <td align="left" class="style34">
                                <asp:Button ID="Btlogin" runat="server" CssClass="menu" Text="  Login" 
                                    Width="100px" />
                                <asp:Label ID="lbError" runat="server" ForeColor="#CC0066"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate> 
      </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
