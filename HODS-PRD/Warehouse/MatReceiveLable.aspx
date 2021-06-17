<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MatReceiveLable.aspx.vb" Inherits="MIS_HTI.MatReceiveLable" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/docTypeD.ascx" tagname="docTypeD" tagprefix="uc2" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 262px;
        }
        .style7
        {
            width: 148px;
        }
    </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 500,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 0,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize: 8
            });
        }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label3" runat="server" Text="Receipt Type"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <uc2:docTypeD ID="ucReceiptType" runat="server" />
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label5" runat="server" Text="Receipt No."></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbReceiptNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label4" runat="server" Text="Receipt Date"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <uc3:Date ID="ucReceiptDate" runat="server" />
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label6" runat="server" Text="Lot"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbLot" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label7" runat="server" Text="App Status"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:DropDownList ID="ddlApp" runat="server">
                            <asp:ListItem>ALL</asp:ListItem>
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td bgcolor="White">
                        &nbsp;</td>
                    <td bgcolor="White">
                        <asp:HyperLink ID="hlEmptyLable" runat="server" Target="_blank">ลาเบลเปล่า</asp:HyperLink>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btPrint" runat="server" Text="Print All" Visible="False" />
                        &nbsp;<asp:Button ID="btShow" runat="server" Text="Show" />
                        &nbsp;<asp:Button ID="btReset" runat="server" Text="Reset" />
                        &nbsp;</td>
                </tr>
            </table>
            <uc4:CountRow ID="ucCountRow" runat="server" />
            <asp:GridView ID="gvShow" runat="server">
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
