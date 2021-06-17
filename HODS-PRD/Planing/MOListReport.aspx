<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MOListReport.aspx.vb" Inherits="MIS_HTI.MOListReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<%@ Register src="../UserControl/GridviewShow.ascx" tagname="GridviewShow" tagprefix="uc3" %>
<%@ Register Src="~/UserControl/GridviewShow.ascx" TagPrefix="uc1" TagName="GridviewShow" %>
<%@ Register Src="~/UserControl/CountRow.ascx" TagPrefix="uc1" TagName="CountRow" %>
<%@ Register Src="~/UserControl/Date.ascx" TagPrefix="uc2" TagName="Date" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style8 {
            width: 75%;
            background-color: #FFFFFF;
        }
        .auto-style14 {
            width: 243px;
        }
        .auto-style15 {
            width: 98px;
        }
        .auto-style18 {
            width: 243px;
            height: 41px;
        }
        .auto-style19 {
            width: 98px;
            height: 41px;
        }
        .auto-style20 {
            height: 41px;
        }
        .auto-style21 {
            width: 144px;
            height: 41px;
        }
        .auto-style22 {
            width: 144px;
        }
    </style>

    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollShow();
        });

        function gridviewScrollShow() {
            gridView1 = $('#<%= gvshow.ClientID %>').gridviewScroll({
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
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>
    <table class="auto-style8" cellpadding="5" cellspacing="5">
        <tr>
            <td style="background-color: #FFFFFF; vertical-align: top;" class="auto-style21">
                <asp:Label ID="Label12" runat="server" Text="MO Type"></asp:Label>
            </td>
            <td colspan="3" style="background-color: #FFFFFF; vertical-align: top;">
                <asp:CheckBoxList ID="cblMoType" runat="server">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="auto-style22">&nbsp;MO DATE FROM :</td>
            <td class="auto-style14"><uc1:Date ID="sdate" runat="server" /></td>
            <td class="auto-style15">&nbsp;MO DATE TO :</td>
            <td><uc1:Date ID="edate" runat="server" /></td>
        </tr>
        <tr>
            <td class="auto-style21">&nbsp;ITEM :</td>
            <td class="auto-style18"><asp:TextBox ID="txtitem" runat="server"></asp:TextBox></td>
            <td class="auto-style19">&nbsp;SPEC :</td>
            <td class="auto-style20"><asp:TextBox ID="txtspec" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="auto-style22">&nbsp;MO STATUS :</td>
            <td class="auto-style14">
                <asp:DropDownList ID="ddlmotus" runat="server">
                   <%-- <asp:ListItem Selected="True" Value="A">ALL Status</asp:ListItem>--%>
                    <asp:ListItem Selected="True" Value="N">Not Complete</asp:ListItem>
                    <asp:ListItem Value="Y">Y:Completed</asp:ListItem>                   
                    <asp:ListItem Value="1">1:Not Produced </asp:ListItem>
                    <asp:ListItem Value="2">2:Issued </asp:ListItem>
                    <asp:ListItem Value="3">3:Producing  </asp:ListItem>
                    
                </asp:DropDownList>
            </td>
            <td class="auto-style15">&nbsp;APP STATUS :</td>
            <td>
                <asp:DropDownList ID="ddlapptus" runat="server">
                    <%--<asp:ListItem Selected="True" Value="A">ALL Status</asp:ListItem>--%>
                    <asp:ListItem Selected="True" Value="Y">Y:Approved</asp:ListItem>
                    <asp:ListItem Value="N">N:Not Approved </asp:ListItem>
                    <asp:ListItem Value="V">V:Cancel </asp:ListItem>
                    <asp:ListItem Value="U">U:Approve failed</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>

        <table style="width: 75%;">
            <tr>
                <td align="center"
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Button ID="btshow" runat="server" Text="Show Report" />
                    &nbsp;<asp:Button ID="btexcel" runat="server" Text="Export Excel" />
                    &nbsp;<asp:Button ID="clear" runat="server" Text="CLEAR" />
                </td>
            </tr>
        </table>

        <uc1:CountRow ID="CountRow" runat="server" />

        <asp:GridView ID="gvshow" runat="server" BorderStyle="None" BorderWidth="0px" CellPadding="4" Width="242px">
                <HeaderStyle BackColor="#3366CC" Font-Bold="True" ForeColor="White" Height="25px" HorizontalAlign="Center" Wrap="False" />
                <RowStyle BackColor="White" Wrap="False" />
            </asp:GridView>

</contenttemplate>
        <triggers>
            <asp:PostBackTrigger ControlID="btexcel" />
        </triggers>
    </asp:UpdatePanel>
</asp:Content>
