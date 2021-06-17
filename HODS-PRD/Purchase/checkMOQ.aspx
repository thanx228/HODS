<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="checkMOQ.aspx.vb" Inherits="MIS_HTI.checkMOQ" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:55%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Check MOQ"></asp:Label>
                    </td>
                </tr>
            </table>
            <table bgcolor="White">
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Supplier"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbSup" runat="server" MaxLength="5" Width="45px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Code Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTypeCode" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Selected="True" Value="1">Materials</asp:ListItem>
                            <asp:ListItem Value="2">Finished Goods</asp:ListItem>
                            <asp:ListItem Value="3">Sub Finished Goods</asp:ListItem>
                            <asp:ListItem Value="4">Spare Part and Another</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbCode" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="60px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateFrom" Format="MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDateTo" runat="server" Width="60px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateTo" Format="MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:53%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td align="center" 
                        style="border: thin solid #0099FF; background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" 
                        style="border: thin solid #0099FF; background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" ForeColor="#0099FF"></asp:Label>
                    </td>
                    <td align="center" 
                        style="border: thin solid #0099FF; background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
