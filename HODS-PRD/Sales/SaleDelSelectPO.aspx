<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleDelSelectPO.aspx.vb" Inherits="MIS_HTI.SaleDelSelectPO" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<%@ Register src="../UserControl/docTypeD.ascx" tagname="docTypeD" tagprefix="uc3" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px;
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
        }
        function gridviewScrollShowSel() {
            gridView1 = $('#<%= gvSel.ClientID %>').gridviewScroll({
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="SO Number"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbNumber" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btShow" runat="server" Text="Search" />
                        &nbsp;<asp:Button ID="btReset" runat="server" Text="Reset" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnShow" runat="server">
                <table style="width: 75%;">
                    <tr>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label15" runat="server" Text="Sale Del Type"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <uc3:docTypeD ID="ucSaleDelType" runat="server" />
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label14" runat="server" Text="Sale Del No"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="lbSelDocNo" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label5" runat="server" Text="Doc Date"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="lbDate" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label16" runat="server" Text="Ship Date"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <uc4:Date ID="ucShipDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label18" runat="server" Text="Remark"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:TextBox ID="tbRemark" runat="server"></asp:TextBox>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label17" runat="server" Text="Ship Time"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:DropDownList ID="ddlShipTime" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label9" runat="server" Text="Cust"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="lbCust" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label7" runat="server" Text="SO No"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="lbSO" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label11" runat="server" Text="Plant"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="lbPlant" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="Label13" runat="server" Text="SO Ship Time"></asp:Label>
                        </td>
                        <td bgcolor="White" class="style6">
                            <asp:Label ID="lbTime" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <uc2:CountRow ID="ucCount" runat="server" />
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="ViewSearch" runat="server">
                    <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4">
                        <Columns>
                            <asp:TemplateField HeaderText="Sel">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbSel" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TD003" HeaderText="Seq" />
                            <asp:BoundField DataField="TD004" HeaderText="Item" />
                            <asp:BoundField DataField="TD005" HeaderText="Desc" />
                            <asp:BoundField DataField="TD006" HeaderText="Spec" />
                            <asp:BoundField DataField="TD007" HeaderText="WH" />
                            <asp:BoundField DataField="TD008" HeaderText="SO Qty" 
                                DataFormatString="{0:N0}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TD009" HeaderText="Del Qty" 
                                DataFormatString="{0:N0}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TD0089" HeaderText="Bal Qty" 
                                DataFormatString="{0:N0}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbQty" runat="server" Width="80px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MC007" HeaderText="Inv Qty" 
                                DataFormatString="{0:N0}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TD011" HeaderText="Price" DataFormatString="{0:N3}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TD010" HeaderText="Unit" />
                            <asp:BoundField DataField="TD013" HeaderText="Plan Del Date" />
                            <asp:BoundField DataField="UDF02" HeaderText="Cust W/O" />
                            <asp:BoundField DataField="UDF03" HeaderText="Cust Line" />
                            <asp:BoundField DataField="UDF04" HeaderText="Model" />
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                            HorizontalAlign="Center" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" ForeColor="#003399" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                        <SortedDescendingHeaderStyle BackColor="#002876" />
                    </asp:GridView>
                    <table style="width: 75%;">
                        <tr>
                            <td style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x" 
                                align="center">
                                <asp:Button ID="btSelect" runat="server" Text="Select" />
                                &nbsp;<asp:Button ID="btSelInven" runat="server" Text="Stock Qty" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="ViewPO" runat="server">
                    <asp:GridView ID="gvSel" runat="server" BackColor="White" BorderColor="#3366CC" 
                        BorderStyle="None" BorderWidth="1px" CellPadding="4">
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
                    <table style="width: 75%;">
                        <tr>
                            <td bgcolor="White">
                                <asp:Label ID="Label19" runat="server" Text="All Amt"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:Label ID="lbAllAmt" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:Label ID="Label21" runat="server" EnableTheming="True" Text="All Qty"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:Label ID="lbAllQty" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width:75%;">
                        <tr>
                            <td style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x" 
                                align="center">
                                <asp:Button ID="btSave" runat="server" Text="Sale Delivery" />
                                &nbsp;<asp:Button ID="btBack" runat="server" Text="Back Select" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
