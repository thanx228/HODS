<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ItemPopup.aspx.vb" Inherits="MIS_HTI.ItemPopup" %>

<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Plan Request Stock</title>
    <style type="text/css">
        .style1
        {
            width: 37px;
        }
        .style2
        {
            width: 189px;
        }
        .style3
        {
            width: 175px;
        }
        .style4
        {
            width: 134px;
        }
        .auto-style1 {
            width: 40%;
        }
        .auto-style2 {
            height: 25px;
        }
    </style>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>

        <table style="width:95%; background-image: url('../Images/btt.jpg'); background-repeat: repeat-x;">
            <tr>
                <td align="center"
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" Text="Item Detail"
                        Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
        <table bgcolor="White" style="width: 95%;">
            <tr>
                <td style="width:10%">
                    <asp:Label ID="Label2" runat="server" Text="Item"></asp:Label>
                </td>
                <td style="width:40%">
                    <asp:Label ID="LbItem" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td style="width:10%">
                    <asp:Label ID="Label3" runat="server" Text="Desc"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="LbDesc" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Spec"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LbSpec" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Unit"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="LbUnit" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            </table>
        <table style="width: 95%;">
            <tr>
                <td align="center" class="auto-style2" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                    <asp:Button ID="BtShow" runat="server" Text="Show Again" />
                </td>
            </tr>
        </table>
        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="9" Width="95%">

            <ajaxToolkit:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>

                    Summary
</HeaderTemplate>
<ContentTemplate>

                    <uc1:CountRow ID="UcCountRowSummary" runat="server">
                    </uc1:CountRow>

                    <asp:GridView ID="GvSummary" runat="server"></asp:GridView>
</ContentTemplate>
</ajaxToolkit:TabPanel><ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2"><HeaderTemplate>

Stock
</HeaderTemplate>
<ContentTemplate>
<uc1:CountRow ID="UcCountRowStock" runat="server"></uc1:CountRow>
<asp:GridView ID="GvStock" runat="server">
</asp:GridView>
</ContentTemplate>
</ajaxToolkit:TabPanel><ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3"><HeaderTemplate>

                    Pur Request
</HeaderTemplate>
<ContentTemplate>
<uc1:CountRow ID="UcCountRowPr" runat="server"></uc1:CountRow>
<asp:GridView ID="GvPr" runat="server">
</asp:GridView>
</ContentTemplate>
</ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4"><HeaderTemplate>

                    Pur Order
</HeaderTemplate>
<ContentTemplate>
<uc1:CountRow ID="UcCountRowPo" runat="server"></uc1:CountRow>
<asp:GridView ID="GvPo" runat="server">
</asp:GridView>
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel10" runat="server" HeaderText="TabPanel10">
    <HeaderTemplate>
        QC Inspection
</HeaderTemplate>
<ContentTemplate>
<uc1:CountRow ID="UcCountRowQc" runat="server"></uc1:CountRow>
<asp:GridView ID="GvQc" runat="server">
</asp:GridView>
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="TabPanel5">
    <HeaderTemplate>
        Sale Order
</HeaderTemplate>
<ContentTemplate>
<uc1:CountRow ID="UcCountRowSo" runat="server"></uc1:CountRow>
<asp:GridView ID="GvSo" runat="server">
</asp:GridView>
</ContentTemplate>
</ajaxToolkit:TabPanel><ajaxToolkit:TabPanel ID="TabPanel6" runat="server" HeaderText="TabPanel6"><HeaderTemplate>

                    Manf Order
</HeaderTemplate>
<ContentTemplate>
<uc1:CountRow ID="UcCountRowMo" runat="server"></uc1:CountRow>
<asp:GridView ID="GvMo" runat="server">
</asp:GridView>
</ContentTemplate>
</ajaxToolkit:TabPanel><ajaxToolkit:TabPanel ID="TabPanel7" runat="server" HeaderText="TabPanel7"><HeaderTemplate>
Mat Issue
</HeaderTemplate>
<ContentTemplate>
<uc1:CountRow ID="UcCountRowIssue" runat="server"></uc1:CountRow>
<asp:GridView ID="GvIssue" runat="server">
</asp:GridView>
</ContentTemplate>
</ajaxToolkit:TabPanel><ajaxToolkit:TabPanel ID="TabPanel8" runat="server" HeaderText="TabPanel8"><HeaderTemplate>

                    BOM
</HeaderTemplate>
<ContentTemplate>
<uc1:CountRow ID="UcCountRowBom" runat="server"></uc1:CountRow>
<asp:GridView ID="GvBom" runat="server">
</asp:GridView>
</ContentTemplate>
</ajaxToolkit:TabPanel><ajaxToolkit:TabPanel ID="TabPanel9" runat="server" HeaderText="TabPanel9"><HeaderTemplate>

                    BOM Child
</HeaderTemplate>
<ContentTemplate>
<uc1:CountRow ID="UcCountRowBomChild" runat="server"></uc1:CountRow>
<asp:GridView ID="GvBomChild" runat="server">
</asp:GridView>
</ContentTemplate>
</ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>