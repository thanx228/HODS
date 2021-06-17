<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="OTRecord_HY.aspx.vb" Inherits="MIS_HTI.OTRecord_HY" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<%@ Register src="../UserControl/DateTextChange.ascx" tagname="DateTextChange" tagprefix="uc1" %>
   
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            margin-right: 0px;
        }
    </style>
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
                barsize:10
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
                barsize:10
              });
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table style="width: 80%;">
                <tr>
                    <td class="auto-style17" bgcolor="White">
                        <asp:Label ID="Label4" runat="server" Text="Dept"></asp:Label>
                    </td>
                    <td colspan="3" bgcolor="White">
                        <asp:CheckBoxList ID="cblDept" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style17" bgcolor="White">
                        <asp:Label ID="Label5" runat="server" Text="Date"></asp:Label>
                    </td>
                    <td class="auto-style15" bgcolor="White">
                        <uc1:DateTextChange ID="ucDate" runat="server" />
                    </td>
                    <td class="auto-style16" bgcolor="White">
                        <asp:Label ID="lbEmpNo" runat="server" Text="EmpNo"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbEmpNo" runat="server" Width="80px"></asp:TextBox>
                        <asp:Label ID="lbGrp" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="Label8" runat="server"  Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style17" bgcolor="White">
                        <asp:Label ID="Label6" runat="server" Text="Shift"></asp:Label>
                    </td>
                    <td class="auto-style15" bgcolor="White">
                        <asp:RadioButtonList ID="rdlShift" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="D">Day</asp:ListItem>
                            <asp:ListItem Value="N">Night</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="auto-style16" bgcolor="White">
                        <asp:Label ID="Label7" runat="server" Text="Over Time"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:RadioButtonList ID="rdlDefault" runat="server" RepeatDirection="Horizontal">
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
            <table style="width: 80%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        &nbsp;<asp:Button ID="btRecord" runat="server" Text="New" />
                        <asp:Button ID="btEdit" runat="server" Text="Edit" />
                        <asp:Button ID="btSave" runat="server" Text="Save" />
                         <asp:Button ID="btUpdate" runat="server" Text="Update" />
                        <asp:Button ID="btCancel" runat="server" Text="Cancel" />
                        <asp:Button ID="btShowRe" runat="server" Text="Report" />
                        <asp:Button ID="btPrint" runat="server" Text="Print Report" />
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
                CellPadding="4" CssClass="auto-style3">
                <Columns>
                    <asp:BoundField DataField="shiftday" HeaderText="ShiftDay" />
                    <asp:BoundField DataField="dept" HeaderText="Dept." />
                    <asp:BoundField DataField="empno" HeaderText="Emp. No." />
                    <asp:BoundField DataField="name" HeaderText="Name" />
                      <asp:BoundField DataField="workdate" HeaderText="Date" />
                    <asp:TemplateField HeaderText="Work Type">
                        <ItemTemplate>
                            <asp:TextBox ID="lbHoliday" runat="server" Width="80" Enabled="False"></asp:TextBox>
                        </ItemTemplate>
                   </asp:TemplateField>
                     <asp:TemplateField HeaderText="Shift Day">
                         <ItemTemplate>
                            <asp:DropDownList ID="ddlShiftDay" runat="server" AutoPostBack="True" onselectedindexchanged="ddlShiftDay_Selectedindexchanged" Enabled="False">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Leave Code">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlleaveCode" runat="server" AutoPostBack="True" onselectedindexchanged="ddlleaveCode_Selectedindexchanged"></asp:DropDownList>                      
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Day/Hrs">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlLeaveCase" runat="server" Width ="80"  AutoPostBack="True" onselectedindexchanged="ddlLeaveCase_Selectedindexchanged" Visible="false">
                                <asp:ListItem Value="">-Select-</asp:ListItem>
                                <asp:ListItem Value="D">วัน Day</asp:ListItem>
                                <asp:ListItem Value="H">ชม. Hour</asp:ListItem>

                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Leave Start">
                        <ItemTemplate>
                            <asp:TextBox ID="tbLeaveStartDate" runat="server" Width="70px" placeholder="__/__/____"  onkeydown="return false;" Visible="false"></asp:TextBox>
                        <asp:CalendarExtender ID="tbLeaveStartDate_CalendarExtender" runat="server"
                            Enabled="true" Format="dd/MM/yyyy" TargetControlID="tbLeaveStartDate" >
                        </asp:CalendarExtender>
                        <asp:TextBox ID="tbLeaveStartTime" runat="server" Width="40px" Visible="false"></asp:TextBox>
                             <asp:MaskedEditExtender ID="tbLeaveStartTime_MaskedEditExtender" runat="server" 
                                    ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                    Mask="99.99" MaskType="Number" TargetControlID="tbLeaveStartTime">
                                </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Leave Finish">
                        <ItemTemplate>
                             <asp:TextBox ID="tbLeaveEndDate" runat="server" Width="70px" placeholder="__/__/____"  onkeydown="return false;" visible="false"></asp:TextBox>
                        <asp:CalendarExtender ID="tbLeaveEndDate_CalendarExtender" runat="server"
                            Enabled="true" Format="dd/MM/yyyy" TargetControlID="tbLeaveEndDate" >
                        </asp:CalendarExtender>
                        <asp:TextBox ID="tbLeaveEndTime" runat="server" Width="40px" visible="false" ></asp:TextBox>
                              <asp:MaskedEditExtender ID="tbLeaveEndTime_MaskedEditExtender" runat="server" 
                                    ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                    Mask="99.99" MaskType="Number" TargetControlID="tbLeaveEndTime">
                                </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                                         <asp:TemplateField HeaderText="OT Lunch hrs.">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbOTLunch" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT Start">
                        <ItemTemplate>
                            <asp:TextBox ID="tbOTStartDate" runat="server" Width="70px" Enabled="False"></asp:TextBox>
                            <asp:TextBox ID="tbOTStartTime" runat="server" Width="40px" Enabled="False"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT Finish">
                       <ItemTemplate>
                        <asp:TextBox ID="tbOTEndDate" runat="server" Width="70px" placeholder="__/__/____"  onkeydown="return false;"></asp:TextBox>
                        <asp:CalendarExtender ID="tbOTEndDate_CalendarExtender" runat="server"
                            Enabled="true" Format="dd/MM/yyyy" TargetControlID="tbOTEndDate" >
                        </asp:CalendarExtender>
                        <asp:TextBox ID="tbOTEndTime" runat="server" Width="40px" ></asp:TextBox>
                        <asp:MaskedEditExtender ID="tbOTEndTime_MaskedEditExtender" runat="server" 
                                    ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                    Mask="99.99" MaskType="Number" TargetControlID="tbOTEndTime">
                                </asp:MaskedEditExtender>
                       </ItemTemplate>
                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="Need Pickup">
                        <ItemTemplate >
                            <asp:CheckBox ID="cbBusLine" runat="server" Checked="True" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pickup Location">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlBusLine" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Holiday">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbHoliday" runat="server" />
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
            <br />

            <asp:GridView ID="gvReport" runat="server" BackColor="White" 
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
                    <asp:BoundField DataField="shift" HeaderText="ShiftDay" />
                    <asp:BoundField DataField="dept" HeaderText="Dept." />
                    <asp:BoundField DataField="empno" HeaderText="Emp. No." />
                    <asp:BoundField DataField="name" HeaderText="Name" />
                      <asp:BoundField DataField="workdate" HeaderText="Date" />
               <asp:TemplateField HeaderText="worktype">
                         <ItemTemplate>
                             <asp:TextBox ID="lbHoliday" runat="server" Width="80" Enabled="False"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Shift Day">
                         <ItemTemplate>
                            <asp:DropDownList ID="ddlShiftDay" runat="server" AutoPostBack="True" onselectedindexchanged="ddlShiftDay_Selectedindexchanged" Enabled="False">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Leave Code">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlleaveCode" runat="server" AutoPostBack="True" onselectedindexchanged="ddlleaveCode_Selectedindexchanged"></asp:DropDownList>                      
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Day/Hrs">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlLeaveCase" runat="server" Width ="80"  AutoPostBack="True" onselectedindexchanged="ddlLeaveCase_Selectedindexchanged" Visible="false">
                                <asp:ListItem Value="">-Select-</asp:ListItem>
                                <asp:ListItem Value="D">วัน Day</asp:ListItem>
                                <asp:ListItem Value="H">ชม. Hour</asp:ListItem>

                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Leave Start">
                        <ItemTemplate>
                            <asp:TextBox ID="tbLeaveStartDate" runat="server" Width="70px" placeholder="__/__/____"  onkeydown="return false;" Visible="false"></asp:TextBox>
                        <asp:CalendarExtender ID="tbLeaveStartDate_CalendarExtender" runat="server"
                            Enabled="true" Format="dd/MM/yyyy" TargetControlID="tbLeaveStartDate" >
                        </asp:CalendarExtender>
                        <asp:TextBox ID="tbLeaveStartTime" runat="server" Width="40px" Visible="false"></asp:TextBox>
                             <asp:MaskedEditExtender ID="tbLeaveStartTime_MaskedEditExtender" runat="server" 
                                    ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                    Mask="99.99" MaskType="Number" TargetControlID="tbLeaveStartTime">
                                </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Leave Finish">
                        <ItemTemplate>
                             <asp:TextBox ID="tbLeaveEndDate" runat="server" Width="70px" placeholder="__/__/____"  onkeydown="return false;" visible="false"></asp:TextBox>
                        <asp:CalendarExtender ID="tbLeaveEndDate_CalendarExtender" runat="server"
                            Enabled="true" Format="dd/MM/yyyy" TargetControlID="tbLeaveEndDate" >
                        </asp:CalendarExtender>

                        <asp:TextBox ID="tbLeaveEndTime" runat="server" Width="40px" visible="false" ></asp:TextBox>
                              <asp:MaskedEditExtender ID="tbLeaveEndTime_MaskedEditExtender" runat="server" 
                                    ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                    Mask="99.99" MaskType="Number" TargetControlID="tbLeaveEndTime">
                                </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                                         <asp:TemplateField HeaderText="OT Lunch hrs.">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbOTLunch" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT Start">
                        <ItemTemplate>
                            <asp:TextBox ID="tbOTStartDate" runat="server" Width="70px" Enabled="False"></asp:TextBox>
                            <asp:TextBox ID="tbOTStartTime" runat="server" Width="40px" Enabled="False"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT Finish">
                       <ItemTemplate>
                        <asp:TextBox ID="tbOTEndDate" runat="server" Width="70px" placeholder="__/__/____"  onkeydown="return false;"></asp:TextBox>
                        <asp:CalendarExtender ID="tbOTEndDate_CalendarExtender" runat="server"
                            Enabled="true" Format="dd/MM/yyyy" TargetControlID="tbOTEndDate" >
                        </asp:CalendarExtender>
                        <asp:TextBox ID="tbOTEndTime" runat="server" Width="40px" ></asp:TextBox>
                        <asp:MaskedEditExtender ID="tbOTEndTime_MaskedEditExtender" runat="server" 
                                    ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                    Mask="99.99" MaskType="Number" TargetControlID="tbOTEndTime">
                                </asp:MaskedEditExtender>
                       </ItemTemplate>
                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="Need Pickup">
                        <ItemTemplate >
                            <asp:CheckBox ID="cbBusLine" runat="server" Checked="True" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pickup Location">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlBusLine" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Holiday">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbHoliday" runat="server" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
