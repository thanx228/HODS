<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="BillInvoice.aspx.vb" Inherits="MIS_HTI.BillInvoice" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        .style4
        {
            width: 39px;
        }
        .style12
        {
            width: 163px;
        }
        .style8
        {
            width: 9px;
        }
        .style11
        {
            width: 136px;
        }
        .style7
        {
            width: 18px;
        }
        .style9
        {
            width: 142px;
        }
        .style10
        {
            width: 26px;
        }
        .style14
        {
            width: 132px;
        }
        .style15
        {
            width: 485px;
        }
        .style19
        {
            width: 123px;
        }
        .style21
        {
            width: 41px;
        }
        .style22
        {
            width: 120px;
        }
        .style24
        {
            width: 88px;
        }
        .style27
        {
            width: 143px;
        }
        .style31
        {
            width: 73px;
        }
        .style32
        {
            width: 137px;
        }
        .style33
        {
            width: 66px;
        }
        .style36
        {
            width: 131px;
        }
        .style38
        {
            width: 110px;
        }
        .style39
        {
            width: 92px;
        }
        .style40
        {
            width: 47px;
        }
        .style41
        {
            width: 235px;
            height: 24px;
        }
        .style42
        {
            height: 24px;
        }
        .style45
        {
            width: 235px;
            height: 28px;
        }
        .style46
        {
            height: 28px;
        }
        .style47
        {
            width: 115px;
        }
        .style48
        {
            width: 148px;
        }
        </style>

      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainer1" runat="server" Height="280%" Width="100%" 
                ActiveTabIndex="0" style="margin-right: 0px" >
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Add Data">

                    <HeaderTemplate>
                        Add Data
                    </HeaderTemplate>

                    <ContentTemplate>
                        <table class="style3">
                            <tr>
                                <td class="style4">
                                    <asp:Label ID="Label11" runat="server" Text="From Date :"></asp:Label>
                                </td>
                                <td class="style48">
                                    <asp:TextBox ID="txtfrom" runat="server" BorderStyle="Solid"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" 
                                        Enabled="True" Format="yyyyMMdd" TargetControlID="txtfrom">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="style8">
                                    <asp:Label ID="Label2" runat="server" Text="To :"></asp:Label>
                                </td>
                                <td class="style11">
                                    <asp:TextBox ID="txtto" runat="server" BorderStyle="Solid"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtto_CalendarExtender" runat="server" Enabled="True" 
                                        Format="yyyyMMdd" TargetControlID="txtto">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="style47">
                                    <asp:Label ID="Label3" runat="server" Text="Cust ID :"></asp:Label>
                                </td>
                                <td class="style9">
                                    <asp:DropDownList ID="DDLCustID" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td class="style10">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    <asp:Label ID="Label21" runat="server" Text="Bill By :"></asp:Label>
                                </td>
                                <td class="style48">
                                    <asp:DropDownList ID="DDLBillby" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td class="style8">
                                    <asp:Label ID="Label22" runat="server" Text="Po :"></asp:Label>
                                </td>
                                <td class="style11">
                                    <asp:TextBox ID="txtpo" runat="server" BorderStyle="Solid"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txtpo_AutoCompleteExtender" runat="server" 
                                        DelimiterCharacters="" Enabled="True" ServicePath="AutoComplate.asmx" MinimumPrefixLength="1" 
                                         ServiceMethod="Get_Po" TargetControlID="txtpo">
                                    </asp:AutoCompleteExtender>
         
                                </td>
                                <td class="style47">
                                    &nbsp;</td>
                                <td class="style9">
                                    <asp:Button ID="Buview" runat="server" Text="Search" />
                                   
                                </td>
                                <td class="style10">
                                    <asp:TextBox ID="ddd" runat="server" Visible="False"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    <asp:Label ID="Label5" runat="server" Text="Bill ing No :"></asp:Label>
                                </td>
                                <td class="style48">
                                    <asp:TextBox ID="txtbilling" runat="server" Enabled="False" 
                                        BorderStyle="Solid"></asp:TextBox>
                                </td>
                                <td class="style8">
                                    <asp:Label ID="Label4" runat="server" Text="Cust ID :"></asp:Label>
                                </td>
                                <td class="style11">
                                    <asp:TextBox ID="txtCustID" runat="server" Enabled="False" BorderStyle="Solid"></asp:TextBox>
                                </td>
                                <td class="style47">
                                    <asp:Label ID="Label6" runat="server" Text="Date :"></asp:Label>
                                </td>
                                <td class="style9">
                                    <asp:TextBox ID="txtdate" runat="server" Enabled="False" BorderStyle="Solid"></asp:TextBox>
                                </td>
                                <td class="style10">
                                    <asp:TextBox ID="txtBillNo" runat="server" Enabled="False" Visible="False"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    <asp:Label ID="Label9" runat="server" Text="Cust Name :"></asp:Label>
                                </td>
                                <td class="style48">
                                    <asp:TextBox ID="txtcustname" runat="server" Enabled="False" Width="160px" 
                                        BorderStyle="Solid"></asp:TextBox>
                                </td>
                                <td class="style8">
                                    <asp:Label ID="Label7" runat="server" Text="Payment :"></asp:Label>
                                </td>
                                <td class="style6" colspan="5">
                                    <asp:TextBox ID="txtpayment" runat="server" Enabled="False" 
                                        BorderStyle="Solid"></asp:TextBox>
                                    <asp:Button ID="Busave" runat="server" 
                                        OnClientClick="return confirm('are you sure save it');" Text="Save" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style41">
                                    <asp:Label ID="Label8" runat="server" Text="Address :"></asp:Label>
                                </td>
                                <td class="style42" colspan="7">
                                    <asp:TextBox ID="txtaddress1" runat="server" Enabled="False" Width="597px" 
                                        BorderStyle="Solid"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style41">
                                    <asp:Label ID="Label10" runat="server" Text="Address :"></asp:Label>
                                </td>
                                <td class="style42" colspan="7">
                                    <asp:TextBox ID="txtaddress2" runat="server" Enabled="False" Width="596px" 
                                        BorderStyle="Solid"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style45">
                                    <asp:Button ID="Buselect" runat="server" Text="Select All" />
                                </td>
                                <td class="style46" colspan="7">
                                    <asp:Button ID="BuClear" runat="server" Text="Clear Data" />
                                </td>
                            </tr>
                        </table>

            <asp:TextBox ID="txtbath" runat="server" Visible="False"></asp:TextBox>

                        <br />
                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="TA001,TA002" Width="896px">
                            <Columns>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Ck" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TA038" HeaderText="Date" SortExpression="TA038" />
                                <asp:BoundField DataField="TA001" HeaderText="Type" ReadOnly="True" 
                                    SortExpression="TA001" />
                                <asp:BoundField DataField="TA002" HeaderText="Order No." ReadOnly="True" 
                                    SortExpression="TA002" />
                                <asp:BoundField DataField="TA020" HeaderText="Plan Receive Date" SortExpression="TA020" />
                                <asp:BoundField DataField="TA042" HeaderText="Tax(B/C)" SortExpression="TA042" />
                                <asp:BoundField DataField="TA041" HeaderText="Tax041" SortExpression="TA041" />
                                <asp:BoundField DataField="TA098" HeaderText="Collection(B/C)" SortExpression="TA098" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:HOOTHAIConnectionString %>" SelectCommand="select TA004, TA008, TA038, TA080, TA001,TA002, TA020, TA042, TA041,TA098 from 
