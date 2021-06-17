<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PrintCheqCash.aspx.vb" Inherits="MIS_HTI.PrintCheqCash" %>
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
            width: 502px;
        }
        .style9
        {
            width: 105px;
            height: 26px;
        }
        .style10
        {
            height: 26px;
        }
        .style11
        {
            height: 26px;
            width: 128px;
        }
        .style12
        {
            width: 128px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 70%;">
                <tr>
                    <td align="left" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Print Cheq Cash"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 70%;">
                <tr>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Cheq No."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style11">
                        <asp:TextBox ID="tbCheq" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style10">
                        </td>
                    <td style="background-color: #FFFFFF" class="style10">
                        </td>
                </tr>
                <tr>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Due Date From"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style12">
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
            </table>
            <table style="width: 70%;">
                <tr>
                    <td align="center" class="style8" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
&nbsp;<asp:Button ID="btShow" runat="server" Text="Show Report" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td 
                        style="border: thin solid #0099FF; background-color: #FFFFFF;">
                        <asp:Label ID="Label6" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" 
                        style="border: thin solid #0099FF; background-color: #FFFFFF;">
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
                Width="411px">
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
