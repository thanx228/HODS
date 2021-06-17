<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PrintCheq.aspx.vb" Inherits="MIS_HTI.PrintCheq" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style5
        {
            width: 479px;
        }
        .style6
        {
            width: 40px;
        }
        .style7
        {
            width: 105px;
        }
        .style8
        {
            width: 83px;
        }
        .style10
        {
            width: 479px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 60%;">
                <tr>
                    <td 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Print Cheq"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Cheq No."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCheq" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Supplier Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSup" runat="server" Width="60px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Due Date From"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateFrom" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Due Date To"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateTo" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="Cheque Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateCheque" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateCheque_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateCheque">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width: 60%;">
                <tr>
                    <td align="center" class="style10" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
&nbsp;<asp:Button ID="btShow" runat="server" Text="Show Report" />
                        <asp:Button ID="BuPrint" runat="server" Text="Print Report" />
                    </td>
                </tr>
            </table>
            <table style="width: 489px; margin-top: 0px;">
                <tr>
                    <td 
                        style="border: thin solid #0099FF; background-color: #FFFFFF;" 
                        align="center" class="style8">
                        <asp:Label ID="Label6" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" 
                        style="border: thin solid #0099FF; background-color: #FFFFFF;" 
                        class="style8">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" 
                        style="border: thin solid #0099FF; background-color: #FFFFFF;" 
                        class="style6">
                        <asp:Label ID="Label8" runat="server" Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                Width="232px">
                <Columns>
                    <asp:ButtonField ButtonType="Image" CommandName="onPrint" HeaderText="Print" 
                        ImageUrl="~/Images/icon_print.png" Text="Button">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
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
    </asp:UpdatePanel>
</asp:Content>
