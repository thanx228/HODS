Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class PlanScheduleCheck
    Inherits System.Web.UI.Page

    'Dim Conn_SQL As New ConnSQL
    'Dim ControlForm As New ControlDataForm
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Dim outCont As New OutputControl
    Dim dbConn As New DataConnectControl
    Dim gvCont As New GridviewControl
    Dim dateCont As New DateControl
    Dim dtCont As New DataTableControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            BtShow_Click(sender, e)
        End If
    End Sub

    Protected Sub btBefore_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btBefore.Click
        Response.Redirect(Server.UrlPathEncode(returnLink(-1)))
    End Sub

    Protected Sub btAfter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btAfter.Click
        Response.Redirect(Server.UrlPathEncode(returnLink()))
    End Sub

    Function returnLink(Optional dayAdd As Integer = 1)
        Dim tDate As Date = dateCont.strToDateTime(lbPlanDate.Text, "yyyyMMdd")
        Dim valDate As String = tDate.AddDays(dayAdd).ToString("yyyyMMdd", New System.Globalization.CultureInfo("en-US"))
        Dim sendPara As String = "plandate=" & outCont.EncodeTo64UTF8(valDate)
        sendPara &= "&wc=" & outCont.EncodeTo64UTF8(lbWc.Text.Trim)
        sendPara &= "&wcName=" & outCont.EncodeTo64UTF8(lbWcName.Text.Trim)
        Return "PlanScheduleCheck.aspx?" & sendPara
    End Function

    Private Sub gvPlan_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPlan.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                With New GridviewRowControl(e.Row)
                    Dim PlanDateToday As String = lbPlanDate.Text.Trim
                    Dim PlanDate As String = .Text("PlanDate")
                    Dim colorFore As Drawing.Color
                    If PlanDateToday = PlanDate Then
                        Dim PQty As Decimal = .Number("PlanQty")
                        Dim MOQty As Decimal = .Number("TA015")
                        Dim ActualQty As Decimal = .Number("ActualQty")
                        Dim UnAppQty As Decimal = .Number("TRANS_NOT")
                        Dim ScrapQty As Decimal = .Number("TRANS_SCRAP")
                        If .Text("PLAN_STATUS") = "ON PLAN" Then '+ ScrapQtyActualQty + UnAppQty >= PQty
                            colorFore = Drawing.Color.Blue
                        Else
                            colorFore = Drawing.Color.Red
                        End If
                        If PQty > MOQty Then
                            colorFore = Drawing.Color.Gray
                        End If
                    ElseIf PlanDate > PlanDateToday Then
                        colorFore = Drawing.Color.Black
                    Else
                        colorFore = Drawing.Color.Red
                    End If
                    e.Row.ForeColor = colorFore
                End With
            End If
        End With
    End Sub

    Protected Sub BtShow_Click(sender As Object, e As EventArgs) Handles BtShow.Click
        'Dim tempTable As String = "TempPlanSchedule" & Session("UserName")
        'CreateTempTable.createTempPlanSchedule(tempTable)
        Dim WHR As String
        Dim SQL As String

        lbWc.Text = outCont.DecodeFrom64(Request.QueryString("wc").Trim)
        lbPlanDate.Text = outCont.DecodeFrom64(Request.QueryString("plandate"))
        lbWcName.Text = outCont.DecodeFrom64(Request.QueryString("wcName"))
        'lbWc.Text = Request.QueryString("wc").ToString.Trim
        Dim pDate As String = lbPlanDate.Text
        If pDate = "" Then
            pDate = DateTime.Now.ToString("dd-MM-yyyy", New System.Globalization.CultureInfo("en-US")).Replace("-", "/")
        End If
        lbPlanDate.Text = pDate

        Dim wc As String = lbWc.Text, planDate As String = lbPlanDate.Text
        Dim sqlPlanSchedule As String = ""
        '_PLAN_DATE_,_WORK_CENTER_
        sqlPlanSchedule = "(select * from (
                            select MoType,MoNo,MoSeq,ProcessCode,PlanDate,WorkCenter,PlanQty,TRANSFER_DATE,TC014_APP TRANS_APP,TC014_NOT TRANS_NOT,TC016 TRANS_SCRAP,CREATE_DATE, case when TRANSFER_DATE is null then 3 else  case when isnull(TC016,0)+isnull(TC014,0)>isnull(PlanQty,0) then 1 else 2 end end ORD_SEQ
                            from (select MoType,MoNo,MoSeq,ProcessCode,PlanDate,WorkCenter,sum(PlanQty) PlanQty from " & PLANSCHEDULE_T.table & " where PlanStatus='P' group by MoType,MoNo,MoSeq,ProcessCode,PlanDate,WorkCenter) PlanSchedule
                            left join V_TRANSFER_SUM_DATE on TC004=MoType and TC005=MoNo and TC006=MoSeq and TRANSFER_DATE=PlanSchedule.PlanDate
                            where PlanDate='_PLAN_DATE_' and WorkCenter='_WORK_CENTER_'
                            UNION
                            select TC004,TC005,TC006,TC007,PlanSchedule.PlanDate,TB005,0,TRANSFER_DATE,TC014_APP,TC014_NOT,TC016,CREATE_DATE,9 from V_TRANSFER_SUM_DATE
                            left join (select distinct MoType,MoNo,MoSeq,ProcessCode,PlanDate,WorkCenter from PlanSchedule where PlanDate='_PLAN_DATE_' and PlanStatus='P' ) PlanSchedule  on PlanSchedule.MoType=TC004 and PlanSchedule.MoNo=TC005 and PlanSchedule.MoSeq=TC006
                            where PlanSchedule.MoType is null and TRANSFER_DATE='_PLAN_DATE_'  and TB005='_WORK_CENTER_'
                           ) PlanScheduleNew ) PLAN_DATA "
        sqlPlanSchedule = sqlPlanSchedule.Replace("_PLAN_DATE_", lbPlanDate.Text).Replace("_WORK_CENTER_", lbWc.Text)
        Dim fldTransfer As String = "PLAN_DATA.TRANS_APP+PLAN_DATA.TRANS_SCRAP+PLAN_DATA.TRANS_NOT"
        Dim al As New ArrayList
        Dim fldName As ArrayList
        Dim colName As ArrayList
        With New ArrayListControl(al)
            .TAL("case P.SetupMold when '1' then 'OK' else '' end " & VarIni.C8 & "SetupMold", "Setup Mold")
            .TAL("P.Urgent" & VarIni.C8 & "Urgent", "OT")
            .TAL("case P.PlanSeqSet when '9999' then '' else P.PlanSeqSet end" & VarIni.C8 & "PlanSeqSet1", "Piority")
            .TAL("P.PlanNote" & VarIni.C8 & "PlanNote", "Note")
            .TAL("PLAN_DATA.PlanDate" & VarIni.C8 & "PlanDate", "Plan Date")
            .TAL("PLAN_DATA.MoType" & VarIni.C8 & "TA001", "MO Type")
            .TAL("PLAN_DATA.MoNo" & VarIni.C8 & "TA002", "MO No.")
            .TAL("PLAN_DATA.MoSeq" & VarIni.C8 & "TA003", "MO Seq.")
            .TAL("MW002", "Operation")
            .TAL("M.TA035" & VarIni.C8 & "TA035", "Spec")
            .TAL("TA015", "MO Qty", "0")
            .TAL("WIP_QTY", "WIP Qty", "0")
            .TAL("PLAN_DATA.PlanQty" & VarIni.C8 & "PlanQty", "Plan Qty", "0") '10
            .TAL("M.STD_MAN" & VarIni.C8 & "STD_MAN", "Labor STD(Sec)", "0")
            .TAL("M.STD_MACH" & VarIni.C8 & "STD_MACH", "Machine STD(Sec)", "0")
            .TAL("(PLAN_DATA.PlanQty*M.STD_MAN)/60" & VarIni.C8 & "C", "Labor Need(Min)", "0")
            .TAL("(PLAN_DATA.PlanQty*M.STD_MACH)/60" & VarIni.C8 & "D", "Machine Need(Min)", "0")
            .TAL("PLAN_DATA.TRANS_APP+PLAN_DATA.TRANS_SCRAP" & VarIni.C8 & "ActualQty", "Actual Qty", "0") '15
            .TAL("PLAN_DATA.TRANS_NOT" & VarIni.C8 & "TRANS_NOT", "Not App Qty", "0") '16
            .TAL("PLAN_DATA.TRANS_SCRAP" & VarIni.C8 & "TRANS_SCRAP", "Scrap Qty", "0")
            .TAL("PLAN_DATA.PlanQty-(" & fldTransfer & ")" & VarIni.C8 & "BalQty", "Balance Plan Qty", "0")
            .TAL("PLAN_DATA.TRANSFER_DATE" & VarIni.C8 & "TRANSFER_DATE", "Actual Date")
            .TAL("PLAN_DATA.CREATE_DATE" & VarIni.C8 & "CREATE_DATE", "Create Date Time")
            .TAL("((" & fldTransfer & ")*M.STD_MAN)/60" & VarIni.C8 & "A", "Labor Usage(Min)", "0")
            .TAL("((" & fldTransfer & ")*M.STD_MACH)/60" & VarIni.C8 & "B", "Machine Usage(Min)", "0")
            .TAL("MA002", "Cust Name")
            .TAL("case when PLAN_DATA.PlanDate='" & planDate & "' and (" & fldTransfer & ")>=PLAN_DATA.PlanQty then 'ON PLAN' else '' end" & VarIni.C8 & "PLAN_STATUS", "Status Plan")
            fldName = .ChangeFormat()
            colName = .ChangeFormat(True)
        End With
        Dim sqlMO As String = "select A.TA001,A.TA002,A.TA003,A.TA004,A.TA006,B.TA015,B.TA026,B.TA027,B.TA035,A.TA010+A.TA013+A.TA016-A.TA011-A.TA012-A.TA014-A.TA015-A.TA048-A.TA056-A.TA058 WIP_QTY, isnull(cast(case B.TA015 when 0 then 0 else A.TA035/B.TA015 end as decimal(10,0)),0) as STD_MACH, isnull(cast(case B.TA015 when 0 then 0 else A.TA022/B.TA015 end as decimal(10,0)),0) as STD_MAN from " & VarIni.ERP & "..SFCTA A left join " & VarIni.ERP & "..MOCTA B on  B.TA001=A.TA001 and B.TA002=A.TA002 "
        Dim strSQL As New SQLString(sqlPlanSchedule, fldName)
        'strSQL.setLeftjoin(" left join " & VarIni.ERP & "..SFCTA M on M.TA001=T.TA001 and M.TA002=T.TA002 ")
        'strSQL.setLeftjoin(" left join " & VarIni.ERP & "..MOCTA M on M.TA001=T.TA001 and M.TA002=T.TA002 ")
        strSQL.setLeftjoin(" left join (" & sqlMO & ") M on M.TA001=PLAN_DATA.MoType and M.TA002=PLAN_DATA.MoNo AND M.TA003=PLAN_DATA.MoSeq ")
        Dim sqlProcessInfo As String = "select MW001,MW002 from " & VarIni.ERP & "..CMSMW where UDF03<>'N'"
        strSQL.setLeftjoin(" left join (" & sqlProcessInfo & ") W on W.MW001=PLAN_DATA.ProcessCode ")
        strSQL.setLeftjoin(" left join " & VarIni.ERP & "..COPTC C on C.TC001=M.TA026 and C.TC002=M.TA027 ")
        strSQL.setLeftjoin(" left join " & VarIni.ERP & "..COPMA A on A.MA001=C.TC004 ")
        strSQL.setLeftjoin(" left join (select SetupMold,PlanDate,MoType,MoNo,MoSeq,case PlanSeqSet when '' then '9999' else PlanSeqSet end PlanSeqSet,Urgent,PlanNote from PlanSchedule) P on P.PlanDate=PLAN_DATA.PlanDate and P.MoType=PLAN_DATA.MoType and P.MoNo=PLAN_DATA.MoNo and P.MoSeq=PLAN_DATA.MoSeq ")

        ' strSQL.setLeftjoin(" left join (select SetupMold,MoType,MoNo,MoSeq from PlanSchedule where SetupMold=1) PP on PP.MoType=PLAN_DATA.MoType and PP.MoNo=PLAN_DATA.MoNo and PP.MoSeq=PLAN_DATA.MoSeq ")

        strSQL.SetWhere(dbConn.WHERE_EQUAL("W.MW001", " IS NOT NULL ", "", False), True)

        strSQL.SetOrderBy("PLAN_DATA.ORD_SEQ desc,cast(P.PlanSeqSet as decimal(10,0)),PLAN_DATA.PlanDate,PLAN_DATA.MoType,PLAN_DATA.MoNo,PLAN_DATA.MoSeq")
        SQL = strSQL.GetSQLString
        gvCont.GridviewColWithLinkFirst(gvPlan, colName, strSplit:=VarIni.C8)
        gvCont.ShowGridView(gvPlan, strSQL.GetSQLString, VarIni.DBMIS)


        WHR = dbConn.WHERE_EQUAL("MD001", lbWc.Text.Trim)
        SQL = "select MD001,MD002,CMSMD.UDF02,isnull(STD_TIME,0) STD_TIME from CMSMD left join (select MX002,SUM((CMSMX.UDF51+UDF52)*CMSMX.UDF53) STD_TIME from CMSMX GROUP BY MX002) CMSMX on MX002=MD001 where 1=1 " & WHR & " order by MD001 "

        'Dim dr As DataRow = dbConn.QueryDataRow(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        Dim dr As New DataRowControl(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        lbWcLoad.Text = dateCont.TimeFormat(dr.Number("STD_TIME"))

        al = New ArrayList
        fldName = New ArrayList
        With New ArrayListControl(al)
            .TAL("isnull(sum(case when PlanDate Is Not null  then 1 else 0 end), 0)" & VarIni.C8 & "cntPlan", "OT")
            .TAL("isnull(sum(case when TRANSFER_DATE Is Not null then 1 else 0 end ), 0)" & VarIni.C8 & "cntTran", "OT")
            .TAL("isnull(sum(case when PlanDate Is Not null  And PlanDate=TRANSFER_DATE And PLAN_DATA.TRANS_APP+PLAN_DATA.TRANS_SCRAP+PLAN_DATA.TRANS_NOT>=PlanQty then 1 else 0 end), 0)" & VarIni.C8 & "cntAct", "OT")
            .TAL("isnull(sum(case when PlanDate Is null And TRANSFER_DATE Is Not null  then 1 else 0 end), 0)" & VarIni.C8 & "cntNoPlan", "OT")
            .TAL("isnull(sum(case when PlanDate Is Not null then PlanQty*STD_MAN else 0 end), 0)" & VarIni.C8 & "StdMan", "OT")
            .TAL("isnull(sum(case when PlanDate Is Not null then PlanQty*STD_MACH else 0 end), 0)" & VarIni.C8 & "StdMch", "OT")
            fldName = .ChangeFormat()
        End With
        strSQL = New SQLString(sqlPlanSchedule, fldName)
        strSQL.setLeftjoin(" left join (" & sqlMO & ") M on M.TA001=PLAN_DATA.MoType and M.TA002=PLAN_DATA.MoNo AND M.TA003=PLAN_DATA.MoSeq ")
        strSQL.setLeftjoin(" left join (" & sqlProcessInfo & ") W on W.MW001=PLAN_DATA.ProcessCode ")
        strSQL.SetWhere(dbConn.WHERE_EQUAL("W.MW001", " IS NOT NULL ", "", False), True)
        SQL = strSQL.GetSQLString
        With New DataRowControl(strSQL.GetSQLString, VarIni.DBMIS, dbConn.WhoCalledMe)
            lbPlan.Text = .Number("cntPlan")
            lbActTran.Text = .Number("cntTran")
            lbActPlan.Text = .Number("cntAct")
            Dim xper As Decimal = 0
            If .Number("cntPlan") > 0 Then
                xper = Math.Round(.Number("cntAct") * 100 / .Number("cntPlan"), 2)
            End If
            lbPer.Text = xper
            lbNoPlan.Text = .Number("cntNoPlan")
            xper = 0
            If .Number("cntPlan") > 0 Then
                xper = Math.Round(.Number("cntNoPlan") * 100 / .Number("cntPlan"), 2)
            End If
            lbPerNoPlan.Text = xper
            lbManTime.Text = dateCont.TimeFormat(.Number("StdMan"))
            lbMchTime.Text = dateCont.TimeFormat(.Number("StdMch"))
        End With
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles BtExport.Click
        For Each row As GridViewRow In gvPlan.Rows
            For Each cell As TableCell In row.Cells
                cell.Height = 20
            Next
        Next
        ExportsUtility.ExportGridviewToMsExcel("DailyPlanCheck_" & lbWc.Text, gvPlan)
    End Sub

End Class