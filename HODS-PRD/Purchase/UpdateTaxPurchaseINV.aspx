<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="UpdateTaxPurchaseINV.aspx.vb" Inherits="MIS_HTI.UpdateTaxPurchaseINV" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register src="../UserControl/GridviewShow.ascx" tagname="GridviewShow" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 75%;
            background-color: #FFFFFF;
        }
        .auto-style9 {
            width: 186px;
        }
        .auto-style18 {
            width: 195px;
        }
        .auto-style26 {
            width: 30px;
        }
        .auto-style29 {
            width: 200px;
        }
        .auto-style31 {
            width: 131px;
        }
        .auto-style34 {
            width: 74px;
        }
        .auto-style35 {
            width: 180px;
        }
        .auto-style39 {
            width: 145px;
        }
        .auto-style41 {
            width: 193px;
        }
        .auto-style42 {
            width: 149px;
        }
        .auto-style44 {
            width: 185px;
        }
        .auto-style45 {
            width: 110px;
        }
        .auto-style47 {
            width: 99px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="auto-style3">
                <tr>
                    <td class="auto-style26">&nbsp;</td>
                    <td class="auto-style9">&nbsp;</td>
                    <td class="auto-style29">&nbsp;</td>
                    <td class="auto-style18">&nbsp;</td>
                    <td class="auto-style29">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style26">&nbsp;</td>
                    <td class="auto-style9">PURCHASE INVOICE TYPE</td>
                    <td class="auto-style29">
                        <asp:TextBox ID="txtInvType" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td class="auto-style18">PURCHASE INVOICE NO</td>
                    <td class="auto-style29">
                        <asp:TextBox ID="txtInvNo" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td class="auto-style29">&nbsp;</td>
                </tr>

            </table>
            <table class="auto-style3">
                <tr>
                    <td class="auto-style26">&nbsp;</td>
                    <td>
                        <asp:Label ID="Label3" runat="server" BackColor="White" ForeColor="Red" Text="Remark"></asp:Label>
                    </td>                   
                </tr>
                <tr>
                    <td class="auto-style26">&nbsp;</td>
                    <td>
                        <asp:Label ID="Label2" runat="server" BackColor="White" ForeColor="Red" Text=" 1.It can be updated after the status was It can be updated after the status was approved"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style26">&nbsp;</td>
                    <td>
                        <asp:Label ID="Label1" runat="server" BackColor="White" ForeColor="Red" Text="2.There is a scope to search for historical data for 1 month."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style26">&nbsp;</td>
                </tr>
            </table>

            <table class="auto-style3">

                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btShow" runat="server" Text="Search" />
                        &nbsp;
                        <asp:Button ID="btcancel" runat="server" Text="Cancel" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </table>


            <table class="auto-style3">
                <tr>
                    <td class="auto-style26">&nbsp;</td>
                    <td class="auto-style45">INVOICE TYPE :</td>
                    <td class="auto-style44" id="InvType">
                        <asp:Label ID="blInvType" runat="server" ForeColor="#0066FF"></asp:Label>
                    </td>
                    <td class="auto-style47">INVOICE NO :</td>
                    <td class="auto-style29">
                        <asp:Label ID="blInvNo" runat="server" ForeColor="#0066FF"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td class="auto-style26">&nbsp;</td>
                    <td class="auto-style45">SUPPLIER :</td>
                    <td class="auto-style44">
                        <asp:Label ID="blSup" runat="server" ForeColor="#0066FF"></asp:Label>
                    </td>
<%--                    <td class="auto-style47">DATE :</td>
                    <td>
                        <asp:Label ID="blDate" runat="server" ForeColor="#0066FF"></asp:Label>
                    </td>--%>
                </tr>
            </table>
            <table class="auto-style3">
                <tr>
                    <td></td>
                </tr>
            </table>

            <table class="auto-style3">
                <tr>
                    <td class="auto-style26">&nbsp;</td>
                    <td class="auto-style41">Amount Un-include Tax(O/C) :</td>
                    <td class="auto-style42">
                        <asp:Label ID="blAmountOC" runat="server" ForeColor="#0066FF"></asp:Label>
                    </td>
                    <td class="auto-style34">Tax (O/C) :</td>
                    <td class="auto-style35">
                        <asp:Label ID="blTaxOC" runat="server" ForeColor="#0066FF"></asp:Label>
                    </td>
                    <td class="auto-style39">Total Amount (O/C) :</td>
                    <td class="auto-style31">
                        <asp:Label ID="blTotalOC" runat="server" ForeColor="#0066FF"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style26">&nbsp;</td>
                    <td class="auto-style41">Amount Un-include Tax(B/C) :</td>
                    <td class="auto-style42">
                        <asp:Label ID="blAmountBC" runat="server" ForeColor="#0066FF"></asp:Label>
                    </td>
                    <td class="auto-style34">Tax (B/C) :</td>
                    <td class="auto-style35">
                        <asp:Button ID="btMinusBC" runat="server" BackColor="White" BorderColor="Red" Text="-" ForeColor="Red" />
                        &nbsp;
                        <asp:Label ID="blTaxBC" runat="server" ForeColor="Red"></asp:Label>
                        &nbsp;
                        <asp:Button ID="btPlusBC" runat="server" BackColor="White" BorderColor="Red" ForeColor="Red" Text="+" />
                    </td>
                    <td class="auto-style39">Total Amount (B/C) :</td>
                    <td class="auto-style31">
                        <asp:Label ID="blTotalBC" runat="server" ForeColor="#0066FF"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </table>

            <table style="width: 75%;">
                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btUpdate" runat="server" Text="Update" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="CountRow" runat="server" />
            <asp:GridView ID="gvshow" runat="server" BorderStyle="None" BorderWidth="0px" CellPadding="4" Width="242px">
                <HeaderStyle BackColor="#3366CC" Font-Bold="True" ForeColor="White" Height="25px" HorizontalAlign="Center" Wrap="False" />
                <RowStyle BackColor="White" Wrap="False" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

