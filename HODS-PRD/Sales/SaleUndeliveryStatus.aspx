<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleUndeliveryStatus.aspx.vb" Inherits="MIS_HTI.SaleUndeliveryStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<%@ Register src="../UserControl/GridviewShow.ascx" tagname="GridviewShow" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            height: 25px;
        }
    .style5
    {
        width: 40px;
    }
    .style6
    {
        width: 140px;
    }
    .style7
    {
        width: 178px;
    }
        .style8
        {
            width: 40px;
        }
        .style9
        {
            height: 38px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 95%;">
                <tr>
                    <td style="background-color: #FFFFFF; vertical-align: top;">
                        <asp:Label ID="Label2" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" colspan="3">
                        <asp:CheckBoxList ID="cblSaleType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style9">
                        <asp:Label ID="Label6" runat="server" Text="Cust ID"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style9">
                        <asp:TextBox ID="tbCustId" runat="server" MaxLength="10" Width="50px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style9">
                        <asp:Label ID="Label11" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style9">
                        <asp:DropDownList ID="ddlCondition" runat="server">
                            <asp:ListItem Selected="True" Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1">Stock&lt;Undelivery</asp:ListItem>
                            <asp:ListItem Value="2">Supply &gt;= Undelivery</asp:ListItem>
                            <asp:ListItem Value="3">Supply &lt; Undelivery</asp:ListItem>
                            <asp:ListItem Value="4">Stock&gt;=Undelivery</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="SO No."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSO" runat="server" MaxLength="12"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="SO Seq"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSoSeq" runat="server" MaxLength="3" Width="30px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Part No"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCode" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label12" runat="server" Text="From Due Date "></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc1:Date ID="UcDateFrom" runat="server" />
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="End Due Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc1:Date ID="UcDateTo" runat="server" />
                    </td>
                </tr>
            </table>
            <table style="width:95%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <uc2:GridviewShow ID="UcGv" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
