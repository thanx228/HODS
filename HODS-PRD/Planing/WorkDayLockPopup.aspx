﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WorkDayLockPopup.aspx.vb" Inherits="MIS_HTI.WorkDayLockPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail MO </title>
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
    </style>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td align="left" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Detail MO"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="btExport" runat="server" Text="Export Excel" />
            <table style="width: 59%;">
                <tr>
                    <td class="style1" style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td class="style1" style="background-color: #FFFFFF">
                        <asp:Label ID="lbWc" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="MO Status"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="lbLock" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="From Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="lbDateFrom" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="To Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="lbDateTo" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 44%;">
                <tr>
                    <td align="center" style="background-color: #FFFFFF; ">
                        <asp:Label ID="Label2" runat="server" Text="Number of Row"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF; ">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF; ">
                        <asp:Label ID="Label4" runat="server" Text="Row"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                >
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
