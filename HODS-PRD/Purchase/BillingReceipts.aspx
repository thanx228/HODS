<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="BillingReceipts.aspx.vb" Inherits="MIS_HTI.BillingReceipts" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register src="../UserControl/DropDownListUserControl.ascx" tagname="DropDownListUserControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            width: 137%;
        }
        .style20
        {
            width: 100%;
        }
        .style21
        {
            width: 77px;
        }
        .style23
        {
            width: 132px;
        }
        .style24
        {
            width: 372px;
        }
        .style27
        {
            width: 97px;
        }
        .style28
        {
            width: 101px;
        }
        .style29
        {
            width: 53px;
        }
        .style30
        {
            width: 89px;
        }
        .style31
        {
            width: 37px;
        }
        .style32
        {
            width: 684px;
        }
        .style33
        {
            width: 667px;
        }
        .style34
        {
            width: 636px;
        }
        .style35
        {
            width: 606px;
        }
        .style36
        {
            width: 577px;
        }
        .style37
        {
            width: 539px;
        }
        .style38
        {
            width: 494px;
        }
        .style39
        {
            width: 455px;
        }
        .style40
        {
            width: 382px;
        }
        .style41
        {
            width: 334px;
        }
        .style43
        {
            width: 68px;
        }
        .style44
        {
            width: 730px;
        }
        .style45
        {
            width: 720px;
        }
        .style46
        {
            width: 702px;
        }
        .style48
        {
            width: 137px;
        }
        .style49
        {
            width: 74px;
        }
        .style50
        {
            width: 153px;
        }
        .style51
        {
            width: 30px;
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
       
            <table class="style20">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" 
                            ForeColor="Blue" Text="Billing Purchase"></asp:Label>
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
       
       <asp:TabContainer ID="TabContainer1" runat="server" Height="100%" Width="100%" 
                ActiveTabIndex="1" >
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Add Data">

                    <ContentTemplate>
                        <table class="style3">
                            <tr>
                                <td class="style24">
                                    <asp:Label ID="Label2" runat="server" Text="From Date :"></asp:Label>
                                </td>
                                <td class="style7">
                                    <asp:TextBox ID="txtfrom" runat="server" BorderStyle="Double"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" Format="yyyyMMdd" TargetControlID="txtfrom" BehaviorID="_content_txtfrom_CalendarExtender">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="style32">
                                    <asp:Label ID="Label3" runat="server" Text="To :"></asp:Label>
                                </td>
                                <td class="style33">
                                    <asp:TextBox ID="txtto" runat="server" BorderStyle="Double"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtto_CalendarExtender" runat="server" 
                                        Format="yyyyMMdd" TargetControlID="txtto" BehaviorID="_content_txtto_CalendarExtender">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="style10">
                                    <asp:Label ID="Label4" runat="server" Text="Supplier :"></asp:Label>
                                </td>
                                <td class="style34">
                                    <asp:DropDownList ID="DDLSup" runat="server" AutoPostBack="True" 
                                        DataSourceID="DataSourceSup" DataTextField="ConcatField" DataValueField="MA001">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="DataSourceSup" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:HOOTHAIConnectionString %>" 
                                        SelectCommand="SELECT MA001,MA001+MA002 AS ConcatField FROM PURMA order by MA001">
                                    </asp:SqlDataSource>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style24">
                                    <asp:Label ID="Label5" runat="server" Text="Billing No :"></asp:Label>
                                </td>
                                <td class="style7">
                                    <asp:TextBox ID="txtbilling" runat="server" BorderStyle="Double" 
                                        ForeColor="Blue"></asp:TextBox>
                                </td>
                                <td class="style32">
                                    <asp:Label ID="Label6" runat="server" Text="SuppID :"></asp:Label>
                                </td>
                                <td class="style33">
                                    <asp:TextBox ID="txtsupid" runat="server" BorderStyle="Double" ForeColor="Blue"></asp:TextBox>
                                </td>
                                <td class="style10">
                                    <asp:Label ID="Label7" runat="server" Text="Date :"></asp:Label>
                                </td>
                                <td class="style34">
                                    <asp:TextBox ID="txtdate" runat="server" BorderStyle="Double" ForeColor="Blue"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style24">
                                    <asp:Label ID="Label8" runat="server" Text="Supp Name :"></asp:Label>
                                </td>
                                <td class="style7">
                                    <asp:TextBox ID="txtname" runat="server" BorderStyle="Double" ForeColor="Blue"></asp:TextBox>
                                </td>
                                <td class="style32">
                                    <asp:Label ID="Label9" runat="server" Text="Payment :"></asp:Label>
                                </td>
                                <td class="style33">
                                    <asp:TextBox ID="txtpayment" runat="server" BorderStyle="Double" 
                                        ForeColor="Blue"></asp:TextBox>
                                </td>
                                <td class="style10">
                                    <asp:Button ID="BuSearch" runat="server" Text="Search" />
                                </td>
                                <td class="style34">
                                    <asp:Button ID="Busave" runat="server" 
                                        OnClientClick="return confirm('are you sure save it');" Text="Save" 
                                        style="height: 26px" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtbillingno" runat="server" Visible="False"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style24">
                                    <asp:Label ID="Label10" runat="server" Text=" "></asp:Label>
                                    <asp:Label ID="Label11" runat="server" Text="Address :"></asp:Label>
                                </td>
                                <td colspan="6">
                                    <asp:TextBox ID="txtaddress1" runat="server" Width="557px" BorderStyle="Double" 
                                        ForeColor="Blue"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style24">
                                    <asp:Label ID="Label12" runat="server" Text="Address :"></asp:Label>
                                </td>
                                <td colspan="6">
                                    <asp:TextBox ID="txtaddress2" runat="server" Width="558px" BorderStyle="Double" 
                                        ForeColor="Blue"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style24">
                                    <asp:Button ID="Buselect" runat="server" Text="Select All" />
                                </td>
                                <td colspan="6">
                                    <asp:Button ID="Buclear" runat="server" Text="Clear All" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>

                         <asp:GridView ID="GridView1" runat="server" 
                AutoGenerateColumns="False" DataKeyNames="TA001,TA002" 
                DataSourceID="SqlDataSource1">
                <Columns>
                 <asp:TemplateField HeaderText="Select">
                <ItemTemplate>
                <asp:CheckBox ID="CheckBox2" runat="server" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                    <asp:BoundField DataField="TA001" HeaderText="Order Type" ReadOnly="True" SortExpression="TA001" />
                    <asp:BoundField DataField="TA002" HeaderText="Order No." ReadOnly="True" SortExpression="TA002" />
                    <asp:BoundField DataField="TA004" HeaderText="Sup ID." SortExpression="TA004" />
                    <asp:BoundField DataField="TA015" HeaderText="Invoice DATE" SortExpression="TA015" />
                    <asp:BoundField DataField="TA021" HeaderText="Invoice" SortExpression="TA021" />
                    <asp:BoundField DataField="TA020" HeaderText="Due Date" SortExpression="TA020" />
                    <asp:BoundField DataField="AMT" HeaderText="Amount" SortExpression="AMT" />
                    <asp:BoundField DataField="TAX" HeaderText="Tax" SortExpression="TAX" />
                    <asp:BoundField DataField="NET" HeaderText="Net Amount" SortExpression="NET" />
                </Columns>
            </asp:GridView>

             <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:HOOTHAIConnectionString %>" 
                
                            SelectCommand="SELECT TA001,TA002,TA004,TA015,TA021,TA020 FROM [HOOTHAI].[dbo].[ACPTA] WHERE not EXISTS(select * from [HOOTHAI_REPORT].[dbo].[BillPurLine] where ACPTA.TA001 = BillPurLine.InvoiceH and ACPTA.TA002 = BillPurLine.InvoiceNo)" 
                            ProviderName="<%$ ConnectionStrings:HOOTHAIConnectionString.ProviderName %>">
            </asp:SqlDataSource>
            <asp:TextBox ID="txtbath" runat="server"></asp:TextBox>

                    </ContentTemplate>

                </asp:TabPanel>

                <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Edit Data">
                  <ContentTemplate>


                      <table class="style36">
                          <tr>
                              <td class="style37">
                                  <asp:Label ID="Label20" runat="server" Text="Month :"></asp:Label>
                              </td>
                              <td class="style38">
                                  <uc1:DropDownListUserControl ID="UcDdlMonthEdit" runat="server" />
                              </td>
                              <td class="style39">
                                  <asp:Label ID="Label22" runat="server" Text="Year :"></asp:Label>
                              </td>
                              <td class="style40">
                                  <uc1:DropDownListUserControl ID="UcDdlYearEdit" runat="server" />
                              </td>
                              <td>
                                  &nbsp;</td>
                              <td>
                                  &nbsp;</td>
                              <td>
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td class="style37">
                                  <asp:Label ID="Label21" runat="server" Text="Supplier :"></asp:Label>
                              </td>
                              <td class="style38">
                                  <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" 
                                      DataSourceID="SqlDataSource6" DataTextField="ConcatField" 
                                      DataValueField="MA001">
                                  </asp:DropDownList>
                                  <asp:ListSearchExtender ID="DropDownList5_ListSearchExtender" runat="server" TargetControlID="DropDownList5" BehaviorID="_content_DropDownList5_ListSearchExtender">
                                  </asp:ListSearchExtender>
                              </td>
                              <td class="style39">
                                  <asp:Button ID="BuEsearch" runat="server" Text="Search" />
                              </td>
                              <td class="style40">
                                  <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
                                      ConnectionString="<%$ ConnectionStrings:HOOTHAIConnectionString %>" 
                                      SelectCommand="SELECT MA001,MA001+MA002 AS ConcatField FROM PURMA order by MA001">
                                  </asp:SqlDataSource>
                              </td>
                              <td>
                                  &nbsp;</td>
                              <td>
                                  &nbsp;</td>
                              <td>
                                  &nbsp;</td>
                          </tr>
                      </table>
                      <asp:GridView ID="GridView4" runat="server" AllowPaging="True" 
                          AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource5">
                          <Columns>
                              <asp:BoundField DataField="BillShow" HeaderText="BillNo." 
                                  SortExpression="BillShow" />
                              <asp:BoundField DataField="SupID" HeaderText="Sup ID" SortExpression="SupID" />
                              <asp:BoundField DataField="SupName" HeaderText="Supplier Name" 
                                  SortExpression="SupName" />
                              <asp:ButtonField ButtonType="Image" CommandName="OnClick" HeaderText="Select" 
                                  ImageUrl="~/Images/icon_checkbox_1.gif">
                              <ItemStyle HorizontalAlign="Center" />
                              </asp:ButtonField>
                          </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                          SelectCommand="SELECT [BillShow], [SupID], [SupName] FROM [BillPurHead]" 
                          ProviderName="<%$ ConnectionStrings:DBMISConnectionString.ProviderName %>">
                      </asp:SqlDataSource>
                      <table class="style36">
                          <tr>
                              <td class="style28">
                                  <asp:Label ID="Label23" runat="server" Text="BillNo :"></asp:Label>
                              </td>
                              <td class="style45">
                                  <asp:TextBox ID="TextBox4" runat="server" BorderStyle="Double" ForeColor="Blue"></asp:TextBox>
                              </td>
                              <td class="style39">
                                  &nbsp;</td>
                              <td class="style44">
                                  <asp:Label ID="Label25" runat="server" Text="Supp ID :"></asp:Label>
                              </td>
                              <td class="style46">
                                  <asp:TextBox ID="TextBox7" runat="server" BorderStyle="Double" ForeColor="Blue"></asp:TextBox>
                              </td>
                              <td>
                                  &nbsp;</td>
                              <td>
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td class="style28">
                                  <asp:Label ID="Label24" runat="server" Text="Supplier Name :"></asp:Label>
                              </td>
                              <td class="style41" colspan="2">
                                  <asp:TextBox ID="TextBox5" runat="server" BorderStyle="Double" Width="342px" 
                                      ForeColor="Blue"></asp:TextBox>
                              </td>
                              <td class="style44">
                                  <asp:Label ID="Label28" runat="server" Text="Date :"></asp:Label>
                              </td>
                              <td class="style46">
                                  <asp:TextBox ID="TextBox10" runat="server" BorderStyle="Double" 
                                      ForeColor="Blue"></asp:TextBox>
                              </td>
                              <td>
                                  &nbsp;</td>
                              <td>
                                  &nbsp;</td>
                          </tr>
                          <tr>
                              <td class="style28">
                                  <asp:Button ID="BuEall" runat="server" Text="Select All" />
                              </td>
                              <td class="style45">
                                  <asp:Button ID="BuEcAll" runat="server" Text="Clear All" />
                              </td>
                              <td class="style39">
                                  &nbsp;</td>
                              <td class="style44">
                                  <asp:Button ID="BuESearch2" runat="server" Text="Search" />
                              </td>
                              <td class="style46">
                                  <asp:Button ID="BuESave" runat="server" Text="Save" OnClientClick="return confirm('Are you sure save it')" />
                              </td>
                              <td>
                                  <asp:TextBox ID="txtbathedit" runat="server" BorderStyle="Double" 
                                      Visible="False"></asp:TextBox>
                              </td>
                              <td>
                                  &nbsp;</td>
                          </tr>
                      </table>
                      <br />


                      <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" 
                          DataKeyNames="TA001,TA002" DataSourceID="SqlDataSource7">
                          <Columns>

                          <asp:TemplateField HeaderText="Select">
                          <ItemTemplate>
                          <asp:CheckBox ID="Ck" runat="server" />
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                          </asp:TemplateField>

                              <asp:BoundField DataField="TA001" HeaderText="Order Type" ReadOnly="True" 
                                  SortExpression="TA001" />
                              <asp:BoundField DataField="TA002" HeaderText="Order No." ReadOnly="True" 
                                  SortExpression="TA002" />
                              <asp:BoundField DataField="TA004" HeaderText="Sup ID." SortExpression="TA004" />
                              <asp:BoundField DataField="TA015" HeaderText="Invoice Date" SortExpression="TA015" />
                              <asp:BoundField DataField="TA021" HeaderText="Invoice" SortExpression="TA021" />
                              <asp:BoundField DataField="TA020" HeaderText="Due Date" SortExpression="TA020" />
                                <asp:BoundField DataField="AMT" HeaderText="Amount" SortExpression="AMT" />
                                <asp:BoundField DataField="TAX" HeaderText="Tax" SortExpression="TAX" />
                                <asp:BoundField DataField="NET" HeaderText="Net Amount" SortExpression="NET" />


                          </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" SelectCommand="SELECT TA001,TA002,TA004,TA015,TA021,TA020 FROM [HOOTHAI].[dbo].[ACPTA] WHERE not EXISTS(select * from [HOOTHAI_REPORT].[dbo].[BillPurLine] 
where ACPTA.TA001 = BillPurLine.InvoiceH and ACPTA.TA002 = BillPurLine.InvoiceNo)">
                      </asp:SqlDataSource>
                      <br />


                  </ContentTemplate>
                </asp:TabPanel>

                 <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Delete Data">
                  <ContentTemplate>

                 <table class="style20">
                <tr>
                    <td class="style21">
                        <asp:Label ID="Label13" runat="server" Text="Billing No :"></asp:Label>
                    </td>
                    <td class="style23">
                        <asp:DropDownList ID="DDLBill1" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:ListSearchExtender ID="DDLBill1_ListSearchExtender" runat="server" 
                            Enabled="True" TargetControlID="DDLBill1">
                        </asp:ListSearchExtender>
                    </td>
                    <td class="style27">
                        <asp:Button ID="BuDSearch" runat="server" Text="Search" />
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                     <tr>
                         <td class="style21">
                             <asp:Label ID="Label29" runat="server" Text="Supp ID :"></asp:Label>
                         </td>
                         <td class="style23">
                             <asp:TextBox ID="TextBox11" runat="server" BorderStyle="Double" Enabled="False" 
                                 ForeColor="Blue" ReadOnly="True"></asp:TextBox>
                         </td>
                         <td class="style27">
                             <asp:Label ID="Label30" runat="server" Text="Supplier Name :"></asp:Label>
                         </td>
                         <td>
                             <asp:TextBox ID="TextBox12" runat="server" BorderStyle="Double" Enabled="False" 
                                 Width="347px" ForeColor="Blue" ReadOnly="True"></asp:TextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td class="style21">
                             <asp:Label ID="Label14" runat="server" Text="Type :"></asp:Label>
                         </td>
                         <td class="style23">
                             <asp:TextBox ID="txttype" runat="server" Enabled="False" BorderStyle="Double" 
                                 ForeColor="Blue" ReadOnly="True"></asp:TextBox>
                         </td>
                         <td class="style27">
                             <asp:Label ID="Label15" runat="server" Text="Purchase No :"></asp:Label>
                         </td>
                         <td>
                             <asp:TextBox ID="txtinvoiceno" runat="server" Enabled="False" 
                                 BorderStyle="Double" ForeColor="Blue" ReadOnly="True"></asp:TextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td class="style21">
                             <asp:Label ID="Label16" runat="server" Text="Invoice No :"></asp:Label>
                         </td>
                         <td class="style23">
                             <asp:TextBox ID="txtinvoice" runat="server" Enabled="False" 
                                 BorderStyle="Double" ForeColor="Blue" ReadOnly="True"></asp:TextBox>
                         </td>
                         <td class="style27">
                             <asp:Button ID="BuDeleteAll" runat="server" 
                                 onclientclick="return confirm('Are you sure delete it')" Text="Delete All" />
                         </td>
                         <td>
                             <asp:Button ID="BuDelete" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure delete it')" />
                             <asp:TextBox ID="TextBox3" runat="server" Visible="False"></asp:TextBox>
                         </td>
                         <td>
                             &nbsp;
                         </td>
                     </tr>
            </table>
                      <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                          DataKeyNames="ID" DataSourceID="SqlDataSource2">
                          <Columns>
                              <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                  ReadOnly="True" SortExpression="ID" />
                              <asp:BoundField DataField="InvoiceH" HeaderText="Type." 
                                  SortExpression="InvoiceH" />
                              <asp:BoundField DataField="InvoiceNo" HeaderText="Purchase No" 
                                  SortExpression="InvoiceNo" />
                              <asp:BoundField DataField="RemarkInvoice" HeaderText="Invoice No" 
                                  SortExpression="RemarkInvoice" />
                              <asp:BoundField DataField="OrderDate" HeaderText="OrderDate" 
                                  SortExpression="OrderDate" />
                              <asp:BoundField DataField="DueDate" HeaderText="DueDate" 
                                  SortExpression="DueDate" />
                              <asp:BoundField DataField="Amount" HeaderText="Amount" 
                                  SortExpression="Amount" />
                              <asp:ButtonField CommandName="OnDelete" Text="Select" />
                          </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                          SelectCommand="SELECT * FROM BillPurLine" 
                          ProviderName="<%$ ConnectionStrings:DBMISConnectionString.ProviderName %>"></asp:SqlDataSource>
                      <br />
            </ContentTemplate>
                </asp:TabPanel>

                 <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Print Report">

                     <ContentTemplate>
                         <table class="style20">
                             <tr>
                                 <td class="style29">
                                     <asp:Label ID="Label18" runat="server" Text="Month :"></asp:Label>
                                 </td>
                                 <td class="style30">
                                     <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True">
                                         <asp:ListItem Value="01">Jan</asp:ListItem>
                                         <asp:ListItem Value="02">Feb</asp:ListItem>
                                         <asp:ListItem Value="03">Mar</asp:ListItem>
                                         <asp:ListItem Value="04">Apr</asp:ListItem>
                                         <asp:ListItem Value="05">May</asp:ListItem>
                                         <asp:ListItem Value="06">June</asp:ListItem>
                                         <asp:ListItem Value="07">July</asp:ListItem>
                                         <asp:ListItem Value="08">Aug</asp:ListItem>
                                         <asp:ListItem Value="09">Sep</asp:ListItem>
                                         <asp:ListItem Value="10">Oct</asp:ListItem>
                                         <asp:ListItem Value="11">Nov</asp:ListItem>
                                         <asp:ListItem Value="12">Dec</asp:ListItem>
                                     </asp:DropDownList>
                                 </td>
                                 <td class="style31">
                                     <asp:Label ID="Label19" runat="server" Text="Year :"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True">
                                         <asp:ListItem>2012</asp:ListItem>
                                         <asp:ListItem Selected="True">2013</asp:ListItem>
                                         <asp:ListItem>2014</asp:ListItem>
                                         <asp:ListItem>2015</asp:ListItem>
                                     </asp:DropDownList>
                                 </td>
                                 <td class="style35">
                                     <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:HOOTHAIConnectionString %>" 
                                         SelectCommand="SELECT MA001,MA001+MA002 AS ConcatField FROM PURMA order by MA001">
                                     </asp:SqlDataSource>
                                 </td>
                                 <td class="style35">
                                     &nbsp;</td>
                                 <td class="style35">
                                     &nbsp;</td>
                                 <td class="style35">
                                     &nbsp;</td>
                             </tr>
                             <tr>
                                 <td class="style29">
                                     <asp:Label ID="Label17" runat="server" Text="Supplier :"></asp:Label>
                                 </td>
                                 <td class="style30">
                                     <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                                         DataSourceID="SqlDataSource3" DataTextField="ConcatField" 
                                         DataValueField="MA001">
                                     </asp:DropDownList>
                                     <asp:ListSearchExtender ID="DropDownList1_ListSearchExtender" runat="server" 
                                         Enabled="True" TargetControlID="DropDownList1">
                                     </asp:ListSearchExtender>
                                 </td>
                                 <td class="style31">
                                     &nbsp;</td>
                                 <td>
                                     <asp:Button ID="Bureport" runat="server" Text="Search" />
                                 </td>
                                 <td class="style35">
                                     <asp:Button ID="BuReportMonth" runat="server" Text="Report per Month" />
                                 </td>
                                 <td class="style35">
                                     &nbsp;</td>
                                 <td class="style35">
                                     &nbsp;</td>
                                 <td class="style35">
                                     &nbsp;</td>
                             </tr>
                             <tr>
                                 <td class="style29">
                                     &nbsp;</td>
                                 <td class="style30">
                                     &nbsp;</td>
                                 <td class="style31">
                                     &nbsp;</td>
                                 <td>
                                     <asp:DropDownList ID="DDlBill2" runat="server" AutoPostBack="True" 
                                         Visible="False">
                                     </asp:DropDownList>
                                 </td>
                                 <td class="style35">
                                     <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>
                                 </td>
                                 <td class="style35">
                                     &nbsp;</td>
                                 <td class="style35">
                                     &nbsp;</td>
                                 <td class="style35">
                                     &nbsp;</td>
                             </tr>
                         </table>
                         <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
                             AllowSorting="True" AutoGenerateColumns="False" 
                             DataSourceID="SqlDataSource4" PageSize="30">
                             <Columns>
                                 <asp:BoundField DataField="BillShow" HeaderText="BillNo" 
                                     SortExpression="BillShow" />
                                 <asp:BoundField DataField="SupID" HeaderText="SupID" SortExpression="SupID" />
                                 <asp:BoundField DataField="SupName" HeaderText="SupName" 
                                     SortExpression="SupName" />
                                 <asp:ButtonField ButtonType="Image" CommandName="OnClick" HeaderText="Print" 
                                     ImageUrl="~/Images/icon_print.png">
                                 <ItemStyle HorizontalAlign="Center" />
                                 </asp:ButtonField>
                             </Columns>
                         </asp:GridView>
                         <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                             ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                             
                             SelectCommand="SELECT [BillShow], [SupID], [SupName] FROM [BillPurHead] order by BillShow desc" 
                             ProviderName="<%$ ConnectionStrings:DBMISConnectionString.ProviderName %>">
                         </asp:SqlDataSource>
                         <br />
                     </ContentTemplate>

                </asp:TabPanel>

                  <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="History">
                     <ContentTemplate>
                          <table class="style20">
                             <tr>
                                 <td class="style43">
                                     <asp:Label ID="Label33" runat="server" Text="Search By :"></asp:Label>
                                 </td>
                                 <td class="style48">
                                     <asp:DropDownList ID="DDLHisSearch" runat="server" AutoPostBack="True">
                                         <asp:ListItem>Invoice No.</asp:ListItem>
                                         <asp:ListItem>Supp ID.</asp:ListItem>
                                         <asp:ListItem>Bill No.</asp:ListItem>
                                     </asp:DropDownList>
                                 </td>
                                 <td class="style49">
                                     <asp:TextBox ID="txthissearch" runat="server"></asp:TextBox>
                                 </td>
                                 <td class="style50">
                                     <asp:Button ID="BuHisSearch" runat="server" Text="Search" />
                                 </td>
                                 <td class="style51">
                                     &nbsp;</td>
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
                                 <td>
                                     &nbsp;</td>
                                 <td>
                                     &nbsp;</td>
                                 <td>
                                     &nbsp;</td>
                             </tr>
                         </table>
                         <asp:GridView ID="GridHistory" runat="server" AllowPaging="True" 
                             AllowSorting="True" AutoGenerateColumns="False" 
                             DataSourceID="DataSourceHistory" PageSize="90">
                             <Columns>
                                 <asp:BoundField DataField="BillShow" HeaderText="Bill No." 
                                     SortExpression="BillShow" />
                                 <asp:BoundField DataField="SupID" HeaderText="Sup ID" SortExpression="SupID" />
                                 <asp:BoundField DataField="SupName" HeaderText="Sup Name" 
                                     SortExpression="SupName" />
                                 <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                 <asp:ButtonField ButtonType="Image" HeaderText="Print" 
                                     ImageUrl="~/Images/icon_print.png" CommandName="OnClick">
                                 <ItemStyle HorizontalAlign="Center" />
                                 </asp:ButtonField>
                             </Columns>
                         </asp:GridView>
                         <asp:SqlDataSource ID="DataSourceHistory" runat="server" 
                             ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                             
                             SelectCommand="select DISTINCT H.BillShow,H.SupID,H.SupName,H.Date FROM [HOOTHAI_REPORT].[dbo].[BillPurHead] H left join [HOOTHAI_REPORT].[dbo].[BillPurLine] L on (H.BillNo = L.BillNo)" 
                             ProviderName="<%$ ConnectionStrings:DBMISConnectionString.ProviderName %>">
                         </asp:SqlDataSource>
                         <br />
                         <br />



                       </ContentTemplate>
                   </asp:TabPanel>

            </asp:TabContainer>

        </ContentTemplate>
        <Triggers>
      

        </Triggers>
    </asp:UpdatePanel>
  
</asp:Content>
