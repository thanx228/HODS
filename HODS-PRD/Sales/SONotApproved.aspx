<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SONotApproved.aspx.vb" Inherits="MIS_HTI.SONotApproved" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 26px;
        }
        .style7
        {
            width: 745px;
        }
        .style8
        {
            width: 88px;
            height: 29px;
        }
        .style9
        {
            width: 106px;
        }
        .style10
        {
            width: 166px;
            height: 29px;
        }
        .style11
        {
            width: 178px;
            height: 29px;
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
                        &nbsp;<asp:Label 
                            ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Sale Order Not Approved"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 70%;">
                <tr>
                    <td style="background-color: #FFFFFF; vertical-align: top;" class="style9">
                        <asp:Label ID="Label13" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblSaleType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            <table style="width: 70%;">
                <tr>
                    <td align="center" class="style6" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btnShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;<asp:Button ID="btExport"  runat="server" Text="Export Excel" AutoPostBack="true" Width="100px" />
                    </td>
                </tr>
            </table>
            <table class="style7">
                <tr>
                    <td align="center" class="style10" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Amout of Rows"></asp:Label>
                    </td>
                    <td align="center" class="style11" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF">
                        &nbsp;<asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" class="style8" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvshow" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="BOM">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlBOM" runat="server" Target="_blank">BOM</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SO Type / No. / Seq" 
                        HeaderText="SO Type / No. / Seq" />
                    <asp:BoundField DataField="Item" HeaderText="Item" />
                    <asp:BoundField DataField="Spec" HeaderText="Spec" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="Qty" HeaderText="Qty">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="Status">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Industry Type" HeaderText="Industry Type">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cust." HeaderText="Cust." />
                    <asp:BoundField DataField="TD013" HeaderText="Plan Delivery Date" />
                    <asp:BoundField DataField="SaleRequestDueDate" 
                        HeaderText="Sale Request Due Date" />
                    <asp:BoundField DataField="BOM" HeaderText="Status BOM" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    HorizontalAlign="Center" />
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
