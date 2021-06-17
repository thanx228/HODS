<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="WorkCenterSummary.aspx.vb" Inherits="MIS_HTI.WorkCenterSummary" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                            Text="Work Center Summary"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="border: thin groove #000000; background-color: #FFFFFF">
                        Work Center</td>
                    <td style="border: thin groove #000000; background-color: #FFFFFF" colspan="3">
                        <asp:CheckBoxList ID="cblWC" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="Silver" 
                        style="border: thin groove #000000; background-color: #FFFFFF">
                        MO Type</td>
                    <td colspan="3" style="border: thin groove #000000; background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblMOType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="border: thin groove #000000; background-color: #FFFFFF">
                        SO Type</td>
                    <td bgcolor="#999999" colspan="3" 
                        style="border: thin groove #000000; background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblSaleType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        MO No.</td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtMONo" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        Cus. ID </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtCusCode" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        SO No.</td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSONo" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        SO Seq.</td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSOSeq" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        Spec.</td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        Plan Start FM</td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        Plan Start To</td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDateTo">
                        </asp:CalendarExtender>
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
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="รายการ"></asp:Label>
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
                    <asp:BoundField HeaderText="WC Name" DataField="W01">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Process Total" DataField="W02">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Process Finished" DataField="W03">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Process On Hand" DataField="W04">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Process Pending" DataField="W05">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Finished%" DataField="W06">
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
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

