<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="OTforAdmin.aspx.vb" Inherits="MIS_HTI.OTforAdmin" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">

        .style8
        {
            width: 161px;
        }
        </style>

     <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollEdit();
            gridviewScrollShowRe();
        });

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
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
                        &nbsp;</td>
                    <td class="style10" style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style8" style="background-color: #FFFFFF">
                        Shift Day/Night</td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:RadioButtonList ID="rdlShift" runat="server" AutoPostBack="true" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">Day</asp:ListItem>
                            <asp:ListItem Value="1">Night</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="style10" style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td class="style10" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateFrom" runat="server" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="tbDateTo" runat="server" Visible="False"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        &nbsp;<asp:Button ID="btEdit" runat="server" Text="Edit" />
                        <asp:Button ID="btUpdate" runat="server" Text="Update" />
                        <asp:Button ID="btCancel" runat="server" Text="Cancel" />
                        <br />
                    </td>
                </tr>
            </table>
            <uc2:CountRow ID="CountRow1" runat="server" />
            <asp:GridView ID="gvEdit" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>

                  <asp:ButtonField ButtonType="Image" CommandName="OnDelete" HeaderText="Delete" 
                    ImageUrl="~/Images/delete.gif" Text="Delete">
                <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>

                   <%-- <asp:TemplateField HeaderText="Absence">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbAbsenceEdit" runat="server" />
                            </CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Absence  Time hrs.">
                        <ItemTemplate>
                            <asp:TextBox ID="tbAbsenceTimeEdit" runat="server" Width="30px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:BoundField DataField="DocNo" HeaderText="DocNo" />
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
<%--                    <asp:BoundField DataField="Start Time" HeaderText="OT Start Time" />
--%>
                       <asp:TemplateField HeaderText="OT Start Time">
                        <ItemTemplate>
                            <asp:TextBox ID="tbStartTimeEdit" runat="server" Width="40px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="tbStartTimeEdit_MaskedEditExtender" runat="server" 
                                ClearMaskOnLostFocus="False" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                Mask="99.99" MaskType="Number" TargetControlID="tbStartTimeEdit">
                            </asp:MaskedEditExtender>
                        </ItemTemplate>
                    </asp:TemplateField>


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
            <br />

           



            <br />
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
