<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="StockCardCarton.aspx.vb" Inherits="MIS_HTI.StockCardCarton" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register src="../UserControl/DropDownListUserControl.ascx" tagname="DropDownListUserControl" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tableMain {
            width: 75%;
            background-color: #FFFFFF;
        }
        .textRight{
            text-align:right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <table cellpadding="5" cellspacing="5" class="tableMain">
                <tr>
                    <td style="width: 20%">&nbsp;</td>
                    <td style="width: 25%">&nbsp;</td>
                    <td style="width: 10%">&nbsp;</td>
                    <td style="width: 45%">&nbsp;</td>
                </tr>
                <tr>
                    <td class="textRight">Month :</td>
                    <td>
                        <uc2:DropDownListUserControl ID="ucYearMonth" runat="server" />
                    </td>
                    <td class="textRight">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="textRight">Item : </td>
                    <td>
                        <asp:TextBox ID="tbItem" runat="server" MaxLength="20" Width="150px"></asp:TextBox>
                    </td>
                    <td class="textRight">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="textRight">Size :</td>
                    <td>
                        <asp:TextBox ID="tbSize" runat="server" MaxLength="30" Width="200px"></asp:TextBox>
                    </td>
                    <td class="textRight">Spec :</td>
                    <td>
                        <asp:TextBox ID="tbSpec" runat="server" MaxLength="70" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="textRight">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="textRight">
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#CC0000" Text="Remark : "></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="Label4" runat="server" ForeColor="#CC0000" Text="WH : 2402 (New)  ,  2501 (Old)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="textRight">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" align="center"
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btSearch" runat="server" Text="Search" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel" />
                        &nbsp;<asp:Button ID="btPrint" runat="server" Text="Print PDF" />
                    </td>
                </tr>
            </table>


            <uc1:CountRow ID="ucCountRows" runat="server" />
            <br />


           <asp:GridView ID="gvShow" runat="server" BorderStyle="None" BorderWidth="0px" CellPadding="4">
                <HeaderStyle BackColor="#3366CC" Font-Bold="True" ForeColor="White" Height="25px" HorizontalAlign="Center" Wrap="False" />
                <RowStyle BackColor="White" Wrap="False" />
            </asp:GridView>

        </ContentTemplate>

        <%--Export Excel--%>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>


    </asp:UpdatePanel>
</asp:Content>
