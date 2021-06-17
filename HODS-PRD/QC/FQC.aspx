<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="FQC.aspx.vb" Inherits="MIS_HTI.FQC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        .style4
        {
            width: 143px;
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
            <table class="style3">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="X-Large" 
                            ForeColor="Blue" Text="FQC"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Type :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:DropDownList ID="DDLType" runat="server" AutoPostBack="True" 
                            DataSourceID="SqlDataSource1" DataTextField="MQ002" DataValueField="MQ001">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="No :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtno" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Operation :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:DropDownList ID="DDLoperation" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="BuReport" runat="server" Text="Report" />
                    </td>
                    <td>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                            SelectCommand="select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002">
                        </asp:SqlDataSource>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" 
                DataSourceID="SqlDataSource2" PageSize="50" CellPadding="4" 
                ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="TA001" HeaderText="TYPE" SortExpression="TA001" />
                    <asp:BoundField DataField="TA002" HeaderText="NO" SortExpression="TA002" />
                    <asp:BoundField DataField="TA003" HeaderText="DATE" SortExpression="TA003" />
                    <asp:BoundField DataField="TA006" HeaderText="ITEM" SortExpression="TA006" />
                    <asp:BoundField DataField="TA015" HeaderText="T Qty" SortExpression="TA015" />
                    <asp:BoundField DataField="TA020" HeaderText="TA020" SortExpression="TA020" />
                    <asp:BoundField DataField="TA034" HeaderText="Desc" SortExpression="TA034" />
                    <asp:BoundField DataField="TA035" HeaderText="Spec" SortExpression="TA035" />
                    <asp:BoundField DataField="TA057" HeaderText="Lot" SortExpression="TA057" />
                    <asp:BoundField DataField="MG003" HeaderText="Insp No." SortExpression="MG003" />
                    <asp:BoundField DataField="MG006" HeaderText="Std Desc" SortExpression="MG006" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle Wrap="False" BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" SelectCommand="select TA001,TA002,TA003,TA006,TA015,TA020,TA034,TA035,TA057,MG003,MG006 from 
MOCTA M left join QMSMG Q on(M.TA006 = Q.MG002)"></asp:SqlDataSource>
<br />
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
