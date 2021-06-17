<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MOReceiptReport.aspx.vb" Inherits="MIS_HTI.MOReceiptReport" %>
<%@ Register Src="~/UserControl/docTypeC.ascx" TagPrefix="uc1" TagName="docTypeC" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/UserControl/CountRow.ascx" TagPrefix="uc1" TagName="CountRow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 122px;
        }
        .auto-style4 {
            width: 209px;
        }
        .auto-style6 {
            width: 10px;
        }
        .auto-style7 {
            width: 121px;
        }
        .auto-style8 {
            width: 77px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td class="auto-style6"></td>
                    <td class="auto-style3">Warehouse :</td>
                    <td>
                        <asp:CheckBoxList ID="ChkWH" runat="server"></asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td class="auto-style6"></td>
                    <td class="auto-style3">Receipt Type :</td>
                    <td>
                        <uc1:docTypeC runat="server" ID="ChkRecType" />
                    </td>
                </tr>
            </table>
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td class="auto-style6"></td>
                    <td class="auto-style7">Receipt No :</td>
                    <td>
                        <asp:TextBox ID="txtRecNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td class="auto-style6"></td>
                    <td class="auto-style3">MO Type :</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtMoType" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style8">MO No :</td>
                    <td>
                        <asp:TextBox ID="txtMoNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6"></td>
                    <td class="auto-style3">Item :</td>
                    <td>
                        <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style8">Spec :</td>
                    <td>
                        <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6"></td>
                    <td class="auto-style3">Data From :</td>
                    <td>
                        <uc2:Date ID="DateF" runat="server" />
                    </td>
                    <td class="auto-style8">Date To :</td>
                    <td>
                        <uc2:Date ID="DateT" runat="server" />
                    </td>
                </tr>
            </table>
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td  align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btReport" runat="server" Text="Show Report" />
                        &nbsp;
                        <asp:Button ID="btExcel" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow runat="server" ID="CountRow" />
            <asp:GridView ID="gvShow" runat="server"></asp:GridView>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
