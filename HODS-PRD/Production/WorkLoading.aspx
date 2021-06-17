<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="WorkLoading.aspx.vb" Inherits="MIS_HTI.WorkLoading" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px;
        }
        .style9
        {
            height: 21px;
            width: 141px;
        }
        .style10
        {
            width: 141px;
        }
        .style11
        {
            height: 21px;
            width: 189px;
        }
        .style12
        {
            width: 189px;
        }
        .style13
        {
            height: 21px;
            width: 165px;
        }
        .style14
        {
            width: 165px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <table style="width:95%;">
                <tr>
                    <td bgcolor="White" class="style9" style="vertical-align: top">
                        <asp:Label ID="Label4" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6" colspan="3">
                        <asp:CheckBoxList ID="cblWc" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style10">
                        <asp:Label ID="Label5" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:CheckBoxList ID="cblType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style9">
                        <asp:Label ID="Label6" runat="server" Text="Cust ID"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style13">
                        <asp:TextBox ID="tbCust" runat="server" Width="60px"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="style11">
                        <asp:Label ID="Label8" runat="server" Text="MO Condition"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:DropDownList ID="ddlCon" runat="server">
                            <asp:ListItem Value="1">ALL</asp:ListItem>
                            <asp:ListItem Value="2">On Process</asp:ListItem>
                            <asp:ListItem Value="3">Pending</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style10">
                        <asp:Label ID="Label7" runat="server" Text="Plan Complete Date FM"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style14">
                        <uc1:Date ID="UcDateFrom" runat="server" />
                    </td>
                    <td bgcolor="White" class="style12">
                        <asp:Label ID="Label9" runat="server" Text="Plan Complete Date To"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <uc1:Date ID="UcDateTo" runat="server" />
                    </td>
                </tr>
            </table>
            <table style="width:95%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x" 
                        align="center">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplLink" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="F01" HeaderText="Work Center" />
                    <asp:BoundField DataField="F02" HeaderText="Work Center Name" />
                    <asp:BoundField DataField="F03" DataFormatString="{0:N0}" 
                        HeaderText="TTL Item" />
                    <asp:BoundField DataField="F04" DataFormatString="{0:N0}" HeaderText="TTL MO" />
                    <asp:BoundField DataField="F05" DataFormatString="{0:N0}" 
                        HeaderText="TTL Qty" />
                    <asp:BoundField DataField="F06" DataFormatString="{0:N0}" 
                        HeaderText="Man Time(Min)" />
                    <asp:BoundField DataField="F07" DataFormatString="{0:N0}" 
                        HeaderText="Mch Time(Min)" />
                    <asp:BoundField DataField="F08" DataFormatString="{0:N0}" 
                        HeaderText="Man Work(Min)" />
                    <asp:BoundField DataField="F09" DataFormatString="{0:N0}" 
                        HeaderText="Mch Work(Min)" />
                    <asp:BoundField DataField="F10" DataFormatString="{0:N0}" 
                        HeaderText="Man Load(Day)" />
                    <asp:BoundField DataField="F11" DataFormatString="{0:N0}" 
                        HeaderText="Mch Load(Day)" />
                    <asp:BoundField DataField="F12" HeaderText="Load End Of" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
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
