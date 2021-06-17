<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UserInfoPopDept.aspx.vb" Inherits="MIS_HTI.UserInfoPopDept" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 161px;
        }
        .style2
        {
        }
        .style3
        {}
        .style4
        {
            width: 43px;
        }
    </style>
</head>
<body background="../Images/bg.jpg">
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 50%;">
                    <tr>
                        <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                            <asp:Label ID="Label7" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                                Text="User And Work Center"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 50%;">
                    <tr>
                        <td bgcolor="White" class="style1">
                            <asp:Label ID="Label2" runat="server" Text="User"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style2">
                            <asp:Label ID="lbUser" runat="server" ForeColor="Blue"></asp:Label>
                            <asp:Label ID="lbId" runat="server"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style4">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="style1">
                            <asp:Label ID="Label6" runat="server" Text="Name"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style2">
                            <asp:Label ID="lbName" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style4">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="style1">Groups</td>
                        <td bgcolor="White" class="style2">
                            <asp:RadioButtonList ID="rblGrp" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value ="user">user</asp:ListItem>
                                <asp:ListItem Value ="dept_head">dept head</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td bgcolor="White" class="style4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="style1" style="vertical-align: top">
                            <asp:Label ID="Label3" runat="server" Text="Dept."></asp:Label>
                        </td>
                        <td bgcolor="White" class="style2" colspan="2">
                            <asp:CheckBoxList ID="cblDept" runat="server">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
                <table style="width:50%;">
                    <tr>
                        <td class="style3" align="center" 
                            style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                            <asp:Button ID="btUnSelAll" runat="server" Text="Un Select All" />
                            <asp:Button ID="btSelAll" runat="server" Text="Select All" />
                            &nbsp;<asp:Button ID="btSave" runat="server" Text="Save" Width="80px" />
                            &nbsp;</td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
