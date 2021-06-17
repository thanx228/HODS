<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleUndeliveryStatusPeriod.aspx.vb" Inherits="MIS_HTI.SaleUndeliveryStatusPeriod" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            height: 25px;
        }
    .style5
    {
        width: 40px;
    }
    .style7
    {
        width: 178px;
            height: 25px;
        }
        .style9
        {
            height: 38px;
        }
        .style6
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
            <table style="width:1389%;">
                <tr>
                    <td align="left" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        class="style4">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Sale Undelivery Period"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 527px;">
                <tr>
                    <td style="background-color: #FFFFFF; vertical-align: top;">
                        <asp:Label ID="Label2" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" colspan="3">
                        <asp:CheckBoxList ID="cblSaleType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style9">
                        <asp:Label ID="Label6" runat="server" Text="Cust ID"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style9">
                        <asp:TextBox ID="tbCustId" runat="server" MaxLength="10" Width="50px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style9">
                        <asp:Label ID="Label11" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style9">
                        <asp:DropDownList ID="ddlCondition" runat="server">
                            <asp:ListItem Selected="True" Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1">Stock&lt;Undelivery</asp:ListItem>
                            <asp:ListItem Value="2">Supply &gt;= Undelivery</asp:ListItem>
                            <asp:ListItem Value="3">Supply &lt; Undelivery</asp:ListItem>
                            <asp:ListItem Value="4">Stock&gt;=Undelivery</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="SO No."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSO" runat="server" MaxLength="12"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="SO Seq"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSoSeq" runat="server" MaxLength="3" Width="30px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Part No"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCode" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label12" runat="server" Text="From Due Date "></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="End Due Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 77%;">
                <tr>
                    <td align="center" class="style6" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btnShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" AutoPostBack="true" 
                            Text="Export Excel" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width: 77%; height: 18px;">
                <tr>
                    <td align="center" class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="Label17" runat="server" Text="Amout of Rows"></asp:Label>
                    </td>
                    <td align="center" class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="Label18" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
