<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ControlInvoice.aspx.vb" Inherits="MIS_HTI.CK" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 75%;
        }
        .style11
        {
            width: 79px;
        }
        .style16
        {
            width: 160px;
        }
        .style17
        {
            width: 182px;
        }
        .style18
        {
            width: 123px;
        }
        .style19
        {
            width: 59px;
        }
        .style20
        {
        }
        .style21
        {
            width: 61px;
        }
        .style23
        {
            width: 107px;
        }
        .style28
        {
            width: 129px;
        }
        .style29
        {
            width: 74px;
        }
        .style30
        {
            width: 50px;
        }
        .style31
        {
            width: 68px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainer1" runat="server" Height="59%" Width="126%" 
                ActiveTabIndex="0" style="margin-right: 0px" >
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Add Data">
                    <HeaderTemplate>
                        Basic Data
                    </HeaderTemplate>
                    <ContentTemplate>
                       <table class="style6">
                <tr>
                    <td colspan="10" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="Large" 
                            ForeColor="Blue" Text="Control Document Invoice &amp; DO"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style19">
                        <asp:Label ID="Label2" runat="server" Text="From Date :"></asp:Label>
                    </td>
                    <td class="style20">
                        <asp:TextBox ID="txtfrom" runat="server" BorderStyle="Solid"></asp:TextBox>
                        <asp:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyyMMdd" TargetControlID="txtfrom">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style21">
                        <asp:Label ID="Label3" runat="server" Text="To Date :"></asp:Label>
                    </td>
                    <td class="style28">
                        <asp:TextBox ID="txtto" runat="server" BorderStyle="Solid"></asp:TextBox>
                        <asp:CalendarExtender ID="txtto_CalendarExtender" runat="server" Enabled="True" 
                            Format="yyyyMMdd" TargetControlID="txtto">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style29">
                        <asp:Label ID="Label4" runat="server" Text="Customer :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtcid" runat="server" BorderStyle="Solid"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtcid_AutoCompleteExtender" runat="server" 
                            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" 
                            ServiceMethod="Get_CID" ServicePath="CID.asmx" TargetControlID="txtcid">
                        </asp:AutoCompleteExtender>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtdate" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style19">
                        <asp:Label ID="Label27" runat="server" Text="Type :"></asp:Label>
                    </td>
                    <td class="style20">
                        <asp:TextBox ID="txttype" runat="server" BorderStyle="Solid"></asp:TextBox>
                    </td>
                    <td class="style21">
                        <asp:Label ID="Label28" runat="server" Text="No :"></asp:Label>
                    </td>
                    <td class="style28">
                        <asp:TextBox ID="txtno" runat="server" BorderStyle="Solid"></asp:TextBox>
                    </td>
                    <td class="style29">
                        <asp:Button ID="BuSearch" runat="server" Text="Search" />
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
            </table>


            <table class="style6">
                <tr>
                    <td class="style18">
                        <asp:Label ID="Label15" runat="server" Text="Customer Code :"></asp:Label>
                    </td>
                    <td class="style16">
                        <asp:Label ID="CCode" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style23">
                        <asp:Label ID="Label16" runat="server" Text="Customer Name :"></asp:Label>
                    </td>
                    <td class="style17">
                        <asp:Label ID="CName" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style11">
                        <asp:Label ID="Label17" runat="server" Text="Bill No :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LBillNo" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
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
                    <td class="style18">
                        <asp:Label ID="Label6" runat="server" Text="Invoice Type :"></asp:Label>
                    </td>
                    <td class="style16">
                        <asp:Label ID="LType" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style23">
                        <asp:Label ID="Label9" runat="server" Text="Invoice No :"></asp:Label>
                    </td>
                    <td class="style17">
                        <asp:Label ID="LNo" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style11">
                        <asp:Label ID="Label24" runat="server" Text="Invoice Date :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="InDate" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
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
                    <td class="style18">
                        <asp:Label ID="Label18" runat="server" Text="Stroe Send Invoice  :"></asp:Label>
                    </td>
                    <td class="style16">
                        <asp:Label ID="StoreInvoice" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style23">
                        <asp:Label ID="Label19" runat="server" Text="Store Send Invoice Date :"></asp:Label>
                    </td>
                    <td class="style17">
                        <asp:Label ID="StoreInDate" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style11">
                        <asp:Label ID="Label22" runat="server" Text="Remark :"></asp:Label>
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtremark" runat="server" Width="347px" BorderStyle="Solid"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style18">
                        <asp:Label ID="Label20" runat="server" Text="Store Send DO :"></asp:Label>
                    </td>
                    <td class="style16">
                        <asp:Label ID="StoreDo" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style23">
                        <asp:Label ID="Label21" runat="server" Text="Store Send DO Date :"></asp:Label>
                    </td>
                    <td class="style17">
                        <asp:Label ID="StoreDoDate" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style11">
                        <asp:Button ID="BuSave" runat="server" Text="Save" 
                            onclientclick="return confirm('are you sure save it');" />
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
            </table>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="TA001,TA002" 
                DataSourceID="SqlDataSource1" CellPadding="4" Width="263px" Font-Size="X-Small" 
                            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                <Columns>

                     <asp:ButtonField ButtonType="Image" CommandName="OnClick" 
                        ImageUrl="~/Images/imagesview.jpg" HeaderText="Select">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>

                    <asp:BoundField DataField="TA004" HeaderText="Customer Code" SortExpression="TA004" />
                    <asp:BoundField DataField="MA002" HeaderText="Customer Name" SortExpression="MA002" />
                    <asp:BoundField DataField="TA038" HeaderText="Date" SortExpression="TA038" />
                    <asp:BoundField DataField="TA001" HeaderText="Type" ReadOnly="True" 
                        SortExpression="TA001" />
                    <asp:BoundField DataField="TA002" HeaderText="Order No." ReadOnly="True" 
                        SortExpression="TA002" />
                    <asp:BoundField DataField="TA020" HeaderText="Plan Receive Date" SortExpression="TA020" />
                    
                   <asp:BoundField DataField="TA041" HeaderText="Amount(Not Includ Vat)" SortExpression="TA041" />
                    <asp:BoundField DataField="TA042" HeaderText="Vat" SortExpression="TA042" />
                    
                    <asp:BoundField DataField="TA098" HeaderText="Total Amount" SortExpression="TA098" />
                   
                    <asp:BoundField DataField="BillShow" HeaderText="Bill No." 
                        SortExpression="BillShow" />
                    <asp:BoundField DataField="StoreInvoice" HeaderText="Invoice" 
                        SortExpression="StoreInvoice" />
                    <asp:BoundField DataField="StoreInDate" HeaderText="Invoice Date" 
                        SortExpression="StoreInDate" />
                    <asp:BoundField DataField="SalesReInDate" HeaderText="Sales Receive Invoice Date" 
                        SortExpression="SalesReInDate" />
                    <asp:BoundField DataField="StoreDO" HeaderText="DO" 
                        SortExpression="StoreDO" />
                    <asp:BoundField DataField="StoreDoDate" HeaderText="Do Date" 
                        SortExpression="StoreDoDate" />
                    <asp:BoundField DataField="SalesReDoDate" HeaderText="Sales Received Do Date" 
                        SortExpression="SalesReDoDate" />

                    <asp:BoundField DataField="Status" HeaderText="Status" 
                        SortExpression="Status" />

                           <asp:BoundField DataField="SalesBy" HeaderText="Sales By" 
                        SortExpression="SalesBy" />

                    <asp:BoundField DataField="RemarkSales" HeaderText="Remark" 
                        SortExpression="RemarkSales" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" Wrap="True" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                SelectCommand="
  
  select A.TA004,C.MA002,A.TA038,A.TA001,A.TA002,A.TA020,A.TA042,A.TA041,A.TA098,H.BillShow,[StoreInvoice],[StoreInDate],[SalesReInDate],[StoreDO],[StoreDoDate],[SalesReDoDate],Sa.SalesBy,Sa.Status,[RemarkSales]
  from [JINPAO80].[dbo].[ACRTA] A left join [DBMIS].[dbo].[BillLine] L 
  on (A.TA001 = L.InvoiceH) and (A.TA002 = L.InvoiceNo)
  left join [DBMIS].[dbo].[Billhead] H on (H.BillNo = L.BillNo)
  left join [DBMIS].[dbo].[ControlStore] St on (A.TA001 = St.TA001) and (A.TA002 = St.TA002)
  left join [DBMIS].[dbo].[ControlSales] Sa on (A.TA001 = Sa.TA001) and (A.TA002 = Sa.TA002)
  left join [JINPAO80].[dbo].[COPMA] C on (A.TA004 = C.MA001) 
   where A.TA001 not like '%EX%'"></asp:SqlDataSource>

                        </ContentTemplate>
                </asp:TabPanel>

                  <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Print Report">
                      <ContentTemplate>




                          <table class="style6">
                              <tr>
                                  <td class="style23">
                                      <asp:Label ID="Label26" runat="server" Text="Invoice Status :"></asp:Label>
                                  </td>
                                  <td class="style28">
                                      <asp:DropDownList ID="DDLStatus" runat="server" AutoPostBack="True">
                                          <asp:ListItem>All</asp:ListItem>
                                          <asp:ListItem Value="Close">Invoice Status Close </asp:ListItem>
                                          <asp:ListItem Value="Open">Invoice Status Open</asp:ListItem>
                                      </asp:DropDownList>
                                  </td>
                                  <td class="style31">
                                      <asp:Label ID="Label29" runat="server" Text="Customer :"></asp:Label>
                                  </td>
                                  <td class="style21">
                                      <asp:TextBox ID="txtrecust" runat="server"></asp:TextBox>
                                      <asp:AutoCompleteExtender ID="txtrecust_AutoCompleteExtender" runat="server" 
                                          DelimiterCharacters="" Enabled="True" ServicePath="CID.asmx" 
                                          MinimumPrefixLength="1" ServiceMethod="Get_CID"
                                          TargetControlID="txtrecust">
                                      </asp:AutoCompleteExtender>
                                  </td>
                                  <td class="style30">
                                      <asp:Button ID="BuShow" runat="server" Text="Show" />
                                  </td>
                                  <td>
                                      <asp:Button ID="BuPrint" runat="server" Text="Print Report" 
                                          style="margin-left: 0px" />
                                  </td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td class="style20" colspan="5">
                                      <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                                  </td>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                          </table>




                          <br />
                          <asp:GridView ID="GridView3" runat="server" CellPadding="4" BackColor="White" 
                              BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
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




                         </ContentTemplate>
                </asp:TabPanel>

            </asp:TabContainer>

         
<br />

        </ContentTemplate>
      
    </asp:UpdatePanel>

</asp:Content>
