Public Class PlanSchuduleSum
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD  order by MD001 " 'where MD001 not in (" & listNotWC & ")
            ControlForm.showCheckboxList(cblWC, SQL, "MD002", "MD001", 5, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim TempTable As String = "TempPlanScheduleSum" & Session("UserName")
        Dim TempTable2 As String = "TempPlanScheduleDetail" & Session("UserName")
        Dim dateToday As Date = DateTime.Today

        Dim strDate As String = returnDate(tbDateFrom.Text.Trim) 'Begin date
        Dim endDate As String = returnDate(tbDateTo.Text.Trim) 'End date

        Dim xd As String = "",xm As String = ""

        Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim lastDate As Date = DateTime.ParseExact(endDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim tomorow As String = lastDate.AddDays(1).ToString("yyyyMMdd")
        Dim dateWork As Integer = DateDiff(DateInterval.Day, beginDate, lastDate)
        CreateTempTable.createTempPlanScheduleSum(TempTable, beginDate, dateWork)
        CreateTempTable.createTempPlanScheduleDetail(TempTable2)

        Dim SQL As String = "",
            WHR As String = "",
            wc As String = "",
            lstWc As String = "",
            cntItem As Decimal = 0,
            USQL As String = "",
            fld As String = "",
            val As String = "",
            fldName As String = "",
            fldVal As String = "",
            ISQL As String = ""

        'get data from plan schedule
        WHR = ""
        WHR = WHR & Conn_SQL.Where("TA006", cblWC)
        WHR = WHR & Conn_SQL.Where("PlanDate", strDate, endDate)

        Sql = "select TA006,PlanDate,count(*) as cnt from PlanSchedule where Cancled='0' " & WHR & " group by TA006,PlanDate order by TA006,PlanDate"
        Dim dt As New DataTable

        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                wc = .Item("TA006")
                If lstWc <> wc Then
                    If lstWc <> "" Then
                        fld = fld & "wc"
                        val = val & "'" & lstWc & "'"
                        ISQL = "insert into " & TempTable & "(" & fld & ") values(" & val & ")"
                        ExeSQL(USQL, ISQL, lstWc, TempTable)
                    End If
                    USQL = " update " & TempTable & " set "
                    fld = ""
                    val = ""
                End If
                fldName = "plan" & .Item("PlanDate")
                fldVal = .Item("cnt")

                USQL = USQL & TextSQL(fldName, fldVal)
                fld = fld & fldName & ","
                val = val & "'" & fldVal & "',"

                lstWc = .Item("TA006")
            End With
        Next
        If lstWc <> "" Then
            fld = fld & "wc"
            val = val & "'" & lstWc & "'"
            ISQL = "insert into " & TempTable & "(" & fld & ") values(" & val & ")"
            ExeSQL(USQL, ISQL, lstWc, TempTable)
        End If

        'detail From transfer
        'For i As Integer = 0 To dateWork
        '    Dim tdate As String = beginDate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        '    'Dim tdate2 As String = beginDate.AddDays(i + 1).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        '    WHR = ""
        '    WHR = WHR & Conn_SQL.Where("TB005", cblWC)
        '    'WHR = WHR & Conn_SQL.Where("substring(SFCTC.CREATE_DATE,1,12)", tdate & "0900", tdate2 & "0900")
        '    WHR = WHR & Conn_SQL.Where("substring(SFCTC.CREATE_DATE,1,8)", tdate, tdate)
        '    SQL = " select TB015,TC004,TC005,TC006,TB015,MAX(substring(SFCTC.CREATE_DATE,1,8)),MAX(TB005),SUM(TC036) " & _
        '          " from JINPAO80.dbo.SFCTC " & _
        '          " left join JINPAO80.dbo.SFCTB on TB001=TC001 and TB002=TC002 " & _
        '          " where 1=1 " & WHR & " group by TC004,TC005,TC006,TB015 "
        '    ISQL = "insert into " & TempTable2 & " (docDate,moType,moNo,moSeq,trnDate,actDate,wc,trnQty) " & SQL
        '    Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
        'Next

        For i As Integer = 0 To dateWork
            Dim xtoday As String = beginDate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim xtomorow As String = beginDate.AddDays(i + 1).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            WHR = ""
            WHR = WHR & Conn_SQL.Where("TB005", cblWC)
            'WHR = WHR & Conn_SQL.Where("substring(SFCTC.CREATE_DATE,1,8)", strDate, endDate)-max(TB015),--MAX(substring(SFCTC.CREATE_DATE,1,8)),
            WHR = WHR & Conn_SQL.Where("substring(SFCTC.CREATE_DATE,1,10)", xtoday & "0900", xtomorow & "0900")

            SQL = " select '" & xtoday & "',TC004,TC005,TC006,max(TB015),'" & xtoday & "',MAX(TB005),SUM(TC036) " & _
                  " from JINPAO80.dbo.SFCTC " & _
                  " left join JINPAO80.dbo.SFCTB on TB001=TC001 and TB002=TC002 " & _
                  " where 1=1 " & WHR & " group by TC004,TC005,TC006 "
            ISQL = "insert into " & TempTable2 & " (docDate,moType,moNo,moSeq,trnDate,actDate,wc,trnQty) " & SQL
            Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
        Next



        
        'detail From Plan Schedule
        WHR = ""
        WHR = WHR & Conn_SQL.Where("TA006", cblWC)
        WHR = WHR & Conn_SQL.Where("PlanDate", strDate, endDate)

        SQL = "select PlanDate,TA001,TA002,TA003,MAX(TA006) TA006,sum(PlanQty) pqty from PlanSchedule where Cancled='0' " & WHR & " group by PlanDate,TA001,TA002,TA003"
        Dim Program As New DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim fldInsHash As Hashtable = New Hashtable
                Dim whrHash As Hashtable = New Hashtable
                whrHash.Add("docDate", .Item("PlanDate")) ' doc date
                whrHash.Add("moType", .Item("TA001")) ' Mo type
                whrHash.Add("moNo", .Item("TA002")) ' Mo no
                whrHash.Add("moSeq", .Item("TA003")) ' Mo seq
                'Insert Zone
                fldInsHash.Add("plnQty", .Item("pqty")) 'plan qty
                fldInsHash.Add("plnDate", .Item("PlanDate")) 'plan date
                fldInsHash.Add("wc", .Item("TA006")) 'work center
                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(TempTable2, fldInsHash, whrHash), Conn_SQL.MIS_ConnectionString)
            End With
        Next

        'get data from transfer actual
        ' WHR = Conn_SQL.Where("trnDate", strDate, endDate)
        WHR = Conn_SQL.Where("trnDate", strDate, endDate)
        'SQL = "select wc,plnDate,trnDate,count(*) cnt from " & TempTable2 & " where plnDate=trnDate group by wc,plnDate,trnDate "
        SQL = "select wc,trnDate,count(*) cnt from " & TempTable2 & " where trnDate<>'' " & WHR & " group by wc,trnDate "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        lstWc = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                wc = .Item("wc")
                If lstWc <> wc Then
                    If lstWc <> "" Then
                        ExeSQL(USQL, "", lstWc, TempTable, True)
                    End If
                    USQL = " update " & TempTable & " set "
                End If
                fldName = "actTran" & .Item("trnDate")
                fldVal = .Item("cnt")
                USQL = USQL & TextSQL(fldName, fldVal)
                lstWc = wc
            End With
        Next
        If lstWc <> "" Then
            ExeSQL(USQL, "", lstWc, TempTable, True)
        End If

        'get data from tranfer on plan 
        WHR = Conn_SQL.Where("plnDate", strDate, endDate)
        SQL = "select wc,plnDate,actDate,count(*) cnt from " & TempTable2 & " where plnDate=actDate and trnQty>=plnQty " & WHR & " group by wc,plnDate,actDate "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        lstWc = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                wc = .Item("wc")
                If lstWc <> wc Then
                    If lstWc <> "" Then
                        ExeSQL(USQL, "", lstWc, TempTable, True)
                    End If
                    USQL = " update " & TempTable & " set "
                End If
                fldName = "actPlan" & .Item("plnDate")
                fldVal = .Item("cnt")
                USQL = USQL & TextSQL(fldName, fldVal)
                lstWc = wc
            End With
        Next
        If lstWc <> "" Then
            ExeSQL(USQL, "", lstWc, TempTable, True)
        End If

        'show data to gridview
        dt = New DataTable
        dt.Columns.Add(New DataColumn("Work Center"))
        dt.Columns.Add(New DataColumn("Message"))
        dt.Columns.Add(New DataColumn("Sum"))
        Dim sumPlan As String = "0",
            sumTrn As String = "0",
            sumAct As String = "0"
        For i As Integer = 0 To dateWork
            Dim tdate As String = beginDate.AddDays(i).ToString("MMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim tdate1 As String = beginDate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            dt.Columns.Add(New DataColumn(tdate))
            sumPlan = sumPlan & "+T.plan" & tdate1
            sumTrn = sumTrn & "+T.actTran" & tdate1
            sumAct = sumAct & "+T.actPlan" & tdate1
        Next
        Dim dr1 As DataRow,
            dr2 As DataRow,
            dr3 As DataRow,
            dr4 As DataRow,
            dr5 As DataRow
        SQL = "select T.*,MD002," & sumPlan & " sumplan," & sumTrn & " sumTrn," & sumAct & " sumAct from " & TempTable & " T left join JINPAO80.dbo.CMSMD on MD001=T.wc order by T.wc"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                wc = .Item("wc").ToString.Trim & ":" & .Item("MD002").ToString.Trim
                Dim col1 As String = "Work Center",
                    col2 As String = "Message",
                    col3 As String = "Sum",
                    sumPlan1 As Decimal = .Item("sumplan"),
                    actPlan1 As Decimal = .Item("sumAct")
                'Plan
                dr1 = dt.NewRow()
                dr1(col1) = wc
                dr1(col2) = "1.Plan"
                dr1(col3) = FormatNumber(sumPlan1, 0, TriState.True)

                'Actaul Transfer
                dr2 = dt.NewRow()
                dr2(col1) = wc
                dr2(col2) = "2.Actual Trans."
                dr2(col3) = FormatNumber(.Item("sumTrn"), 0, TriState.True)

                'Actual On plan
                dr3 = dt.NewRow()
                dr3(col1) = wc
                dr3(col2) = "3.Actual on Plan"
                dr3(col3) = FormatNumber(actPlan1, 0, TriState.True)

                'on Time%
                dr4 = dt.NewRow()
                dr4(col1) = wc
                dr4(col2) = "4.On Time %"
                Dim sumPer As Decimal = 0
                If sumPlan1 > 0 Then
                    sumPer = (actPlan1 / sumPlan1) * 100
                End If
                dr4(col3) = FormatNumber(sumPer, 2, TriState.True)

                If cbAdd.Checked Then
                    dr5 = dt.NewRow()
                    dr5(col1) = wc
                    dr5(col2) = "5."
                    dr5(col3) = ""
                End If
                For j As Integer = 0 To dateWork
                    Dim tdate As String = beginDate.AddDays(j).ToString("MMdd", System.Globalization.CultureInfo.InvariantCulture)
                    Dim tdate1 As String = beginDate.AddDays(j).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                    Dim valSumPlan As Decimal = .Item("plan" & tdate1),
                        valActPlan As Decimal = .Item("actPlan" & tdate1)
                    dr1(tdate) = FormatNumber(valSumPlan, 0, TriState.True)
                    dr2(tdate) = FormatNumber(.Item("actTran" & tdate1), 0, TriState.True)
                    dr3(tdate) = FormatNumber(valActPlan, 0, TriState.True)
                    sumPer = 0
                    If valSumPlan > 0 Then
                        sumPer = (valActPlan / valSumPlan) * 100
                    End If
                    dr4(tdate) = FormatNumber(sumPer, 2, TriState.True)
                    If cbAdd.Checked Then
                        dr5(tdate) = ""
                    End If
                Next
                dt.Rows.Add(dr1)
                dt.Rows.Add(dr2)
                dt.Rows.Add(dr3)
                dt.Rows.Add(dr4)
                If cbAdd.Checked Then
                    dt.Rows.Add(dr5)
                End If
            End With
        Next
        If Program.Rows.Count > 0 Then
            gvShow.DataSource = dt
            gvShow.DataBind()
        End If
        Dim rowLimit As Integer = 4
        If cbAdd.Checked Then
            rowLimit = 5
        End If
        lbCount.Text = gvShow.Rows.Count / rowLimit
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)
        'Dim aa As String = " select * from " & TempTable
        'Dim bb As String = ""

    End Sub

    Private Function returnDate(dateVal As String) As String
        Dim dateToday As Date = DateTime.Today,
            strDate As String = "",
            xd As String = "",
            xm As String = ""
        If dateVal <> "" Then
            strDate = configDate.dateFormat2(dateVal)
        Else
            xd = dateToday.Day
            If xd.Length = 1 Then
                xd = "0" & xd
            End If
            xm = dateToday.Month
            If xm.Length = 1 Then
                xm = "0" & xm
            End If
            strDate = dateToday.Year & xm & xd
        End If
        Return strDate
    End Function

    Sub ExeSQL(USQL As String, ISQL As String, wc As String, tempTable As String, Optional Update As Boolean = False)
        Dim SQL As String = " if exists(select * from " & tempTable & " where wc='" & wc.Trim & "' ) " & USQL.Substring(0, USQL.Count - 1) & " where wc='" & wc.Trim & "' else " & ISQL
        If Update Then
            SQL = " if exists(select * from " & tempTable & " where wc='" & wc.Trim & "' ) " & USQL.Substring(0, USQL.Count - 1) & " where wc='" & wc.Trim & "' "
        End If
        Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
    End Sub

    Function TextSQL(fldName As String, Val As Decimal) As String
        Return fldName & "='" & Val & "',"
    End Function

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("PlanScheduleSum" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                '4.On Time %
                Dim msg As String = .DataItem("Message").ToString.Trim
                If msg = "4.On Time %" Then
                    .ForeColor = Drawing.Color.Blue
                End If
            End If
        End With
    End Sub
End Class