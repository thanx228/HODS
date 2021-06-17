<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="customs.aspx.vb" Inherits="MIS_HTI.customs" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            width: 151px;
        }
        .style3
        {
            width: 97px;
        }
        .style4
        {
            width: 96px;
        }
        .style5
        {
            width: 140px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 62%; height: 91px;">
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label1" runat="server" Text="Customer"></asp:Label>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="tbCust" runat="server" Width="66px"></asp:TextBox>
                        </td>
                        <td class="style4">
                            &nbsp;</td>
                        <td class="style5">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label2" runat="server" Text="Invoice Type"></asp:Label>
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="ddlInvType" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="style4">
                            <asp:Label ID="Label4" runat="server" Text="Invoice No"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:TextBox ID="tbInvNo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label3" runat="server" Text="Date From"></asp:Label>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="tbDateFrom" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                                Enabled="True" TargetControlID="tbDateFrom">
                            </asp:CalendarExtender>
                        </td>
                        <td class="style4">
                            <asp:Label ID="Label5" runat="server" Text="Date To"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:TextBox ID="tbDateTo" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                                Enabled="True" TargetControlID="tbDateTo">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            RemarkType</td>
                        <td class="style2">
                            <asp:DropDownList ID="ddlType" runat="server">
                                <asp:ListItem Selected="True">Computer</asp:ListItem>
                                <asp:ListItem>Appliances</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style4">
                            &nbsp;</td>
                        <td class="style5">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Button ID="btSearch" runat="server" Text="Search" />
                        </td>
                        <td class="style2">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td class="style5">
                            <br />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" DataKeyNames="InvType,InvNo" DataSourceID="SqlDataSource1" 
                    GridLines="Horizontal">
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="InvType" HeaderText="InvType" ReadOnly="True" 
                            SortExpression="InvType" />
                        <asp:BoundField DataField="InvNo" HeaderText="InvNo" ReadOnly="True" 
                            SortExpression="InvNo" />
                        <asp:BoundField DataField="InvDate" HeaderText="InvDate" 
                            SortExpression="InvDate" />
                        <asp:BoundField DataField="Cust" HeaderText="Cust" SortExpression="Cust" />
                        <asp:BoundField DataField="CustName" HeaderText="CustName" 
                            SortExpression="CustName" />
                    </Columns>
                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                    SelectCommand="SELECT DISTINCT [TA001] as InvType, [TA002] as InvNo, [TA038] as InvDate, [TA004] as Cust, [TA008] as CustName FROM [ACRTA] WHERE (([TA001] = @TA001) AND ([TA004] = @TA004)) ORDER BY [TA001], [TA002]">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlInvType" Name="TA001" 
                            PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter ControlID="tbCust" Name="TA004" PropertyName="Text" 
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:Button ID="btSelect" runat="server" Text="Select AMT US" />
                <asp:Button ID="btSelectTH" runat="server" Text="Select AMT TH" />
                <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" 
                    GridLines="None" >
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <asp:Button ID="btPrint" runat="server" Text="Print Report" />
                <asp:Button ID="btPrintDel" runat="server" Text="Print Delta" />
                <asp:Button ID="btPrintChin" runat="server" Text="Print Chin i" />
                <br />
                <br />
                <asp:Button ID="btPrintFisher" runat="server" Text="Print Fisher" />
                <asp:Button ID="btExcel" runat="server" Text="Excel" />
                <br />
                <br />
                </ContentTemplate>
               
               <Triggers>
               <asp:PostBackTrigger ControlID ="btExcel" />
               <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="RowCommand" />
               </Triggers>

        </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
