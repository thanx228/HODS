<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleInvoiceReport.aspx.vb" Inherits="MIS_HTI.SaleInvoiceReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>

<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>

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

            <table class="tableMain">
                <tr>
                    <td class="textRight">Invoice Type :</td>
                    <td colspan="3">
                        <asp:CheckBoxList ID="cblInvType" runat="server" CellPadding="5" CellSpacing="5">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">Invoice No :</td>
                    <td>
                        <asp:TextBox ID="tbInvoice" runat="server" MaxLength="20" Width="150px"></asp:TextBox>
                    </td>
                    <td class="textRight">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="textRight">&nbsp;</td>
                    <td class="textRight" colspan="3"><hr></td>
                </tr>
                <tr>
                    <td class="textRight">Source Order Type :</td>
                    <td colspan="3">
                        <asp:CheckBoxList ID="cblSourceType" runat="server" CellPadding="5" CellSpacing="5">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">Source No :</td>
                    <td>
                        <asp:TextBox ID="tbSourceNo" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td class="textRight">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="textRight">&nbsp;</td>
                    <td class="textRight" colspan="3"><hr></td>
                </tr>
                <tr>
                    <td class="textRight">Customer :</td>
                    <td>
                        <asp:TextBox ID="tbCustomer" runat="server" MaxLength="10" Width="50px"></asp:TextBox>
                    </td>
                    <td class="textRight">Cust PO :</td>
                    <td>
                        <asp:TextBox ID="tbCustPO" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">Item :</td>
                    <td>
                        <asp:TextBox ID="tbItem" runat="server" MaxLength="20" Width="150px"></asp:TextBox>
                    </td>
                    <td class="textRight">Spec :</td>
                    <td>
                        <asp:TextBox ID="tbSpec" runat="server" MaxLength="20" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">
                        <asp:DropDownList ID="ddlSearchDate" runat="server">
                            <asp:ListItem Value="0">Order Date</asp:ListItem>
                            <asp:ListItem Value="1">Invoice Date</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;From :</td>
                    <td>
                        <uc2:Date ID="ucDateFrom" runat="server" />
                    </td>
                    <td class="textRight">To :</td>
                    <td>
                        <uc2:Date ID="ucDateTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center"
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btSearch" runat="server" Text="Search" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel" />
                    </td>
                </tr>
            </table>

            <uc1:CountRow ID="ucCountRow" runat="server" />

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
