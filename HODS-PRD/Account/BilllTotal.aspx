<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="BilllTotal.aspx.vb" Inherits="MIS_HTI.BilllTotal" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 481px;
            height: 400px;
        }
        .style5
        {
        }
        .style6
        {
            width: 133px;
        }
        .style7
        {
            width: 53px;
        }
        .style8
        {
            width: 142px;
        }
        .style9
        {
            width: 78px;
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
            <table style="width:65%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" 
                            ForeColor="#3366FF" Text=" Report Billing (Sales) Sub Total"></asp:Label>
                    </td>
                </tr>
            </table>
                <table>
                <tr>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:Label ID="Label1" runat="server" Text="From Date :"></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtfrom" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyyMMdd" TargetControlID="txtfrom">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="To Date :"></asp:Label>
                    </td>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtto" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtto_CalendarExtender" runat="server" Enabled="True" 
                            Format="yyyyMMdd" TargetControlID="txtto">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Button ID="BuSearch" runat="server" Text="Search" Width="100px" />
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Button ID="BuPrint" runat="server" Text="Report" Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Item "></asp:Label>
                    </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="LCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Record"></asp:Label>
                    </td>
                    <td class="style8" style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtbath" runat="server" Visible="False"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataSourceID="SqlDataSource1" CellPadding="4" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                <Columns>
                 <asp:BoundField DataField="BillShow" HeaderText="Bill No." 
                        SortExpression="BillShow" />

                   <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />

                    <asp:BoundField DataField="CustID" HeaderText="Cust ID" 
                        SortExpression="CustID" />
                  
                     <asp:BoundField DataField="CustName" HeaderText="Customer Name" 
                        SortExpression="CustName" />

                           <asp:BoundField DataField="InvoiceH" HeaderText="Invoice Type" 
                        SortExpression="InvoiceH" />

                    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No." 
                        SortExpression="InvoiceNo" />

                    <asp:BoundField DataField="Payment" HeaderText="Payment" 
                        SortExpression="Payment" />

                    <asp:BoundField DataField="Balance" HeaderText="Balance" 
                        SortExpression="Balance" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                SelectCommand="select * from Billhead H left join BillLine L on (H.BillNo = L.BillNo)">
            </asp:SqlDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
  
</asp:Content>
