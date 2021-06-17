<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="LotControl.aspx.vb" Inherits="MIS_HTI.LotControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 65%;
        }
        .style5
        {
        }
        .style7
        {
            width: 91px;
        }
        .style9
        {
            width: 115px;
        }
        .style10
        {
            width: 107px;
        }
        .style12
        {
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="style4">
                <tr>
                    <td class="style12" colspan="2" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" 
                            ForeColor="Blue" Text="Lot Control Sheet"></asp:Label>
                    </td>
                </tr>
                </table>
                <table>
                <tr>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Work Order Type :"></asp:Label>
                    </td>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="DDLType" runat="server" AutoPostBack="True" 
                            DataSourceID="SqlDataSource2" DataTextField="MQ002" DataValueField="MQ001">
                        </asp:DropDownList>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Work Order No. :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="WNo" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                            SelectCommand="select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002">
                        </asp:SqlDataSource>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Item No :"></asp:Label>
                    </td>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:TextBox ID="Item" runat="server"></asp:TextBox>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="Spec :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="Spec" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="MO Date :"></asp:Label>
                    </td>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:TextBox ID="MODate" runat="server"></asp:TextBox>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        <asp:Button ID="BuSearch" runat="server" Text="Search" />
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Button ID="Print" runat="server" Text="Print" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="TA001,TA002" 
                DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" 
                GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="TA001" HeaderText="Work Order Type" ReadOnly="True" 
                        SortExpression="TA001" />
                    <asp:BoundField DataField="TA002" HeaderText="Work Order No" ReadOnly="True" 
                        SortExpression="TA002" />
                    <asp:BoundField DataField="TA003" HeaderText="Date" SortExpression="TA003" />
                    <asp:BoundField DataField="TA006" HeaderText="Item" SortExpression="TA006" />
                    <asp:BoundField DataField="TA035" HeaderText="Spec" SortExpression="TA035" />
                    <asp:BoundField DataField="TA015" HeaderText="Quantity" SortExpression="TA015" 
                        DataFormatString = {0:F} >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:ButtonField ButtonType="Image" CommandName="OnPrint" HeaderText="Print" 
                        ImageUrl="~/Images/icon_print.png">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                SelectCommand="select TA001,TA002,TA003,TA006,TA035,TA015 from MOCTA">
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>
