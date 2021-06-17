<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="WorkCenterInfo.aspx.vb" Inherits="MIS_HTI.WorkCenterInfo" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 90px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label3" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlWC" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label4" runat="server" Text="Mach/Line"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:CheckBoxList ID="cblMachLine" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            <table style="width: 75%; background-image: url('../Images/btt.jpg'); background-repeat: repeat-x;">
                <tr>
                    <td align="center">
                        <asp:Button ID="btShow" runat="server" Text="Show" />
                        &nbsp;<asp:Button ID="btUpdate" runat="server" Text="Update" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="CountRow1" runat="server" />
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="MD001" HeaderText="WC Code" />
                    <asp:BoundField DataField="MD002" HeaderText="WC Name" />
                    <asp:BoundField DataField="MX001" HeaderText="Mach/Line Code" />
                    <asp:BoundField DataField="MX003" HeaderText="Mach/Line Name" />
                    <asp:TemplateField HeaderText="Work Type">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlWorkType" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Muti MO">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbMultiMO" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shift">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlShift" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Process Type">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlProcessType" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Normal Time(Min)">
                        <ItemTemplate>
                            <asp:TextBox ID="tbNormalTime" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Over Time(Min)">
                        <ItemTemplate>
                            <asp:TextBox ID="tbOverTime" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Count">
                        <ItemTemplate>
                            <asp:TextBox ID="tbCount" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Process">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplProcess" runat="server">Process</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
