<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="WorkDayLock.aspx.vb" Inherits="MIS_HTI.WorkDayLock" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
        }
        .style4
        {
            height: 25px;
        }
        </style>
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:47%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label4" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Work Days(Lock)"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:37%;">
                <tr>
                    <td class="style4" style="background-color: #FFFFFF">
                        <asp:Label ID="Label1" runat="server" Text="Work Center :"></asp:Label>
                    </td>
                    <td class="style4" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="DDLWC" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Date From :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtdate1_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtdate1" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Date To :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtdate2" runat="server" Width="80px"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtdate2_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtdate2" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
            </table>
            <asp:Label ID="Label5" runat="server" Font-Size="Medium" ForeColor="Red" 
                Text="MO Status  1= ALL OK,2=Over Load,3=Materials Short and 4 = not 1,2,3"></asp:Label>
            <table style="width:46%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="BuSearch" runat="server" Text="Show Report" Width="100px" />
                        <asp:Button ID="btExport" runat="server" Text="Excel Export" Width="100px" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                Width="384px">
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
