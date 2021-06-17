<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PurchaseRequestSummary.aspx.vb" Inherits="MIS_HTI.PurchaseRequestSummary" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            width: 61px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 52%;">
                <tr>
                    <td align="left" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" Text="Purchase Request Summary" 
                            Font-Size="1.2em" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Code Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Selected="True" Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1">Materials</asp:ListItem>
                            <asp:ListItem Value="2">Finish Goods</asp:ListItem>
                            <asp:ListItem Value="3">Sub Finish Goods</asp:ListItem>
                            <asp:ListItem Value="4">Spart Part and Another</asp:ListItem>
                            <asp:ListItem Value="5">Supply Product</asp:ListItem>
                            <asp:ListItem Value="6">Supply Other</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="PR Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPrType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="End Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDate" runat="server" BorderColor="Aqua" BorderStyle="Solid"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 52%; margin-bottom: 0px;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                        <asp:Button ID="btExport" runat="server" Text="Export Excel" Width="100px" />
                    </td>
                </tr>
            </table>
            <table>
                <tr style="border-top-style: solid; border-width: thin; border-color: #0033CC; background-color: #FFFFFF">
                    <td align="center">
                        <asp:Label ID="Label4" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label6" runat="server" Text="รายการ"></asp:Label>
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
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="True" />
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
