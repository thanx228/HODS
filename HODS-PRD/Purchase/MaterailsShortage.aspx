<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MaterailsShortage.aspx.vb" Inherits="MIS_HTI.MaterailsShortage" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-1.9.1.min.js" type="text/javascript"> </script>
    <script type="text/javascript">
        $(function () {
            $('#<%=btShow.ClientID %>').click(function () {
                $('#divShow').show();
                $('#divPlanCallIn').hide();
                $('#divPOCallIn').hide();
            });
        });

        $(function () {
            $('#<%=btPlanCallIn.ClientID %>').click(function () {
                $('#divPlanCallIn').show();
                $('#divShow').hide();
                $('#divPOCallIn').hide();
            });
        });
      
        $(function () {
            $('#<%=btPOCallIn.ClientID %>').click(function () {
                $('#divPOCallIn').show();
                $('#divShow').hide();
                $('#divPlanCallIn').hide();
            });
        });

    </script>
    <style type="text/css">
        .style3
        {
            width: 40px;
        }
        .style4
        {
            width: 40px;
        }
        .style5
        {
            width: 127px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:56%;">
                <tr>
                    <td  style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Materials Shortage"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:65%; background-color: #FFFFFF;">
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Code Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlCodeType" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Selected="True" Value="1">Materials</asp:ListItem>
                            <asp:ListItem Value="2">Finished Goods</asp:ListItem>
                            <asp:ListItem Value="3">Semi FG</asp:ListItem>
                            <asp:ListItem Value="4">Spare Part and Another</asp:ListItem>
                            <asp:ListItem Value="5">Suply Product</asp:ListItem>
                            <asp:ListItem Value="6">Suply Other</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlCondition" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Shortage</asp:ListItem>
                            <asp:ListItem Value="2">Call In</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCode" runat="server" Width="155px" BorderColor="Blue" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server" Width="149px" BorderColor="Blue" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="From Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateFrom" runat="server" BorderColor="Blue" 
                            BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="End Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" BorderColor="Blue" 
                            BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:65%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        &nbsp;<asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;<asp:Button ID="btPlanCallIn" runat="server" Text="Plan Call In" 
                            Width="100px" />
                        &nbsp;<asp:Button ID="btPOCallIn" runat="server" Text="PO Call In" 
                            Width="100px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Report" Width="100px" />
                    </td>
                </tr>
            </table>
            <table style="width: 65%;">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <div id="devShow">
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
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
            </div>
            <div id="divPlanCallIn">

            <asp:GridView ID="gvPlanCallIn" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
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
            </div>
            <div id="divPOCallIn">
            <asp:GridView ID="gvPoCallIn" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
