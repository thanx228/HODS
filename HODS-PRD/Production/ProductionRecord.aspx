<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ProductionRecord.aspx.vb" Inherits="MIS_HTI.ProductionRecord" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc3" %>
<%@ Register src="../UserControl/machineD.ascx" tagname="machineD" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 792px;
        }
        .style10
        {
            width: 85px;
        }
        .style11
        {
            width: 324px;
        }
        .style12
        {
            width: 79px;
        }
        .style16
        {
            height: 34px;
            margin-left: 80px;
        }
        .style19
        {
            width: 95px;
        }
        .style22
        {
            width: 135px;
        }
        .style24
        {
            
        }
        .style25
        {
            width: 135px;
        }
        .style26
        {
            
        }
        .style27
        {
            width: 135px;
            font-weight: 700;
            height: 30px;
        }
        .style32
        {
            width: 95px;
        }
        .style34
        {
            
        }
        .style35
        {
            width: 155px;
        }
        .style39
        {
            width: 792px;
       }
        .style40
        {
            width: 155px;
        }
        .style43
        {
            
        }
        .style46
        {
            height: 30px;
        }
        .style47
        {
            width: 135px;
            height: 30px;
        }
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    .style48
    {
        width: 340px;
    }
        .style49
        {
            width: 169px;
        }
        .style50
        {
            width: 155px;
            height: 34px;
        }
        .style51
        {
            width: 340px;
            height: 34px;
        }
        .style52
        {
            width: 169px;
            height: 34px;
        }
        .style7
        {
            width: 137px;
        }
        .style53
        {
            width: 124px;
        }
        .style54
        {
            width: 150px;
        }
        .style55
        {
            width: 124px;
            height: 28px;
        }
        .style56
        {
            width: 150px;
            height: 28px;
        }
        .style57
        {
            width: 137px;
            height: 28px;
        }
        .style58
        {
            height: 28px;
        }
    </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollShow();
            gridviewScrollList();
            gridviewScrollQty();
        });

        function gridviewScrollList() {
            gridView1 = $('#<%= gvListMO.ClientID %>').gridviewScroll({
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
        function gridviewScrollQty() {
            gridView1 = $('#<%= gvListQty.ClientID %>').gridviewScroll({
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
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <table style="width: 75%;">
                        <tr>
                            <td bgcolor="White" colspan="4">
                                <asp:Label ID="Label47" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="White" class="style35">
                                <asp:Label ID="Label24" runat="server" Text="1.Work Center"></asp:Label>
                            </td>
                            <td bgcolor="White" class="style48">
                                <asp:DropDownList ID="ddlWc" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:Label ID="lbWC" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lbTypeWC" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td bgcolor="White" class="style49">
                                <asp:Label ID="Label20" runat="server" Text="2.MC/Line"></asp:Label>
                            </td>
                            <td bgcolor="White" class="style16">
                                <asp:DropDownList ID="ddlMC" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:Label ID="lbMo" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lbChk" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="White" class="style50">
                                <asp:CheckBox ID="cbInd" runat="server" AutoPostBack="True" 
                                    Text="3.One Man(Opt Code)" />
                            </td>
                            <td bgcolor="White" class="style51">
                                <asp:TextBox ID="tbOperInd" runat="server"></asp:TextBox>
                            </td>
                            <td bgcolor="White" class="style52">
                                &nbsp;</td>
                            <td bgcolor="White" class="style16">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td bgcolor="White" class="style40">
                                <asp:Label ID="Label5" runat="server" Text="4.MO"></asp:Label>
                            </td>
                            <td bgcolor="White" class="style48">
                                <asp:TextBox ID="tbMO" runat="server" CssClass="numberStringOnly" 
                                    MaxLength="11" Width="200px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="tbMO_MaskedEditExtender" runat="server" 
                                    ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                    Mask="9999-99999999999-9999" MaskType="Number" TargetControlID="tbMO">
                                </asp:MaskedEditExtender>
                                &nbsp;<asp:Button ID="btChk" runat="server" Text="5.Check" UseSubmitBehavior="False" 
                                    onclientclick="this.disabled = true; this.value = 'Check..';" />
                            </td>
                            <td bgcolor="White" class="style49">
                                <asp:CheckBox ID="cbSet" runat="server" Enabled="False" Text="Setting" />
                            </td>
                            <td bgcolor="White" class="style43">
                                <asp:CheckBox ID="cbShift" runat="server" Text="Night Shift" />
                            </td>
                        </tr>
                    </table>
                    <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
                        <asp:View ID="View3" runat="server">
                            <asp:GridView ID="gvListMO" runat="server" AutoGenerateColumns="False" 
                                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                                CellPadding="4">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sel">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbSel" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="D" HeaderText="Transfer No" />
                                    <asp:BoundField DataField="A" HeaderText="MO" />
                                    <asp:BoundField DataField="M" HeaderText="OP Desc" />
                                    <asp:BoundField DataField="I" HeaderText="Desc" />
                                    <asp:BoundField DataField="B" HeaderText="Spec" />
                                    <asp:BoundField DataField="C" DataFormatString="{0:#,#}" HeaderText="MO Qty" />
                                    <asp:BoundField DataField="J" DataFormatString="{0:#,#}" HeaderText="Com.Qty" />
                                    <asp:BoundField DataField="K" DataFormatString="{0:#,#}" 
                                        HeaderText="Scrap Qty" />
                                    <asp:BoundField DataField="E" HeaderText="Item" />
                                    <asp:BoundField DataField="H" HeaderText="Assy Desc" />
                                    <asp:BoundField DataField="F" HeaderText="Assy Part" />
                                    <asp:BoundField DataField="G" HeaderText="Assy Item" />
                                    <asp:BoundField DataField="L" HeaderText="MO Status" />
                                    <asp:TemplateField HeaderText="Point Name">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbSubProcess" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Point">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbPoint" runat="server" CssClass="numberOnly" Width="50px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                <RowStyle Wrap="False" BackColor="White" ForeColor="#003399" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                <SortedDescendingHeaderStyle BackColor="#002876" />
                            </asp:GridView>
                            <table style="width: 75%;">
                                <tr>
                                    <td bgcolor="White" class="style24">
                                        <asp:Label ID="Label8" runat="server" Text="Operator ID"></asp:Label>
                                    </td>
                                    <td bgcolor="White" class="style25">
                                        <asp:TextBox ID="tbOper1" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White" class="style25">
                                        <asp:TextBox ID="tbOper2" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White" class="style24">
                                        <asp:TextBox ID="tbOper3" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="White" class="style46">
                                        </td>
                                    <td bgcolor="White" class="style47">
                                        <asp:TextBox ID="tbOper4" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White" class="style47">
                                        <asp:TextBox ID="tbOper5" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White" class="style46">
                                        <asp:TextBox ID="tbOper6" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="White" class="style26">
                                        &nbsp;</td>
                                    <td bgcolor="White" class="style27">
                                        <asp:TextBox ID="tbOper7" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White" class="style27">
                                        <asp:TextBox ID="tbOper8" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White" class="style26">
                                        <asp:TextBox ID="tbOper9" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="White" class="style46">
                                        </td>
                                    <td bgcolor="White" class="style47">
                                        <asp:TextBox ID="tbOper10" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White" class="style47">
                                        <asp:TextBox ID="tbOper11" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White" class="style46">
                                        <asp:TextBox ID="tbOper12" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="White">
                                        &nbsp;</td>
                                    <td bgcolor="White" class="style22">
                                        <asp:TextBox ID="tbOper13" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White" class="style22">
                                        <asp:TextBox ID="tbOper14" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White">
                                        <asp:TextBox ID="tbOper15" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="White">
                                        &nbsp;</td>
                                    <td bgcolor="White" class="style22">
                                        <asp:TextBox ID="tbOper16" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White" class="style22">
                                        <asp:TextBox ID="tbOper17" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White">
                                        <asp:TextBox ID="tbOper18" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="White">
                                        &nbsp;</td>
                                    <td bgcolor="White" class="style22">
                                        <asp:TextBox ID="tbOper19" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White" class="style22">
                                        <asp:TextBox ID="tbOper20" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White">
                                        <asp:TextBox ID="tbOper21" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="White">
                                        &nbsp;</td>
                                    <td bgcolor="White" class="style22">
                                        <asp:TextBox ID="tbOper22" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White" class="style22">
                                        <asp:TextBox ID="tbOper23" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="White">
                                        <asp:TextBox ID="tbOper24" runat="server" MaxLength="6" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="View4" runat="server">
                            <table style="width: 75%;">
                                <tr>
                                    <td bgcolor="White" class="style10">
                                        <asp:Label ID="Label22" runat="server" Text="Include"></asp:Label>
                                    </td>
                                    <td bgcolor="White" class="style11">
                                        <asp:CheckBox ID="cbBreakTime" runat="server" Text="Break Time" />
                                    </td>
                                    <td bgcolor="White" class="style12">
                                        <asp:CheckBox ID="cbCancle" runat="server" Text="Cancle" />
                                    </td>
                                    <td bgcolor="White">
                                        <asp:Label ID="Label28" runat="server" Text="Reason"></asp:Label>
                                    </td>
                                    <td bgcolor="White">
                                        <asp:TextBox ID="tbCanReason" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvListQty" runat="server" AutoGenerateColumns="False" 
                                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                                CellPadding="4">
                                <Columns>
                                    <asp:TemplateField HeaderText="Loss" Visible="False">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hplLoss" runat="server" Target="_blank">Loss</asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="G" HeaderText="Transfer No" />
                                    <asp:BoundField DataField="A" HeaderText="MO" />
                                    <asp:BoundField DataField="O" HeaderText="OP Desc" />
                                    <asp:BoundField DataField="K" HeaderText="Desc" />
                                    <asp:BoundField DataField="C" HeaderText="Spec" />
                                    <asp:BoundField DataField="D" DataFormatString="{0:#,#}" HeaderText="MO Qty" />
                                    <asp:BoundField DataField="E" HeaderText="Setting" />
                                    <asp:BoundField DataField="F" HeaderText="Last Record" />
                                    <asp:TemplateField HeaderText="Accept Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbAcceptQty" runat="server" Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Return Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbDefQty" runat="server" Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Scrap Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbScrapQty" runat="server" Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Scrap Code">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlScrapCode" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="L" DataFormatString="{0:#,#}" HeaderText="Com.Qty" />
                                    <asp:BoundField DataField="M" DataFormatString="{0:#,#}" 
                                        HeaderText="Scrap Qty" />
                                    <asp:BoundField DataField="N" HeaderText="MO Status" />
                                    <asp:BoundField DataField="I" HeaderText="Shift" />
                                    <asp:BoundField DataField="J" HeaderText="Process Type" />
                                    <asp:BoundField DataField="B" HeaderText="Point Name/Opt" />
                                    <asp:BoundField DataField="P" DataFormatString="{0:#,#}" HeaderText="Point" />
                                    <asp:BoundField DataField="H" HeaderText="Last ID" />
                                </Columns>
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                <RowStyle Wrap="False" BackColor="White" ForeColor="#003399" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                <SortedDescendingHeaderStyle BackColor="#002876" />
                            </asp:GridView>
                            <br />
                        </asp:View>
                    </asp:MultiView>
                        <table style="width:75%;">
                            <tr>
                                <td align="center" class="style39" 
                                    style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                                    <asp:Button ID="btSave" runat="server" 
                                        onclientclick="this.disabled = true; this.value = 'Saving...';" Text="Save" 
                                        UseSubmitBehavior="False" />
                                    &nbsp;<asp:Button ID="btResetKey" runat="server" Text="Reset" 
                                        UseSubmitBehavior="False" />
                                    &nbsp;<asp:Button ID="btReport" runat="server" Text="Report" 
                                        UseSubmitBehavior="False" />
                                </td>
                            </tr>
                        </table>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <table style="width: 75%;">
                        <tr>
                            <td bgcolor="White" colspan="4">
                                <asp:Label ID="Label48" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="White" class="style32">
                                <asp:Label ID="Label15" runat="server" Text="MO"></asp:Label>
                            </td>
                            <td bgcolor="White" class="style34">
                                <asp:TextBox ID="tbMOChk" runat="server" CssClass="numberStringOnly" MaxLength="11" Width="200px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="tbMOChk_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="9999-99999999999-9999" MaskType="Number" TargetControlID="tbMOChk">
                                </asp:MaskedEditExtender>
                            </td>
                            <td bgcolor="White" class="style34">
                                <asp:Label ID="Label21" runat="server" Text="Date Record"></asp:Label>
                            </td>
                            <td bgcolor="White" class="style34">
                                <uc3:Date ID="Date1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="White" class="style19">
                                <asp:Label ID="Label25" runat="server" Text="Work Center"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:DropDownList ID="ddlWcChk" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td bgcolor="White">
                                <asp:Label ID="Label26" runat="server" Text="M/C"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <uc4:machineD ID="machineChk" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table style="width:75%;">
                        <tr>
                            <td class="style6" 
                                
                                style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x" 
                                align="center">
                                <asp:Button ID="Button1" runat="server" Text="Clear Yesterday" />
                                &nbsp;<asp:Button ID="btUpdate" runat="server" Text="Update" Visible="False" />
                                &nbsp;<asp:Button ID="btSearch" runat="server" Text="Search" 
                                    UseSubmitBehavior="False" 
                                    onclientclick="this.disabled = true; this.value = 'Searching...';" />
                                &nbsp;<asp:Button ID="btResetReport" runat="server" Text="Reset" 
                                    UseSubmitBehavior="False" />
                                &nbsp;<asp:Button ID="btKey" runat="server" Text="Key In" 
                                    UseSubmitBehavior="False" />
                                &nbsp;<asp:Button ID="btReCal" runat="server" Text="Re Cal " Visible="False" />
                            </td>
                        </tr>
                    </table>
                    <uc2:CountRow ID="CountRow1" runat="server" />
                    <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Loss">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplLoss" runat="server" Target="_blank">Loss</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DN" HeaderText="Doc No" />
                            <asp:BoundField DataField="S" HeaderText="Job Type" />
                            <asp:BoundField DataField="A" HeaderText="MO" />
                            <asp:BoundField DataField="P" HeaderText="Process" />
                            <asp:BoundField DataField="O" HeaderText="Spec" />
                            <asp:BoundField DataField="D" HeaderText="W/C" />
                            <asp:BoundField DataField="E" HeaderText="M/C" />
                            <asp:BoundField DataField="G" HeaderText="Date Work" />
                            <asp:BoundField DataField="H" 
                                HeaderText="Time Start">
                            </asp:BoundField>
                            <asp:BoundField DataField="K" HeaderText="Time End" />
                            <asp:BoundField DataField="L" DataFormatString="{0:#,#}" 
                                HeaderText="Accept Qty">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="M" DataFormatString="{0:#,#}" 
                                HeaderText="Return Qty">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="T" DataFormatString="{0:#,#}" 
                                HeaderText="Scrap Qty" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="U" HeaderText="Scrap Code" />
                            <asp:BoundField DataField="B" HeaderText="Work Type" />
                            <asp:BoundField DataField="F" HeaderText="Work Time(Min)" 
                                DataFormatString="{0:#,#}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F1" DataFormatString="{0:#,#}" 
                                HeaderText="Loss Time(Min)">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="I" HeaderText="Man Power" 
                                DataFormatString="{0:#,#}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C" HeaderText="Transfer  No." />
                            <asp:BoundField DataField="Y" HeaderText="Cancle" />
                            <asp:BoundField DataField="Z" HeaderText="Reason" />
                            <asp:BoundField DataField="PT" HeaderText="Point Name/Opt" />
                            <asp:BoundField DataField="PC" HeaderText="Point" DataFormatString="{0:#,#}" />
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                        <SortedDescendingHeaderStyle BackColor="#002876" />
                    </asp:GridView>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
