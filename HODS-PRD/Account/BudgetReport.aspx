<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="BudgetReport.aspx.vb" Inherits="MIS_HTI.BudgetReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            
        }
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
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Budgeting Report"></asp:Label>
                    </td>
                </tr>
            </table>
            <table bgcolor="White" style="width: 50%;">
                <tr>
                    <td class="style33">
                        Date From&nbsp; :&nbsp;
                    </td>
                    <td class="style34">
                        <asp:TextBox ID="txtDateFrom" runat="server" Width="90px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style33">
                        &nbsp;To&nbsp; :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td class="style37">
                        <asp:TextBox ID="txtDateTo" runat="server" Width="90px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style33">
                        Dept.</td>
                    <td class="style34">
                        <asp:DropDownList ID="ddlDept" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="style33">
                        &nbsp;</td>
                    <td class="style37">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width: 70%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        class="style6">
                        <asp:Button ID="btnShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;<asp:Button ID="btnExcel" runat="server" Text="Export Excel" />
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
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                 <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                        </asp:TemplateField>
                    <asp:BoundField DataField="D01" HeaderText="Dept." />
                    <asp:BoundField DataField="D02" 
                        HeaderText="Sub Total_Item">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="D03" DataFormatString="{0:#,##0.000}" 
                        HeaderText="Total Non-Inv.">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
