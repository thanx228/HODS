<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="saleOrderDelay.aspx.vb" Inherits="MIS_HTI.saleOrderDelay" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 71%;">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Delivery Date From"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDateFrom" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Delivery Date To"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDateTo" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btDelayMO" runat="server" Text="Delay MO" />
                    </td>
                    <td align="center">
                        &nbsp;<asp:Button ID="btNotIssueSO" runat="server" Text="Not Issue MO" />
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbShowText" runat="server"></asp:Label>
            <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                GridLines="Vertical">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
            <asp:Panel ID="Panel1" runat="server" Height="27px">
                <asp:DropDownList ID="ddlCondition" runat="server" Height="23px" Width="110px">
                </asp:DropDownList>
                <asp:Button ID="btDetailDelay" runat="server" Text="Detail Delay MO" />
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" Height="21px">
                <asp:Button ID="btNotIssueDetail" runat="server" Text="Detail Not Issue SO" />
            </asp:Panel>
            <asp:Panel ID="Panel3" runat="server">
                <table style="width: 69%; height: 34px;">
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="มีรายการจำนวน"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:Label ID="lbCountRow" runat="server" ForeColor="Red"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:Label ID="Label5" runat="server" ForeColor="Blue" Text="    รายการ"></asp:Label>
                            <asp:Button ID="btExcel" runat="server" Text="Excel" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:GridView ID="GridView2" runat="server" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                GridLines="Vertical">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </ContentTemplate>
         <Triggers>
               <asp:PostBackTrigger ControlID ="btExcel" />
               <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="RowCommand" />
               </Triggers>
    </asp:UpdatePanel>
</asp:Content>
