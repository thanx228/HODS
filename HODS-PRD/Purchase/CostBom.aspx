<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="CostBom.aspx.vb" Inherits="MIS_HTI.CostBom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style5
        {
            width: 676px;
        }
        .style7
        {
           
        }
        .style9
        {
            
        }
        .style10
        {
            width: 676px;
        }
        .style11
        {
            
        }
        .style12
        {
            width: 676px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:36%;">
                <tr>
                    <td align="center" class="style7" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label3" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Cost BOM"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:36%;">
                <tr>
                    <td style="background-color: #FFFFFF" class="style11">
                        <asp:Label ID="Label1" runat="server" Text="ITEM"></asp:Label>
                    </td>
                    <td class="style12" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCode" runat="server" Width="160px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style9">
                        <asp:Label ID="Label4" runat="server" Text="QTY"></asp:Label>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbQty" runat="server" Width="50px">1</asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width:36%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report"  />
                        <asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                ShowFooter="True">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" Wrap="False" />
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
