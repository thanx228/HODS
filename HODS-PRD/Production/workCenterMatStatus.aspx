<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="workCenterMatStatus.aspx.vb" Inherits="MIS_HTI.workCenterMatStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px;
        }
        .style9
        {
            width: 195px;
        }
        .style10
        {
            height: 21px;
            width: 195px;
        }
        .style13
        {
            height: 21px;
            width: 154px;
        }
        .style14
        {
            width: 154px;
        }
        .style15
        {
            width: 222px;
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
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label13" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="Work Center Materials Status"></asp:Label>
                    </td>
                </tr>
            </table>
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td class="style13" style="vertical-align: top">
                        <asp:Label ID="Label9" runat="server" Text="Report By"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="ddlReport" runat="server">
                            <asp:ListItem Value="1">Summary</asp:ListItem>
                            <asp:ListItem Value="2">Detail</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Label5" runat="server" Text="MO Condition"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:DropDownList ID="ddlCon" runat="server">
                            <asp:ListItem Value="1">ALL</asp:ListItem>
                            <asp:ListItem Value="2">On Process</asp:ListItem>
                            <asp:ListItem Value="3">Pending</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style13" style="vertical-align: top">
                        <asp:Label ID="Label3" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td class="style6" colspan="3">
                        <asp:DropDownList ID="ddlWC" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style14">
                        <asp:Label ID="Label4" runat="server" Text="Mat Type"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:CheckBoxList ID="cblMatType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Raw Materials</asp:ListItem>
                            <asp:ListItem Value="3">Sub Part</asp:ListItem>
                            <asp:ListItem Value="4">Spart Part and Other</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="style14">
                        <asp:Label ID="Label6" runat="server" Text="Produce Item"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:TextBox ID="tbProduceItem" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Produce Spec"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbProduceSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style13">
                        <asp:Label ID="Label7" runat="server" Text="Mat Item"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:TextBox ID="tbMatItem" runat="server"></asp:TextBox>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Label11" runat="server" Text="Mat Spec"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox ID="tbMatSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style14">
                        <asp:Label ID="Label8" runat="server" Text="Plan Start Date FM"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:TextBox ID="tbDateFM" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFM_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFM">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="Plan Start Date TO"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDateTO" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTO_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTO">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('http://localhost:54341/Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" bgcolor="White" class="style15">
                        <asp:Label ID="Label14" runat="server" Text="Amount of rows"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="Label15" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
