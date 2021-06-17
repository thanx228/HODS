<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="OperationReportSummary.aspx.vb" Inherits="MIS_HTI.OperationReportSummary" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 73px;
        }
        .style7
        {
            height: 21px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:59%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Operation Report Summary"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="border: thin groove #000000; background-color: #FFFFFF" 
                        class="style6">
                        Work Center</td>
                    <td style="border: thin groove #000000; background-color: #FFFFFF" colspan="3">
                        <asp:CheckBoxList ID="cblWC" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="Silver" 
                        style="border: thin groove #000000; background-color: #FFFFFF" 
                        class="style6">
                        Report Type</td>
                    <td colspan="3" style="border: thin groove #000000; background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblREType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="border: thin groove #000000; background-color: #FFFFFF" 
                        class="style6">
                        Summary</td>
                    <td style="border: thin groove #000000; background-color: #FFFFFF" colspan="3">
                        <asp:CheckBoxList ID="CheckSum" runat="server" EnableTheming="True" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">WC Name</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">Report Type</asp:ListItem>
                            <asp:ListItem Value="2" Selected="True">Date</asp:ListItem>
                            <asp:ListItem Value="3" Selected="True">MO No.</asp:ListItem>
                            <asp:ListItem Value="4" Selected="True">Item</asp:ListItem>
                            <asp:ListItem Value="5" Selected="True">OP ID</asp:ListItem>
                            <asp:ListItem Value="6" Selected="True">Code No.</asp:ListItem>
                        </asp:CheckBoxList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style6">
                        Date From<asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        Date To<asp:CalendarExtender ID="txtDateTo_CalendarExtender0" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDateTo">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width:60%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" 
                            style="height: 26px" />
                        <asp:Button ID="btExport" runat="server" Text="Excel Export" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width:60%;">
                <tr>
                    <td align="center" style="background-color: #FFFFFF" class="style7">
                        <asp:Label ID="Label10" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF" class="style7">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF" class="style7">
                        <asp:Label ID="Label11" runat="server" Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    
                </Columns>
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
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

