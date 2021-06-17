<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SODeliveryDelayStatus.aspx.vb" Inherits="MIS_HTI.SODeliveryDelayStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 26px;
        }
        .style7
        {
            width: 527px;
        }
        .style8
        {
            width: 31px;
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
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Label 
                            ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="SO Delivery Delay Status"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td style="background-color: #FFFFFF; vertical-align: top;">
                        <asp:Label ID="Label13" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td colspan="3" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:Label ID="Label14" runat="server" Text="SO No."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:TextBox ID="tbSO" runat="server" MaxLength="12"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:Label ID="Label6" runat="server" Text="Cust ID"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style6">
                        <asp:TextBox ID="tbCustId" runat="server" MaxLength="10" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label12" runat="server" Text="Delivery Due"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbFromDate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="tbFromDate_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbFromDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label15" runat="server" Text="Type Show"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlShow" runat="server">
                            <asp:ListItem Value="Sum">Sum</asp:ListItem>
                            <asp:ListItem Value="Detail">Detail</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Spec"></asp:Label>
                        .</td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlCon" runat="server">
                            <asp:ListItem>FG &lt; SO Bal.</asp:ListItem>
                            <asp:ListItem>FG &gt; SO Bal.</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
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
                    <td align="center" class="style4" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Amout Sum &amp; Detail of Rows"></asp:Label>
                    </td>
                    <td align="center" class="style4" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF">
                        <asp:Label ID="lbCountSum" runat="server" ForeColor="Blue"></asp:Label>
                        &nbsp;&nbsp; &amp;&nbsp;&nbsp;
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" class="style8" 
                        style="border: thin solid #FFFFFF; background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShowSum" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:BoundField HeaderText="Cus. Name" DataField="Cus. Name" />
                    <asp:BoundField HeaderText="Total Item" DataField="Total Item" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="1 - 6 Days" DataField="1 - 6 Days" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="7 - 14 Days" DataField="7 - 14 Days" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="15 - 21 Days" DataField="15 - 21 Days">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="&gt; 21 Days" DataField="&gt; 21 Days" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <br />
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="Cus. Name" HeaderText="Cus. Name" />
                    <asp:BoundField DataField="SO No" HeaderText="SO No" />
                    <asp:BoundField DataField="Item" HeaderText="Item" />
                    <asp:BoundField DataField="Spec" HeaderText="Spec." />
                    <asp:BoundField DataField="SO Qty" DataFormatString="{0:#,##0.000}" 
                        HeaderText="SO Qt'y">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SO Bal." DataFormatString="{0:#,##0.000}" 
                        HeaderText="So Bal.">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FG" DataFormatString="{0:#,##0.000}" HeaderText="FG">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Deliver Date" HeaderText="Deliver Date" />
                    <asp:BoundField DataField="Delay Days" HeaderText="Delay Days">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <br />
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
