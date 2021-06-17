<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ControlDo.aspx.vb" Inherits="MIS_HTI.ControlDo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 113%;
        }
        .style7
        {
            width: 80px;
        }
        .style9
        {
            width: 68px;
        }
        .style10
        {
            width: 133px;
        }
        .style11
        {
            width: 70px;
        }
        .style12
        {
            width: 145px;
        }
        .style15
        {
        }
        .style16
        {
            width: 158px;
        }
        .style17
        {
            width: 122px;
        }
        .style19
        {
            width: 143px;
        }
        .style20
        {
            width: 1px;
        }
        .style21
        {
            width: 131px;
        }
        .style22
        {
            width: 46px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="style6">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Blue" 
                            Text="Control Document Invoice &amp; DO" Font-Size="1.1em"></asp:Label>
                    </td>
                </tr>
            </table>
          


                <table class="style6" bgcolor="White">
                    <tr>
                        <td class="style7">
                            <asp:Label ID="Label3" runat="server" Text="From Date :"></asp:Label>
                        </td>
                        <td class="style12">
                            <asp:TextBox ID="txtfrom" runat="server" BorderStyle="Solid"></asp:TextBox>
                            <asp:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" 
                                Enabled="True" Format="yyyyMMdd" TargetControlID="txtfrom">
                            </asp:CalendarExtender>
                        </td>
                        <td class="style9">
                            <asp:Label ID="Label4" runat="server" Text="To Date :"></asp:Label>
                        </td>
                        <td class="style10">
                            <asp:TextBox ID="txtto" runat="server" BorderStyle="Solid"></asp:TextBox>
                            <asp:CalendarExtender ID="txtto_CalendarExtender" runat="server" Enabled="True" 
                                Format="yyyyMMdd" TargetControlID="txtto">
                            </asp:CalendarExtender>
                        </td>
                        <td class="style11">
                            <asp:Label ID="Label5" runat="server" Text="Customer :"></asp:Label>
                        </td>
                        <td class="style21">
                            <asp:TextBox ID="txtcust" runat="server" BorderStyle="Solid"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtcust_AutoCompleteExtender" runat="server" 
                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" 
                                ServiceMethod="Get_CID" ServicePath="CID.asmx" TargetControlID="txtcust">
                            </asp:AutoCompleteExtender>
                        </td>
                        <td class="style22">
                            <asp:Label ID="Label17" runat="server" Text="Status :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DDLStatus" runat="server" AutoPostBack="True">
                                <asp:ListItem>All</asp:ListItem>
                                <asp:ListItem>Open</asp:ListItem>
                                <asp:ListItem>Close</asp:ListItem>
                            </asp:DropDownList>
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
                    <tr>
                        <td class="style7">
                            <asp:Label ID="Label15" runat="server" Text="Type :"></asp:Label>
                        </td>
                        <td class="style12">
                            <asp:TextBox ID="txttype" runat="server" BorderStyle="Solid"></asp:TextBox>

                        </td>
                        <td class="style9">
                            <asp:Label ID="Label16" runat="server" Text="No :"></asp:Label>
                        </td>
                        <td class="style10">
                            <asp:TextBox ID="txtno" runat="server" BorderStyle="Solid"></asp:TextBox>
                        </td>
                        <td class="style11">
                            <asp:Button ID="BUSearch" runat="server" Text="Search" />
                        </td>
                        <td class="style21">
                            <asp:Button ID="BuReport" runat="server" Text="Report" />
                        </td>
                        <td class="style22">
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
          
            <table class="style6" bgcolor="White">
                <tr>
                    <td class="style17" bgcolor="White">
                        <asp:Label ID="Label12" runat="server" Text="Customer Code :"></asp:Label>
                    </td>
                    <td class="style19" bgcolor="White">
                        <asp:Label ID="CID" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style16" bgcolor="White">
                        <asp:Label ID="Label13" runat="server" Text="Customer Name :"></asp:Label>
                    </td>
                    <td class="style20" bgcolor="White">
                        <asp:Label ID="CName" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="TA038" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtdate" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style17" bgcolor="White">
                        <asp:Label ID="Label10" runat="server" Text="Invoice Type :"></asp:Label>
                    </td>
                    <td class="style19" bgcolor="White">
                        <asp:Label ID="Type" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style16" bgcolor="White">
                        <asp:Label ID="Label11" runat="server" Text="Invoice No : "></asp:Label>
                    </td>
                    <td class="style20" bgcolor="White">
                        <asp:Label ID="No" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="BillNo" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style17" bgcolor="White">
                        <asp:Label ID="Label6" runat="server" Text="Store Send Invoice :"></asp:Label>
                    </td>
                    <td class="style19" bgcolor="White">
                        <asp:DropDownList ID="DDLInvoice" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True">No</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="DateInvoice" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style16" bgcolor="White">
                        <asp:Label ID="Label8" runat="server" Text="Store Send DO :"></asp:Label>
                    </td>
                    <td class="style20" bgcolor="White">
                        <asp:DropDownList ID="DDLDO" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True">No</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="DateDo" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:Button ID="BuSave" runat="server" 
                            onclientclick="return confirm('are you sure save it');" Text="Save" />
                        <asp:Button ID="BuCancel" runat="server" Text="Cancel" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style17" bgcolor="White">
                        <asp:Label ID="Label14" runat="server" Text="Remark :"></asp:Label>
                    </td>
                    <td class="style15" colspan="4" bgcolor="White">
                        <asp:TextBox ID="txtremark" runat="server" BorderStyle="Solid" Width="521px"></asp:TextBox>
                    </td>
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
                DataSourceID="SqlDataSource1" PageSize="20" CellPadding="4" 
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
                   
                    <asp:BoundField DataField="BillShow" HeaderText="Bill No" ReadOnly="True" 
                        SortExpression="BillShow" />
                    <asp:BoundField DataField="StoreInvoice" HeaderText="Store Send Invoice" 
                        SortExpression="StoreInvoice" />
                    <asp:BoundField DataField="StoreInDate" HeaderText="Store Send Invoice Date" 
                        SortExpression="StoreInDate" />
                    <asp:BoundField DataField="StoreDO" HeaderText="Store Send DO" 
                        SortExpression="StoreDO" />
                    <asp:BoundField DataField="StoreDoDate" HeaderText="Store Send Do Date" 
                        SortExpression="StoreDoDate" />
                      

                        <asp:BoundField DataField="Status" HeaderText="Status" 
                        SortExpression="Status" />

                    <asp:BoundField DataField="StoreBy" HeaderText="Store By" 
                        SortExpression="StoreBy" />
                    <asp:BoundField DataField="RemarkStore" HeaderText="Remark" 
                        SortExpression="RemarkStore" />
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
                SelectCommand=" select A.TA004,C.MA002,A.TA038,A.TA001,A.TA002,A.TA020,A.TA042,A.TA041,A.TA098,H.BillShow,[StoreInvoice],[StoreInDate],[StoreDO],[StoreDoDate],[Status],[StoreBy],[RemarkStore]
  from [JINPAO80].[dbo].[ACRTA] A left join [DBMIS].[dbo].[BillLine] L 
  on (A.TA001 = L.InvoiceH) and (A.TA002 = L.InvoiceNo)
  left join [DBMIS].[dbo].[Billhead] H on (H.BillNo = L.BillNo)
  left join [DBMIS].[dbo].[ControlStore] St on (A.TA001 = St.TA001) and (A.TA002 = St.TA002) left join [JINPAO80].[dbo].[COPMA] C on (A.TA004 = C.MA001) where A.TA001 not like '%EX%'"></asp:SqlDataSource>
            <br />
        
          
        </ContentTemplate>
    </asp:UpdatePanel>
  
</asp:Content>
