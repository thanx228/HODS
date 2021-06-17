<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="BarcodeDelta.aspx.vb" Inherits="MIS_HTI.BarcodeDelta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        .style5
        {
            width: 75px;
        }
        .style6
        {
            width: 84px;
        }
        .style7
        {
            width: 139px;
        }
        .style8
        {
            width: 2px;
        }
        .style9
        {
            width: 69px;
        }
        .style10
        {
            width: 81px;
        }
        .style11
        {
            width: 130px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    <asp:ScriptManager ID="ScriptManager1" runat="server">
              </asp:ScriptManager>
            
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                  <ContentTemplate>

              <table class="style4">
                <tr>
                    <td colspan="18" align="center" bgcolor="Maroon">
                        <asp:Label ID="Label1" runat="server" BorderStyle="Solid" Font-Size="X-Large" 
                            ForeColor="White" Text="Label Barcode DELTA"></asp:Label>
             
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label2" runat="server" Text="Search By :"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:DropDownList ID="DDLSearch" runat="server" AutoPostBack="True">
                            <asp:ListItem>Invoice No</asp:ListItem>
                            <asp:ListItem>Po No</asp:ListItem>
                            <asp:ListItem>Desc</asp:ListItem>
                            <asp:ListItem>Spec</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style7">
                        <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
                    </td>
                    <td class="style8">
                        <asp:Button ID="Busearch" runat="server" Text="Search" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:TextBox ID="TypeNo" runat="server" Visible="False"></asp:TextBox>
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
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label3" runat="server" Text="Invoice Type :"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:Label ID="Ltype" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Label7" runat="server" Text="Item :"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Litem" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="Full Box :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Lfbox" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtpo" runat="server" Visible="False"></asp:TextBox>
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
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label4" runat="server" Text="Invoice No :"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:Label ID="Lno" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Label5" runat="server" Text="P/N :"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Lpn" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server" Text="Last Box :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Llbox" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtdate" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtsnoshow" runat="server" Visible="False"></asp:TextBox>
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
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label13" runat="server" Text="Vender :"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:Label ID="Lvender" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Label6" runat="server" Text="QTY :"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Lqty" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label19" runat="server" Text="Last Qty :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Llqty" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="SNo" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label21" runat="server" Text="Sno" Visible="False"></asp:Label>
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
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label15" runat="server" Text="PLANT :"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:TextBox ID="txtplant" runat="server" Width="84px"></asp:TextBox>
                    </td>
                    <td class="style7">
                        &nbsp;</td>
                    <td class="style8">
                        <asp:Button ID="Busave" runat="server" Text="Save" />
                    </td>
                    <td>
                        <asp:Label ID="Label20" runat="server" Text="Qty/Box :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQBox" runat="server" Width="63px"  AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="RNo" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label22" runat="server" Text="Rno" Visible="False"></asp:Label>
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
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                    <td class="style8">
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










          

            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="TA001,TA002" 
                DataSourceID="DataSourceInvoice">
                <Columns>
                    <asp:ButtonField CommandName="OnClick" Text="Select" />
                    <asp:BoundField DataField="TA001" HeaderText="Type" ReadOnly="True" 
                        SortExpression="TA001" />
                    <asp:BoundField DataField="TA002" HeaderText="No." ReadOnly="True" 
                        SortExpression="TA002" />
                    <asp:BoundField DataField="TA004" HeaderText="Cust ID" SortExpression="TA004" />
                    <asp:BoundField DataField="TA008" HeaderText="Cust Name" SortExpression="TA008" />
                    <asp:BoundField DataField="TB039" HeaderText="Item" SortExpression="TB039" />
                    <asp:BoundField DataField="TB040" HeaderText="Desc" SortExpression="TB040" />
                    <asp:BoundField DataField="TB041" HeaderText="Spec" SortExpression="TB041" />
                    <asp:BoundField DataField="TB048" HeaderText="Po No" SortExpression="TB048" />
                    <asp:BoundField DataField="TB022" HeaderText="Qty" SortExpression="TB022" DataFormatString = {0:F3}/>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="DataSourceInvoice" runat="server" 
                ConnectionString="<%$ ConnectionStrings:JINPAO80ConnectionString %>" SelectCommand="select TA001,TA002,TA004,TA008,TB039,TB040,TB041,TB048,TB022 from ACRTA H left join ACRTB L on (H.TA001 = L.TB001) 
and (H.TA002 = L.TB002) where TA004 = 'D003' or TA004 = 'D005' or TA004 = 'D006' or TA004 = 'D008' or TA004 = 'D010' 
or TA004 = 'D013' or  TA004 = 'D017' or TA004 = 'D018' or TA004 = 'D021'">
            </asp:SqlDataSource>
            

 <table class="style4">
                  <tr>
                      <td class="style9">
                          <asp:Label ID="Label16" runat="server" Text="Search By :"></asp:Label>
                      </td>
                      <td class="style10">
                          <asp:DropDownList ID="DDLDel" runat="server" AutoPostBack="True">
                              <asp:ListItem>Po.</asp:ListItem>
                              <asp:ListItem>Desc.</asp:ListItem>
                              <asp:ListItem>Invoice No</asp:ListItem>
                              <asp:ListItem>Serial No.</asp:ListItem>
                          </asp:DropDownList>
                      </td>
                      <td class="style11">
                          <asp:TextBox ID="txtdel" runat="server"></asp:TextBox>
                      </td>
                      <td>
                          <asp:Button ID="SearchDel" runat="server" Text="Search" />
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
              <asp:GridView ID="GridDELTA" runat="server" AllowPaging="True" 
                  AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Type,No" 
                  DataSourceID="DataSourceDELTA">
                  <Columns>
                    
                      <asp:BoundField DataField="No" HeaderText="No" ReadOnly="True" 
                          SortExpression="No" />
                      <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item" />
                      <asp:BoundField DataField="Desc" HeaderText="Desc" SortExpression="Desc" />
                      <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />
                      <asp:BoundField DataField="Unit" HeaderText="Unit" SortExpression="Unit" />
                      <asp:BoundField DataField="Po" HeaderText="Po" SortExpression="Po" />
                      <asp:BoundField DataField="SNoShow" HeaderText="Serial No." 
                          SortExpression="SNoShow" />

                      <asp:ButtonField ButtonType="Image" CommandName="OnPrint" HeaderText="Print" 
                          ImageUrl="~/Images/icon_print.png" Text="Button">
                      <ItemStyle HorizontalAlign="Center" />
                      </asp:ButtonField>

                  </Columns>
              </asp:GridView>
              <asp:SqlDataSource ID="DataSourceDELTA" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                  SelectCommand="SELECT * FROM [LabelDELTA]"></asp:SqlDataSource>
                  </ContentTemplate>
              </asp:UpdatePanel>
              <br />
       
</asp:Content>
