<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="QCost.aspx.vb" Inherits="MIS_HTI.QCost" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .style33
        {
            width: 92px;
        }
        .style34
        {
            width: 10px;
        }
        .style37
        {
            width: 332px;
        }
        .style38
        {
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table style="width: 70%;">
                <tr>
                    <td align="left" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat; margin-left: 40px;">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Q - Cost"></asp:Label>
                    </td>
                </tr>
            </table>
            <table bgcolor="White" style="width: 70%;">
                <tr>
                    <td class="style38">
                        Report Type&nbsp; :&nbsp;
                    </td>
                    <td class="style34">
                        <asp:DropDownList ID="ddlReport" runat="server" Width="130px">
                            <asp:ListItem Value="All">All</asp:ListItem>
                            <asp:ListItem Value="1804 Scrap">1804 Scrap</asp:ListItem>
                            <asp:ListItem Value="D403 IPQC">D403 IPQC</asp:ListItem>
                            <asp:ListItem Value="D404 FQC">D404 FQC</asp:ListItem>
                            <asp:ListItem Value="D406 Cus. Claim">D406 Cus. Claim</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style33">
                        &nbsp;
                    </td>
                    <td class="style37">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style38">
                        Doc. Date FM&nbsp;:&nbsp;
                    </td>
                    <td class="style34">
                        <asp:TextBox ID="txtDateFrom" runat="server" Width="90px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style33">
                        &nbsp;To&nbsp; :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td class="style37">
                        <asp:TextBox ID="txtDateTo" runat="server" Width="90px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style38">
                        Work Center&nbsp; :&nbsp;&nbsp;</td>
                    <td class="style34">
                        <asp:TextBox ID="txtWork" runat="server" Width="90px"></asp:TextBox>
                    </td>
                    <td class="style33">
                        Scrap Amt. Top</td>
                    <td class="style37">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table style="width: 70%;">
                <tr>
                    <td align="center" class="style6" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btnShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" Width="100px" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td align="center" class="style4" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Amout of Rows"></asp:Label>
                    </td>
                    <td align="center" class="style4" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" class="style4" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="TD004" HeaderText="Work Center" />
                    <asp:BoundField DataField="TD001" HeaderText="Report Type" />
                    <asp:BoundField DataField="TE006" HeaderText="Report No." />
                    <asp:BoundField DataField="TE007" HeaderText="Doc. Date" />
                    <asp:BoundField DataField="UDF52" DataFormatString="{0:#,##0.00}" 
                        HeaderText="Scrap Qt'y">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD004" HeaderText="Defect Qt'y" />
                    <asp:BoundField HeaderText="Material Cost" />
                    <asp:BoundField HeaderText="Total Scrap Cost" />
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
<br />
<br />
<br />
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
