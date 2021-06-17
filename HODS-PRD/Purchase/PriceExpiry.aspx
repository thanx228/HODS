<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PriceExpiry.aspx.vb" Inherits="MIS_HTI.PriceExpiry" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tableShow {
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
            <table cellpadding="5" cellspacing="5" class="tableShow">
                <tr>
                    <td style="width:15%;">&nbsp;</td>
                    <td style="width:35%;">&nbsp;</td>
                    <td style="width:15%;">&nbsp;</td>
                    <td style="width:35%;">&nbsp;</td>
                </tr>
                <tr>
                    <td class="textRight">Report For : </td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True">
                            <asp:ListItem Value="0">Price Approval Order</asp:ListItem>
                            <asp:ListItem Value="1">Price Approval Order by subcontractor</asp:ListItem>
                            <asp:ListItem Value="2">MO Outsource</asp:ListItem>
                            <asp:ListItem Value="3">Item Expire</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="textRight">Status Price :</td>
                    <td>
                        <asp:DropDownList ID="ddlStusPrice" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Expire</asp:ListItem>
                            <asp:ListItem Value="2">Not Expire</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                        <asp:CheckBox ID="cbItemOut" runat="server" Font-Bold="True" ForeColor="#CC0000" Text="Price Item Subcontract" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td class="textRight">Type :</td>
                    <td colspan="3">
                        <asp:CheckBoxList ID="cblDocType" runat="server" CellPadding="5" CellSpacing="5">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">Document No :</td>
                    <td>
                        <asp:TextBox ID="tbDocNo" runat="server" Width="250px"></asp:TextBox>
                    </td>
                    <td class="textRight">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="textRight">Expiry Date From :</td>
                    <td>
                        <uc2:Date ID="ucDateFrom" runat="server" />
                    </td>
                    <td class="textRight">Expiry Date To :</td>
                    <td>
                        <uc2:Date ID="ucDateTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="textRight">Item :</td>
                    <td>
                        <asp:TextBox ID="tbItem" runat="server" Width="250px"></asp:TextBox>
                    </td>
                    <td class="textRight">Item Desc :</td>
                    <td>
                        <asp:TextBox ID="tbItemDesc" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">Item Spec :</td>
                    <td>
                        <asp:TextBox ID="tbItemSpec" runat="server" Width="250px"></asp:TextBox>
                    </td>
                    <td class="textRight">Supplier :</td>
                    <td>
                        <asp:TextBox ID="tbSupCode" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">&nbsp;</td>
                    <td>
                        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="Date Diff (Days) = Present Date - Expiry Date"></asp:Label>
                    </td>
                    <td class="textRight">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>




             <br />




             <table class="tableShow">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x" align="center">
                        <asp:Button ID="btShow" runat="server" Text="Show" />
                        &nbsp;<asp:Button ID="btExcel" runat="server" Text="Excel" />                 
                    </td>
                </tr>
            </table>

            <uc1:CountRow ID="ucCountRow" runat="server" />

            <asp:GridView ID="gvShow" runat="server">
            </asp:GridView>

        </ContentTemplate>
   
        <Triggers>
            <asp:PostBackTrigger ControlID="btExcel" />
        </Triggers>

    </asp:UpdatePanel>

</asp:Content>
