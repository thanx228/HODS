<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS_SUB.Master" CodeBehind="CustPORecordPopup.aspx.vb" Inherits="MIS_HTI.CustPORecordPopup" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc3" %>
<%@ Register src="../UserControl/DropDownListUserControl.ascx" tagname="DropDownListUserControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 95%;
        }
        .auto-style2 {
            height: 24px;
        }
     
        </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
   <%--     function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 500,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize: 8
            });
        }--%>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <ajaxToolkit:TabContainer ID="TcMain" runat="server" ActiveTabIndex="0" Width="95%">
                <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        Transaction
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table bgcolor="White" style="width: 95%;">
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Cust PO"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LbCustPO" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="Doc Date"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LbDocDate" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Cust Code"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LbCustCode" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text="Cust Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LbCustName" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Plant"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LbPlant" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label13" runat="server" Text="Status"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LbStatus" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="Label22" runat="server" Text="Remark"></asp:Label>
                                </td>
                                <td class="auto-style2" colspan="3">
                                    <asp:Label ID="LbRemarkHead" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label24" runat="server" Text="Spec"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TbSpec" runat="server" AutoPostBack="True"></asp:TextBox>
                                    <ajaxToolkit:AutoCompleteExtender ID="TbSpec_AutoCompleteExtender" runat="server" BehaviorID="_content_TbSpec_AutoCompleteExtender" CompletionInterval="10" CompletionSetCount="12" DelimiterCharacters="" ServiceMethod="SearchSpec" ServicePath="~/Service/ServiceItem.asmx" TargetControlID="TbSpec">
                                    </ajaxToolkit:AutoCompleteExtender>
                                    <asp:Label ID="LbSpec" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label15" runat="server" Text="Item"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LbItem" runat="server" ForeColor="Blue"></asp:Label>
                                    <asp:Label ID="LbItemRecord" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label16" runat="server" Text="Qty"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TbQty" runat="server" AutoPostBack="True" TextMode="Number" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Text="Void Qty"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TbVoidQty" runat="server" AutoPostBack="True" TextMode="Number" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="Label18" runat="server" Text="Invoice Qty"></asp:Label>
                                </td>
                                <td class="auto-style2">
                                    <asp:Label ID="LbInvoiceQty" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                                <td class="auto-style2">
                                    <asp:Label ID="Label19" runat="server" Text="Balance Qty"></asp:Label>
                                </td>
                                <td class="auto-style2">
                                    <asp:Label ID="LbBalQty" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label20" runat="server" Text="Price"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LbPrice" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label21" runat="server" Text="Status"></asp:Label>
                                </td>
                                <td>
                                    <uc1:DropDownListUserControl ID="UcDdlStatus" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label23" runat="server" Text="Remark"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="TbRemark" runat="server" Width="600px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table class="auto-style1">
                            <tr>
                                <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                                    <asp:Button ID="btSave" runat="server" Text="Save" />
                                    &nbsp;<asp:Button ID="btNew" runat="server" Text="New" />
                                    &nbsp;<asp:Button ID="BtUpdatePrice" runat="server" Text="Update Price" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    <HeaderTemplate>
                        Invoice
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:GridView ID="GvInvoice" runat="server" CssClass="table table-sm table-condensed table-striped table-hover">
                        </asp:GridView>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
            <uc3:CountRow ID="ucCountHead" runat="server" />
            
                <asp:GridView ID="gvShow" runat="server" 
                    AutoGenerateColumns="False" 
                    BackColor="White" 
                    BorderColor="#3366CC" BorderStyle="None" 
                    BorderWidth="1px" CellPadding="4" 
                    CssClass="table table-sm table-condensed table-striped table-hover">
                    <Columns>
                        <asp:ButtonField ButtonType="Image" CommandName="onEdit" HeaderText="Edit" 
                            ImageUrl="~/Images/edit.gif" Text="Button" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="Item" HeaderText="Item" />
                        <asp:BoundField DataField="MB003" HeaderText="Spec" />
                        <asp:BoundField DataField="Qty" DataFormatString="{0:N0}" HeaderText="Qty">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QtyVoid" DataFormatString="{0:N0}" HeaderText="Void Qty">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TB022" DataFormatString="{0:N0}" HeaderText="Invoice Qty">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BQTY" DataFormatString="{0:N0}" HeaderText="Bal Qty">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Price" DataFormatString="{0:N3}" HeaderText="Price">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StatusDetail" HeaderText="Status" />
                        <asp:BoundField DataField="CreateByD" HeaderText="Create By" />
                        <asp:BoundField DataField="CreateDateD" HeaderText="Create Date" />
                        <asp:BoundField DataField="ChangeByD" HeaderText="Change By" />
                        <asp:BoundField DataField="ChangeDateD" HeaderText="Change Date" />
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
            
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
