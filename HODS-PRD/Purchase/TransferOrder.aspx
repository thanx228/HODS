<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="TransferOrder.aspx.vb" Inherits="MIS_HTI.TransferOrder" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            width: 159%;
        }
        .style9
        {
            width: 128px;
        }
        .style11
        {
            width: 27px;
        }
        .style12
        {
            width: 125px;
        }
        .style13
        {
            width: 39px;
        }
        .style14
        {
            width: 48px;
        }
        .style15
        {
            width: 100%;
        }
        .style17
        {
            width: 33px;
        }
        .style19
        {
            width: 141px;
        }
        .style20
        {
            width: 1985px;
        }
        .style21
        {
            width: 58px;
        }
        .style23
        {
            width: 83px;
        }
        .style25
        {
            width: 60px;
        }
        .style27
        {
            width: 76px;
        }
        .style28
        {
            width: 117px;
        }
        .style29
        {
            width: 136px;
        }
        .style31
        {
            width: 86px;
        }
        .style33
        {
            width: 88px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

       <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Large" 
                            ForeColor="#0000CC" Text="Transfer Outsource To Po"></asp:Label>

           <asp:TabContainer ID="TabContainer1" runat="server" Height="100%" Width="100%" 
                ActiveTabIndex="3" >
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Add Data">

                    <HeaderTemplate>
                        Add Data
                    </HeaderTemplate>
                    <ContentTemplate>

                      <table class="style3">
                <tr>
                    <td class="style14">
                        <asp:Label ID="Label12" runat="server" Text="From Date :"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:TextBox ID="txtfrom" runat="server" BorderStyle="Solid"></asp:TextBox>
                        <asp:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyyMMdd" TargetControlID="txtfrom">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style11">
                        <asp:Label ID="Label15" runat="server" Text="To Date :"></asp:Label>
                    </td>
                    <td class="style12">
                        <asp:TextBox ID="txtto" runat="server" BorderStyle="Solid"></asp:TextBox>
                        <asp:CalendarExtender ID="txtto_CalendarExtender" runat="server" Enabled="True" 
                            Format="yyyyMMdd" TargetControlID="txtto">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style13">
                        <asp:TextBox ID="TextBox3" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style14">
                        <asp:Label ID="Label13" runat="server" Text="OSNo :"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:TextBox ID="txttid" runat="server" BorderStyle="Solid"></asp:TextBox>
                    </td>
                    <td class="style11">
                        <asp:Label ID="Label16" runat="server" Text="Date :"></asp:Label>
                    </td>
                    <td class="style12">
                        <asp:TextBox ID="txtdate" runat="server" BorderStyle="Solid"></asp:TextBox>
                    </td>
                    <td class="style13">
                        <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                          <tr>
                              <td class="style14">
                                  <asp:Label ID="Label17" runat="server" Text="Remark :"></asp:Label>
                              </td>
                              <td class="style9">
                                  <asp:TextBox ID="txtremark" runat="server" BorderStyle="Solid"></asp:TextBox>
                              </td>
                              <td class="style11">
                                  &nbsp;</td>
                              <td class="style12">
                                  &nbsp;</td>
                              <td class="style13">
                                  &nbsp;</td>
                              <td>
                                  &nbsp;</td>
                          </tr>
                <tr>
                    <td class="style14">
                        <asp:Label ID="Label14" runat="server" Text="Type :"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:DropDownList ID="Dtype1" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td class="style11">
                        <asp:DropDownList ID="DType2" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td class="style12">
                        <asp:DropDownList ID="DType3" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td class="style13">
                        <asp:Button ID="Busearch" runat="server" Text="Search" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style14">
                        <asp:Button ID="Buaddall" runat="server" Text="Select All" />
                    </td>
                    <td class="style9">
                        <asp:Button ID="Buclearall" runat="server" Text="Clear All" />
                    </td>
                    <td class="style11">
                        <asp:Button ID="Busave" runat="server" 
                            OnClientClick="return confirm('are you sure save it');" Text="Save" />
                    </td>
                    <td class="style12">
                        <asp:Button ID="Buclear" runat="server" Text="Clear" />
                    </td>
                    <td class="style13">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>

                     <asp:GridView ID="GridViewAdd" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="TB001,TB002" DataSourceID="SqlDataSource1" PageSize="30" 
                AllowPaging="True">
                <Columns>

                     <asp:TemplateField HeaderText="Select" >
                <ItemTemplate>
                <asp:CheckBox ID="Ck" runat="server" />
                </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>

                    <asp:BoundField DataField="TB001" HeaderText="Type" ReadOnly="True" 
                        SortExpression="TB001" />
                    <asp:BoundField DataField="TB002" HeaderText="No" ReadOnly="True" 
                        SortExpression="TB002" />
                    <asp:BoundField DataField="TB003" HeaderText="Date" SortExpression="TB003" />
                    <asp:BoundField DataField="TC018" HeaderText="Amount" SortExpression="TC018" />

                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                SelectCommand="select TB001,TB002,TB003,TC018 from [JINPAO80].[dbo].[SFCTB] H JOIN [JINPAO80].[dbo].[SFCTC] L ON(H.TB001 = L.TC001) AND (H.TB002 = L.TC002)
WHERE not EXISTS(select * from [DBMIS].[dbo].[OutSourceL] 
where H.TB001  = OutSourceL.Ttype  and H.TB002 = OutSourceL.Tno )">
            </asp:SqlDataSource>

                        <br />
                        <br />

                      </ContentTemplate>
                </asp:TabPanel>

                 <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Delete Data">
                      <ContentTemplate>


                          <table class="style15">
                              <tr>
                                  <td class="style17">
                                      <asp:Label ID="Label18" runat="server" Text="OSNo :"></asp:Label>
                                  </td>
                                  <td class="style19">
                                      <asp:DropDownList ID="DDLDelete" runat="server" AutoPostBack="True">
                                      </asp:DropDownList>
                                      <asp:ListSearchExtender ID="DDLDelete_ListSearchExtender" runat="server" 
                                          Enabled="True" TargetControlID="DDLDelete">
                                      </asp:ListSearchExtender>
                                  </td>
                                  <td class="style20">
                                      <asp:Button ID="Budsearch" runat="server" Text="Search" />
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
                                  <td class="style17">
                                      <asp:Button ID="Budall" runat="server" Text="Select All" />
                                  </td>
                                  <td class="style19">
                                      <asp:Button ID="Bucall" runat="server" Text="Clear All" />
                                  </td>
                                  <td class="style20">
                                      <asp:Button ID="Buddelete" runat="server" Text="Delete" 
                                          onclientclick="return confirm('are you sure delete it');" />
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
                          </table>
                          <asp:GridView ID="Griddelete" runat="server" AutoGenerateColumns="False" 
                              DataKeyNames="ID" DataSourceID="SqlDataSource2">
                              <Columns>
                                
                                   <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Ck" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" />
                                  <asp:BoundField DataField="Ttype" HeaderText="Ttype" SortExpression="Ttype" />
                                  <asp:BoundField DataField="Tno" HeaderText="Tno" SortExpression="Tno" />
                                  <asp:BoundField DataField="DateL" HeaderText="Date" SortExpression="DateL" />
                                  <asp:BoundField DataField="Amount" HeaderText="Amount" 
                                      SortExpression="Amount" />
                                 
                              </Columns>
                          </asp:GridView>
                          <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                              SelectCommand="SELECT * FROM [OutSourceL]"></asp:SqlDataSource>
                          <br />
                          <br />
                          <br />
                          <br />


                         </ContentTemplate>
                </asp:TabPanel>


                  <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Edit Data">
                  
                      <HeaderTemplate>
                          Edit Data
                      </HeaderTemplate>
                  
                      <ContentTemplate>
                          <table class="style15">
                              <tr>
                                  <td class="style21">
                                      <asp:Label ID="Label19" runat="server" Text="OSNo :"></asp:Label>
                                  </td>
                                  <td class="style23">
                                      <asp:DropDownList ID="DDLTID" runat="server" AutoPostBack="True" 
                                          DataSourceID="SqlDataSource3" DataTextField="OSNo" DataValueField="OSNo" 
                                          style="margin-left: 0px">
                                      </asp:DropDownList>
                                  </td>
                                  <td>
                                      <asp:Button ID="Buesearch" runat="server" Text="Search" />
                                      <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                          ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                                          SelectCommand="SELECT [OSNo] FROM [OutSourceH]"></asp:SqlDataSource>
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
                              </tr>
                          </table>
                          <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
                              AutoGenerateColumns="False" DataKeyNames="OSNo" 
                              DataSourceID="SqlDataSource4">
                              <Columns>
                                  <asp:BoundField DataField="OSNo" HeaderText="OSNo" ReadOnly="True" 
                                      SortExpression="OSNo" />
                                  <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                  <asp:BoundField DataField="Type1" HeaderText="Type1" SortExpression="Type1" />
                                  <asp:BoundField DataField="Type2" HeaderText="Type2" SortExpression="Type2" />
                                  <asp:BoundField DataField="Type3" HeaderText="Type3" SortExpression="Type3" />
                                  <asp:ButtonField ButtonType="Image" CommandName="OnClick" 
                                      ImageUrl="~/Images/imagesview.jpg" HeaderText="Click" >
                                  <ItemStyle HorizontalAlign="Center" />
                                  </asp:ButtonField>
                              </Columns>
                          </asp:GridView>
                          <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                              SelectCommand="SELECT [OSNo], [Date], [Type1], [Type2], [Type3] FROM [OutSourceH]">
                          </asp:SqlDataSource>
                          <table class="style15">
                              <tr>
                                  <td class="style33">
                                      <asp:Label ID="Label20" runat="server" Text="OSNo :"></asp:Label>
                                  </td>
                                  <td class="style25">
                                      <asp:Label ID="lbltid" runat="server" ForeColor="Blue"></asp:Label>
                                  </td>
                                  <td class="style31">
                                      <asp:Label ID="Label21" runat="server" Text="Date :"></asp:Label>
                                  </td>
                                  <td>
                                      <asp:Label ID="lbldate" runat="server" ForeColor="Blue"></asp:Label>
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
                              </tr>
                              <tr>
                                  <td class="style33">
                                      <asp:Label ID="Label24" runat="server" Text="Type :"></asp:Label>
                                  </td>
                                  <td class="style25">
                                      <asp:Label ID="lbltype1" runat="server" ForeColor="Blue"></asp:Label>
                                  </td>
                                  <td class="style31">
                                      <asp:Label ID="lbltype2" runat="server" ForeColor="Blue"></asp:Label>
                                  </td>
                                  <td>
                                      <asp:Label ID="lbltype3" runat="server" ForeColor="Blue"></asp:Label>
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
                              </tr>
                              <tr>
                                  <td class="style33">
                                      <asp:Label ID="Label22" runat="server" Text="From Date :"></asp:Label>
                                  </td>
                                  <td class="style25">
                                      <asp:TextBox ID="txtefrom" runat="server" BorderStyle="Solid"></asp:TextBox>
                                      <asp:CalendarExtender ID="txtefrom_CalendarExtender" runat="server" Format="yyyyMMdd"
                                          Enabled="True" TargetControlID="txtefrom">
                                      </asp:CalendarExtender>
                                  </td>
                                  <td class="style31">
                                      <asp:Label ID="Label23" runat="server" Text="To Date :"></asp:Label>
                                  </td>
                                  <td>
                                      <asp:TextBox ID="txteto" runat="server" BorderStyle="Solid"></asp:TextBox>
                                      <asp:CalendarExtender ID="txteto_CalendarExtender" runat="server" Format="yyyyMMdd" 
                                          Enabled="True" TargetControlID="txteto">
                                      </asp:CalendarExtender>
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
                              </tr>
                              <tr>
                                  <td class="style33">
                                      <asp:Button ID="Bueselectall" runat="server" Text="Select All" />
                                  </td>
                                  <td class="style25">
                                      <asp:Button ID="Bueclearall" runat="server" Text="Clear All" />
                                  </td>
                                  <td class="style31">
                                      <asp:Button ID="Buesave" runat="server" Text="Save" 
                                          OnClientClick="return confirm('Are you sure Edit it')" />
                                  </td>
                                  <td>
                                      <asp:Button ID="BuESearch2" runat="server" Text="Search" />
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
                              </tr>
                          </table>
                          <asp:GridView ID="Gridedit" runat="server" AutoGenerateColumns="False" DataKeyNames="TB001,TB002" 
                              DataSourceID="SqlDataSource5">
                              <Columns>

                              <asp:TemplateField HeaderText="Select" >
                <ItemTemplate>
                <asp:CheckBox ID="Ck" runat="server" />
                </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>

                                  <asp:BoundField DataField="TB001" HeaderText="Type" ReadOnly="True" 
                                      SortExpression="TB001" />
                                  <asp:BoundField DataField="TB002" HeaderText="No." ReadOnly="True" 
                                      SortExpression="TB002" />
                                  <asp:BoundField DataField="TB003" HeaderText="Date" SortExpression="TB003" />
                                  <asp:BoundField DataField="TC018" HeaderText="Amount" SortExpression="TC018" />
                              </Columns>
                          </asp:GridView>
                          <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" 
                              SelectCommand="select TB001,TB002,TB003,TC018 from [JINPAO80].[dbo].[SFCTB] H JOIN [JINPAO80].[dbo].[SFCTC] L ON(H.TB001 = L.TC001) AND (H.TB002 = L.TC002) WHERE not EXISTS(select * from [DBMIS].[dbo].[OutSourceL] where H.TB001  = OutSourceL.Ttype  and H.TB002 = OutSourceL.Tno )">
                          </asp:SqlDataSource>
                          <br />
                          <br />
                          <br />
                          <br />
                        </ContentTemplate>
                  
                </asp:TabPanel>

                 <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Print Report">
                      <ContentTemplate>


                          <table class="style15">
                              <tr>
                                  <td class="style27">
                                      <asp:Label ID="Label25" runat="server" Text="OSNo :"></asp:Label>
                                  </td>
                                  <td class="style28">
                                      <asp:DropDownList ID="DDLRosno" runat="server" AutoPostBack="True">
                                      </asp:DropDownList>
                                  </td>
                                  <td class="style29">
                                      <asp:Button ID="Bursearch" runat="server" Text="Search" />
                                  </td>
                                  <td>
                                      <asp:TextBox ID="txtOSNo" runat="server" Visible="False"></asp:TextBox>
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
                              </tr>
                          </table>


                          <asp:GridView ID="Gridreport" runat="server" AllowPaging="True" 
                              AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="OSNo" 
                              DataSourceID="DataSourceReport" PageSize="20">
                              <Columns>
                                  <asp:BoundField DataField="OSNo" HeaderText="OSNo" ReadOnly="True" 
                                      SortExpression="OSNo" />
                                 
                                  <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                
                                  <asp:BoundField DataField="Remark" HeaderText="Remark" 
                                      SortExpression="Remark" />
                                
                                  <asp:ButtonField ButtonType="Image" CommandName="OnPrint" HeaderText="Print" 
                                      ImageUrl="~/Images/icon_print.png">
                                  <ItemStyle HorizontalAlign="Center" />
                                  </asp:ButtonField>
                                
                              </Columns>
                          </asp:GridView>
                          <asp:SqlDataSource ID="DataSourceReport" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                              SelectCommand="SELECT * FROM [OutSourceH]"></asp:SqlDataSource>
                          <br />
                          <br />
                          <br />
                          <br />

                         </ContentTemplate>
                </asp:TabPanel>
                 </asp:TabContainer>

<br />

        </ContentTemplate>
    </asp:UpdatePanel>
   
        <br />
  
</asp:Content>
