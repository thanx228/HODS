<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleUndeliveryNoFifo.aspx.vb" Inherits="MIS_HTI.SaleUndeliveryNoFifo" %>
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
    .style6
    {
        width: 140px;
    }
    .style7
    {
        width: 178px;
    }
        .style8
        {
            width: 40px;
        }
        .style9
        {
            height: 38px;
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
                    <td align="left" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        class="style4">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Sale Undelivery No FIFO "></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
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
                        <asp:TextBox ID="txtFromDueDate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtFromDueDate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDueDate">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="End Due Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDueDate" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDueDate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDueDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;" >
                <tr>
                    <td align="center" class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" class="style8" style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="รายการ"></asp:Label>
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