[HOOTHAI].[dbo].[ACRTA] WHERE not EXISTS(select * from [HOOTHAI_REPORT].[dbo].[BillLine] 
where ACRTA.TA001 = BillLine.InvoiceH and ACRTA.TA002 = BillLine.InvoiceNo) "></asp:SqlDataSource>
                        <br />

                    </ContentTemplate>
                </asp:TabPanel>

                 <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Delete Data">
                      <ContentTemplate>


                          <table class="style3">
                              <tr>
                                  <td class="style24">
                                      <asp:Label ID="Label18" runat="server" Text="Billing No :"></asp:Label>
                                  </td>
                                  <td class="style27">
                                      <asp:TextBox ID="txtdelBill" runat="server" BorderStyle="Solid"></asp:TextBox>
                                  </td>
                                  <td class="style31">
                                      <asp:Button ID="BuDelSearch" runat="server" Text="Search" />
                                  </td>
                                  <td>
                                      <asp:TextBox ID="txtidEdit" runat="server" Visible="False"></asp:TextBox>
                                  </td>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td class="style24">
                                      <asp:Label ID="Label19" runat="server" Text="Invoice Type :"></asp:Label>
                                  </td>
                                  <td class="style27">
                                      <asp:TextBox ID="txttype" runat="server" BorderStyle="Solid"></asp:TextBox>
                                  </td>
                                  <td class="style31">
                                      <asp:Label ID="Label20" runat="server" Text="Invoice No :"></asp:Label>
                                  </td>
                                  <td>
                                      <asp:TextBox ID="txtinvoiceno" runat="server" BorderStyle="Solid"></asp:TextBox>
                                  </td>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td class="style24">
                                      <asp:Button ID="BuDAll" runat="server" Text="Select All" />
                                  </td>
                                  <td class="style27">
                                      <asp:Button ID="BuCAll" runat="server" Text="Clear All" />
                                  </td>
                                  <td class="style31">
                                      &nbsp;</td>
                                  <td>
                                      <asp:Button ID="BuDelData" runat="server" Text="Delete" 
                                          onclientclick="return confirm('are you sure save it');" />
                                  </td>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                          </table>
                       
                          <asp:GridView ID="Griddelete" runat="server" AutoGenerateColumns="False" 
                              DataKeyNames="ID" DataSourceID="DataSourceDelete">
                              <Columns>

                               <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Ck" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                  <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                      ReadOnly="True" SortExpression="ID" />
                                  <asp:BoundField DataField="InvoiceH" HeaderText="InvoiceH" 
                                      SortExpression="InvoiceH" />
                                  <asp:BoundField DataField="InvoiceNo" HeaderText="InvoiceNo" 
                                      SortExpression="InvoiceNo" />
                                  <asp:BoundField DataField="OrderDate" HeaderText="OrderDate" 
                                      SortExpression="OrderDate" />
                                  <asp:BoundField DataField="DueDate" HeaderText="DueDate" 
                                      SortExpression="DueDate" />
                                  <asp:BoundField DataField="Amount" HeaderText="Amount" 
                                      SortExpression="Amount" />
                                  <asp:BoundField DataField="Paid" HeaderText="Paid" SortExpression="Paid" />
                              </Columns>
                          </asp:GridView>

                          <asp:SqlDataSource ID="DataSourceDelete" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                              SelectCommand="SELECT [ID], [InvoiceH], [InvoiceNo], [OrderDate], [DueDate], [Amount], [Paid] FROM [BillLine]">
                          </asp:SqlDataSource>

                       </ContentTemplate>
                </asp:TabPanel>


                  <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Edit Data">
                  
                      <HeaderTemplate>
                          Edit Data
                      </HeaderTemplate>
                  
                      <ContentTemplate>
                          <table class="style3">
                              <tr>
                                  <td class="style19">
                                      <asp:Label ID="Label26" runat="server" Text="Customer :"></asp:Label>
                                  </td>
                                  <td class="style14">
                                      <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                                      </asp:DropDownList>
                                  </td>
                                  <td class="style21">
                                      <asp:Button ID="BuSearch" runat="server" Text="View Report" />
                                  </td>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td class="style19">
                                      <asp:Label ID="Label27" runat="server" Text="Month  :"></asp:Label>
                                  </td>
                                  <td class="style14">
                                      <asp:TextBox ID="txtmonth" runat="server" BorderStyle="Solid"></asp:TextBox>
                                      <asp:CalendarExtender ID="txtmonth_CalendarExtender" runat="server" 
                                          Enabled="True" Format="MM/yyyy" TargetControlID="txtmonth">
                                      </asp:CalendarExtender>
                                  </td>
                                  <td class="style21">
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                          </table>

                          <asp:GridView ID="GridView5" runat="server" AllowPaging="True" 
                              AllowSorting="True" AutoGenerateColumns="False" DataSourceID="DataSourceEdit">
                              <Columns>
                                  <asp:BoundField DataField="BillShow" HeaderText="BillShow" 
                                      SortExpression="BillShow" />
                                  <asp:BoundField DataField="CustID" HeaderText="CustID" 
                                      SortExpression="CustID" />
                                  <asp:BoundField DataField="CustName" HeaderText="CustName" 
                                      SortExpression="CustName" />
                                  <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                  <asp:ButtonField ButtonType="Image" CommandName="OnClick" 
                                      ImageUrl="~/Images/Addnew.gif" />
                              </Columns>
                          </asp:GridView>
                          <asp:SqlDataSource ID="DataSourceEdit" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                              SelectCommand="SELECT [BillShow], [CustID], [CustName], [Date] FROM [Billhead]">
                          </asp:SqlDataSource>
                          <table class="style3">
                              <tr>
                                  <td class="style19">
                                      <asp:Label ID="Label13" runat="server" Text="Billing No :"></asp:Label>
                                  </td>
                                  <td class="style32">
                                      <asp:Label ID="lblbillno" runat="server" ForeColor="Blue"></asp:Label>
                                  </td>
                                  <td class="style33">
                                      <asp:Label ID="Label28" runat="server" Text="CustID :"></asp:Label>
                                  </td>
                                  <td class="style36">
                                      <asp:Label ID="lblcustid" runat="server" ForeColor="Blue"></asp:Label>
                                  </td>
                                  <td class="style38">
                                      <asp:Label ID="Label32" runat="server" Text="Cust Name :"></asp:Label>
                                  </td>
                                  <td>
                                      <asp:Label ID="lblcustomer" runat="server" ForeColor="Blue"></asp:Label>
                                  </td>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td class="style19">
                                      <asp:Label ID="Label23" runat="server" Text="Date :"></asp:Label>
                                  </td>
                                  <td class="style32">
                                      <asp:Label ID="lbldate" runat="server" ForeColor="Blue"></asp:Label>
                                  </td>
                                  <td class="style33">
                                      <asp:Label ID="Label29" runat="server" Text="PO :"></asp:Label>
                                  </td>
                                  <td class="style36">
                                      <asp:TextBox ID="txtPoEdit" runat="server" BorderStyle="Solid"></asp:TextBox>
                                      <asp:AutoCompleteExtender ID="txtPoEdit_AutoCompleteExtender" runat="server" 
                                          DelimiterCharacters="" Enabled="True" ServicePath="AutoComplate.asmx" 
                                          MinimumPrefixLength="1" ServiceMethod="Get_Po"
                                          TargetControlID="txtPoEdit">
                                      </asp:AutoCompleteExtender>
                                  </td>
                                  <td class="style38">
                                      <asp:Label ID="Label33" runat="server" Text="Edit By :"></asp:Label>
                                  </td>
                                  <td>
                                      <asp:DropDownList ID="DDLBilledit" runat="server" AutoPostBack="True">
                                      </asp:DropDownList>
                                  </td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td class="style19">
                                      <asp:Label ID="Label24" runat="server" Text="From Date :"></asp:Label>
                                  </td>
                                  <td class="style32">
                                      <asp:TextBox ID="txtfromdate" runat="server" BorderStyle="Solid"></asp:TextBox>
                                      <asp:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" 
                                          Enabled="True" TargetControlID="txtfromdate" Format="yyyyMMdd">
                                      </asp:CalendarExtender>
                                  </td>
                                  <td class="style33">
                                      <asp:Label ID="Label30" runat="server" Text="To Date :"></asp:Label>
                                  </td>
                                  <td class="style36">
                                      <asp:TextBox ID="txttodate" runat="server" BorderStyle="Solid"></asp:TextBox>
                                      <asp:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                          Enabled="True" TargetControlID="txttodate" Format="yyyyMMdd">
                                      </asp:CalendarExtender>
                                  </td>
                                  <td class="style38">
                                      &nbsp;</td>
                                  <td>
                                      <asp:TextBox ID="BillNoEdit" runat="server" BorderStyle="Solid"></asp:TextBox>
                                  </td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td class="style19">
                                      <asp:Button ID="Bueditall" runat="server" Text="Select All" />
                                  </td>
                                  <td class="style32">
                                      <asp:Button ID="Bueditcall" runat="server" Text="Clear All" />
                                  </td>
                                  <td class="style33">
                                      <asp:Button ID="BuEdit" runat="server" 
                                          OnClientClick="return confirm('Are you sure Edit it')" Text="Save" />
                                  </td>
                                  <td class="style36">
                                      <asp:Button ID="BuEditSearch" runat="server" Text="Search" />
                                  </td>
                                  <td class="style38">
                                      &nbsp;</td>
                                  <td>
                                      <asp:TextBox ID="txtbathedit" runat="server" BorderStyle="Solid" 
                                          Visible="False"></asp:TextBox>
                                  </td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                          </table>
                          <br />

                          <asp:GridView ID="GridEdit" runat="server" DataSourceID="SqlDataSource4" 
                              AutoGenerateColumns="False" DataKeyNames="TA001,TA002">
                           <Columns>

                            <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Ck" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                   <asp:BoundField DataField="TA004" HeaderText="Cust ID." SortExpression="TA004" />
                                   <asp:BoundField DataField="TA038" HeaderText="Date" SortExpression="TA038" />
                                   <asp:BoundField DataField="TA001" HeaderText="Type" ReadOnly="True" 
                                       SortExpression="TA001" />
                                   <asp:BoundField DataField="TA002" HeaderText="Order No." ReadOnly="True" 
                                       SortExpression="TA002" />
                                   <asp:BoundField DataField="TA020" HeaderText="Plan Receive Date" SortExpression="TA020" />
                                   <asp:BoundField DataField="TA042" HeaderText="Tax(B/C)" SortExpression="TA042" />
                                   <asp:BoundField DataField="TA041" HeaderText="Tax041" SortExpression="TA041" />
                                   <asp:BoundField DataField="TA098" HeaderText="Collection(B/C)" SortExpression="TA098" />

                              </Columns>
                          </asp:GridView>

                          <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:HOOTHAIConnectionString %>" 
                              SelectCommand="select distinct TA004, TA038, TA001,TA002, TA020, TA042, TA041,TA098 from [HOOTHAI].[dbo].[ACRTA] A JOIN [HOOTHAI].[dbo].[ACRTB] B ON (A.TA001 = B.TB001) and (A.TA002 = B.TB002) WHERE not EXISTS(select * from [HOOTHAI_REPORT].[dbo].[BillLine] where A.TA001 = BillLine.InvoiceH and A.TA002 = BillLine.InvoiceNo)">
                          </asp:SqlDataSource>

                      </ContentTemplate>
                  
                </asp:TabPanel>

                 <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Print Report">
                      <ContentTemplate>

                       <table class="style15">
                <tr>
                    <td class="style22">
                        <asp:Label ID="Label16" runat="server" Text="Customer :"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:DropDownList ID="DDLCustomer" runat="server" Height="16px">
                        </asp:DropDownList>
                    </td>
                    <td class="style39">
                        <asp:Label ID="Label17" runat="server" Text="Month :"></asp:Label>
                    </td>
                    <td class="style40">
                        <asp:TextBox ID="TextBox3" runat="server" BorderStyle="Solid"></asp:TextBox>
                        <asp:CalendarExtender ID="TextBox3_CalendarExtender" runat="server" 
                            Enabled="True"  Format="MM/yyyy" TargetControlID="TextBox3">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:Button ID="BuReport" runat="server" Text="Search By Cust" />
                    </td>
                </tr>
                           <tr>
                               <td class="style22">
                                   <asp:Label ID="Label34" runat="server" Text="Bill By :"></asp:Label>
                               </td>
                               <td class="style10">
                                   <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True">
                                   </asp:DropDownList>
                               </td>
                               <td class="style39">
                                   <asp:Button ID="Bubill" runat="server" Text="Search By Bill By" />
                               </td>
                               <td class="style40">
                                   <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>
                               </td>
                               <td>
                                   &nbsp;</td>
                           </tr>
            </table>


                          <asp:GridView ID="GridView4" runat="server" AllowPaging="True" 
                              AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="BillNo" 
                              DataSourceID="SqlDataSource1">
                              <Columns>
                                        <asp:BoundField DataField="BillShow" HeaderText="BillNo" 
                                      SortExpression="BillShow" />

                                  <asp:BoundField DataField="CustID" HeaderText="CustID" 
                                      SortExpression="CustID" />
                                  
                                   <asp:BoundField DataField="CustName" HeaderText="CustName" 
                                      SortExpression="CustName" />

                                       <asp:BoundField DataField="BillBy" HeaderText="Bill By" 
                                      SortExpression="BillBy" />

                                       <asp:BoundField DataField="EditBy" HeaderText="Edit By" 
                                      SortExpression="EditBy" />

                                  <asp:ButtonField ButtonType="Image" CommandName="OnClick" 
                                      ImageUrl="~/Images/icon_print.png" HeaderText="Print" >

                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>

                              </Columns>
                          </asp:GridView>
                          <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                              SelectCommand="SELECT * FROM [Billhead] order by BillShow desc"></asp:SqlDataSource>
                      </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
