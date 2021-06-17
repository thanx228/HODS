<%@ Page Title="" EnableEventValidation="false" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS_SUB.Master" CodeBehind="PlanScheduleAdd.aspx.vb" Inherits="MIS_HTI.PlanScheduleAdd" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc1" %>
<%@ Register src="../UserControl/DropDownListUserControl.ascx" tagname="DropDownListUserControl" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .bgWhite {
            background-color: white;
        }

        .width75 {
            width: 75%;
        }

        .width100 {
            width: 100%;
        }

        .textRight {
            text-align: right;
        }

        .displayNone {
            display: none;
        }

        .numberOnly {
            background-color: #FFFFCC;
            border: 1px solid #c4c4c4;
            font-size: 13px;
            padding: 4px 4px 4px 4px;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            box-shadow: 0px 0px 8px #d9d9d9;
            -moz-box-shadow: 0px 0px 8px #d9d9d9;
            -webkit-box-shadow: 0px 0px 8px #d9d9d9;
            margin-left: 5px;
        }
    </style>


    <script type="text/javascript" src="../Scripts/GridviewScroll/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/GridviewScroll/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../Scripts/gridviewScroll.min.js"></script>
    <link href="../Scripts/GridviewScroll/GridviewScroll.css" rel="stylesheet" />

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


        function GvShowScroll() {
            gridView1 = $('#<%=gvShow.ClientID%>').gridviewScroll({
                width: screen.availWidth,
                height: screen.availHeight * 0.6,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 0,
                arrowsize: 30,
                varrowtopimg: "../../Images/arrowvt.png",
                varrowbottomimg: "../../Images/arrowvb.png",
                harrowleftimg: "../../Images/arrowhl.png",
                harrowrightimg: "../../Images/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize: 14
            });
        }

        function GvSelectScroll() {
            gridView1 = $('#<%=gvSelect.ClientID%>').gridviewScroll({
                width: screen.availWidth,
                height: screen.availHeight * 0.6,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 0,
                arrowsize: 30,
                varrowtopimg: "../../Images/arrowvt.png",
                varrowbottomimg: "../../Images/arrowvb.png",
                harrowleftimg: "../../Images/arrowhl.png",
                harrowrightimg: "../../Images/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize: 14
            });
        }



        function isNumber(num) {
            num = Number(parseInt(num.replace(/,/g, "").replace(/ /g, "")));
            if (Number.isNaN(num)) {
                num = 0;
            }
            return num;
        }
        function chkQty(elm) {
            var row = elm.parentNode.parentNode;
            var planQty = isNumber(parseInt(elm.value));
            var balQty = isNumber(parseInt(row.cells[17].innerHTML));
            var planedQty = isNumber(parseInt(row.cells[15].innerHTML));
            var TimeStd = isNumber(parseInt(row.cells[5].innerHTML));

            var chkQty = balQty + planedQty;
            if (planQty > isNumber(chkQty)) {
                alert("Plan Qty is over Planed Qty + Bal Qty!!");
                elm.value = chkQty;
                planQty = chkQty;
            }
            row.cells[6].innerHTML = Math.ceil((planQty * TimeStd) / 60);
            var sMan = 0;
            var sumUsageTime = 0;
            var sumBalTime = isNumber($('#<%=lbStdTime.ClientID %>').html());
            $("#<%=gvSelect.ClientID%> tr:has(td)").each(function () {
                var $tdElement19 = $(this).find("td:eq(6)"); //Cache Quantity column.
                var usageTime = parseInt($tdElement19.text());
                sumUsageTime += usageTime;
                sumBalTime -= usageTime;
            });
            //alert(sumBalTime);
            $('#<%=lbUsageTime.ClientID%>').html(sumUsageTime);
            $('#<%=lbBalTime.ClientID%>').html(sumBalTime);

        }
        // onchange="ShowProcess();"
        function MouseEvents(objRef, evt) {

            if (objRef.getElementsByTagName("input").length != "0") {
                var checkbox = objRef.getElementsByTagName("input")[0];
                if (evt.type == "mouseover") {
                    objRef.style.backgroundColor = "grey";
                }
                else {
                    if (checkbox.checked) {
                        objRef.style.backgroundColor = "aqua";
                    }
                    else if (evt.type == "mouseout") {
                        objRef.style.backgroundColor = "white";
                    }
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="bgWhite width75" cellpadding="5" cellspacing="5">
                <tr>
                    <td style="width:20%;" class="textRight">
                        <asp:Label ID="Label8" runat="server" Text="Plan Date : "></asp:Label>
                    </td>
                   <td style="width:30%;">
                        <asp:Label ID="lbPlanDate" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td style="width:10%;" class="textRight">
                        <asp:Label ID="Label9" runat="server" Text="Work Center* : "></asp:Label>
                    </td>
                    <td style="width:40%;">
                        <asp:Label ID="lbWc" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">
                        <asp:Label ID="Label22" runat="server" Text="SO Type : "></asp:Label>
                    </td>
                    <td>
                        <uc2:DropDownListUserControl ID="UcDdlSoType" runat="server" />
                    </td>
                    <td class="textRight">
                        <asp:Label ID="Label23" runat="server" Text="SO No. : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbSoNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">
                        <asp:Label ID="Label24" runat="server" Text="MO Type : "></asp:Label>
                    </td>
                    <td>
                        <uc2:DropDownListUserControl ID="UcDdlMoType" runat="server" />
                    </td>
                    <td class="textRight">
                        <asp:Label ID="Label25" runat="server" Text="MO No : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbMoNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">
                        <asp:Label ID="Label26" runat="server" Text="Item : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbItem" runat="server"></asp:TextBox>
                    </td>
                    <td class="textRight">
                        <asp:Label ID="Label27" runat="server" Text="Spec : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="textRight">
                        <asp:Label ID="Label10" runat="server" Text="Process* : "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProcess" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="textRight">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="textRight">
                        <asp:Label ID="Label17" runat="server" Text="End Plan Start Date*"></asp:Label>
                    </td>
                    <td>
                        <uc1:Date ID="ucEndPlanStart" runat="server" />
                    </td>
                    <td class="textRight">&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="cbMoWorking" runat="server" Text="WIP QTY&gt;0*" />
                    </td>
                </tr>
            </table>
            <table class="bgWhite width75">
                <tr>
                    <td align="center"
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btSelect" runat="server" Text="Select" Width="100px" UseSubmitBehavior="false" />
                        &nbsp;<asp:Button ID="btSearch" runat="server" Text="Search" Width="100px" UseSubmitBehavior="false" />
                        &nbsp;<asp:Button ID="btClear" runat="server" Text="Clear" Width="100px" UseSubmitBehavior="false" />
                        &nbsp;<asp:Button ID="btSave" runat="server" Text="Save" Width="100px" UseSubmitBehavior="false" />
                        &nbsp;<asp:Button ID="btUpdate" runat="server" Text="Update"
                            style="margin-top: 2px" Visible="False" />
                        &nbsp;&nbsp;</td>
                </tr>
            </table>

            <table class="bgWhite width75">
                <tr>
                    <td align="center">
                        <asp:Label ID="Label21" runat="server" Text="Unit Of Time = Minute"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label13" runat="server" Text="Standard Hour"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label14" runat="server" Text="Usage Hour"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label15" runat="server" Text="Balance Hour"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="background-color: #CCCCCC">
                        <asp:Label ID="lbWorkType" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #CCCCCC">
                        <asp:Label ID="lbStdTime" runat="server" ForeColor="Blue">0</asp:Label>
                    </td>
                    <td align="center" style="background-color: #CCCCCC">
                        <asp:Label ID="lbUsageTime" runat="server" ForeColor="Blue">0</asp:Label>
                    </td>
                    <td align="center" style="background-color: #CCCCCC">
                        <asp:Label ID="lbBalTime" runat="server" ForeColor="Blue">0</asp:Label>
                    </td>
                </tr>
            </table>

            <asp:TabContainer ID="TcMain" runat="server" ActiveTabIndex="0" AutoPostBack="True" Width="75%" Height="100px" >

                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" style="position: absolute;">
                    <HeaderTemplate>
                        Plan
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:GridView ID="gvSelect" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Cancle">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbCancled" runat="server" AutoPostBack="True" OnCheckedChanged="cbCancled_OnCheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Set Seq">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbSetSeq" runat="server" Width="30px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OT">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbUrgent" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Note">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbMch" runat="server" Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="TA003" HeaderText="MO Seq." />
                                <asp:BoundField DataField="TIME_STD" DataFormatString="{0:#,#}" HeaderText="Time Standard(Sec)">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIME_USAGE" DataFormatString="{0:#,#}" HeaderText="Time Usage(Min)">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TA001" HeaderText="MO Type." />
                                <asp:BoundField DataField="TA002" HeaderText="MO No." />
                                <asp:BoundField DataField="MA002" HeaderText="Cust Name" />
                                <asp:BoundField DataField="TA035" HeaderText="Spec" />
                                <asp:BoundField DataField="WorkCenter" HeaderText="W/C" />
                                <asp:BoundField DataField="TA015" DataFormatString="{0:#,#}" HeaderText="MO Qty">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TA011" DataFormatString="{0:#,#}" HeaderText="Finished Qty">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="WipQty" DataFormatString="{0:#,#}" HeaderText="WIP Qty">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PlanedQty" DataFormatString="{0:#,#}" HeaderText="Planed Qty">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Plan Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbPlanQty" CssClass="numberOnly" runat="server" AutoPostBack="True" min="0" OnTextChanged="tbPlanQty_TextChanged" step="1" TextMode="Number" Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="balQty" DataFormatString="{0:#,#}" HeaderText="MO Bal Qty">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TA008" HeaderText="Plan Start Date" />
                                <asp:BoundField DataField="TA010" HeaderText="Plan Complete Date" />
                                <asp:BoundField DataField="TA006" HeaderText="Item" />
                                <asp:BoundField DataField="TA004" HeaderText="Operation" />

                                <asp:BoundField DataField="MF019" DataFormatString="{0:#,#}" HeaderText="Batch Qty" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HR8QTY" DataFormatString="{0:#,#}" HeaderText="8 Hour Qty" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MF009" HeaderText="Fiexed Man Hour" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MF010" HeaderText="Dynamic Man Hour" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MF024" HeaderText="Fiexed Machine Hour" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MF025" HeaderText="Dynamic Machine Hour" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                               

                                <asp:BoundField DataField="PlanSeq" HeaderText="Plan Seq" />
                                <asp:BoundField DataField="PlanQty" DataFormatString="{0:#,#}" HeaderText="Plan Qty Record">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ACTUAL_TRAN_QTY_APP" DataFormatString="{0:#,#}" HeaderText="Actual Transer Qty(App)">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ACTUAL_TRAN_QTY_NOT" DataFormatString="{0:#,#}" HeaderText="Actual Transer Qty(Not App)">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PLAN_BAL_QTY" DataFormatString="{0:#,#}" HeaderText="Plan Balance Qty">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PlanStatus" HeaderText="Plan Status Record" />
                                <asp:BoundField DataField="PlanSeqSet" HeaderText="Plan Set Seq Record" />
                                <asp:BoundField DataField="Urgent" HeaderText="Urgent Record" />
                                <asp:BoundField DataField="PlanNote" HeaderText="Plan Note Record" />
                                <asp:BoundField DataField="PlanTimeStd" HeaderText="PlanTimeStd Record" />
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" Wrap="False" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                       
               <table  class="bgWhite width100" cellpadding="5" cellspacing="5">
                <tr>
                    <td class="auto-style6">
                        <asp:Label ID="Label5" runat="server" Text="บันทึกการวางแผนแล้ว" ForeColor="Black"></asp:Label>
                    </td>
                    <td class="auto-style6">
                        <asp:Label ID="Label6" runat="server" Text="ยกเลิกแผนที่วางไว้" ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="เพิ่มแผนแต่ยังไม่บันทึก" ForeColor="Magenta"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="แผนถูกดำเนินการไปแล้วบางส่วน" ForeColor="Green"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="แผนถูกดำเนินการครบถ้วนแล้ว" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>

                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2" style="position: absolute;">
                    <HeaderTemplate>
                        Search
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" runat="server" AutoPostBack="True" OnCheckedChanged="cbSelect_OnCheckedChanged"  />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="TA008" HeaderText="Plan Start Date" />
                                <asp:BoundField DataField="TA010" HeaderText="Plan Complete Date" />
                                <asp:BoundField DataField="TA003" HeaderText="MO Seq." />
                                <asp:BoundField DataField="TIME_STD" DataFormatString="{0:#,#}" HeaderText="Time Standard(Sec)">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIME_USAGE" DataFormatString="{0:#,#}" HeaderText="Time Usage(Min)">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TA001" HeaderText="MO Type." />
                                <asp:BoundField DataField="TA002" HeaderText="MO No." />
                                <asp:BoundField DataField="MA002" HeaderText="Cust Name" />
                                <asp:BoundField DataField="TA035" HeaderText="Spec" />
                                <asp:BoundField DataField="WorkCenter" HeaderText="WC" />
                                <asp:BoundField DataField="TA015" DataFormatString="{0:#,#}" HeaderText="MO Qty">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TA011" DataFormatString="{0:#,#}" HeaderText="Finished Qty">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="WipQty" DataFormatString="{0:#,#}" HeaderText="WIP Qty">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PlanedQty" DataFormatString="{0:#,#}" HeaderText="Planed Qty">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="waitTrnQty" DataFormatString="{0:#,#}" HeaderText="Wait Transfer Qty">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LastPlanDate1" HeaderText="Last Daily Plan Date" />
                                <asp:BoundField DataField="balQty" DataFormatString="{0:#,#}" HeaderText="MO Bal Qty">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TA006" HeaderText="Item" />
                                <asp:BoundField DataField="TA004" HeaderText="Operation" />

                                <asp:BoundField DataField="MF019" DataFormatString="{0:#,#}" HeaderText="Batch Qty" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HR8QTY" DataFormatString="{0:#,#}" HeaderText="8 Hour Qty" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MF009" HeaderText="Fiexed Man Hour" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MF010" HeaderText="Dynamic Man Hour" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>                         
                                <asp:BoundField DataField="MF024" HeaderText="Fiexed Machine Hour" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MF025" HeaderText="Dynamic Machine Hour" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="orderBy" HeaderText="Order By" />
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

                        <table class="bgWhite width100">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="ยังไม่มีการวางแผน" ForeColor="Black"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="เคยวางแผนแล้ว" ForeColor="Red"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="วางแผนไปแล้วบางส่วน" ForeColor="Magenta"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="วางจำนวนเกินยอดสั่งผลิต" ForeColor="Green"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label28" runat="server" Text="วางแผนครบแล้ว" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>       


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlProcess" />
        </Triggers>
    </asp:UpdatePanel>
    </asp:Content>