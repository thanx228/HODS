<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="LRPreport.aspx.vb" Inherits="MIS_HTI.LRPreport" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <style type="text/css">
        .tableShow {
            width: 95%;
            background-color: #FFFFFF;
        }
        .textRight{
            text-align:right;
        }
    </style>

    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            GvScrollShow();
        });
        function GvScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth -30,
                height: 500,
                freezesize: 0,
                arrowsize: 30,
                headerrowcount: 1,
                railsize: 16,
                barsize: 8
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <table cellpadding="5" cellspacing="5" class="tableShow">
                <tr>
                    <td class="textRight">Report :</td>
                    <td>
                        <asp:DropDownList ID="ddlReport" runat="server" AutoPostBack="True">
                            <asp:ListItem Value="1">Production</asp:ListItem>
                            <asp:ListItem Value="2">Purchase</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;</td>
                    <td class="textRight">
                        <asp:Label ID="lbType" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server">
                        </asp:DropDownList>
                        &nbsp;<asp:CheckBox ID="cbReleased" runat="server" Text="Released" />
                    </td>
                </tr>
                <tr>
                    <td class="textRight">Plan Batch No : </td>
                    <td>
                        <asp:TextBox ID="tbBatchNo" runat="server" Width="200px" MaxLength="30"></asp:TextBox>
                    </td>
                    <td class="textRight">Version : </td>
                    <td>
                        <asp:TextBox ID="tbVersion" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">Item : </td>
                    <td>
                        <asp:TextBox ID="tbItem" runat="server" Width="200px" MaxLength="30"></asp:TextBox>
                    </td>
                    <td class="textRight">Spec : </td>
                    <td>
                        <asp:TextBox ID="tbSpec" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">SO Type : </td>
                    <td>
                        <asp:DropDownList ID="ddlSoType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="textRight">&nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="textRight">SO :</td>
                    <td>
                        <asp:TextBox ID="tbSo" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                    </td>
                    <td class="textRight">SO Seq :</td>
                    <td>
                        <asp:TextBox ID="tbSoSeq" runat="server" Width="100px" MaxLength="5"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">
                        <asp:DropDownList ID="ddlDateType" runat="server">
                            <asp:ListItem Value="0">SO Order Date</asp:ListItem>
                            <asp:ListItem Value="1">SO Plan Delivery Date</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <uc2:Date ID="ucDateFrom" runat="server" />
                    </td>
                    <td class="textRight">Date To :</td>
                    <td>
                        <uc2:Date ID="ucDateTo" runat="server" />
                    </td>
                </tr>
            </table>

            <table class="tableShow">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x" 
                        align="center">
                        <asp:Button ID="btShow" runat="server" Text="Show" />
                        &nbsp;<asp:Button ID="btExcel" runat="server" Text="Excel" />
                    </td>
                </tr>
            </table>

            <uc1:CountRow ID="ucCountRow" runat="server" />

             <asp:GridView ID="gvShow" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="4">
                <HeaderStyle BackColor="#003399" ForeColor="#CCCCFF" HorizontalAlign="Center" Wrap="False" />
                 <RowStyle Wrap="False" />
            </asp:GridView>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btExcel" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>
