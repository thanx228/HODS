<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="wipOnprocess.aspx.vb" Inherits="MIS_HTI.wipOnprocess" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px;
        }
        .style7
        {
            width: 226px;
        }
        .style9
        {
            height: 24px;
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
                        <asp:Label ID="Label6" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="On Process Over Due"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td bgcolor="White" style="vertical-align: top">
                        <asp:Label ID="Label7" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:CheckBoxList ID="cblWc" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style9">
                        <asp:Label ID="Label10" runat="server" Text="Customer"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style9">
                        <asp:TextBox ID="tbCust" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="style9">
                        </td>
                    <td bgcolor="White" class="style9">
                        </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style9">
                        <asp:Label ID="Label8" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style9">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="style9">
                        <asp:Label ID="Label11" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style9">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style10">
                        <asp:Label ID="Label9" runat="server" Text="Receipt Date  FM"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style10">
                        <asp:TextBox ID="tbDateFM" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFM_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFM">
                        </asp:CalendarExtender>
                    </td>
                    <td bgcolor="White" class="style10">
                        <asp:Label ID="Label12" runat="server" Text="Receipt Date TO"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style10">
                        <asp:TextBox ID="tbDateTO" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTO_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTO">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        align="center">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" bgcolor="White" class="style7">
                        <asp:Label ID="Label3" runat="server" Text="Amout of Rows"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" bgcolor="White">
                        <asp:Label ID="Label5" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="MW002" HeaderText="Process Name" />
                    <asp:BoundField DataField="MA002" HeaderText="Cust Name" />
                    <asp:BoundField DataField="TA026" HeaderText="SO No." />
                    <asp:BoundField DataField="TA001" HeaderText="MO No" />
                    <asp:BoundField DataField="TA035" HeaderText="Spec" />
                    <asp:BoundField DataField="TA015" DataFormatString="{0:N2}" 
                        HeaderText="Plan Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TA010" DataFormatString="{0:N2}" 
                        HeaderText="Input Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wip" DataFormatString="{0:N2}" 
                        HeaderText="WIP Qty" />
                    <asp:BoundField DataField="TA017" DataFormatString="{0:N2}" 
                        HeaderText="Not Approve">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TA008" HeaderText="Plan Start Date" />
                    <asp:BoundField DataField="F02" HeaderText="Plan Schedule Date" />
                    <asp:BoundField DataField="F04" HeaderText="Today" />
                    <asp:BoundField DataField="F01" HeaderText="Last Recevie Date" />
                    <asp:BoundField DataField="F03" DataFormatString="{0:N0}" HeaderText="WIP Day">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TA034" HeaderText="Remark" />
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
