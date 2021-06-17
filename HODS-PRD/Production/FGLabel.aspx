<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="FGLabel.aspx.vb" Inherits="MIS_HTI.FGLabel" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style7
        {
        }
        .style8
        {
            height: 21px;
            width: 141px;
        }
        .style10
        {
            height: 21px;
            }
        .style11
        {
            width: 272px;
        }
        .style14
        {
            width: 141px;
            height: 26px;
        }
        .style15
        {
            width: 237px;
            height: 26px;
        }
        .style17
        {
            height: 26px;
        }
        .style23
        {
            height: 21px;
            width: 434px;
        }
        .style24
        {
            width: 434px;
            height: 26px;
        }
        .style25
        {
            width: 434px;
        }
        .style33
        {
            width: 237px;
            height: 21px;
        }
        .style34
        {
            width: 237px;
        }
        .style35
        {
            height: 21px;
            width: 160px;
        }
        .style36
        {
            width: 160px;
            height: 26px;
        }
        .style37
        {
            width: 160px;
        }
        </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 500,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 0,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize: 8
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <table style="width: 75%;">
                        <tr>
                            <td class="style7" style="background-color: #FFFFFF">
                                MO Receipt Type-No</td>
                            <td class="style25" style="background-color: #FFFFFF">
                                <asp:DropDownList ID="ddlMoRType" runat="server">
                                </asp:DropDownList>
                                &nbsp;-
                                <asp:TextBox ID="tbMoRNo" runat="server"></asp:TextBox>
                                &nbsp;-
                                <asp:TextBox ID="tbMoRSeq" runat="server" Width="75px"></asp:TextBox>
                            </td>
                            <td colspan="2" style="background-color: #FFFFFF">
                                <asp:Label ID="Label18" runat="server" Text="App. Status"></asp:Label>
                            </td>
                            <td style="background-color: #FFFFFF">
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" style="background-color: #FFFFFF">
                                MO Type-No</td>
                            <td class="style23" style="background-color: #FFFFFF">
                                &nbsp;<asp:DropDownList ID="ddlMoType" runat="server">
                                </asp:DropDownList>
                                -
                                <asp:TextBox ID="tbMoNo" runat="server"></asp:TextBox>
                                &nbsp;</td>
                            <td class="style10" style="background-color: #FFFFFF">
                                Item</td>
                            <td class="style10" colspan="2" style="background-color: #FFFFFF">
                                <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style14" style="background-color: #FFFFFF">
                                <asp:Label ID="Label16" runat="server" Text="MO Receipt Date"></asp:Label>
                            </td>
                            <td class="style24" style="background-color: #FFFFFF">
                                <asp:TextBox ID="tbDate" runat="server" Width="80px"></asp:TextBox>
                                <asp:CalendarExtender ID="tbDate_CalendarExtender" runat="server" 
                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDate"></asp:CalendarExtender>
                            </td>
                            <td class="style17" colspan="2" style="background-color: #FFFFFF">
                                Desc.</td>
                            <td class="style17" style="background-color: #FFFFFF">
                                <asp:TextBox ID="tbDesc" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 75%;">
                        <tr>
                            <td align="center" 
                                style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                                &nbsp;<asp:Button ID="btSearch" runat="server" Text="Search" />
                                &nbsp;<asp:Button ID="btPrintEmpty" runat="server" Text="Label Empty" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <table style="width: 75%;">
                        <tr>
                            <td class="style8" style="background-color: #FFFFFF">
                                Doc. Record</td>
                            <td class="style33" style="background-color: #FFFFFF">
                                <asp:Label ID="lbDateRec" runat="server" BackColor="#FFCC99" 
                                    BorderColor="#C4C4C4" BorderWidth="1px" Font-Bold="True"></asp:Label>
                            </td>
                            <td colspan="2" style="background-color: #FFFFFF" class="style35">
                                Item</td>
                            <td style="background-color: #FFFFFF" class="style10">
                                <asp:Label ID="lbItem" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" style="background-color: #FFFFFF">
                                MO Receipt Type-No</td>
                            <td class="style33" style="background-color: #FFFFFF">
                                <asp:Label ID="lbMoR" runat="server" BackColor="#FFCC99" BorderColor="#C4C4C4" 
                                    BorderWidth="1px" Font-Bold="True"></asp:Label>
                                &nbsp;</td>
                            <td class="style35" colspan="2" style="background-color: #FFFFFF">
                                Item Desc</td>
                            <td style="background-color: #FFFFFF" class="style10">
                                <asp:Label ID="lbItemRev" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" style="background-color: #FFFFFF">
                                MO Type-No</td>
                            <td class="style33" style="background-color: #FFFFFF">
                                <asp:Label ID="lbMo" runat="server"></asp:Label>
                            </td>
                            <td class="style10" style="background-color: #FFFFFF">
                                Item Spec</td>
                            <td class="style10" colspan="2" style="background-color: #FFFFFF">
                                <asp:Label ID="lbSpec" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" style="background-color: #FFFFFF">
                                <asp:Label ID="Label24" runat="server" Text="Lot No"></asp:Label>
                            </td>
                            <td class="style33" style="background-color: #FFFFFF">
                                <asp:Label ID="lbLotNo" runat="server"></asp:Label>
                            </td>
                            <td class="style10" style="background-color: #FFFFFF">
                                <asp:Label ID="Label25" runat="server" Text="Test Report No"></asp:Label>
                            </td>
                            <td class="style10" colspan="2" style="background-color: #FFFFFF">
                                <asp:Label ID="lbTestNo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style14" style="background-color: #FFFFFF">
                                SO Type-No</td>
                            <td class="style15" style="background-color: #FFFFFF">
                                <asp:Label ID="lbSo" runat="server"></asp:Label>
                            </td>
                            <td class="style36" colspan="2" style="background-color: #FFFFFF">
                                <asp:Label ID="Label21" runat="server" Text="Cust of Cust"></asp:Label>
                            </td>
                            <td class="style17" style="background-color: #FFFFFF">
                                <asp:Label ID="lbCustofCust" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" style="background-color: #FFFFFF">
                                Qty</td>
                            <td class="style33" style="background-color: #FFFFFF">
                                <asp:Label ID="lbQty" runat="server"></asp:Label>
                            </td>
                            <td class="style35" colspan="2" style="background-color: #FFFFFF">
                                Item Weight</td>
                            <td class="style10" style="background-color: #FFFFFF">
                                <asp:Label ID="lbItemWgh" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7" style="background-color: #FFFFFF">
                                Qty/Carton</td>
                            <td class="style34" style="background-color: #FFFFFF">
                                <asp:TextBox ID="tbQtyCtn" runat="server" CssClass="numberOnly" 
                                    Width="50px"></asp:TextBox>
                            </td>
                            <td colspan="2" style="background-color: #FFFFFF" class="style37">
                                Carton Wight</td>
                            <td style="background-color: #FFFFFF">
                                <asp:TextBox ID="tbCtnWgh" runat="server" CssClass="numberOnly" 
                                    Width="80px"></asp:TextBox>
                                &nbsp;<asp:Button ID="btCal" runat="server" Text="Cal" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style7" style="background-color: #FFFFFF">
                                Carton No.</td>
                            <td class="style34" style="background-color: #FFFFFF">
                                <asp:TextBox ID="tbCtnNo" runat="server"></asp:TextBox>
                            </td>
                            <td colspan="2" style="background-color: #FFFFFF" class="style37">
                                Carton Spec</td>
                            <td style="background-color: #FFFFFF">
                                <asp:Label ID="lbCtnSpec" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7" style="background-color: #FFFFFF">
                                Full Box</td>
                            <td class="style34" style="background-color: #FFFFFF">
                                <asp:Label ID="lbFull" runat="server"></asp:Label>
                            </td>
                            <td colspan="2" style="background-color: #FFFFFF" class="style37">
                                Not Full Box</td>
                            <td style="background-color: #FFFFFF">
                                <asp:Label ID="lbNotFull" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7" style="background-color: #FFFFFF">
                                Full Box Net Wight</td>
                            <td class="style34" style="background-color: #FFFFFF">
                                <asp:Label ID="lbFullN" runat="server"></asp:Label>
                            </td>
                            <td class="style37" colspan="2" style="background-color: #FFFFFF">
                                Not Full Box Net Wight</td>
                            <td style="background-color: #FFFFFF">
                                <asp:Label ID="lbNotFullN" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7" style="background-color: #FFFFFF">
                                Full Box Gross Wight</td>
                            <td class="style34" style="background-color: #FFFFFF">
                                <asp:Label ID="lbFullG" runat="server"></asp:Label>
                                <br />
                            </td>
                            <td class="style37" colspan="2" style="background-color: #FFFFFF">
                                Not Full Box Gross Wight</td>
                            <td style="background-color: #FFFFFF">
                                <asp:Label ID="lbNotFullG" runat="server"></asp:Label>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="style7" style="background-color: #FFFFFF">
                                <asp:Label ID="Label23" runat="server" Text="Pack By"></asp:Label>
                            </td>
                            <td class="style34" style="background-color: #FFFFFF">
                                <asp:TextBox ID="tbPack" runat="server" Width="208px"></asp:TextBox>
                            </td>
                            <td colspan="2" style="background-color: #FFFFFF" class="style37">
                                <asp:Label ID="Label22" runat="server" Text="Format Print"></asp:Label>
                            </td>
                            <td style="background-color: #FFFFFF">
                                <asp:DropDownList ID="ddlFormatPrint" runat="server">
                                    <asp:ListItem Selected="True" Value="HT">Hoothai</asp:ListItem>
                                    <asp:ListItem Value="JP">Jinpao</asp:ListItem>
                                    <asp:ListItem Value="FS">Fisher</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7" colspan="5" style="background-color: #FFFFFF">
                                <asp:Label ID="lbError" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 75%;">
                        <tr>
                            <td align="center" 
                                style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                                &nbsp;&nbsp;&nbsp;<asp:Button ID="btSave" runat="server" 
                                    Text="Save &amp; Preview" Autopostback = "false"
                                    UseSubmitBehavior="true" />
                                &nbsp;<asp:Button ID="btPrint" runat="server" Text="Preview" />
                                &nbsp;<asp:Button ID="btCancel" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
            <table style="width: 75%;">
                <tr>
                    <td align="center" class="style11" style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Amount of Rows"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label12" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">

                <Columns >
                
                <asp:ButtonField ButtonType="Image" CommandName="OnView" HeaderText="View" 
                    ImageUrl="~/Images/imagesview.jpg" Text="View" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>
                
                    <asp:BoundField DataField="A" HeaderText="Doc No" />
                    <asp:BoundField DataField="B" HeaderText="MO Reipt Type/No/Seq" />
                    <asp:BoundField DataField="C" HeaderText="MO Type/No" />
                    <asp:BoundField DataField="F" HeaderText="Item" />
                    <asp:BoundField DataField="G" HeaderText="Desc" />
                    <asp:BoundField DataField="H" HeaderText="Cust Spec" />
                    <asp:BoundField DataField="I" DataFormatString="{0:N0}" HeaderText="MO Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="J" DataFormatString="{0:N0}" HeaderText="Comp. Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="K" DataFormatString="{0:N0}" HeaderText="Scarp Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="L" HeaderText="Customer" />
                    <asp:BoundField DataField="M" HeaderText="MO Status" />
                    <asp:BoundField DataField="N" DataFormatString="{0:N3}" HeaderText="Item Wght">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="O" DataFormatString="{0:N0}" 
                        HeaderText="MO Reipt Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="P" DataFormatString="{0:N0}" HeaderText="Qty/Carton">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Q" DataFormatString="{0:N3}" 
                        HeaderText="Carton  Wght" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="R" HeaderText="Carton Item" />
                    <asp:BoundField DataField="S" HeaderText="Carton Spec" />
                    <asp:BoundField DataField="Z" HeaderText="Pack By" />
                
                    <asp:BoundField DataField="D" HeaderText="SO Type/No/Seq" />
                    <asp:BoundField DataField="E" HeaderText="Cust PO" />
                    <asp:BoundField DataField="TD020" HeaderText="Cust of Cust" />
                    <asp:BoundField DataField="UDF01" HeaderText="Lot No" />
                    <asp:BoundField DataField="UDF06" HeaderText="Test No." />
                
                    <asp:BoundField DataField="INVUDF01" HeaderText="Pos" />
                
                </Columns>

                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    HorizontalAlign="Center" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />



            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
