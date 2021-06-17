<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS_SUB.Master"
 EnableEventValidation = "false"  CodeBehind="PlanScheduleManfOrder.aspx.vb" Inherits="MIS_HTI.PlanScheduleManfOrder" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc4" %>
<%@ Register src="../UserControl/shiftD.ascx" tagname="shiftD" tagprefix="uc5" %>
<%@ Register src="../UserControl/docTypeD.ascx" tagname="docTypeD" tagprefix="uc6" %>
<%@ Register src="../UserControl/GridviewShow.ascx" tagname="GridviewShow" tagprefix="uc7" %>
<%@ Register src="../UserControl/DropDownListUserControl.ascx" tagname="DropDownListUserControl" tagprefix="uc8" %>
<%@ Register Src="~/UserControl/Date.ascx" TagPrefix="uc1" TagName="Date" %>

<%@ Register src="../UserControl/DateTextChange.ascx" tagname="DateTextChange" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px; 
        }
        .auto-style8 {
            width: 128px;
            height: 19px;
        }
        .auto-style9 {
            width: 146px;
            height: 19px;
        }
        .auto-style29 {
            width: 227px;
            height: 18px;
        }
        .auto-style30 {
            width: 128px;
            height: 18px;
        }
        .auto-style31 {
            width: 146px;
            height: 18px;
        }
        .auto-style35 {
            width: 212px;
            height: 19px;
        }
        .auto-style36 {
            width: 212px;
            height: 18px;
        }
        .auto-style37 {
            width: 227px;
            height: 19px;
        }
        .auto-style38 {
            width: 1249px;
        }
        .auto-style40 {
            height: 19px;
        }
        .auto-style43 {
            height: 16px;
        }
    </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
            window.moveTo(0, 0);
            window.resizeTo(screen.availWidth, screen.availHeight);
        });

        function gridviewScrollShow() {
            gridView1 = $('#<%=GvProcess.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 400,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 7,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize: 14
            });
        }
    <%--    function isNumber(num) {
           // alert(num);
            return Number(parseInt(num.replace(/,/g, "").replace(/ /g, "")));
        }
        function chkQty(elm) {
            //event.isDefaultPrevented();
            //sumPlanQty();
            //alert('1');
            var row = elm.parentNode.parentNode;
            var moType = row.cells[2].innerHTML;
            var valPlanQty = elm.value.replace(/,/g, "")
            var planQty = 0
            if (valPlanQty != '') planQty = isNumber(valPlanQty);
            if (planQty < 0) {
                planQty = 0;
                elm.value = planQty;
            }
            //alert('2');
            var balQty = isNumber($("#<%=lbMoBalQty.ClientID %>").text());
            var valWipQty = isNumber($("#<%=lbWIPQty.ClientID %>").text());
            var wipQty = 0;
            //alert('3=' + valWipQty);
            if (valWipQty != "") { wipQty = valWipQty; }
            //alert('4=' + valWipQty);
            if (wipQty > balQty) balQty = wipQty;
             var planedQty = 0;
            //Number(parseInt($("#<%=lbPlanedQty.ClientID %>").text().replace(/,/g, "")));
            valPlanQty = $("#<%=lbPlanedQty.ClientID %>").text().replace(/,/g, "")
            if (valPlanQty != '') planedQty = Number(parseInt(valPlanQty));
            var planQtyLabel = isNumber($("#<%=lbPlanQty.ClientID %>").text());
            
            if (planQtyLabel + planedQty > balQty){
               alert("Plan Qty > Planed Qty + Bal Qty!!");
                var qtyChk = planQtyLabel + planedQty  - planQty;
                qtyChk = balQty - qtyChk
                if (qtyChk < 0) qtyChk = 0;
                elm.value = qtyChk;
                planQty = qtyChk;
                elm.focus();
                return false;
                //sumPlanQty();
            }
            //alert('4');
            var fix_labor = isNumber($("#<%=lbFixLabor.ClientID %>").text());
            var std_labor = isNumber($("#<%=lbStdLabor.ClientID %>").text());
            var fix_mach = isNumber($("#<%=lbFixMach.ClientID %>").text());
            var std_mach = isNumber($("#<%=lbStdMach.ClientID %>").text());
            //alert(sstdMch);
            var uTime = 0
            if (planQty > 0)
            {
                 if (moType == 'M') uTime = Math.ceil((fix_mach + (planQty * std_mach)) / 60);
                 else uTime = Math.ceil((fix_labor + (planQty * std_labor)) / 60);
            }
            row.cells[10].innerHTML = uTime;
            uTime = 0
            var sum_qty = planQtyLabel + planedQty
            if (sum_qty > 0) {
                if (moType == 'M') uTime = Math.ceil((fix_mach + (sum_qty * std_mach)) / 60);
                else uTime = Math.ceil((fix_labor + (sum_qty * std_labor)) / 60);
            }
            row.cells[11].innerHTML = uTime;
        } 
        
        function checkCancel(elm) {
            var row = elm.parentNode.parentNode;
            var capa = isNumber(row.cells[3].innerHTML);
            var allUsageTime = isNumber(row.cells[4].innerHTML);
            var usageTime = isNumber(row.cells[9].innerHTML);            
            if (row.checked) row.cells[4].innerHTML = allUsageTime - usageTime;
            //sumPlanQty();
        }
        function sumPlanQty(){
            var pQty = 0;
            $("#<%=gvShow.ClientID%> tr:has(td)").each(function () {
                if ($(this).find("input[type=checkbox][id*='cbCancel']").prop("checked")) {
                    pQty += 0;
                }
                else {
                    var getPlanQty = $(this).find($("[id*='tbPlanQty']")).val();
                    if (getPlanQty == "") getPlanQty = 0;
                    pQty += isNumber(getPlanQty);
                }
                $("#<%=lbPlanQty.ClientID%>").text(pQty);
            });          
        }

        function set_urgent(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                row.style = "font-weight:bold;white-space: nowrap;";
            }
            else {
                //If not checked change back to original color
                row.style = "font-weight:normal;white-space: nowrap;";
            }
        }--%>

   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table bgcolor="#FF9933" style="width: 95%;">
                <tr>
                    <td style="width: 10%;">
                        <asp:Label ID="Label45" runat="server" Text="Plan Date"></asp:Label>
                    </td>
                    <td style="width: 15%;">
                        <uc3:DateTextChange ID="ucDatePlan" runat="server" />
                    </td>
                    <td style="width: 10%;">
                        <asp:Label ID="Label54" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td style="width: 15%;">
                        <uc8:DropDownListUserControl ID="UcDdlMoType" runat="server" />
                    </td>
                    <td style="width: 10%;">
                        <asp:Label ID="Label55" runat="server" Text="MO No"></asp:Label>
                    </td>
                    <td style="width: 15%;">
                        <asp:TextBox ID="TbMoNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 95%;">
                <tr>
                    <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x" class="auto-style38">
                        <asp:Button ID="btSearch" runat="server" Text="Search" />
                        &nbsp;<asp:Button ID="btSave" runat="server" Text="Save" OnClientClick="sumPlanQty();" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GvProcess" runat="server" CssClass="table">
            </asp:GridView>
            <asp:Panel ID="PnShowEdit" runat="server">
                <table style="width: 95%;">
                    <tr>
                        <td bgcolor="White" class="auto-style35">
                            <asp:Label ID="Label7" runat="server" Text="Product Item"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style37">
                            <asp:Label ID="lbItem" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style8">
                            <asp:Label ID="Label9" runat="server" Text="Item Name"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbDesc" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="Label23" runat="server" Text="Specifaction"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbSpec" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="auto-style35">
                            <asp:Label ID="Label21" runat="server" Text="Sale Order"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style37">
                            <asp:Label ID="lbSaleDocNo" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style8">
                            <asp:Label ID="Label13" runat="server" Text="Cust Name"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbCustName" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="Label59" runat="server" Text="S/O Due Date"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbSoDueDate" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="auto-style35">
                            <asp:Label ID="Label56" runat="server" Text="Model"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style37">
                            <asp:Label ID="lbModel" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style8">
                            <asp:Label ID="Label57" runat="server" Text="Cust WO"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbCustWo" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="Label58" runat="server" Text="Cust Line"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbCustLine" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="auto-style35">
                            <asp:Label ID="Label5" runat="server" Text="MO"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style37">
                            <asp:Label ID="lbMoType" runat="server" ForeColor="Blue"></asp:Label>
                            -<asp:Label ID="lbMONo" runat="server" ForeColor="Blue"></asp:Label>
                            -<asp:Label ID="lbMoSeq" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style8">
                            <asp:Label ID="Label26" runat="server" Text="W/C"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbWC" runat="server" ForeColor="Blue"></asp:Label>
                            &nbsp;-&nbsp;<asp:Label ID="lbWcName" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="Label27" runat="server" Text="Process"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbProcess" runat="server" ForeColor="Blue"></asp:Label>
                            -<asp:Label ID="lbProcessName" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="auto-style35">
                            <asp:Label ID="Label17" runat="server" Text="Production Qty"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style37">
                            <asp:Label ID="lbQty" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style8">
                            <asp:Label ID="lbl555" runat="server" Text="Completed Qty"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbCompleteQty" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbl556" runat="server" Text="Scrap Qty"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lblScarpQty" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="auto-style35" style="background-color: lightblue">
                            <asp:Label ID="Label35" runat="server" Text="MO Bal Qty"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style37" style="background-color: lightblue">
                            <asp:Label ID="lbMoBalQty" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style8" style="background-color: lightblue">
                            <asp:Label ID="lbl557" runat="server" Text="WIP Qty"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9" style="background-color: lightblue">
                            <asp:Label ID="lbWIPQty" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="Label33" runat="server" Text="Std Time(s)"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbStdTime" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="auto-style36">
                            <asp:Label ID="Label24" runat="server" Text="Unit"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style29">
                            <asp:Label ID="lblUnit" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style30">
                            <asp:Label ID="Label62" runat="server" Text="Operator"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style31">
                            <asp:Label ID="lbOperator" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style31">
                            &nbsp;</td>
                        <td bgcolor="White" class="auto-style31">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="auto-style43" colspan="6" style="background-color: #3366FF"></td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="auto-style35">
                            <asp:Label ID="Label30" runat="server" Text="Plan Date"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style37">
                            <asp:Label ID="lbPlanDate" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style8">
                            <asp:Label ID="Label60" runat="server" Text="Record Seq"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbRecordSeq" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="Label47" runat="server" Text="Plan Machine"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbPlanMach" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="auto-style35">
                            <asp:Label ID="Label37" runat="server" Text="Planed Qty"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style37">
                            <asp:Label ID="lbPlanedQty" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style8">
                            <asp:Label ID="Label38" runat="server" Text="Transfer Qty"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbTransferQty" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="Label61" runat="server" Text="Real Machine"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:Label ID="lbRealMach" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="auto-style35">
                            <asp:Label ID="Label50" runat="server" Text="Plan Qty"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style37">
                            <asp:TextBox ID="TbPlanQty" runat="server" min="1" placeholder="Qty" TextMode="Number" Width="50px" AutoPostBack="True"></asp:TextBox>
                            <asp:Label ID="lbPlanQty" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style8">
                            <asp:Label ID="Label48" runat="server" Text="Set Seq"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:TextBox ID="TbSetSeq" runat="server"></asp:TextBox>
                            <asp:Label ID="lbPlanSetSeq" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:CheckBox ID="CbUrgent" runat="server" Text="Over Time" />
                            <asp:Label ID="lbPlanUrgent" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style9">
                            <asp:CheckBox ID="CbPlan" runat="server" Checked="True" Text="Plan " />
                            <asp:Label ID="lbPlanStatus" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="auto-style35">
                            <asp:Label ID="Label53" runat="server" Text="Time Usage(HH:MM:SS)"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style37">
                            <asp:Label ID="lbUsageTime" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style8">
                            <asp:Label ID="Label52" runat="server" Text="Note"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style40" colspan="3">
                            <asp:TextBox ID="TbNote" runat="server" Width="250px"></asp:TextBox>
                            <asp:Label ID="lbPlanNote" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="GvBOM" runat="server" CssClass="table">
                </asp:GridView>
            </asp:Panel>
            <%--<br />--%>
            <%--<hr style="color:white;" />--%>
      <%--      <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="Grid-Header" 
                BackColor="#666666" BorderColor="#666666" BorderStyle="Double" BorderWidth="2px" CellSpacing="4" 
                CellPadding="4">--%>
    
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
