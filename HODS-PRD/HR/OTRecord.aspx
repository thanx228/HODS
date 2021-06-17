<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="OTRecord.aspx.vb" Inherits="MIS_HTI.OTRecord" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<%@ Register src="../UserControl/DateTextChange.ascx" tagname="DateTextChange" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollShow();
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
        </script>
     <style type="text/css">
         .auto-style15 {
             width: 297px;
         }
         .auto-style16 {
             width: 73px;
         }
         .auto-style17 {
             width: 68px;
         }
         .auto-stylestyl10 {
             z-index : 100;
         }

         .Hide { display:none; }
         .auto-style19 {
             width: 95%;
             height: 28px;
         }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

             </ContentTemplate>
           </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 95%;">
                <tr>
                    <td bgcolor="White" class="auto-style17">
                        <asp:Label ID="Label4" runat="server" Text="Dept"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:CheckBoxList ID="cblDept" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="auto-style17">
                        <asp:Label ID="Label5" runat="server" Text="Date"></asp:Label>
                    </td>
                    <td bgcolor="White" class="auto-style15">
                        <uc1:DateTextChange ID="ucDate" runat="server" />
                    </td>
                    <td bgcolor="White" class="auto-style16">
                        <asp:Label ID="lbEmpNo" runat="server" Text="EmpNo"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbEmpNo" runat="server" Width="80px"></asp:TextBox>
                        <asp:Label ID="lbGrp" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="Label8" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="auto-style17">
                        <asp:Label ID="Label6" runat="server" Text="Shift"></asp:Label>
                    </td>
                    <td bgcolor="White" class="auto-style15">
                        <asp:RadioButtonList ID="rdlShift" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="D">Day</asp:ListItem>
                            <asp:ListItem Value="N">Night</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td bgcolor="White" class="auto-style16">
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
                <tr>
                    <td bgcolor="White" class="auto-style17">&nbsp;</td>
                    <td bgcolor="White" colspan="3">
                        <asp:Label ID="lbNote" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 95%;">
                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">&nbsp;<asp:Button ID="btRecord" runat="server" Text="NEW" />
                        <asp:Button ID="btEdit" runat="server" Text="Edit" />
                        <asp:Button ID="btSave" runat="server" c  Text="SAVE" />
                        <asp:Button ID="btUpdate" runat="server" Text="Update" />
                        <asp:Button ID="btCancel" runat="server" Text="Cancel" />
                        <asp:Button ID="btShowRe" runat="server" Text="Report" />
                        <asp:Button ID="btPrint" runat="server" Text="Print Report" />
                        <asp:Button ID="btExcel" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <uc2:CountRow ID="CountRow1" runat="server" />
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="COMP_CODE" HeaderText="Comp" />
                    <asp:BoundField DataField="DEPT_NAME" HeaderText="Dept. Name" />
                    <asp:BoundField DataField="SHIFTDAYS" HeaderText="Shift Day" />
                    <asp:BoundField DataField="WORK_TYPE" HeaderText="Work Type" />
                    <asp:BoundField DataField="EMP_CODE" HeaderText="Code" />
                    <asp:BoundField DataField="EMP_NAME" HeaderText="Name" />
                    <asp:BoundField DataField="JOB_NAME" HeaderText="JOB" />
                    <asp:BoundField DataField="WORK_DATE" HeaderText="Work Date">
                    <HeaderStyle CssClass="Hide" />
                    <ItemStyle CssClass="Hide" />
                    </asp:BoundField>
                    <asp:BoundField DataField="workStartDate" HeaderText="Date Time Start" />
                    <asp:BoundField DataField="WORK_BEGIN_TIME" HeaderText="Time Start">
                    <ItemStyle CssClass="Hide" />
                    <HeaderStyle CssClass="Hide" />
                    </asp:BoundField>
                    <asp:BoundField DataField="workEndDate" HeaderText="Date Time Finish" />
                    <asp:BoundField DataField="WORK_END_TIME" HeaderText="Time Finish">
                    <HeaderStyle CssClass="Hide" />
                    <ItemStyle CssClass="Hide" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Lunch">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbOTLunch" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Need">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbOverTime" runat="server" AutoPostBack="True" OnCheckedChanged="cbOverTime_OnCheckedChanged" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT Start">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlOTStartDate" runat="server" Width="90">
                            </asp:DropDownList>
                            <asp:TextBox ID="tbOTStartTime" runat="server" Width="40px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbOTStartTime_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99.99" MaskType="Number" TargetControlID="tbOTStartTime">
                            </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OT Finish">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlOTEndDate" runat="server" Width="90">
                            </asp:DropDownList>
                            <asp:TextBox ID="tbOTEndTime" runat="server" Width="40px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbOTEndTime_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99.99" MaskType="Number" TargetControlID="tbOTEndTime">
                            </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Need">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbBusLine" runat="server" AutoPostBack="True" Checked="True" onselectedindexchanged="cbBusLine_OnCheckedChanged" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pick up">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlBusLine" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Need">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlLeaveCase" runat="server" AutoPostBack="True" onselectedindexchanged="ddlLeaveCase_Selectedindexchanged" Width="80">
                                <asp:ListItem Value="N">ทำงาน</asp:ListItem>
                                <asp:ListItem Value="D">ลาเต็มวัน</asp:ListItem>
                                <asp:ListItem Value="H">ลาเป็นชั่วโมง</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Leave">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlleaveCode" runat="server" Visible="false" Width="80">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Leave Start">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlLeaveStartDate" runat="server" Visible="false" Width="90">
                            </asp:DropDownList>
                            <asp:TextBox ID="tbLeaveStartTime" runat="server" Visible="false" Width="40px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbLeaveStartTime_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99.99" MaskType="Number" TargetControlID="tbLeaveStartTime">
                            </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Leave Finish">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlLeaveEndDate" runat="server" visible="false" Width="90">
                            </asp:DropDownList>
                            <asp:TextBox ID="tbLeaveEndTime" runat="server" visible="false" Width="40px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbLeaveEndTime_MaskedEditExtender" runat="server" ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99.99" MaskType="Number" TargetControlID="tbLeaveEndTime">
                            </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DEPT_CODE" HeaderText="Dept.">
                    <HeaderStyle CssClass="Hide" />
                    <ItemStyle CssClass="Hide" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DOCNO_OT" HeaderText="Doc No">
                    <HeaderStyle CssClass="Hide" />
                    <ItemStyle CssClass="Hide" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OT_STARTED_DATE" HeaderText="OT STARTED DATE">
                    <HeaderStyle CssClass="Hide" />
                    <ItemStyle CssClass="Hide" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OT_STARTED_TIME" HeaderText="OT STARTED TIME">
                    <HeaderStyle CssClass="Hide" />
                    <ItemStyle CssClass="Hide" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OT_FINISHED_DATE" HeaderText="OT FINISHED DATE">
                    <HeaderStyle CssClass="Hide" />
                    <ItemStyle CssClass="Hide" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OT_FINISHED_TIME" HeaderText="OT FINISHED TIME">
                    <HeaderStyle CssClass="Hide" />
                    <ItemStyle CssClass="Hide" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" HorizontalAlign="Center" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:GridView ID="gvReport" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
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
            <asp:PostBackTrigger ControlID="btExcel" />
            <asp:AsyncPostBackTrigger ControlID="rdlShift" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
