<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="checkCodeStatus.aspx.vb" Inherits="MIS_HTI.checkCodeStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            
        }
        .style4
        {
            
            width: 650px;
        }
        .style5
        {
            width: 432px;
        }
        .style6
        {
            width: 201px;
        }
        .style7
        {
            
        }
        .style8
        {
            width: 678px;
        }
        .style9
        {
            width: 432px;
        }
        .style10
        {
            height: 26px;
        }
        .style11
        {
            width: 201px;
            height: 26px;
        }
    </style>
    <script type="text/javascript">
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        class="style9">
                        <asp:Label ID="Label8" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Check Code Status"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label1" runat="server" Text="Code Type"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlTypeCode" runat="server">
                            <asp:ListItem Selected="True" Value="1">Materials</asp:ListItem>
                            <asp:ListItem Value="2">Finish Goods</asp:ListItem>
                            <asp:ListItem Value="3">Semi Finish Goods</asp:ListItem>
                            <asp:ListItem Value="4">Spare Part &amp; Another</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label3" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td class="style6" bgcolor="White">
                        <asp:DropDownList ID="ddlCondition" runat="server">
                            <asp:ListItem Value="0">All Demand &amp; Supply</asp:ListItem>
                            <asp:ListItem Value="1">Supply &gt;=Demand,Demand&gt;0</asp:ListItem>
                            <asp:ListItem Value="2">Demand&gt;=Supply,Supply&gt;0</asp:ListItem>
                            <asp:ListItem Value="3">Demand=0,Supply&gt;0</asp:ListItem>
                            <asp:ListItem Value="4">Supply =0,Demand&gt;0</asp:ListItem>
                            <asp:ListItem Value="5">Stock &gt;= Demand</asp:ListItem>
                            <asp:ListItem Value="6">Demand &gt; Supply</asp:ListItem>
                            <asp:ListItem Value="7">Stock &gt; 0</asp:ListItem>
                            <asp:ListItem Value="8">Support&gt;0</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style10">
                        <asp:Label ID="Label2" runat="server" Text="Code"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style10">
                        <asp:TextBox ID="tbCode" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="style10">
                        <asp:Label ID="Label4" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td class="style11" bgcolor="White">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" class="style7" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                        <asp:Button ID="btExport" runat="server" Text="Export Excel" />
                        
                    </td>
                </tr>
            </table>
            <table style="width: 75%; height: 34px;">
                <tr>
                    <td align="center" class="style8" 
                        style="background-color: #FFFFFF; border: thin solid #00CCFF">
                        <asp:Label ID="Label5" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" class="style4" 
                        style="background-color: #FFFFFF; border: thin solid #00CCFF">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" class="style3" 
                        style="background-color: #FFFFFF; border: thin solid #00CCFF">
                        <asp:Label ID="Label7" runat="server" Text="รายการ"></asp:Label>
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
