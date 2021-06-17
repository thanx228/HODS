<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="CustomsSale.aspx.vb" Inherits="MIS_HTI.CustomsSale" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style9
        {
        }
        .style10
        {
            width: 192px;
        }
        .style11
        {
            width: 155px;
        }
        .style12
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 75%; height: 91px;">
                        <tr>
                            <td class="style9" bgcolor="White" colspan="4" 
                                style="background-image: url('../Images/btt.jpg')">
                                Customs</td>
                        </tr>
                        <tr>
                            <td bgcolor="White" class="style9">
                                <asp:Label ID="Label1" runat="server" Text="Customer"></asp:Label>
                            </td>
                            <td bgcolor="White" class="style10">
                                <asp:TextBox ID="tbCust" runat="server" Width="66px"></asp:TextBox>
                            </td>
                            <td bgcolor="White" class="style11">
                            </td>
                            <td bgcolor="White">
                            </td>
                        </tr>
                        <tr>
                            <td class="style9" bgcolor="White">
                                <asp:Label ID="Label2" runat="server" Text="Invoice Type"></asp:Label>
                            </td>
                            <td class="style10" bgcolor="White">
                                <asp:DropDownList ID="ddlInvType" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="style11" bgcolor="White">
                                <asp:Label ID="Label4" runat="server" Text="Invoice No"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:TextBox ID="tbInvNo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style9" bgcolor="White">
                                <asp:Label ID="Label3" runat="server" Text="Date From"></asp:Label>
                            </td>
                            <td class="style10" bgcolor="White">
                                <asp:TextBox ID="tbDateFrom" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                                Enabled="True" TargetControlID="tbDateFrom">
                                </asp:CalendarExtender>
                            </td>
                            <td class="style11" bgcolor="White">
                                <asp:Label ID="Label5" runat="server" Text="Date To"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:TextBox ID="tbDateTo" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                                Enabled="True" TargetControlID="tbDateTo">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="style9" bgcolor="White">
                            RemarkType</td>
                            <td class="style10" bgcolor="White">
                                <asp:DropDownList ID="ddlType" runat="server">
                                    <asp:ListItem Selected="True">Computer</asp:ListItem>
                                    <asp:ListItem>Automative</asp:ListItem>
                                    <asp:ListItem>Appliances</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style11" bgcolor="White">
                            </td>
                            <td bgcolor="White">
                            </td>
                        </tr>
                    </table>
                    <table style="width:75%;">
                        <tr>
                            <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                                align="center" class="style12">
                                <asp:Button ID="btSearch" runat="server" Text="Search" />
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
                    ConnectionString="<%$ ConnectionStrings:HOOTHAIConnectionString %>" 
                    SelectCommand="SELECT DISTINCT [TA001] as InvType, [TA002] as InvNo, [TA038] as InvDate, [TA004] as Cust, [TA008] as CustName FROM [ACRTA] WHERE (([TA001] = @TA001) AND ([TA004] = @TA004)) ORDER BY [TA001], [TA002]">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlInvType" Name="TA001" 
                            PropertyName="SelectedValue" Type="String" />
                            <asp:ControlParameter ControlID="tbCust" Name="TA004" PropertyName="Text" 
                            Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:Button ID="btSelect" runat="server" Text="Select AMT US" />
                    &nbsp;&nbsp;
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
                    <asp:Button ID="btPrintDel" runat="server" Text="Print Delta" />
                    &nbsp;&nbsp; &nbsp;<asp:Button ID="btExcel" runat="server" Text="Excel" />
                    <br />
                    <br />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID ="btExcel" />
                    <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="RowCommand" />
                </Triggers>
    </asp:UpdatePanel>
    </asp:Content>
