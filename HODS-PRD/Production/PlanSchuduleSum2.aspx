<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PlanSchuduleSum2.aspx.vb" Inherits="MIS_HTI.PlanSchuduleSum2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 132px;
        }
        .style7
        {
            width: 240px;
        }
        .style8
        {
            width: 275px;
        }
        .style9
        {
            width: 132px;
            height: 26px;
        }
        .style10
        {
            height: 26px;
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
                        <asp:Label ID="Label8" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="Plan Schedule Summary Dialy"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label3" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:CheckBoxList ID="cblWC" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style9">
                        <asp:Label ID="Label4" runat="server" Text="Plan Date From"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style10">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td bgcolor="White" class="style10">
                        <asp:Label ID="Label5" runat="server" Text="Plan Date To"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style10">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
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
                    <td bgcolor="White" align="center" class="style7">
                        <asp:Label ID="Label6" runat="server" Text="Amount of Rows"></asp:Label>
                    </td>
                    <td bgcolor="White" align="center" class="style8">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td bgcolor="White" align="center">
                        <asp:Label ID="Label7" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="A" HeaderText="Plan Date" />
                    <asp:BoundField DataField="B" HeaderText="WC" />
                    <asp:BoundField DataField="C" HeaderText="WC Name" />
                    <asp:BoundField DataField="D" DataFormatString="{0:N0}" HeaderText="Plan" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="E" DataFormatString="{0:N0}" HeaderText="Act Plan" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F" DataFormatString="{0:N2}" 
                        HeaderText="Act Plan(%)" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="G" DataFormatString="{0:N0}" 
                        HeaderText="Plan Delay" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="H" DataFormatString="{0:N2}" 
                        HeaderText="Plan Delay(%)" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="I" DataFormatString="{0:N0}" HeaderText="Act Over" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="J" DataFormatString="{0:N2}" 
                        HeaderText="Act Over (%)" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="K" DataFormatString="{0:N2}" HeaderText="Act All" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="L" DataFormatString="{0:N2}" 
                        HeaderText="Act All(%)" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="M" DataFormatString="{0:N0}" 
                        HeaderText="Plan Cancled" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="N" DataFormatString="{0:N2}" 
                        HeaderText="Plan Cancled(%)" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
