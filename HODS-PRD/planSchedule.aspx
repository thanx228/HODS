<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="planSchedule.aspx.vb" Inherits="MIS_HTI.planSchedule" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
    </style>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <p>
        <table style="width: 37%;">
            <tr>
                <td class="style1" style="background-color: #FFFFFF">
                    <asp:Label ID="Label1" runat="server" Text="WC"></asp:Label>
                </td>
                <td class="style1" style="background-color: #FFFFFF">
                    <asp:DropDownList ID="ddlWc" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="background-color: #FFFFFF">
                    <asp:Button ID="btCheck" runat="server" Text="Check Plan" Width="100px" />
                </td>
            </tr>
        </table>
    </p>
    </form>
    </body>
</html>
