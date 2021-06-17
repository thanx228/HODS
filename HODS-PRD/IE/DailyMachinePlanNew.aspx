<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="DailyMachinePlanNew.aspx.vb" Inherits="MIS_HTI.DailyMachinePlanNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px;
        }
        .style7
        {
            width: 240px;
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
                        <asp:Label ID="Label3" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="Daily Machine Plan"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White" style="vertical-align: top">
                        <asp:Label ID="Label4" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:CheckBoxList ID="cblWC" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" style="vertical-align: top">
                        <asp:Label ID="Label5" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:CheckBoxList ID="cblType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label6" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label10" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label7" runat="server" Text="Mat Item"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:TextBox ID="tbMatItem" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label12" runat="server" Text="Mat Spec"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:TextBox ID="tbMatSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label8" runat="server" Text="Date FM"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbDateFM" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFM_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFM">
                        </asp:CalendarExtender>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label16" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbDateTO" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTO_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTO">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label9" runat="server" Text="MO Status"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlCon" runat="server">
                            <asp:ListItem Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1">Onprocess</asp:ListItem>
                            <asp:ListItem Value="2">Pending</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td bgcolor="White">
                        &nbsp;</td>
                    <td bgcolor="White">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" bgcolor="White" class="style7">
                        <asp:Label ID="Label13" runat="server" Text="Amount of Rows"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="Label15" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="F01" HeaderText="Cust Name" />
                    <asp:BoundField DataField="F02" HeaderText="WC Name" />
                    <asp:BoundField DataField="F03" HeaderText="MO" />
                    <asp:BoundField DataField="F04" HeaderText="JP Part" />
                    <asp:BoundField DataField="F05" HeaderText="JP Spec" />
                    <asp:BoundField DataField="F06" HeaderText="Mat Item" />
                    <asp:BoundField DataField="F07" HeaderText="Mat Desc" />
                    <asp:BoundField DataField="F08" HeaderText="Mat Spec" />
                    <asp:BoundField DataField="F09" HeaderText="Plan Start Date" />
                    <asp:BoundField DataField="F10" DataFormatString="{0:N3}" HeaderText="QPA">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F11" DataFormatString="{0:N3}" 
                        HeaderText="Not Issue">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F12" HeaderText="Unit" />
                    <asp:BoundField DataField="F13" DataFormatString="{0:N0}" HeaderText="Plan Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F16" DataFormatString="{0:N0}" 
                        HeaderText="Complete Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F14" DataFormatString="{0:N0}" HeaderText="WIP Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F15" HeaderText="MO Status" />
                </Columns>
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
    </asp:UpdatePanel>
</asp:Content>
