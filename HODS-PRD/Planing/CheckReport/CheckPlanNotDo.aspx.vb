Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class CheckPlanNotDo
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
        If Date1.Text = "" Then
            Date1.Text = "20210504"
        End If
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
        Dim tDate As Date = dateCont.strToDateTime(Date1.Text, "yyyyMMdd")
        Dim valDate As String = tDate.AddDays(dayAdd).ToString("yyyyMMdd", New System.Globalization.CultureInfo("en-US"))
        Dim sendPara As String = "plandate=" & outCont.EncodeTo64UTF8(valDate)
        'sendPara &= "&wc=" & outCont.EncodeTo64UTF8(lbWc.Text.Trim)
        'sendPara &= "&wcName=" & outCont.EncodeTo64UTF8(lbWcName.Text.Trim)
        Return "PlanScheduleCheck.aspx?" & sendPara
    End Function

    Private Sub gvPlan_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPlan.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                Dim PlanDateToday As String = Date1.Text.Trim
                Dim PlanDate As String = .DataItem("PlanDate").ToString.Trim

                If PlanDateToday = PlanDate Then
                    Dim PQty As Decimal = CDec(.DataItem("PlanQty").ToString.Trim.Replace(",", ""))
                    Dim MOQty As Decimal = CDec(.DataItem("TA015").ToString.Trim.Replace(",", ""))
                    Dim ActualQty As Decimal = CDec(.DataItem("ActualQty").ToString.Trim.Replace(",", ""))
                    Dim UnAppQty As Decimal = CDec(.DataItem("UnAppQty").ToString.Trim.Replace(",", ""))
                    Dim ScrapQty As Decimal = CDec(.DataItem("ScrapQty").ToString.Trim.Replace(",", ""))



                    If ActualQty + UnAppQty = 0 Then
                        .ForeColor = Drawing.Color.YellowGreen
                        .Cells(11).BackColor = Drawing.Color.Yellow
                        .Cells(16).BackColor = Drawing.Color.Yellow
                        If Not ReportType.Items(1).Selected And PlanOrCancel.SelectedValue = 1 Then
                            .Visible = False
                        End If
                    Else
                        If ActualQty + UnAppQty >= PQty Then '+ ScrapQty
                            .ForeColor = Drawing.Color.Blue
                            .Cells(11).BackColor = Drawing.Color.Yellow
                            .Cells(16).BackColor = Drawing.Color.Yellow
                            If ActualQty + UnAppQty > PQty Then
                                .ForeColor = Drawing.Color.LightBlue
                            End If
                            If Not ReportType.Items(3).Selected And PlanOrCancel.SelectedValue = 1 Then
                                .Visible = False
                            End If
                        Else
                            .ForeColor = Drawing.Color.Red
                            If Not ReportType.Items(2).Selected And PlanOrCancel.SelectedValue = 1 Then
                                .Visible = False
                            Else
                                .Cells(11).BackColor = Drawing.Color.Yellow
                                .Cells(16).BackColor = Drawing.Color.Yellow
                            End If
                        End If
                        End If


                    If PQty > MOQty Then
                        .ForeColor = Drawing.Color.Gray
                        .Cells(11).BackColor = Drawing.Color.Yellow
                        .Cells(16).BackColor = Drawing.Color.Yellow
                    End If




                ElseIf PlanDate > PlanDateToday Then
                        .ForeColor = Drawing.Color.Black
                    Else
                    .ForeColor = Drawing.Color.Orange
                    .Cells(11).BackColor = Drawing.Color.Yellow
                    .Cells(16).BackColor = Drawing.Color.Yellow
                    If Not ReportType.Items(0).Selected And PlanOrCancel.SelectedValue = 1 Then
                        .Visible = False
                    End If
                End If
            Else

            End If
        End With
    End Sub

    Protected Sub BtShow_Click(sender As Object, e As EventArgs) Handles BtShow.Click
        Dim tempTable As String = "TempPlanSchedule" & Session("UserName")
        CreateTempTable.createTempPlanSchedule(tempTable)

        'lbWc.Text = outCont.DecodeFrom64(Request.QueryString("wc").Trim)
        'Date1.Text = "20210504" 'outCont.DecodeFrom64("MjAyMTA1MDQ=")
        'lbWcName.Text = outCont.DecodeFrom64(Request.QueryString("wcName"))
        'lbWc.Text = Request.QueryString("wc").ToString.Trim
        Dim pDate As String = Date1.Text
        If pDate = "" Then
            pDate = DateTime.Now.ToString("dd-MM-yyyy", New System.Globalization.CultureInfo("en-US")).Replace("-", "/")
        End If
        Date1.Text = pDate

        'Dim wc As String = lbWc.Text,
        Dim planDate As String = Date1.Text
        Dim SQL As String = "",
            ISQL As String = "",
            XSQL As String = ""
        Dim Program As New DataTable,
            Program1 As New DataTable
        ',PlanSeqSet,PlanNote
        'Plan Schedule date select seq=1
        If DDLWorkCenter.SelectedValue = "0" Then
            SQL = "select " & PLANSCHEDULE_T.MoType & "," & PLANSCHEDULE_T.MoNo & "," & PLANSCHEDULE_T.MoSeq & "," & PLANSCHEDULE_T.ProcessCode & "," & PLANSCHEDULE_T.WorkCenter & ",PlanDate,sum(PlanQty),max('1') from PlanSchedule where PlanDate='" & planDate & "' and "
        Else
            SQL = "select " & PLANSCHEDULE_T.MoType & "," & PLANSCHEDULE_T.MoNo & "," & PLANSCHEDULE_T.MoSeq & "," & PLANSCHEDULE_T.ProcessCode & "," & PLANSCHEDULE_T.WorkCenter & ",PlanDate,sum(PlanQty),max('1') from PlanSchedule where PlanDate='" & planDate & "' and " & PLANSCHEDULE_T.WorkCenter & "='" & DDLWorkCenter.SelectedValue & "' and "
        End If

        If PlanOrCancel.SelectedValue = 1 Then
            SQL = SQL & PLANSCHEDULE_T.PlanStatus & "='P'"
        Else
            SQL = SQL & PLANSCHEDULE_T.PlanStatus & "='C'"
        End If
        SQL = SQL & " group by " & PLANSCHEDULE_T.MoType & "," & PLANSCHEDULE_T.MoNo & "," & PLANSCHEDULE_T.MoSeq & "," & PLANSCHEDULE_T.ProcessCode & "," & PLANSCHEDULE_T.WorkCenter & "," & PLANSCHEDULE_T.Urgent & ",PlanDate" '
        ISQL = "insert into " & tempTable & "(TA001,TA002,TA003,TA004,TA006,PlanDate,PlanQty,orderBy) " & SQL
        dbConn.TransactionSQL(ISQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        'Acctual date select
        Dim lastDate As Date = DateTime.ParseExact(planDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim tomorow As String = lastDate.AddDays(1).ToString("yyyyMMdd")
        Dim WHR As String = ""
        WHR = dbConn.WHERE_BETWEEN("substring(SFCTC.CREATE_DATE,1,12)", planDate & "0901", tomorow & "0900")

        SQL = " select TC004,TC005,TC006,TC007,TB005,MAX(TB015) TB015,SUM(case TB013 when 'Y' then cast(TC014 as decimal(15,2)) else 0 end) App,  " &
              " SUM(cast(TC016 as decimal(15,2))) Scp,SUM(case TB013 when 'N' then cast(TC036 as decimal(15,2)) else 0 end) NotApp, " &
              " MAX(SFCTC.CREATE_DATE) CREATE_DATE " &
              " from SFCTC left join SFCTB on TB001=TC001 and TB002=TC002 "
        If DDLWorkCenter.SelectedValue <> "0" Then
            SQL = SQL & " where  TB005='" & DDLWorkCenter.SelectedValue & "'" & WHR
        Else
            SQL = SQL & " where  1=1 AND TB005 in ('W01','W02','W03','W04','W05','W06') " & WHR
        End If

        SQL = SQL & " group by TC004,TC005,TC006,TC007,TB005 "
        Program = dbConn.Query(SQL, VarIni.ERP)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                ' Dim USQL As String = ""
                Dim whrHash As Hashtable = New Hashtable From {
                    {"TA001", .Item("TC004")}, ' Mo type
                    {"TA002", .Item("TC005")}, ' Mo no
                    {"TA003", .Item("TC006")}, ' Mo seq
                    {"TA004", .Item("TC007")}, ' operation
                    {"TA006", .Item("TB005")} ' WC
                }
                'Insert Zone
                Dim fldInsHash As Hashtable = New Hashtable From {
                    {"ActualQty", .Item("App")}, 'Actual qty  approve
                    {"ScrapQty", .Item("Scp")}, 'Scarp Qty
                    {"UnAppQty", .Item("NotApp")}, 'Actual qty not approve
                    {"ActualDate", .Item("TB015")}, 'Actual Date
                    {"TranAcc", .Item("CREATE_DATE")} 'CREATE DATE  .Item("CREATE_DATE")
                }

                Dim SSQL As String = "select top 1 PlanQty,PlanDate from PlanSchedule where " & PLANSCHEDULE_T.MoType & "='" & .Item("TC004") & "' and " & PLANSCHEDULE_T.MoNo & "='" & .Item("TC005") & "'and " & PLANSCHEDULE_T.MoSeq & "='" & .Item("TC006") & "'and " & PLANSCHEDULE_T.ProcessCode & "='" & .Item("TC007") & "' and " & PLANSCHEDULE_T.WorkCenter & "='" & .Item("TB005") & "' and PlanDate<='" & planDate & "' and " & PLANSCHEDULE_T.PlanStatus & "='P' order by PlanDate desc " 'and PlanDate<='" & planDate & "'
                Program1 = dbConn.Query(SSQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                If Program1.Rows.Count > 0 Then
                    With Program1.Rows(0)
                        If planDate <> .Item("PlanDate") Then
                            fldInsHash.Add("PlanQty", .Item("PlanQty")) 'plan Qty
                            fldInsHash.Add("PlanDate", .Item("PlanDate")) 'plan Date
                            fldInsHash.Add("orderBy", "2") 'SEQ
                        Else
                            fldInsHash.Add("orderBy", "1") 'SEQ
                        End If
                    End With
                End If
                'Dim aa As String = Conn_SQL.GetSQL(tempTable, fldInsHash, whrHash)
                dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
            End With
        Next

        'Acctual date not select
        SQL = "select TA001,TA002,TA003,TA004,TA006 from " & tempTable & " where ActualDate='' "
        Program = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                ' Dim USQL As String = ""
                Dim fldInsHash As Hashtable = New Hashtable
                Dim whrHash As Hashtable = New Hashtable From {
                    {"TA001", .Item("TA001")}, ' Mo type
                    {"TA002", .Item("TA002")}, ' Mo no
                    {"TA003", .Item("TA003")}, ' Mo seq
                    {"TA004", .Item("TA004")}, ' operation
                    {"TA006", .Item("TA006")} ' wc
                 }
                'Insert Zone
                'fldInsHash.Add("orderBy", "2") 'SEQ
                Dim SSQL As String = ""
                'If .Item("TA001") = "5102" And .Item("TA002") = "20140717023" And .Item("TA003") = "0030" Then
                '    Dim aa As String = ""
                'End If
                SSQL = " select count(*) cnt,SUM(case TB013 when 'Y' then cast(TC014 as decimal(15,2)) else 0 end) App,  " &
                       " SUM(cast(TC016 as decimal(15,2))) Scp,SUM(case TB013 when 'N' then cast(TC036 as decimal(15,2)) else 0 end) NotApp, " &
                       " MAX(TB003) TB003,MAX(SFCTC.CREATE_DATE) CREATE_DATE " &
                       " from SFCTC left join SFCTB on TB001=TC001 and TB002=TC002 " &
                       " where  TC004='" & .Item("TA001") & "' and TC005='" & .Item("TA002") & "'  and TC006='" & .Item("TA003") & "' " &
                       " and TC007='" & .Item("TA004") & "' and TB005='" & .Item("TA006") & "' " &
                       "" & dbConn.WHERE_BETWEEN("substring(SFCTC.CREATE_DATE,1,12)", planDate & "0901", tomorow & "0900")
                '" and (TB003='" & planDate & "' or substring(SFCTC.CREATE_DATE,1,8) ='" & planDate & "') " ' and  TB003<>'" & planDate & "'

                Program1 = dbConn.Query(SSQL, VarIni.ERP, dbConn.WhoCalledMe)
                If Program1.Rows.Count > 0 Then
                    With Program1.Rows(0)
                        If CInt(.Item("cnt").ToString.Trim) > 0 Then
                            fldInsHash.Add("ActualQty", .Item("App")) 'Actual qty  approve
                            fldInsHash.Add("ScrapQty", .Item("Scp")) 'Scarp Qty
                            fldInsHash.Add("UnAppQty", .Item("NotApp")) 'Actual qty not approve
                            fldInsHash.Add("ActualDate", .Item("TB003")) 'Actual Date
                            fldInsHash.Add("TranAcc", .Item("CREATE_DATE")) 'CREATE DATE
                            dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, whrHash, "UU"), VarIni.DBMIS, dbConn.WhoCalledMe)
                        End If
                    End With
                End If
            End With
        Next
        'standard time
        'SQL = " select T.TA001,T.TA002,T.TA003,T.TA004,T.TA006," &
        '      " isnull(cast(case B.TA015 when 0 then 0 else A.TA035/B.TA015 end as decimal(10,0)),0) as stdMch," &
        '      " isnull(cast(case B.TA015 when 0 then 0 else A.TA022/B.TA015 end as decimal(10,0)),0) as stdMan from  " & tempTable & " T   " &
        '      " left join " & VarIni.ERP & "..SFCTA A on A.TA001=T.TA001 and A.TA002=T.TA002 and A.TA003=T.TA003 and A.TA004=T.TA004 and A.TA006=T.TA006 " &
        '      " left join " & VarIni.ERP & "..MOCTA B on  B.TA001=A.TA001 and B.TA002=A.TA002 "
        'Program = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        'For i As Integer = 0 To Program.Rows.Count - 1
        '    With Program.Rows(i)
        '        ' Dim USQL As String = ""
        '        Dim whrHash As Hashtable = New Hashtable From {
        '            {"TA001", .Item("TA001")}, ' Mo type
        '            {"TA002", .Item("TA002")}, ' Mo no
        '            {"TA003", .Item("TA003")}, ' Mo seq
        '            {"TA004", .Item("TA004")}, ' operation
        '            {"TA006", .Item("TA006")} ' wc
        '        }
        '        'Insert Zone
        '        Dim fldInsHash As Hashtable = New Hashtable From {
        '            {"StdMan", .Item("stdMan")}, 'plan Qty
        '            {"StdMch", .Item("stdMch")} 'plan Date
        '        }
        '        dbConn.TransactionSQL(dbConn.GetSQL(tempTable, fldInsHash, whrHash), VarIni.DBMIS, dbConn.WhoCalledMe)
        '    End With
        'Next


        Dim al As New ArrayList
        Dim fldName As ArrayList
        Dim colName As ArrayList
        With New ArrayListControl(al)
            .TAL("Urgent", "OT")
            .TAL("case PlanSeqSet when '9999' then '' else PlanSeqSet end" & VarIni.C8 & "PlanSeqSet1", "Piority")
            .TAL("PlanNote", "Note")
            .TAL("T.PlanDate" & VarIni.C8 & "PlanDate", "Plan Date")
            .TAL("T.TA001" & VarIni.C8 & "TA001", "MO Type")
            .TAL("T.TA002" & VarIni.C8 & "TA002", "MO No.")
            .TAL("T.TA003" & VarIni.C8 & "TA003", "MO Seq.")
            .TAL("MW002", "Operation")
            .TAL("M.TA035" & VarIni.C8 & "TA035", "Spec")
            .TAL("TA015", "MO Qty", "0")
            .TAL("WIP_QTY", "WIP Qty", "0")
            .TAL("T.PlanQty" & VarIni.C8 & "PlanQty", "Plan Qty", "0") '10
            .TAL("STD_MAN", "Labor STD(Sec)", "0")
            .TAL("STD_MACH", "Machine STD(Sec)", "0")
            .TAL("(T.PlanQty*STD_MAN)/60" & VarIni.C8 & "C", "Labor Need(Min)", "0")
            .TAL("(T.PlanQty*STD_MACH)/60" & VarIni.C8 & "D", "Machine Need(Min)", "0")
            .TAL("T.ActualQty+T.ScrapQty" & VarIni.C8 & "ActualQty", "Actual Qty", "0") '15
            .TAL("UnAppQty", "Not App Qty", "0") '16
            .TAL("ScrapQty", "Scrap Qty", "0")
            .TAL("T.PlanQty-(T.ActualQty+T.ScrapQty+T.UnAppQty)" & VarIni.C8 & "BalQty", "Balance Plan Qty", "0")
            .TAL("ActualDate", "Actual Date")
            .TAL("TranAcc", "Create Date Time")
            .TAL("((T.ActualQty+T.ScrapQty+T.UnAppQty)*STD_MAN)/60" & VarIni.C8 & "A", "Labor Usage(Min)", "0")
            .TAL("((T.ActualQty+T.ScrapQty+T.UnAppQty)*STD_MACH)/60" & VarIni.C8 & "B", "Machine Usage(Min)", "0")
            .TAL("MA002", "Cust Name")
            fldName = .ChangeFormat()
            colName = .ChangeFormat(True)
        End With
        Dim sqlMO As String = "select A.TA001,A.TA002,A.TA003,A.TA004,A.TA006,B.TA015,B.TA026,B.TA027,B.TA035,A.TA010+A.TA013+A.TA016-A.TA011-A.TA012-A.TA014-A.TA015-A.TA048-A.TA056-A.TA058 WIP_QTY, isnull(cast(case B.TA015 when 0 then 0 else A.TA035/B.TA015 end as decimal(10,0)),0) as STD_MACH, isnull(cast(case B.TA015 when 0 then 0 else A.TA022/B.TA015 end as decimal(10,0)),0) as STD_MAN from " & VarIni.ERP & "..SFCTA A left join " & VarIni.ERP & "..MOCTA B on  B.TA001=A.TA001 and B.TA002=A.TA002 "
        Dim strSQL As New SQLString(tempTable & " T ", fldName)
        'strSQL.setLeftjoin(" left join " & VarIni.ERP & "..SFCTA M on M.TA001=T.TA001 and M.TA002=T.TA002 ")
        'strSQL.setLeftjoin(" left join " & VarIni.ERP & "..MOCTA M on M.TA001=T.TA001 and M.TA002=T.TA002 ")
        strSQL.setLeftjoin(" left join (" & sqlMO & ") M on M.TA001=T.TA001 and M.TA002=T.TA002 AND M.TA003=T.TA003 ")
        strSQL.setLeftjoin(" left join " & VarIni.ERP & "..CMSMW W on W.MW001=T.TA004 ")
        strSQL.setLeftjoin(" left join " & VarIni.ERP & "..COPTC C on C.TC001=M.TA026 and C.TC002=M.TA027 ")
        strSQL.setLeftjoin(" left join " & VarIni.ERP & "..COPMA A on A.MA001=C.TC004 ")
        strSQL.setLeftjoin(" left join (select PlanDate,MoType,MoNo,MoSeq,case PlanSeqSet when '' then '9999' else PlanSeqSet end PlanSeqSet,Urgent,PlanNote from PlanSchedule) P on P.PlanDate=T.PlanDate and P.MoType=T.TA001 and P.MoNo=T.TA002 and P.MoSeq=T.TA003 ")

        strSQL.SetWhere(dbConn.WHERE_EQUAL("W.UDF03", "N", "<>"), True)

        strSQL.SetOrderBy("PlanQty,ActualQty,T.orderBy,cast(PlanSeqSet as decimal(10,0)),T.PlanDate,T.TA001,T.TA002,T.TA003")

        'set data to gridview Plan Schedule
        'SQL = " Select T.TA004,A.MA002," &
        '      " cast(((T.ActualQty+T.ScrapQty+T.UnAppQty)*StdMch)/60 As Decimal(10,0)) B,M.TA015,W.MW002," &
        '      " Case When TranAcc='' then '' else (case when substring(TranAcc,1,8)=rtrim(T.ActualDate) then '' else substring(TranAcc,1,8) end)+(case when substring(TranAcc,1,8)=rtrim(T.ActualDate) then '' else ' ' end)+substring(TranAcc,9,2)+':'+substring(TranAcc,11,2)+':'+substring(TranAcc,13,2) end TranAcc" &
        '      " from " & tempTable & " T " &
        '      " left join " & VarIni.ERP & "..MOCTA M on M.TA001=T.TA001 and M.TA002=T.TA002 " &
        '      " left join " & VarIni.ERP & "..CMSMW W on W.MW001=T.TA004 " &
        '      " left join " & VarIni.ERP & "..COPTC C on C.TC001=M.TA026 and C.TC002=M.TA027 " &
        '      " left join " & VarIni.ERP & "..COPMA A on A.MA001=C.TC004 " &
        '      " order by T.orderBy,T.PlanSeqSet,T.PlanDate,T.TA001,T.TA002,T.TA003 " 'where PlanQty>'0'

        gvCont.GridviewColWithLinkFirst(gvPlan, colName, strSplit:=VarIni.C8)
        gvCont.ShowGridView(gvPlan, strSQL.GetSQLString, VarIni.DBMIS)



        'WHR = dbConn.WHERE_EQUAL("MD001", lbWc.Text.Trim)
        SQL = "select MD001,MD002,CMSMD.UDF02,isnull(STD_TIME,0) STD_TIME from CMSMD left join (select MX002,ROUND(SUM((CMSMX.UDF51+UDF52)*CMSMX.UDF53)/60,0) STD_TIME from CMSMX GROUP BY MX002) CMSMX on MX002=MD001 where 1=1 AND MD001 in ('W01','W02','W03','W04','W05','W06') order by MD001 "

        'Dim dr As DataRow = dbConn.QueryDataRow(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        Dim dr As New DataRowControl(SQL, VarIni.ERP, dbConn.WhoCalledMe)
        lbWcLoad.Text = dateCont.ConvertHHMM(dr.Number("STD_TIME"), True)

        SQL = " select isnull(sum(case when PlanDate='" & planDate & "' then 1 else 0 end),0) cntPlan," &
              " isnull(sum(case when ActualDate='" & planDate & "' then 1 else 0 end ),0) cntTran ," &
              " isnull(sum(case when PlanDate='" & planDate & "'  and ActualQty+ScrapQty>=PlanQty and PlanQty>0 then 1 else 0 end),0) cntAct," &
              " isnull(sum(case when PlanDate<>'" & planDate & "'  then 1 else 0 end),0) cntNoPlan," &
              " isnull(sum(case when PlanDate='" & planDate & "' then PlanQty*STD_MAN else 0 end),0) StdMan," &
              " isnull(sum(case when PlanDate='" & planDate & "' then PlanQty*STD_MACH else 0 end),0) StdMch " &
              " from " & tempTable & " T "
        SQL &= " left join (" & sqlMO & ") M on M.TA001=T.TA001 and M.TA002=T.TA002 AND M.TA003=T.TA003 "
        'and PlanDate=ActualDate

        With New DataRowControl(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
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
            lbManTime.Text = dateCont.ConvertHHMM(.Number("StdMan"))
            lbMchTime.Text = dateCont.ConvertHHMM(.Number("StdMch"))
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
        ExportsUtility.ExportGridviewToMsExcel("CheckPlanNotDo", gvPlan)
    End Sub


End Class