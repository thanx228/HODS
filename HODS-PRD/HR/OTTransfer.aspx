<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="OTTransfer.aspx.vb" Inherits="MIS_HTI.OTTransfer" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<%@ Register src="../UserControl/workCenterC.ascx" tagname="workCenterC" tagprefix="uc4" %>
<%@ Register src="../UserControl/DeptC.ascx" tagname="DeptC" tagprefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">

        .style114
        {
            height: 2px;
        }
        .style112
        {
            height: 25px;
            }
        .style113
        {
            height: 25px;
            width: 264px;
        }
        .style58
        {
            width: 296px;
            height: 25px;
        }
        .auto-style2 {
            height: 21px;
        }
        .auto-style3 {
            height: 21px;
            width: 100px;
        }
        .auto-style4 {
            height: 28px;
            width: 91px;
        }
        .auto-style5 {
            height: 28px;
        }
        .auto-style8 {
            margin-right: 0px;
        }
        .auto-style11 {
            height: 28px;
            width: 54px;
        }
        .auto-style12 {
            height: 28px;
            width: 135px;
        }
        </style>

     <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollShow();
            gridviewScrollEdit();
            gridviewScrollNew();
        });

        function gridviewScrollNew() {
            gridView1 = $('#<%= gvNew.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 500,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 2,
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

        function gridviewScrollEdit() {
            gridView1 = $('#<%= gvEdit.ClientID %>').gridviewScroll({
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

   </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <uc3:HeaderForm ID="HeaderForm1" runat="server" />
            <table style="width: 75%;">
                <tr>
                    <td bgcolor="White" class="auto-style3" style="vertical-align: top">
                        <asp:Label ID="Label10" runat="server" Text="Shift Day"></asp:Label>
                    </td>
                    <td bgcolor="White" class="auto-style2">
                        <asp:DropDownList ID="ddlShiftDays" runat="server">
                            <asp:ListItem>Day(7)</asp:ListItem>
                            <asp:ListItem Value="Day(8)"></asp:ListItem>
                            <asp:ListItem Value="Night"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="auto-style3" style="vertical-align: top">
                        <asp:Label ID="Label5" runat="server" Text="Dept"></asp:Label>
                    </td>
                    <td bgcolor="White" class="auto-style2">
                        <uc5:DeptC ID="ucDept" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" AutoPostBack="true" Height="250px" VerticalStripWidth="200px" Width="100%">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        New&nbsp; Record
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table bgcolor="White" style="width: 75%; height: 98px;">
                            <tr>
                                <td class="style112">
                                    <asp:Label ID="Label11" runat="server" Text="Date Of OT"></asp:Label>
                                </td>
                                <td class="style112">
                                    <uc1:Date ID="ucDateNew" runat="server" />
                                </td>
                                <td class="style112"></td>
                                <td class="style112">
                                    <asp:TextBox ID="tbDate" runat="server" AutoPostBack="True" Enabled="False" Width="80px" Visible="False"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDate">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="style114">
                                    <asp:Label ID="lbEmpNo" runat="server" Text="Emp. No."></asp:Label>
                                </td>
                                <td class="style112">
                                    <asp:TextBox ID="tbEmpNo" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td class="style113">
                                    <asp:TextBox ID="tbEmpNo0" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td class="style58">
                                    <asp:TextBox ID="tbEmpNo1" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style114">&nbsp;</td>
                                <td class="style112">
                                    <asp:TextBox ID="tbEmpNo2" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td class="style113">
                                    <asp:TextBox ID="tbEmpNo3" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td class="style58">
                                    <asp:TextBox ID="tbEmpNo4" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#CCFFFF" colspan="4">Default Value</td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label ID="Label7" runat="server" Text="Start Time"></asp:Label>
                                </td>
                                <td class="auto-style12">
                                    <asp:TextBox ID="tbStartTimeHead0" runat="server" Width="40px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="tbStartTimeHead0_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Number" TargetControlID="tbStartTimeHead0">
                                    </asp:MaskedEditExtender>
                                </td>
                                <td class="auto-style11">
                                    <asp:Label ID="Label8" runat="server" Text="End Time"></asp:Label>
                                </td>
                                <td class="auto-style5">
                                    <asp:TextBox ID="tbEndTimeHead0" runat="server" Width="40px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="tbEndTimeHead0_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Number" TargetControlID="tbEndTimeHead0">
                                    </asp:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label ID="Label9" runat="server" Text="Reason"></asp:Label>
                                </td>
                                <td class="auto-style12">
                                    <asp:TextBox ID="tbLocationHead" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style11">&nbsp;</td>
                                <td class="auto-style5">&nbsp;</td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel3">
                    <HeaderTemplate>
                        Edit Record
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table bgcolor="White" style="width: 75%; height: 98px;">
                            <tr>
                                <td class="style114">Dept. To</td>
                                <td class="style112" colspan="3">
                                    <asp:DropDownList ID="ddlDeptToEdit" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                                    <asp:CheckBox ID="cbOutEdit" runat="server" Text="Out Location" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style114">Date</td>
                                <td class="style112">
                                    <uc1:Date ID="ucDateEdit" runat="server" />
                                </td>
                                <td class="style112">&nbsp;</td>
                                <td class="style58">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style112">
                                    <asp:Label ID="lbEmpNo0" runat="server" Text="Emp. No."></asp:Label>
                                </td>
                                <td class="style112">
                                    <asp:TextBox ID="tbEmpNoEdit" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td class="style113">
                                    <asp:TextBox ID="tbEmpNoEdit0" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td class="style58">
                                    <asp:TextBox ID="tbEmpNoEdit1" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style114">&nbsp;</td>
                                <td class="style112">
                                    <asp:TextBox ID="tbEmpNoEdit2" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td class="style113">
                                    <asp:TextBox ID="tbEmpNoEdit3" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td class="style58">
                                    <asp:TextBox ID="tbEmpNoEdit4" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel2">
                    <HeaderTemplate>
                        Report
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table bgcolor="White" style="width: 75%; height: 98px;">
                            <tr>
                                <td class="style114">Dept. To</td>
                                <td class="style112" colspan="3">
                                    <asp:DropDownList ID="ddlDeptTo" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                                    <asp:CheckBox ID="cbOut" runat="server" Text="Out Location" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style114">Date From</td>
                                <td class="style112">
                                    <uc1:Date ID="Date1" runat="server" />
                                </td>
                                <td align="right" class="style113">Date To</td>
                                <td class="style58">
                                    <uc1:Date ID="Date2" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style112">Emp. No.</td>
                                <td class="style112">
                                    <asp:TextBox ID="tbEmpNoRe" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td class="style113">
                                    <asp:TextBox ID="tbEmpNoRe0" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td class="style58">
                                    <asp:TextBox ID="tbEmpNoRe1" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="style114">&nbsp;</td>
                                <td align="left" class="style114">
                                    <asp:TextBox ID="tbEmpNoRe2" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td align="left" class="style114">
                                    <asp:TextBox ID="tbEmpNoRe3" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td align="left" class="style114">
                                    <asp:TextBox ID="tbEmpNoRe4" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <table bgcolor="White" style="width: 77%; height: 1px;">
                <tr>
                    <td align="center" class="style114" style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btNew" runat="server" Text="New" Visible="False" />
                        <asp:Button ID="btSave" runat="server" Text="Save" Visible="False" />
                        <asp:Button ID="btEdit" runat="server" Text="Edit" Visible="False" />
                        <asp:Button ID="btUpdate" runat="server" Text="Update" Visible="False" />
                        <asp:Button ID="btCancel" runat="server" Text="Cancel" Visible="False" />
                        <asp:Button ID="btShow" runat="server" Text="Report" Visible="False" />
                        <asp:Button ID="btExport" runat="server" Text="Export Excel" Visible="False" />
                    </td>
                </tr>
            </table>
            <uc2:CountRow ID="CountRow1" runat="server" />
            <asp:GridView ID="gvNew" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="auto-style8" >
                <Columns>
                    <asp:BoundField DataField="EmpNo" HeaderText="Emp. No." />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Shift" HeaderText="Shift" />
                    <asp:BoundField DataField="Line" HeaderText="Line" />
                    <asp:BoundField DataField="Dept." HeaderText="From Dept." />
                    <asp:TemplateField HeaderText="To Dept.">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlDeptNew" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From WC">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlFromWC" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To WC">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlToWC" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlStartDateNew" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Time">
                        <ItemTemplate>
                            <asp:TextBox ID="tbStartTimeNew" runat="server" Width="40px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbStartTimeNew_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Number" TargetControlID="tbStartTimeNew">
                            </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Date">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlEndDateNew" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Time">
                        <ItemTemplate>
                            <asp:TextBox ID="tbEndTimeNew" runat="server" Width="40px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbEndTimeNew_MaskedEditExtender0" runat="server" ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Number" TargetControlID="tbEndTimeNew">
                            </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason">
                        <ItemTemplate>
                            <asp:TextBox ID="tbLocation" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ShiftDay" HeaderText="Shift of Day" />
                    <asp:BoundField DataField="DateofOT" HeaderText="Date of OT" />
                    <asp:BoundField DataField="OTEndDate" HeaderText="OT Date End" />
                    <asp:BoundField DataField="OTStartTime" HeaderText="OT Start Time" />
                    <asp:BoundField DataField="OTEndTime" HeaderText="OT End Time " />
                    <asp:BoundField DataField="Holiday" HeaderText="Holiday" />
                    <asp:BoundField DataField="AbsenceTime" HeaderText="Abs. Time" />
                    <asp:BoundField DataField="DocNo" HeaderText="Doc No" />
                    <asp:ButtonField ButtonType="Image" CommandName="OnDelete" HeaderText="Delete" ImageUrl="~/Images/delete.gif" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:GridView ID="gvEdit" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="DocNo" DataSourceID="SqlDataSource1" Visible="False">
                <Columns>
                    <asp:BoundField DataField="DocNo" HeaderText="DocNo" />
                    <asp:BoundField DataField="EmpNo" HeaderText="Emp. No." />
                    <asp:BoundField DataField="EmpName" HeaderText="Name" />
                    <asp:BoundField DataField="Shift" HeaderText="Shift" />
                    <asp:BoundField DataField="Line" HeaderText="Line" />
                    <asp:BoundField DataField="DeptFrom" HeaderText="From Dept." />
                    <asp:TemplateField HeaderText="To Dept.">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlDept" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From WC">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlFromWC" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To WC">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlToWC" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DateofSupport">
                        <ItemTemplate>
                            <asp:TextBox ID="tbEditDate" runat="server" AutoPostBack="True" Width="80px"></asp:TextBox>
                            <asp:CalendarExtender ID="tbEditDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbEditDate">
                            </asp:CalendarExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Time">
                        <ItemTemplate>
                            <asp:TextBox ID="tbStartTime" runat="server" Width="40px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbStartTime_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Number" TargetControlID="tbStartTime">
                            </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Time">
                        <ItemTemplate>
                            <asp:TextBox ID="tbEndTime" runat="server" Width="40px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbEndTime_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Number" TargetControlID="tbEndTime">
                            </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason">
                        <ItemTemplate>
                            <asp:TextBox ID="tbLocationEdit" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" ImageUrl="~/Images/delete.gif" onclientclick="return confirm('Are you sure delete Emp. No.?')" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" DeleteCommand="DELETE FROM [employeeTransfer] WHERE [DocNo] = @DocNo">
                <DeleteParameters>
                    <asp:Parameter Name="DocNo" Type="String" />
                </DeleteParameters>
            </asp:SqlDataSource>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

