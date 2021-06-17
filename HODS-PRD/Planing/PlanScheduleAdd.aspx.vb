Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class PlanScheduleAdd
    Inherits System.Web.UI.Page

    Dim outCont As New OutputControl
    Dim dbConn As New DataConnectControl
    Dim dateCont As New DateControl
    Dim gvCont As New GridviewControl
    Dim dt As DataTable = New DataTable
    Dim dateToday As String = Date.Now.ToString("yyyyMMdd", New CultureInfo("en-US"))


    '========================================= REMARK DEVELOP : อ่านก่อน Dev ==========================================

    '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '/////////////////////////////หากจะเพิ่ม ลบ Column โปรดตรวจเช็คให้ละเอียด เพราะมีการนับลำดับ Cell////////////////////////////
    '////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    'เงื่อนไข Column : 8 Hour Qty 
    '1.floor((480 * 0.9) * 60 / StanddardTime(Src))  ||||||  ชม.การทำงาน * 9เปอร์เซนต์ * 60วินาที / ระยะเวลาผลิตต่อชิ้น  [ปัดเศษลง]
    '2.ผลลัพธ์จากสูตรข้อ 1 และ WIP และ MoBal แสดงเฉพาะ Column ที่น้อยที่สุด และต้องมากกว่า 0
    '3.ผลลัพธ์จากสูตรข้อ 2 หากมากกว่า MoBal ให้แสดงผล MoBal แต่หากน้อยกว่า MoBal ให้แสดงผล สูตรข้อ 2

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '/////////////////////////////////////////////// CHECK DATABASE ////////////////////////////////////////////////
    Dim tbMaster As String = String.Empty 'Table PlanSchedule ข้อมูลจริง // [ถ้าเป็น Test จะไม่เข้า Update SFCTA ตรงปุ่ม Save] 
    'Dim tbMaster As String = "Test" 'Table PlanScheduleTest ทดสอบ // [ถ้าเป็น Test จะไม่เข้า Update SFCTA ตรงปุ่ม Save] 
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim ddlCont As New DropDownListControl
            lbWc.Text = outCont.DecodeFrom64(Request.QueryString("wc").Trim)
            lbPlanDate.Text = outCont.DecodeFrom64(Request.QueryString("plandate"))

            Dim SQL As String = "select MW001,MW001+':'+MW002 as MW002 from CMSMW where MW005='" & lbWc.Text.Trim & "' and UDF03 <> 'N' order by MW001"
            ddlCont.showDDL(ddlProcess, SQL, VarIni.ERP, "MW002", "MW001", True)

            'sale order type
            UcDdlSoType.showDocType("22", True)
            'mo type
            UcDdlMoType.showDocType("51,52", True)

            Dim WHR As String = String.Empty
            WHR = dbConn.WHERE_EQUAL("MD001", lbWc.Text.Trim)
            SQL = "select MD001,MD002,CMSMD.UDF02,isnull(STD_TIME,0) STD_TIME from CMSMD left join (select MX002,SUM((CMSMX.UDF51+UDF52)*CMSMX.UDF53) STD_TIME from CMSMX GROUP BY MX002) CMSMX on MX002=MD001 where 1=1 " & WHR & " order by MD001 "

            Dim dr As New DataRowControl(SQL, VarIni.ERP, dbConn.WhoCalledMe)
            lbWorkType.Text = dr.Text("UDF02")
            lbStdTime.Text = dateCont.TimeFormat(dr.Number("STD_TIME"))

            showData(lbWc.Text.Trim, lbPlanDate.Text)
            Dim selIndex As Integer = 1
            If gvSelect.Rows.Count > 0 Then
                'btViewSelect_Click(sender, e)
                selIndex = 0
                'Else
                '    'btViewSearch_Click(sender, e)
            End If
            TcMain.ActiveTabIndex = selIndex
            'Botton set show
            btClear.Visible = True
            showButton()
        End If
    End Sub

    Private Sub showButton()
        Dim planDate As String = dateCont.strToDateTime(lbPlanDate.Text.Trim, "yyyyMMdd").AddDays(1).ToString("yyyyMMdd") & "0800"
        Dim dateToday1 As String = DateTime.Now.ToString("yyyyMMddHHmm", New CultureInfo("en-US"))

        Dim DatePlanAvailiable As Boolean = False
        If planDate >= dateToday1 Then
            DatePlanAvailiable = True
        End If

        'search
        btSearch.Visible = DatePlanAvailiable

        Dim setVisible As Boolean = False
        'save
        If TcMain.ActiveTabIndex = 0 AndAlso gvSelect.Rows.Count > 0 Then
            setVisible = DatePlanAvailiable
        End If
        btSave.Visible = setVisible
        'select
        setVisible = False
        If TcMain.ActiveTabIndex = 1 AndAlso gvShow.Rows.Count > 0 AndAlso SelectedRow(gvShow) > 0 Then
            setVisible = DatePlanAvailiable
        End If
        btSelect.Visible = setVisible

    End Sub

    Private Sub showData(ByVal wc As String, ByVal plandate As String)
        Dim SQL As String = ""

        Dim stdLabor As String = "case when MOCTA.TA015=0 then 0 else round(SFCTA.TA022/MOCTA.TA015,0) end"
        Dim stdMachine As String = "case when MOCTA.TA015=0 then 0 else round(SFCTA.TA035/MOCTA.TA015,0) end"

        Dim ttLabor As String = "round(((PlanQty)*(" & stdLabor & "))/60,0)"
        Dim ttMachine As String = "round(((PlanQty)*(" & stdMachine & "))/60,0)"

        Dim fldStandardTime As String = " case CMSMD.UDF02 when 'Machine' then " & stdMachine & " when 'Machine-AP100' then 0  else " & stdLabor & " end "

        Dim fldCase As String = ""
        fldCase &= " case CMSMD.UDF02 when 'Machine' then case isnull(SFCTA.TA035,0) when 0 then 0 else " & ttMachine & " end  "
        fldCase &= " when 'Machine-AP100' then 0 "
        fldCase &= " else case isnull(SFCTA.TA022,0) when 0 then 0 else " & ttLabor & "  end end  "

        Dim al As New ArrayList
        Dim fldName As ArrayList
        With New ArrayListControl(al)
            .TAL("SFCTA.TA001", "")
            .TAL("SFCTA.TA002", "")
            .TAL("SFCTA.TA003", "")
            .TAL("MOCTA.TA006", "")
            .TAL("MOCTA.TA035", "")
            .TAL("SFCTA.TA006" & VarIni.C8 & "WorkCenter", "")
            .TAL("SFCTA.TA004", "")
            .TAL("SFCTA.TA008", "")
            .TAL("MOCTA.TA010", "")
            .TAL("MOCTA.TA015", "")
            .TAL("SFCTA.TA011+SFCTA.TA014-SFCTA.TA013" & VarIni.C8 & "TA011", "0")
            .TAL("SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048" & VarIni.C8 & "WipQty", "")
            .TAL("MOCTA.TA015+SFCTA.TA013-SFCTA.TA011-SFCTA.TA014-SFCTA.TA012-SFCTA.TA016" & VarIni.C8 & "balQty", "")
            .TAL("isnull(P.PlanedQty,0)" & VarIni.C8 & "PlanedQty", "")
            .TAL("MA002", "")
            .TAL(fldStandardTime & VarIni.C8 & " TIME_STD", "")
            .TAL(fldCase & VarIni.C8 & " TIME_USAGE", "")
            .TAL("isnull(V.TC014_APP,0)+isnull(V.TC016_APP,0)" & VarIni.C8 & "ACTUAL_TRAN_QTY_APP", "Actual Transfer Qty(app)")
            .TAL("isnull(V.TC014_NOT,0)+isnull(V.TC016_NOT,0)" & VarIni.C8 & "ACTUAL_TRAN_QTY_NOT", "Actual Transfer Qty(not app)")
            .TAL(PLANSCHEDULE_T.PlanQty & "-isnull(V.TC014,0)-isnull(V.TC016,0)" & VarIni.C8 & "PLAN_BAL_QTY", "")
            .TAL(PLANSCHEDULE_T.PlanSeq, "")
            .TAL(PLANSCHEDULE_T.PlanQty, "")
            .TAL(PLANSCHEDULE_T.PlanSeqSet, "")
            .TAL(PLANSCHEDULE_T.PlanNote, "")
            .TAL(PLANSCHEDULE_T.Urgent, "")
            .TAL(PLANSCHEDULE_T.PlanStatus, "")
            .TAL(PLANSCHEDULE_T.PlanTimeStd, "")

            .TAL("BOMMF.MF019", "") 'Batch Qty
            .TAL("CAST(RIGHT(100 + FLOOR(BOMMF.MF009 / 3600),2) AS varchar) +':'+ CAST(RIGHT(100 + FLOOR((BOMMF.MF009 % 3600) / 60),2) AS varchar) +':'+ CAST(RIGHT(100 + BOMMF.MF009 % 60,2) AS varchar) MF009", "") 'Fiexed Man-Hour // Format time 0000:00:00
            .TAL("CAST(RIGHT(100 + FLOOR(BOMMF.MF010 / 3600),2) AS varchar) +':'+ CAST(RIGHT(100 + FLOOR((BOMMF.MF010 % 3600) / 60),2) AS varchar) +':'+ CAST(RIGHT(100 + BOMMF.MF010 % 60,2) AS varchar) MF010", "") 'Dynamic Man-Hour // Format time 0000:00:00
            .TAL("CAST(RIGHT(100 + FLOOR(BOMMF.MF024 / 3600),2) AS varchar) +':'+ CAST(RIGHT(100 + FLOOR((BOMMF.MF024 % 3600) / 60),2) AS varchar) +':'+ CAST(RIGHT(100 + BOMMF.MF024 % 60,2) AS varchar) MF024", "") 'Fiexed Machine-Hour // Format time 0000:00:00
            .TAL("CAST(RIGHT(100 + FLOOR(BOMMF.MF025 / 3600),2) AS varchar) +':'+ CAST(RIGHT(100 + FLOOR((BOMMF.MF025 % 3600) / 60),2) AS varchar) +':'+ CAST(RIGHT(100 + BOMMF.MF025 % 60,2) AS varchar) MF025", "") 'Dynamic Machine-Hour // Format time 0000:00:00
            .TAL("case when round(" & fldStandardTime & ",0) = 0 then 0 else FLOOR((480 * 0.9) * 60 / (" & fldStandardTime & ")) end" & VarIni.C8 & "HR8QTY", "") '(480 * 0.9) * 60 / TIME_STD

            fldName = .ChangeFormat()
        End With
        Dim strsql As New SQLString(PLANSCHEDULE_T.table_full & tbMaster & " P ", fldName)
        strsql.setLeftjoin(" left join SFCTA on SFCTA.TA001=" & PLANSCHEDULE_T.MoType & " and SFCTA.TA002=" & PLANSCHEDULE_T.MoNo & " and SFCTA.TA003=" & PLANSCHEDULE_T.MoSeq & " and SFCTA.TA006=" & PLANSCHEDULE_T.WorkCenter & " ")
        strsql.setLeftjoin(" left join MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 ")
        strsql.setLeftjoin(" left join COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027 ")
        strsql.setLeftjoin(" left join COPMA on MA001 = COPTC.TC004 ")
        strsql.setLeftjoin(" left join CMSMD on MD001 = " & PLANSCHEDULE_T.WorkCenter & " ")
        strsql.setLeftjoin(" left join " & VarIni.DBMIS & "..V_TRANSFER_SUM_DATE V on V.TC004=P.MoType and V.TC005=P.MoNo and V.TC006=P.MoSeq and V.TRANSFER_DATE=P.PlanDate ")
        strsql.setLeftjoin(" left join BOMMF on BOMMF.MF003 = SFCTA.TA003 and BOMMF.MF006 = SFCTA.TA006 and BOMMF.MF001 = MOCTA.TA006 AND BOMMF.MF002 = '01'")

        Dim whr As String = ""
        whr &= dbConn.WHERE_EQUAL(PLANSCHEDULE_T.WorkCenter, wc)
        whr &= dbConn.WHERE_EQUAL(PLANSCHEDULE_T.PlanDate, plandate)
        strsql.SetWhere(whr, True)
        strsql.SetOrderBy(PLANSCHEDULE_T.PlanSeq)

        dt = dbConn.Query(strsql.GetSQLString, VarIni.ERP, dbConn.WhoCalledMe)
        ViewState("dt") = dt
        gvCont.ShowGridView(gvSelect, dt)
        'set display none column
        Dim hideColumn As New List(Of Integer) From {
                11, 20, 21, 28, 29, 33, 34, 35, 36, 37
            }
        gvCont.hideColumn(gvSelect, hideColumn, "displayNone")
        '(lbWc.Text.Trim, plandate)
        SumUsageWorkTime(gvSelect)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", "GvSelectScroll();", True)
    End Sub

    Sub cbCancled_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cbCancled As CheckBox = DirectCast(sender, CheckBox)
        gridviewRowForeColor(cbCancled.Parent.Parent)
        Dim gvselect As GridView = DirectCast(cbCancled.Parent.Parent.Parent.Parent, GridView)
        SumUsageWorkTime(gvselect)
    End Sub

    Sub cbSelect_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cbSelect As CheckBox = DirectCast(sender, CheckBox)
        Dim gvShow As GridView = DirectCast(cbSelect.Parent.Parent.Parent.Parent, GridView)
        Dim rowSelected As Integer = SelectedRow(gvShow)
        Dim setVisible As Boolean = False
        If rowSelected > 0 Then
            setVisible = True
        End If
        btSelect.Visible = setVisible
    End Sub

    Sub gridviewRowForeColor(ByRef gr As GridViewRow)
        With New GridviewRowControl(gr)
            Dim colorFore As Drawing.Color = Drawing.Color.Black

            Dim cbCancled As CheckBox = gr.FindControl("cbCancled")
            Dim pSeq As Integer = .Number(28)
            Dim TransferQtyApp As Decimal = .Number(30)
            Dim TransferQtyNot As Decimal = .Number(31)
            If pSeq <= 0 Then
                colorFore = Drawing.Color.Magenta
            End If
            If TransferQtyApp > 0 OrElse TransferQtyNot > 0 Then
                cbCancled.Enabled = False
                cbCancled.Checked = False
                colorFore = Drawing.Color.Green
                Dim tbPlanQty As TextBox = gr.FindControl("tbPlanQty")
                If TransferQtyApp + TransferQtyNot >= outCont.checkNumberic(tbPlanQty) Then
                    colorFore = Drawing.Color.Blue
                End If
            End If
            If cbCancled.Checked Then
                colorFore = Drawing.Color.Red
            End If
            gr.ForeColor = colorFore
        End With
    End Sub

    Function SelectedRow(gv As GridView) As Integer
        Dim rowSelected As Integer = 0
        For Each gr As GridViewRow In gv.Rows
            With New GridviewRowControl(gr)
                Dim cbSelect As CheckBox = .FindControl("cbSelect")
                If cbSelect.Checked Then
                    rowSelected += 1
                End If
            End With
        Next
        Return rowSelected
    End Function


    Protected Sub tbPlanQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tbPlanQty As TextBox = DirectCast(sender, TextBox)
        Dim pQty As Decimal = outCont.checkNumberic(tbPlanQty)
        Dim gr As GridViewRow = DirectCast(tbPlanQty.Parent.Parent, GridViewRow)
        With New GridviewRowControl(gr)
            Dim pSeq As Integer = .Number(28) 'Plan Seq
            Dim mBalQty As Decimal = .Number(17) 'MO Bal Qty
            Dim planRecordQty As Decimal = .Number(29) 'Plan Qty Record
            Dim planLessQty As Decimal = 0
            If pSeq > 0 Then 'old record
                mBalQty += .Number(30) 'trasfer qty approve // Actual Transer Qty(App)
                planLessQty = .Number(31) 'transfer qty not approve // Actual Transer Qty(Not App)
            End If
            Dim valSetPlan As Decimal = pQty
            If pQty > mBalQty Then
                valSetPlan = mBalQty
            End If
            If planLessQty > 0 And pQty < planLessQty Then
                valSetPlan = planLessQty
            End If
            tbPlanQty.Text = valSetPlan
            Dim stdTime As Decimal = .Number(5)
            gr.Cells(6).Text = Math.Round((valSetPlan * stdTime) / 60, 0)
        End With
        'summary plan
        Dim gvselect As GridView = DirectCast(tbPlanQty.Parent.Parent.Parent.Parent, GridView)
        SumUsageWorkTime(gvselect)
    End Sub

    Sub SumUsageWorkTime(gv As GridView)
        Dim stdhourMch As Integer = dateCont.CalSecond(lbStdTime.Text.Trim),
          usageHourMch As Integer = 0

        For Each gr As GridViewRow In gv.Rows
            With New GridviewRowControl(gr)
                Dim cbCancled As CheckBox = .FindControl("cbCancled")
                If Not cbCancled.Checked Then
                    usageHourMch += .Number(6) * 60
                End If
            End With
        Next
        Dim setColor As Drawing.Color = Drawing.Color.Blue
        If usageHourMch > stdhourMch Then
            setColor = Drawing.Color.Red
        End If
        lbUsageTime.ForeColor = setColor
        lbBalTime.ForeColor = setColor
        stdhourMch -= usageHourMch
        lbUsageTime.Text = dateCont.TimeFormat(usageHourMch)

        lbBalTime.Text = dateCont.TimeFormat(stdhourMch)
    End Sub

    Private Sub gvSelect_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSelect.RowDataBound
        With e.Row
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim item As String = .Cells(2).Text & "-" & .Cells(3).Text & "-" & .Cells(4).Text
                For Each button As ImageButton In e.Row.Cells(0).Controls.OfType(Of ImageButton)()
                    If button.CommandName = "Delete" Then
                        button.Attributes("onclick") = "if(!confirm('Do you want to delete " + item + "?')){ return false; };"
                    End If
                Next
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
                'add link detail

                'show element on gvselect
                Dim gr As New GridviewRowControl(e.Row)
                Dim planseq As String = gr.Number(PLANSCHEDULE_T.PlanSeq)
                Dim planQty As Integer = 0
                Dim moBalQty As Decimal = gr.Number("balQty") 'outCont.checkNumberic(.Cells(17).Text) 'MO Bal Qty
                If planseq > 0 Then 'recorded
                    'plan qty
                    planQty = outCont.checkNumberic(gr.Text(PLANSCHEDULE_T.PlanQty)) 'plan qty
                Else 'plan new
                    'plan qty
                    Dim wipQty As Decimal = gr.Number("WipQty") 'outCont.checkNumberic(.Cells(14).Text) 'wip Qty
                    planQty = If(wipQty > 0, wipQty, moBalQty)
                End If
                Dim TransferQtyApp As Decimal = gr.Number("ACTUAL_TRAN_QTY_APP")
                Dim TransferQtyNot As Decimal = gr.Number("ACTUAL_TRAN_QTY_NOT")

                'plan qty
                Dim tbPlanQty As TextBox = .FindControl("tbPlanQty")
                tbPlanQty.Text = planQty '//แบบเดิม
                If gr.Number("balQty") = 0 Then
                    tbPlanQty.ReadOnly = True
                End If

                'set seq
                Dim tbSetSeq As TextBox = .FindControl("tbSetSeq")
                tbSetSeq.Text = gr.Text(PLANSCHEDULE_T.PlanSeqSet)
                'set ot
                Dim cbUrgent As CheckBox = .FindControl("cbUrgent")
                cbUrgent.Checked = If(gr.Text(PLANSCHEDULE_T.Urgent) = "Y", True, False)

                'machine or note
                Dim txtMch As TextBox = .FindControl("tbMch")
                txtMch.Text = gr.Text(PLANSCHEDULE_T.PlanNote)

                'plan status
                Dim cbCancled As CheckBox = .FindControl("cbCancled")
                cbCancled.Checked = If(gr.Text(PLANSCHEDULE_T.PlanStatus) = "C", True, False)

                If TransferQtyApp > 0 OrElse TransferQtyNot > 0 Then
                    cbCancled.Enabled = False
                    cbCancled.Checked = False
                    If TransferQtyApp + TransferQtyNot >= planQty Then
                        If moBalQty = 0 Then
                            tbPlanQty.Enabled = False
                        End If
                    End If
                End If
                gridviewRowForeColor(e.Row)

                '///////////////////////////////////////////////////////////////////////////
                '/////////////////////////// COLUMN 8 Hour Qty//////////////////////////////
                '///////////////////////////////////////////////////////////////////////////

                Dim wip As Integer = gr.Number("WipQty")
                Dim moBal As Integer = gr.Number("balQty")
                Dim hr8Qty As Integer = gr.Number("HR8QTY")
                Dim arlHr As New ArrayList From {
                   wip, moBal, hr8Qty
                }
                Dim arlTotal As New ArrayList
                For i = 0 To 2
                    If arlHr(i) > 0 Then
                        arlTotal.Add(arlHr(i))
                    End If
                Next
                Dim bal8hrQty As Integer = arlTotal.ToArray.Min

                'Cell 23 // 8 Hour Qty
                'ถ้า 8 ชม มากกว่า MoBal ให้แสดง MoBal
                If bal8hrQty > moBal Then
                    .Cells(23).Text = If(moBal = 0, "", moBal)
                Else
                    .Cells(23).Text = bal8hrQty
                End If


                '///////////////////////////////////////////////////////////////////////////
                '///////////////////////////////////////////////////////////////////////////
                '///////////////////////////////////////////////////////////////////////////

                Dim planStatus As String = gr.Text("PlanStatus")
                If planStatus = "" Then
                    tbPlanQty.Text = gr.Number("HR8QTY")
                End If

            End If

        End With
    End Sub

    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click

        Dim SQL As String = "",
            WHR As String = "",
            ORD As String = "",
            ISQL As String = "",
            wc As String = lbWc.Text.Trim,
             Program As New DataTable,
             item As String = "",
             qty As Decimal = 0,
             USQL As String = ""
        Dim fldWipQty As String = "SFCTA.TA010+SFCTA.TA013+SFCTA.TA016-SFCTA.TA011-SFCTA.TA012-SFCTA.TA014-SFCTA.TA015-SFCTA.TA048"
        Dim fldPrdQty As String = "MOCTA.TA015+SFCTA.TA013-SFCTA.TA011-SFCTA.TA014-SFCTA.TA012-SFCTA.TA016"
        Dim fldBalQty As String = "MOCTA.TA015+SFCTA.TA013-SFCTA.TA011-SFCTA.TA014-SFCTA.TA012-SFCTA.TA056-SFCTA.TA016"

        WHR &= dbConn.WHERE_EQUAL(fldPrdQty, 0, ">", False)
        WHR &= dbConn.WHERE_EQUAL("SFCTA.TA006", wc)
        WHR &= dbConn.WHERE_IN("MOCTA.TA011", "y,Y", True, True)
        If cbMoWorking.Checked Then
            WHR &= dbConn.WHERE_EQUAL(fldWipQty, 0, ">", False)
        End If
        WHR &= dbConn.WHERE_EQUAL("SFCTA.TA008", ucEndPlanStart.text, "<=")
        If ddlProcess.Text = "0" Then
            Dim sSQL As String = "select MW001 from CMSMW where MW005='" & lbWc.Text.Trim & "' "
            WHR &= "and SFCTA.TA004 in (" & sSQL & ") "
        Else
            WHR &= dbConn.WHERE_EQUAL("SFCTA.TA004", ddlProcess)
        End If

        WHR &= dbConn.WHERE_IN("MOCTA.TA026", UcDdlSoType.getObject)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA027", TbSoNo)
        WHR &= dbConn.WHERE_IN("SFCTA.TA001", UcDdlMoType.getObject,, True)
        WHR &= dbConn.WHERE_LIKE("SFCTA.TA002", TbMoNo)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA006", TbItem)
        WHR &= dbConn.WHERE_LIKE("MOCTA.TA035", TbSpec)

        Dim SQL1 As String = " select MoType,MoNo,MoSeq,sum(case when PlanDate>='" & dateToday & "' then isnull(PlanQty,0) else 0 end) PlanedQty,isnull(max(PlanDate),'99999999') LastPlanDate  from " & PLANSCHEDULE_T.table_full & tbMaster & " " &
                             " where WorkCenter='" & wc & "' And " & PLANSCHEDULE_T.PlanStatus & "='P' group by MoType,MoNo,MoSeq "

        Dim stdLabor As String = "round(SFCTA.TA022/MOCTA.TA015,0)"
        Dim stdMachine As String = "round(SFCTA.TA035/MOCTA.TA015,0)"

        Dim ttLabor As String = "round(((MOCTA.TA015+SFCTA.TA013-SFCTA.TA011-SFCTA.TA014-SFCTA.TA012-SFCTA.TA056-SFCTA.TA016)*(" & stdLabor & "))/60,0)"
        Dim ttMachine As String = "round(((MOCTA.TA015+SFCTA.TA013-SFCTA.TA011-SFCTA.TA014-SFCTA.TA012-SFCTA.TA056-SFCTA.TA016)*(" & stdMachine & "))/60,0)"

        Dim fldStandardTime As String = " case CMSMD.UDF02 when 'Machine' then " & stdMachine & " when 'Machine-AP100' then 0 else " & stdLabor & " end "
        Dim fldUsageTime As String = " case CMSMD.UDF02 when 'Machine' then case when SFCTA.TA035='0' then '0' else " & ttMachine & "  end when 'Machine-AP100' then 0 else case when SFCTA.TA022='0' then '0' else " & ttLabor & " end end "

        Dim fldOrderBy As String = " case when PLAN_TABLE.LastPlanDate<>'99999999' and PLAN_TABLE.PlanedQty>0 and PLAN_TABLE.PlanedQty<" & fldBalQty & " and PLAN_TABLE.LastPlanDate>= '" & dateToday & "' then '1' "
        fldOrderBy &= " when PLAN_TABLE.PlanedQty=0 and PLAN_TABLE.LastPlanDate< '" & dateToday & "' then '2' "
        fldOrderBy &= " when PLAN_TABLE.LastPlanDate='99999999' then '3' "
        fldOrderBy &= " when PLAN_TABLE.LastPlanDate<>'99999999' and PLAN_TABLE.PlanedQty>0 and " & fldBalQty & "=PLAN_TABLE.PlanedQty and PLAN_TABLE.LastPlanDate>= '" & dateToday & "' then '4' "
        fldOrderBy &= " when PLAN_TABLE.LastPlanDate<>'99999999' and PLAN_TABLE.PlanedQty>0 and PLAN_TABLE.PlanedQty>" & fldBalQty & " and PLAN_TABLE.LastPlanDate>= '" & dateToday & "' then '5' END "

        Dim fldName As New ArrayList From {
            "SFCTA.TA008",'Plan Start Date
            "MOCTA.TA010",'Plan completed Date
            "SFCTA.TA003",'MO Seq.
            fldStandardTime & VarIni.char8 & "TIME_STD",'time standard
            fldUsageTime & VarIni.char8 & "TIME_USAGE",'time usage
            "SFCTA.TA001",'MO Type.
            "SFCTA.TA002",'MO No.
            "COPMA.MA002",'Cust Name
            "MOCTA.TA035",'Spec
            "SFCTA.TA006 " & VarIni.char8 & PLANSCHEDULE_T.WorkCenter,'Work Center
            "MOCTA.TA015",'MO Qty
            "SFCTA.TA011+SFCTA.TA014-SFCTA.TA013" & VarIni.char8 & "TA011",'Finished Qty
            fldWipQty & VarIni.char8 & "WipQty",'WIP Qty
            "PLAN_TABLE.PlanedQty",'Planed Qty
            "SFCTA.TA017 " & VarIni.char8 & "waitTrnQty",'Wait Transfer Qty
            "PLAN_TABLE.LastPlanDate" & VarIni.char8 & "LastPlanDate1",'Last Daily Plan Date
            fldBalQty & VarIni.char8 & "balQty",'MO Bal Qty
            "MOCTA.TA006",'Item
            "SFCTA.TA004",'Operation
            "isnull(" & fldOrderBy & ",'0')" & VarIni.char8 & "orderBy",'order by
            "BOMMF.MF019", 'Batch Qty
            "CAST(RIGHT(100 + FLOOR(BOMMF.MF009 / 3600),2) AS varchar) +':'+ CAST(RIGHT(100 + FLOOR((BOMMF.MF009 % 3600) / 60),2) AS varchar) +':'+ CAST(RIGHT(100 + BOMMF.MF009 % 60,2) AS varchar) MF009", 'Fiexed Man-Hour // Format time 0000:00:00
            "CAST(RIGHT(100 + FLOOR(BOMMF.MF010 / 3600),2) AS varchar) +':'+ CAST(RIGHT(100 + FLOOR((BOMMF.MF010 % 3600) / 60),2) AS varchar) +':'+ CAST(RIGHT(100 + BOMMF.MF010 % 60,2) AS varchar) MF010", 'Dynamic Man-Hour // Format time 0000:00:00
            "CAST(RIGHT(100 + FLOOR(BOMMF.MF024 / 3600),2) AS varchar) +':'+ CAST(RIGHT(100 + FLOOR((BOMMF.MF024 % 3600) / 60),2) AS varchar) +':'+ CAST(RIGHT(100 + BOMMF.MF024 % 60,2) AS varchar) MF024", 'Fiexed Machine-Hour // Format time 0000:00:00
            "CAST(RIGHT(100 + FLOOR(BOMMF.MF025 / 3600),2) AS varchar) +':'+ CAST(RIGHT(100 + FLOOR((BOMMF.MF025 % 3600) / 60),2) AS varchar) +':'+ CAST(RIGHT(100 + BOMMF.MF025 % 60,2) AS varchar) MF025",  'Dynamic Machine-Hour // Format time 0000:00:00
            "CASE WHEN round(" & fldStandardTime & ",0) = 0 then 0 else FLOOR((480 * 0.9) * 60 / (" & fldStandardTime & ")) end" & VarIni.C8 & "HR8QTY" '(480 * 0.9) * 60 / TIME_STD
        }

        Dim strSQL As New SQLString(SFCTA.table, fldName)
        strSQL.setLeftjoin(" left join MOCTA on MOCTA.TA001 = SFCTA.TA001 And MOCTA.TA002 = SFCTA.TA002 ")
        strSQL.setLeftjoin(" left join COPTC on TC001=MOCTA.TA026 And TC002=MOCTA.TA027 ")
        strSQL.setLeftjoin(" left join COPMA on COPMA.MA001=COPTC.TC004 ")
        strSQL.setLeftjoin(" left join CMSMD on CMSMD.MD001=COPTC.TC004 ")
        strSQL.setLeftjoin(" left join (" & SQL1 & ") PLAN_TABLE on PLAN_TABLE.MoType=SFCTA.TA001 and PLAN_TABLE.MoNo=SFCTA.TA002 and PLAN_TABLE.MoSeq=SFCTA.TA003 ")
        strSQL.setLeftjoin(" left join BOMMF on BOMMF.MF003 = SFCTA.TA003 and BOMMF.MF006 = SFCTA.TA006 and BOMMF.MF001 = MOCTA.TA006 AND BOMMF.MF002 = '01' ")
        strSQL.SetWhere(WHR, True)
        strSQL.SetOrderBy("orderBy, MOCTA.TA010,SFCTA.TA001,SFCTA.TA002,SFCTA.TA003")
        gvCont.ShowGridView(gvShow, strSQL.GetSQLString, VarIni.ERP)
        TcMain.ActiveTabIndex = 1
        showButton()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", "GvShowScroll();", True)
        'btViewSearch_Click(sender, e)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound

        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                .Attributes.Add("onmouseover", "MouseEvents(this, event)")
                .Attributes.Add("onmouseout", "MouseEvents(this, event)")
                Dim planedQty As Decimal = outCont.checkNumberic(.DataItem("PlanedQty").ToString.Trim.Replace(",", ""))
                Dim lastPlanDate As String = .DataItem("LastPlanDate1").ToString.Trim.Replace("&nbsp;", "")
                Dim balQty As Decimal = outCont.checkNumberic(.DataItem("balQty").ToString.Trim.Replace(",", ""))
                If planedQty > 0 And planedQty = balQty And lastPlanDate <> "" Then 'วางแผนครบแล้ว
                    .ForeColor = Drawing.Color.Blue
                ElseIf planedQty > 0 And planedQty < balQty And lastPlanDate <> "" Then 'วางแผนไปแล้วบางส่วน
                    .ForeColor = Drawing.Color.Magenta
                ElseIf planedQty = 0 And lastPlanDate <> "" Then 'เคยวางแผนแล้ว
                    .ForeColor = Drawing.Color.Red
                ElseIf planedQty > 0 And planedQty > balQty And lastPlanDate <> "" Then 'วางจำนวนเกินยอดสั่งผลิต
                    .ForeColor = Drawing.Color.Green
                Else 'ยังไม่มีการวางแผน
                    .ForeColor = Drawing.Color.Black
                End If



                '///////////////////////////////////////////////////////////////////////////
                '/////////////////////////// COLUMN 8 Hour Qty//////////////////////////////
                '///////////////////////////////////////////////////////////////////////////

                Dim gr As New GridviewRowControl(e.Row)
                Dim wip As Integer = gr.Number("WipQty")
                Dim moBal As Integer = gr.Number("balQty")
                Dim hr8Qty As Integer = gr.Number("HR8QTY")
                Dim arlHr As New ArrayList From {
                   wip, moBal, hr8Qty
                }
                Dim arlTotal As New ArrayList
                For i = 0 To 2
                    If arlHr(i) > 0 Then
                        arlTotal.Add(arlHr(i))
                    End If
                Next
                Dim bal8hrQty As Integer = arlTotal.ToArray.Min

                'Cell 21 // 8 Hour Qty
                'ถ้า 8 ชม มากกว่า MoBal ให้แสดง MoBal
                If bal8hrQty > moBal Then
                    .Cells(21).Text = If(moBal = 0, "", moBal)
                Else
                    .Cells(21).Text = bal8hrQty
                End If

                '///////////////////////////////////////////////////////////////////////////
                '///////////////////////////////////////////////////////////////////////////
                '///////////////////////////////////////////////////////////////////////////


            End If
        End With

    End Sub

    Sub setHeadColSelect()
        dt.Columns.Add("TA008")
        dt.Columns.Add("TA010")
        dt.Columns.Add("TA003")
        dt.Columns.Add("TIME_STD", GetType(Decimal))
        dt.Columns.Add("TIME_USAGE", GetType(Decimal))
        dt.Columns.Add("TA001")
        dt.Columns.Add("TA002")
        dt.Columns.Add("MA002")
        dt.Columns.Add("TA035")
        dt.Columns.Add("WorkCenter")
        dt.Columns.Add("TA015", GetType(Decimal))
        dt.Columns.Add("TA011", GetType(Decimal))
        dt.Columns.Add("WipQty", GetType(Decimal))
        dt.Columns.Add("PlanedQty", GetType(Decimal))
        dt.Columns.Add("balQty", GetType(Decimal))
        dt.Columns.Add("TA006")
        dt.Columns.Add("TA004")
        dt.Columns.Add("MF019", GetType(Decimal)) 'Batch Qty
        dt.Columns.Add("MF009") 'Fiexed Man-Hour
        dt.Columns.Add("MF010") 'Dynamic Man-Hour
        dt.Columns.Add("MF024") 'Fiexed Machine-Hour
        dt.Columns.Add("MF025") 'Dynamic Machine-Hour
        dt.Columns.Add("HR8QTY", GetType(Decimal)) '(480 * 0.9) * 60 / TIME_STD
        dt.Columns.Add("PlanSeq")
        dt.Columns.Add(PLANSCHEDULE_T.PlanQty, GetType(Decimal))
        dt.Columns.Add("ACTUAL_TRAN_QTY_APP", GetType(Decimal))
        dt.Columns.Add("ACTUAL_TRAN_QTY_NOT", GetType(Decimal))
        dt.Columns.Add("PLAN_BAL_QTY", GetType(Decimal))
        dt.Columns.Add(PLANSCHEDULE_T.PlanSeqSet)
        dt.Columns.Add(PLANSCHEDULE_T.PlanNote)
        dt.Columns.Add(PLANSCHEDULE_T.Urgent)
        dt.Columns.Add(PLANSCHEDULE_T.PlanStatus)
        dt.Columns.Add(PLANSCHEDULE_T.PlanTimeStd, GetType(Decimal))
    End Sub

    Protected Sub btSelect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSelect.Click

        showData(lbWc.Text.Trim, lbPlanDate.Text.Trim)
        If dt.Columns.Count = 0 Then
            setHeadColSelect()
        End If
        'check time to zero
        For Each gr As GridViewRow In gvShow.Rows
            With New GridviewRowControl(gr)
                Dim cbSelect As CheckBox = .FindControl("cbSelect")
                If cbSelect.Checked Then
                    Dim timeSTD As Decimal = .Number(4)
                    If timeSTD <= 0 Then
                        show_message.ShowMessage(Page, "No Dynamic time for MO(type-No-Seq) " & .Text(6) & "-" & .Text(7) & "-" & .Text(3) & " please contract to PE dept(ไม่มีเวลาในการทำงาน รบกวนติดต่อ PE ก่อนวางแผน).", UpdatePanel1)
                        Exit Sub
                    End If
                End If
            End With
        Next

        Dim rowAdd As Integer = 0
        For Each gr As GridViewRow In gvShow.Rows
            With New GridviewRowControl(gr)
                Dim cbSelect As CheckBox = .FindControl("cbSelect")
                If cbSelect.Checked Then
                    Dim dr As DataRow() = dt.Select("TA001='" & .Text(6) & "' and TA002='" & .Text(7) & "' and TA003='" & .Text(3) & "' ")
                    'dt.
                    If dr.Length <= 0 Then
                        dt.Rows.Add()
                        Dim j As Integer = 1
                        dt.Rows(dt.Rows.Count - 1)("TA008") = .Text(j) 'Plan start date
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("TA010") = .Text(j) 'Plan complete date
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("TA003") = .Text(j) 'MO Seq
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("TIME_STD") = .Number(j) 'Std Man
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("TIME_USAGE") = .Number(j) 'Std Mch
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("TA001") = .Text(j) 'MO type
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("TA002") = .Text(j) 'MO No
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("MA002") = .Text(j) 'customer
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("TA035") = .Text(j) 'Spec
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("WorkCenter") = .Text(j) 'WC
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("TA015") = .Number(j) 'MO Qty
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("TA011") = .Number(j) 'Finished Qty
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("WipQty") = .Number(j) 'WIP Qty
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("PlanedQty") = .Number(j) 'Planed Qty
                        j += 3
                        dt.Rows(dt.Rows.Count - 1)("balQty") = .Number(j) 'Bal Qty
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("TA006") = .Text(j) 'item
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("TA004") = .Text(j) 'Operation
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("MF019") = .Number(j) 'Batch Qty
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("HR8QTY") = .Number(j) 'Hour 8 Qty
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("MF009") = .Text(j) 'Fiexed Man-Hour
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("MF010") = .Text(j) 'Dynamic Man-Hour
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("MF024") = .Text(j) 'Fiexed Machine-Hour
                        j += 1
                        dt.Rows(dt.Rows.Count - 1)("MF025") = .Text(j) 'Dynamic Machine-Hour

                        dt.Rows(dt.Rows.Count - 1)("PlanSeq") = "0" '
                        dt.Rows(dt.Rows.Count - 1)(PLANSCHEDULE_T.PlanQty) = "0" '
                        dt.Rows(dt.Rows.Count - 1)("ACTUAL_TRAN_QTY_APP") = "0" '
                        dt.Rows(dt.Rows.Count - 1)("ACTUAL_TRAN_QTY_NOT") = "0" '
                        dt.Rows(dt.Rows.Count - 1)("PLAN_BAL_QTY") = "0" '
                        dt.Rows(dt.Rows.Count - 1)(PLANSCHEDULE_T.PlanSeqSet) = "" '
                        dt.Rows(dt.Rows.Count - 1)(PLANSCHEDULE_T.PlanNote) = "" '
                        dt.Rows(dt.Rows.Count - 1)(PLANSCHEDULE_T.Urgent) = "" '
                        dt.Rows(dt.Rows.Count - 1)(PLANSCHEDULE_T.PlanStatus) = "" '
                        dt.Rows(dt.Rows.Count - 1)(PLANSCHEDULE_T.PlanTimeStd) = "0" '
                        dt.AcceptChanges()
                        rowAdd += 1
                    End If
                End If
            End With
        Next
        If rowAdd > 0 Then
            ViewState("dt") = dt
            gvCont.ShowGridView(gvSelect, dt)
            'setDatatoText(lbWc.Text.Trim, lbPlanDate.Text.Trim)
            SumUsageWorkTime(gvSelect)
            'btViewSelect_Click(sender, e)

            'set display none column
            Dim hideColumn As New List(Of Integer) From {
                11, 20, 21, 28, 29, 33, 34, 35, 36, 37
            }
            gvCont.hideColumn(gvSelect, hideColumn, "displayNone")
            TcMain.ActiveTabIndex = 0
            showButton()
        Else
            show_message.ShowMessage(Page, "กรุณาเลือกรายการที่ต้องการ!!!(รายการที่เลือกถูกเลือกไปแล้วหรือไม่มีรายการที่เลือก)", UpdatePanel1)
        End If
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", "GvSelectScroll();", True)
    End Sub

    Protected Sub btClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btClear.Click
        gvShow.DataSource = ""
        gvShow.DataBind()
        'gvSelect.DataSource = ""
        'gvSelect.DataBind()
        'lbUsageHourMan.Text = 0
        lbUsageTime.Text = 0
        'lbBalHourMan.Text = 0
        lbBalTime.Text = 0
        ucEndPlanStart.Text = ""
        cbMoWorking.Checked = False
        'cbSort1.Checked = False
        'cbSort2.Checked = False
        'btSelect.Visible = False
        'btSave.Visible = False
        showButton()
    End Sub

    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click

        Dim line As Integer = 0
        Dim whrHash As Hashtable = New Hashtable,
            fldInsHash As Hashtable = New Hashtable,
            planDate As String = lbPlanDate.Text.Trim,
            wc As String = lbWc.Text.Trim,
            fldUpdHash As Hashtable = New Hashtable
        Dim valUser As String = Session("UserName")

        Dim sqlList As New ArrayList
        Dim LastLine As Integer = 0

        For Each gvRow As GridViewRow In gvSelect.Rows
            With New GridviewRowControl(gvRow)
                Dim SQLType As String = "I"
                Dim cbCancled As CheckBox = .FindControl("cbCancled")
                Dim tbSetSeq As TextBox = .FindControl("tbSetSeq")
                Dim cbUrgent As CheckBox = .FindControl("cbUrgent")
                Dim tbNote As TextBox = .FindControl("tbMch")
                Dim tbPlanQty As TextBox = .FindControl("tbPlanQty")

                Dim planStutusNew As String = If(cbCancled.Checked, "C", "P")
                Dim urgentNew As String = If(cbUrgent.Checked, "Y", "N")
                Dim planQtyNew As Decimal = outCont.checkNumberic(tbPlanQty)
                Dim planTimeStdNew As Decimal = .Number(5) 'Time Standard(Sec)

                Dim planSeqRecord As Integer = .Number(28) 'Plan Seq
                Dim planQtyRecord As Decimal = .Number(29) 'Plan Qty Record
                Dim planStatusRecord As String = .Text(33) 'Plan Status Record
                Dim planSeqSetRecord As String = .Text(34) 'Plan Set Seq Record
                Dim urgentRecord As String = .Text(35) 'Urgent Record
                Dim planNoteRecord As String = .Text(36) 'Plan Note Record
                Dim plantimestdRecord As Decimal = .Number(37) 'PlanTimeStd Record

                Dim PlanQtyChange As Boolean = planQtyRecord <> planQtyNew
                Dim planStatusChange As Boolean = planStatusRecord <> planStutusNew
                Dim planSeqSetChange As Boolean = planSeqSetRecord <> tbSetSeq.Text.Trim
                Dim urgentChange As Boolean = urgentRecord <> urgentNew
                Dim planNoteChagne As Boolean = planNoteRecord <> tbNote.Text.Trim
                Dim planTimeStdChange As Boolean = plantimestdRecord <> planTimeStdNew

                Dim checkChange As Boolean = PlanQtyChange Or planStatusChange Or planSeqSetChange Or urgentChange Or planNoteChagne Or planTimeStdChange
                Dim SaveNeed As Boolean = (planSeqRecord = 0 And Not cbCancled.Checked) Or planSeqRecord > 0
                'line = planSeqRecord
                'check save 
                If checkChange And SaveNeed Then
                    Dim moType As String = .Text(7),
                        moNo As String = .Text(8),
                        moSeq As String = .Text(4),
                        operation As String = .Text(21)
                    Dim valDateTime As String = DateTime.Now.ToString("yyyyMMdd hhmmss")
                    If planSeqRecord > 0 Then
                        SQLType = "U"
                    Else
                        planSeqRecord = LastLine + 1
                    End If

                    line = planSeqRecord
                    whrHash = New Hashtable From {
                        {PLANSCHEDULE_T.PlanDate, planDate}, 'Plan Date
                        {PLANSCHEDULE_T.WorkCenter, wc}, 'WC
                        {PLANSCHEDULE_T.PlanSeq, line} 'PlanSeq
                    }
                    'insert
                    fldInsHash = New Hashtable From {
                        {PLANSCHEDULE_T.PlanSeqSet, tbSetSeq.Text.Trim}, 'PlanSeqSet
                        {PLANSCHEDULE_T.MoType, moType}, 'MO Type
                        {PLANSCHEDULE_T.MoNo, moNo}, 'MO No
                        {PLANSCHEDULE_T.MoSeq, moSeq}, 'MO Seq
                        {PLANSCHEDULE_T.ProcessCode, operation}, 'Operation
                        {PLANSCHEDULE_T.PlanedQty, .Number(15)} 'PlanedQty
                    }

                    Dim stdTime As Decimal = .Number(5) 'stardard time(sec)
                    Dim planQty As Decimal = .Number(17) 'MO Bal Qty
                    Dim wipQty As Decimal = .Number(14) 'wip Qty
                    planQty = If(wipQty > 0, wipQty, planQty)
                    If planQtyNew > 0 Then
                        planQty = planQtyNew
                    End If
                    fldInsHash.Add(PLANSCHEDULE_T.PlanQty, planQty) 'PlanQty
                    fldInsHash.Add(PLANSCHEDULE_T.PlanTimeStd, stdTime) 'Plan Time(Sec)
                    fldInsHash.Add(PLANSCHEDULE_T.PlanTime, stdTime * planQty) 'Plan Time(Sec)

                    fldInsHash.Add(PLANSCHEDULE_T.Urgent, urgentNew) 'urgent
                    fldInsHash.Add(PLANSCHEDULE_T.PlanNote, tbNote.Text.Trim) 'mch/note
                    If cbCancled.Checked Then
                        fldInsHash.Add(PLANSCHEDULE_T.CancledBy, valUser) 'user record
                        fldInsHash.Add(PLANSCHEDULE_T.CancledDate, valDateTime) 'date time record
                    End If
                    fldInsHash.Add(PLANSCHEDULE_T.PlanStatus, planStutusNew) 'plan status

                    Dim fldUser As String = PLANSCHEDULE_T.CreateBy
                    Dim fldDateTime As String = PLANSCHEDULE_T.CreateDate
                    If SQLType = "U" Then
                        fldUser = PLANSCHEDULE_T.ChangeBy
                        fldDateTime = PLANSCHEDULE_T.ChangeDate
                    End If
                    fldInsHash.Add(fldUser, valUser)
                    fldInsHash.Add(fldDateTime, valDateTime)

                    sqlList.Add(dbConn.GetSQL(PLANSCHEDULE_T.table & tbMaster, fldInsHash, whrHash, SQLType))

                End If
                LastLine = planSeqRecord
            End With
        Next
        Dim msg As String = "Not Transaction Record (ไม่มีการบันทึก)"
        If sqlList.Count > 0 Then

            'insert/update to planschedule
            dbConn.TransactionSQL(sqlList, VarIni.DBMIS, dbConn.WhoCalledMe)


            'update
            Dim fldName As New ArrayList From {
                PLANSCHEDULE_T.MoType & "+'-'+rtrim(" & PLANSCHEDULE_T.MoNo & ")+'-'+" & PLANSCHEDULE_T.MoSeq
            }
            Dim strSQL As New SQLString(PLANSCHEDULE_T.table_full & tbMaster, fldName)
            Dim whr As String = ""
            whr &= dbConn.WHERE_EQUAL(PLANSCHEDULE_T.WorkCenter, wc)
            whr &= dbConn.WHERE_EQUAL(PLANSCHEDULE_T.PlanDate, planDate)

            strSQL.SetWhere(whr, True)
            Dim USQL As String = "update SFCTA set UDF01='" & planDate & "',UDF02='" & planDate & "' where TA006='" & wc & "' and TA001+'-'+TA002+'-'+TA003 in (" & strSQL.GetSQLString & ")"
            If tbMaster <> "Test" Then
                'ถ้าเป็นข้อมูล Test จะไม่เข้า Update
                dbConn.TransactionSQL(USQL, VarIni.ERP, dbConn.WhoCalledMe)
            End If

            showData(lbWc.Text, lbPlanDate.Text)
            'show_message.ShowMessage(Page, "Transaction complete(บันทึกรายการสำเร็จแล้ว " & sqlList.Count.ToString("#,#") & " รายการ)", UpdatePanel1)
            msg = "Transaction complete(บันทึกรายการสำเร็จแล้ว " & sqlList.Count.ToString("#,#") & " รายการ)"
            'refresh opener page
            'Response.Write("<script>window.opener.location.reload();</script>")
            'Else

        End If
        show_message.ShowMessage(Page, msg, UpdatePanel1)

    End Sub

    Private Sub TcMain_ActiveTabChanged(sender As Object, e As EventArgs) Handles TcMain.ActiveTabChanged
        showButton()
    End Sub

    'Protected Sub BindGrid()
    '    gvSelect.DataSource = TryCast(ViewState("dt"), DataTable)
    '    gvSelect.DataBind()
    'End Sub

    'Private Sub gvSelect_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvSelect.RowDeleting
    '    Dim index As Integer = Convert.ToInt32(e.RowIndex)
    '    Dim dt As DataTable = TryCast(ViewState("dt"), DataTable)
    '    dt.Rows(index).Delete()
    '    ViewState("dt") = dt
    '    BindGrid()
    '    SumUsageWorkTime(gvSelect)
    'End Sub

    'Protected Sub btViewSearch_Click(sender As Object, e As EventArgs) Handles btViewSearch.Click
    '    'mvPlan.SetActiveView(vSearch)
    'End Sub

    'Protected Sub btViewSelect_Click(sender As Object, e As EventArgs) Handles btViewSelect.Click
    '    'mvPlan.SetActiveView(vPlan)
    'End Sub

    'Protected Sub btUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btUpdate.Click
    '    Dim wc As String = lbWc.Text.Trim
    '    Dim usql1 As String = "update " & table2 & " set PlanedQty='0' where TA006='" & wc & "'"
    '    Conn_SQL.Exec_Sql(usql1, Conn_SQL.MIS_ConnectionString)

    '    Dim sql As String = "select * from " & table2 & " where TA006='" & wc & "' order by TA001,TA002,TA003,TA004"
    '    sql = "select TA001,TA002,TA003,TA004,sum(PlanQty) as cnt from " & table & " where TA006='" & wc & "' group by TA001,TA002,TA003,TA004 order by TA001,TA002,TA003,TA004 "
    '    Dim adt As New DataTable
    '    dt = Conn_SQL.Get_DataReader(sql, Conn_SQL.MIS_ConnectionString)
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        With dt.Rows(i)
    '            Dim SQL1 As String = ""
    '            SQL1 = " select TA006,sum(PlanQty) as sQty from " & table & " where TA006='" & wc & "' " & _
    '                  " and TA001='" & .Item("TA001") & "' and TA002='" & .Item("TA002") & "' " & _
    '                  " and TA003='" & .Item("TA003") & "' and TA004='" & .Item("TA004") & "'  " & _
    '                  " group by TA006 "
    '            Dim Program1 As New DataTable
    '            Program1 = Conn_SQL.Get_DataReader(SQL1, Conn_SQL.MIS_ConnectionString)
    '            If Program1.Rows.Count > 0 Then
    '                Dim usql As String = "update " & table2 & " set PlanedQty='" & Program1.Rows(0).Item("sQty") & "'  " & _
    '                                     " where TA006='" & wc & "' and TA001='" & .Item("TA001") & "' and TA002='" & .Item("TA002") & "' " & _
    '                                     " and TA003='" & .Item("TA003") & "' and TA004='" & .Item("TA004") & "'  "


    '                Dim pQty As Decimal = Program1.Rows(0).Item("sQty")
    '                Conn_SQL.Exec_Sql(usql, Conn_SQL.MIS_ConnectionString)
    '                'whrHash = New Hashtable
    '                'whrHash.Add("TA001", moType) 'Plan Date
    '                'whrHash.Add("TA002", moNo) 'WC
    '                'whrHash.Add("TA003", moSeq) 'PlanSeq
    '                'whrHash.Add("TA004", operation) 'Plan Date
    '                'whrHash.Add("TA006", wc) 'WC
    '                'fldInsHash = New Hashtable
    '                'fldInsHash.Add("PlanedQty", pQty) 'Planed Qty
    '            End If
    '        End With
    '    Next
    'End Sub
End Class