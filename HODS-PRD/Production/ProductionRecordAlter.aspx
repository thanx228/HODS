<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ProductionRecordAlter.aspx.vb" Inherits="MIS_HTI.ProductionRecordAlter" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/machineD.ascx" tagname="machineD" tagprefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px;
        }
        .style7
        {
            height: 21px;
            width: 173px;
        }
        .style8
        {
            width: 173px;
        }
        .style11
        {
            width: 173px;
            height: 30px;
        }
        .style12
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label3" runat="server" Text="Doc No"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbDocNo" runat="server"></asp:TextBox>
                        <asp:Label ID="lbId" runat="server"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" class="style6" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btShow" runat="server" Text="Search" />
                        &nbsp;<asp:Button ID="btReset" runat="server" Text="Reset" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server">
                <asp:GridView ID="gvShow" runat="server" Height="122px">
                </asp:GridView>
                <asp:GridView ID="gvOper" runat="server">
                </asp:GridView>
                <table style="width: 75%;">
                    <tr>
                        <td bgcolor="White" class="style7">
                            MO New</td>
                        <td bgcolor="White" class="style6">
                            <asp:TextBox ID="tbMO" runat="server" CssClass="numberStringOnly" 
                                MaxLength="11" Width="200px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbMO_MaskedEditExtender" runat="server" 
                                ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                Mask="9999-99999999999-9999" MaskType="Number" TargetControlID="tbMO">
                            </asp:MaskedEditExtender>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label25" runat="server" Text="WC"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6" colspan="3">
                            <asp:Label ID="lbWC" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="style7">
                            <asp:Label ID="Label22" runat="server" Text="Work Type"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:DropDownList ID="ddlSet" runat="server" AutoPostBack="True">
                                <asp:ListItem>Sel</asp:ListItem>
                                <asp:ListItem Value="Y">Setting</asp:ListItem>
                                <asp:ListItem Value="N">Running</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lbSet" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label26" runat="server" Text="Machine/Line"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6" colspan="3">
                            <asp:DropDownList ID="ddlMch" runat="server">
                            </asp:DropDownList>
                            <asp:Label ID="lbMchOld" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="style7">
                            <asp:Label ID="Label20" runat="server" Text="Shift"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:DropDownList ID="ddlShift" runat="server">
                                <asp:ListItem Value="D">Day</asp:ListItem>
                                <asp:ListItem Value="N">Night</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label21" runat="server" Text="Break Time"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="lbBreak" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label24" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="lbStatus" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="style7">
                            <asp:Label ID="Label18" runat="server" Text="Date start"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <uc3:Date ID="ucDateS" runat="server" />
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label19" runat="server" Text="Date End"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6" colspan="3">
                            <uc3:Date ID="ucDateE" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="style11">
                            <asp:Label ID="Label14" runat="server" Text="Time Start"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style12">
                            <asp:TextBox ID="tbTimeS" runat="server" Width="60px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbTimeS_MaskedEditExtender" runat="server" 
                                ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                Mask="99:99" MaskType="Time" TargetControlID="tbTimeS">
                            </asp:MaskedEditExtender>
                        </td>
                        <td bgcolor="White" class="style12">
                            <asp:Label ID="Label15" runat="server" Text="Time End"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style12" colspan="3">
                            <asp:TextBox ID="tbTimeE" runat="server" Width="60px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbTimeE_MaskedEditExtender" runat="server" 
                                ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                Mask="99:99" MaskType="Time" TargetControlID="tbTimeE">
                            </asp:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="style8">
                            <asp:Label ID="Label7" runat="server" Text="Accept Qty"></asp:Label>
                        </td>
                        <td bgcolor="White">
                            <asp:TextBox ID="tbAcceptQty" runat="server"></asp:TextBox>
                        </td>
                        <td bgcolor="White">
                            <asp:Label ID="Label12" runat="server" Text="Return Qty"></asp:Label>
                        </td>
                        <td bgcolor="White" colspan="3">
                            <asp:TextBox ID="tbDefQty" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="style8">
                            <asp:Label ID="Label8" runat="server" Text="Scarp Qty"></asp:Label>
                        </td>
                        <td bgcolor="White">
                            <asp:TextBox ID="tbScrapQty" runat="server"></asp:TextBox>
                        </td>
                        <td bgcolor="White">
                            <asp:Label ID="Label13" runat="server" Text="Scrap Code"></asp:Label>
                        </td>
                        <td bgcolor="White" colspan="3">
                            <asp:DropDownList ID="ddlScrapCode" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table style="width: 75%; background-image: url('../Images/btt.jpg'); background-repeat: repeat-x;">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btUpdate" runat="server" 
                                onclientclick="return confirm('Are you update it');" Text="Update" />
                            &nbsp;<asp:Button ID="btDelete" runat="server" 
                                onclientclick="return confirm('Are you delete it');" Text="Delete" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
