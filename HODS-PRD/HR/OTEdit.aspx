<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="OTEdit.aspx.vb" Inherits="MIS_HTI.OTEdit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollShow();
            gridviewScrollWork();
            gridviewScrollShowRe();
            gridviewScrollEdit();
        });

        function gridviewScrollWork() {
            gridView1 = $('#<%= gvWork.ClientID %>').gridviewScroll({
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

        function gridviewScrollEdit() {
            gridView1 = $('#<%= gvEdit.ClientID %>').gridviewScroll({
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

        function gridviewScrollShowRe() {
            gridView1 = $('#<%= gvShowRe.ClientID %>').gridviewScroll({
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
          <uc3:HeaderForm ID="HeaderForm1" runat="server" />
            <table style="width: 75%;">
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        Dept.</td>
                    <td class="style10" colspan="3" style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblDept" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        Date</td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbOTDate" runat="server" Width="80px" AutoPostBack="true" ></asp:TextBox>
                        <asp:CalendarExtender ID="tbOTDate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbOTDate">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:Label ID="lbEmpNo" runat="server" Text="Emp. No."></asp:Label>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbEmpNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        Shift</td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblShift" runat="server">
                        </asp:CheckBoxList>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Area"></asp:Label>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlArea" runat="server">
                            <asp:ListItem Value="1">Production</asp:ListItem>
                            <asp:ListItem Value="2">Office</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        Default Holiday</td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:CheckBox ID="cbSetHoliday" runat="server" Text="Holiday" />
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        Default ทานอาหาร</td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:CheckBox ID="cbSetDinner" runat="server" Text="ทานอาหาร" />
                        <asp:TextBox ID="tbDateFrom" runat="server" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="tbDateTo" runat="server" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        Shift Day/Night</td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:RadioButtonList ID="rdlShift" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" >
                            <asp:ListItem Selected="True" Value="0">Day</asp:ListItem>
                            <asp:ListItem Value="1">Night</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        Default&nbsp; OT End Time</td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:RadioButtonList ID="rdlDefault" runat="server" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem>NO OT</asp:ListItem>
                            <asp:ListItem>16:00</asp:ListItem>
                            <asp:ListItem Selected="True">19:00</asp:ListItem>
                            <asp:ListItem>21:00</asp:ListItem>
                            <asp:ListItem>04:00</asp:ListItem>
                            <asp:ListItem>07:00</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        &nbsp;<asp:Button ID="btRecord" runat="server" Text="New" />
                        <asp:Button ID="btEdit" runat="server" Text="Edit" />
                        <asp:Button ID="btShowRe" runat="server" Text="Report" />
                        <asp:Button ID="btSave" runat="server" Text="Save" />
                        <asp:Button ID="btUpdate" runat="server" Text="Update" />
                        <asp:Button ID="btCancel" runat="server" Text="Cancel" />
                        <asp:Button ID="btExport" runat="server" Text="Print Report" />
                        <asp:Button ID="btExcel" runat="server" Text="Export Excel" />
                        <br />
                    </td>
                </tr>
            </table>
            <uc2:CountRow ID="CountRow1" runat="server" />
            <asp:Label ID="lbSelect" runat="server" Font-Bold="True" Font-Italic="False" 
                ForeColor="#CC3300" Text="Select Shift"></asp:Label>
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" style="margin-bottom: 0px">
                <Columns>
                    <asp:BoundField DataField="Shift" HeaderText="Shift" />
                    <asp:BoundField DataField="Dept." HeaderText="Dept." />
                    <asp:BoundField DataField="Line" HeaderText="Line" />
                    <asp:BoundField DataField="EmpNo" HeaderText="Emp. No." />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:TemplateField HeaderText="Holiday">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbHoliday" runat="server" />
                            </CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Absence">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbAbsence" runat="server" />
                            </CheckBox>
                            <%--  <asp:CheckBox ID = "CheckBox1" runat = "server" OnClick ="checkAbsence(this);" /></CheckBox>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Absence  Time hrs.">
                        <ItemTemplate>
                            <asp:TextBox ID="tbAbsenceTime" runat="server" Width="30px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shift Day">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlShiftDay" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT Lunch hrs.">
                        <ItemTemplate>
                            <asp:TextBox ID="tbOTLunch" runat="server" Width="30px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT End Time">
                        <ItemTemplate>
                            <asp:RadioButtonList ID="rdlEndTime" runat="server" 
                                RepeatDirection="Horizontal" />
                            </RadioButtonList>
                            <asp:TextBox ID="tbEndTime" runat="server" Width="40px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbEndTime_MaskedEditExtender" runat="server" 
                                ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                Mask="99.99" MaskType="Number" TargetControlID="tbEndTime">
                            </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bus Line">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlBusLine" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ทานอาหาร">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbDinner" runat="server" />
                            </CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT Over Date">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbOT" runat="server" />
                            </CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
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
            <asp:Label ID="lbOther" runat="server" Font-Bold="True" Font-Italic="False" 
                ForeColor="#CC3300" Text="Other Shift"></asp:Label>       
            <asp:GridView ID="gvWork" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="Shift" HeaderText="Shift" />
                    <asp:BoundField DataField="Dept." HeaderText="Dept." />
                    <asp:BoundField DataField="Line" HeaderText="Line" />
                    <asp:BoundField DataField="EmpNo" HeaderText="Emp. No." />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:TemplateField HeaderText="Holiday">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbHolidayWork" runat="server" />
                            </CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Work">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbWork" runat="server" />
                            </CheckBox>
                            <%--  <asp:CheckBox ID = "CheckBox1" runat = "server" OnClick ="checkAbsence(this);" /></CheckBox>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shift Day">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlShiftDayWork" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT Lunch hrs.">
                        <ItemTemplate>
                            <asp:TextBox ID="tbOTLunchWork" runat="server" Width="30px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT End Time">
                        <ItemTemplate>
                            <asp:RadioButtonList ID="rdlEndTimeWork" runat="server" 
                                RepeatDirection="Horizontal" />
                            </RadioButtonList>
                            <asp:TextBox ID="tbEndTimeWork" runat="server" Width="40px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbEndTimeWork_MaskedEditExtender" runat="server" 
                                ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                Mask="99.99" MaskType="Number" TargetControlID="tbEndTimeWork">
                            </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bus Line">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlBusLineWork" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ทานอาหาร">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbDinnerWork" runat="server" />
                            </CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT Over Date">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbOTWork" runat="server" />
                            </CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
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
            <asp:GridView ID="gvShowRe" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:GridView ID="gvEdit" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="Absence">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbAbsenceEdit" runat="server" />
                            </CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Absence  Time hrs.">
                        <ItemTemplate>
                            <asp:TextBox ID="tbAbsenceTimeEdit" runat="server" Width="30px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Shift" HeaderText="Shift" />
                    <asp:BoundField DataField="Dept" HeaderText="Dept." />
                    <asp:BoundField DataField="Line" HeaderText="Line" />
                    <asp:BoundField DataField="EmpNo" HeaderText="Emp. No." />
                    <asp:BoundField DataField="EmpName" HeaderText="Name" />
                    <asp:TemplateField HeaderText="Holiday">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbHolidayEdit" runat="server" />
                            </CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="OT Type" HeaderText="OT Type" />
                    <asp:TemplateField HeaderText="Shift Day">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlShiftDayEdit" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT Lunch hrs.">
                        <ItemTemplate>
                            <asp:TextBox ID="tbOTLunchEdit" runat="server" Width="30px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="OTStartDate" HeaderText="OT Start Date" />
                    <asp:BoundField DataField="Start Time" HeaderText="OT Start Time" />
                    <asp:BoundField DataField="End Date" HeaderText="OT End Date" />
                    <asp:TemplateField HeaderText="OT End Time">
                        <ItemTemplate>
                            <asp:TextBox ID="tbEndTimeEdit" runat="server" Width="40px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbEndTimeEdit_MaskedEditExtender" runat="server" 
                                ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                Mask="99.99" MaskType="Number" TargetControlID="tbEndTimeEdit">
                            </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DateofOT" HeaderText="Date of OT" />
                    <asp:TemplateField HeaderText="Bus Line">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlBusLineEdit" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ทานอาหาร">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbDinnerEdit" runat="server" />
                            </CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT Over Date">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbOTEdit" runat="server" />
                            </CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="True" />
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
            <asp:PostBackTrigger ControlID="btExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
