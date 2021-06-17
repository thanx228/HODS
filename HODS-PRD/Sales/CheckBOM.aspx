<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="CheckBOM.aspx.vb" Inherits="MIS_HTI.CheckBOM" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style7
        {
            width: 159px;
        }
        .style8
        {
            width: 155px;
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
                    <td style="background-image: url('http://localhost:54341/Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label3" runat="server" Font-Size="1.1em" ForeColor="Blue" 
                            Text="Check BOM"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White" class="style7" style="vertical-align: top">
                        <asp:Label ID="Label4" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:CheckBoxList ID="cblSaleType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label15" runat="server" Text="Cust ID"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbCust" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="style8">
                        <asp:Label ID="Label14" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:CheckBox ID="cbApp" runat="server" Text="Approved" />
                        &nbsp;
                        <asp:CheckBox ID="cbNoBOM" runat="server" Text="NO BOM" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label5" runat="server" Text="SO NO"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSaleNo" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="style8">
                        <asp:Label ID="Label6" runat="server" Text="SO Seq"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSOSeq" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label16" runat="server" Text="Property"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:CheckBoxList ID="cblProperty" runat="server" RepeatColumns="5" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="P">P:Purchase</asp:ListItem>
                            <asp:ListItem Value="M">M:Manufacture</asp:ListItem>
                            <asp:ListItem Value="S">S:Subcontract</asp:ListItem>
                            <asp:ListItem Value="Y">Y:Phantom</asp:ListItem>
                            <asp:ListItem Value="C">C:Configuration</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label7" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="style8">
                        <asp:Label ID="Label8" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label9" runat="server" Text="Plan Delivery Date From"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td bgcolor="White" class="style8">
                        <asp:Label ID="Label10" runat="server" Text="Plan Delivery Date To"></asp:Label>
                    </td>
                    <td bgcolor="White">
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
                        style="background-image: url('http://localhost:54341/Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" 
                            Height="26px" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label11" runat="server" Text="Amount of Rows"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label13" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="BOM">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlBOM" runat="server" Target="_blank">BOM</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
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
