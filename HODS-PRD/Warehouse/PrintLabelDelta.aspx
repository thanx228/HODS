<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PrintLabelDelta.aspx.vb" Inherits="MIS_HTI.PrintLabelDelta" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc3" %>
<%@ Register src="../UserControl/docTypeD.ascx" tagname="docTypeD" tagprefix="uc4" %>
<%@ Register assembly="Spire.Barcode" namespace="Spire.Barcode.WebUI" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollgvShow() {
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
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:HeaderForm ID="ucHeader" runat="server" />
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Cust ID"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCust" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Plant"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPlant" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Invoice Type"></asp:Label>
                    </td>
                    <td>
                        <uc4:docTypeD ID="ucInvType" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Invoice No"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbInvNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Invoice Date "></asp:Label>
                    </td>
                    <td>
                        <uc3:Date ID="ucDate" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Time"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbTime" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Gen Barcode"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGenBar" runat="server">
                            <asp:ListItem Value="ALL">ALL</asp:ListItem>
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btGenBar" runat="server" Text="Generate Barcode" />
                        &nbsp;<asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btReset" runat="server" Text="Reset" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                        <asp:Button ID="btPrint" runat="server" Text="Print All" 
                            UseSubmitBehavior="False" onclientclick="SetTarget();" />
                    </td>
                </tr>
            </table>
            <uc2:CountRow ID="ucCount" runat="server" />
            <asp:GridView ID="gvShow" runat="server">
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
