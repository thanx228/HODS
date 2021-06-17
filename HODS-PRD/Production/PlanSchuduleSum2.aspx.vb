Public Class PlanSchuduleSum2
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD  order by MD001 " 'where MD001 not in (" & listNotWC & ")
            ControlForm.showCheckboxList(cblWC, SQL, "MD002", "MD001", 5, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim TempTable As String = "TempPlanScheduleSum2" & Session("UserName")
        Dim TempTable2 As String = "TempPlanScheduleDetail" & Session("UserName")
        Dim dateToday As Date = DateTime.Today

        Dim strDate As String = returnDate(tbDateFrom.Text.Trim) 'Begin date
        Dim endDate As String = returnDate(tbDateTo.Text.Trim) 'End date

        Dim xd As String = ""
        Dim xm As String = ""

        Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim lastDate As Date = DateTime.ParseExact(endDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim tomorow As String = lastDate.AddDays(1).ToString("yyyyMMdd")
        Dim dateWork As Integer = DateDiff(DateInterval.Day, beginDate, lastDate)
        CreateTempTable.createTempPlanScheduleSum2(TempTable)
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

        Dim dt As New DataTable
        WHR = ""
        WHR = WHR & Conn_SQL.Where("TB005", cblWC)
        WHR = WHR & Conn_SQL.Where("substring(SFCTC.CREATE_DATE,1,8)", strDate, endDate)
        'WHR = WHR & Conn_SQL.Where("substring(SFCTC.CREATE_DATE,1,10)", strDate & "09", tomorow & "09")

        SQL = " select TB015,TC004,TC005,TC006,TB015,MAX(substring(SFCTC.CREATE_DATE,1,8)),MAX(TB005),SUM(TC036) " & _
              " from JINPAO80.dbo.SFCTC " & _
              " left join JINPAO80.dbo.SFCTB on TB001=TC001 and TB002=TC002 " & _
              " where 1=1 " & WHR & " group by TC004,TC005,TC006,TB015 "
        ISQL = "insert into " & TempTable2 & " (docDate,moType,moNo,moSeq,trnDate,actDate,wc,trnQty) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)
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
                whrHash.Add("docDate", .Item("PlanDate")) ' Mo type
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
        'get data plan
        SQL = "select TA006,PlanDate,sum(case when Cancled='0' then 1 else 0 end),sum(case when Cancled='0' then 0 else 1 end) from PlanSchedule where 1=1 " & WHR & " group by PlanDate,TA006"
        ISQL = "insert into " & TempTable & "(wc,planDate,planItem,planCan) " & SQL
        Conn_SQL.Exec_Sql(ISQL, Conn_SQL.MIS_ConnectionString)

        'get data from transfer actual
        'SQL = "select wc,plnDate,trnDate,count(*) cnt from " & TempTable2 & " where plnDate=trnDate group by wc,plnDate,trnDate "
        SQL = "select wc,trnDate,count(*) cnt from " & TempTable2 & " where trnDate<>'' " & Conn_SQL.Where("trnDate", strDate, endDate) & " group by wc,trnDate "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                Dim fldInsHash As Hashtable = New Hashtable
                Dim whrHash As Hashtable = New Hashtable
                whrHash.Add("planDate", .Item("trnDate").ToString.Trim) ' Plan Date
                whrHash.Add("wc", .Item("wc").ToString.Trim) ' WC
                'Insert Zone
                fldInsHash.Add("actTran", .Item("cnt")) 'count actual transfer
                Dim aaa As String = Conn_SQL.GetSQL(TempTable, fldInsHash, whrHash, "U")
                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(TempTable, fldInsHash, whrHash, "U"), Conn_SQL.MIS_ConnectionString)
            End With
        Next
        
        'get data from tranfer on plan 
        SQL = "select wc,plnDate,count(*) cnt from " & TempTable2 & " where plnDate=actDate and trnQty>=plnQty " & Conn_SQL.Where("plnDate", strDate, endDate) & " group by wc,plnDate "
        dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        lstWc = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                Dim fldInsHash As Hashtable = New Hashtable
                Dim whrHash As Hashtable = New Hashtable
                whrHash.Add("planDate", .Item("plnDate")) ' Mo type
                whrHash.Add("wc", .Item("wc")) ' Mo type
                'Insert Zone
                fldInsHash.Add("actPlan", .Item("cnt")) 'count actual transfer
                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(TempTable, fldInsHash, whrHash, "U"), Conn_SQL.MIS_ConnectionString)
            End With
        Next

        'show data to gridview

        SQL = " select planDate A,wc B,MD002 C,planItem D,actPlan E," & _
              " case when planItem=0 then 0 else (actPlan/planItem)*100 end F,planItem-actPlan G," & _
              " case when planItem=0 then 0 else ((planItem-actPlan)/planItem)*100 end H,actTran-actPlan I," & _
              " case when planItem=0 then 0 else ((actTran-actPlan)/planItem)*100 end J,actTran K," & _
              " case when planItem=0 then 0 else (actTran/planItem)*100 end L, planCan M , " & _
              " case when planItem=0 then 0 else (planCan/planItem)*100 end N " & _
              " from " & TempTable & " T left join JINPAO80.dbo.CMSMD on MD001=T.wc order by T.wc "

        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
        btExport.Visible = True
        System.Threading.Thread.Sleep(1000)

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

    'Sub ExeSQL(USQL As String, ISQL As String, wc As String, tempTable As String, Optional Update As Boolean = False)
    '    Dim SQL As String = " if exists(select * from " & tempTable & " where wc='" & wc.Trim & "' ) " & USQL.Substring(0, USQL.Count - 1) & " where wc='" & wc.Trim & "' else " & ISQL
    '    If Update Then
    '        SQL = " if exists(select * from " & tempTable & " where wc='" & wc.Trim & "' ) " & USQL.Substring(0, USQL.Count - 1) & " where wc='" & wc.Trim & "' "
    '    End If
    '    Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
    'End Sub

    'Function TextSQL(fldName As String, Val As Decimal) As String
    '    Return fldName & "='" & Val & "',"
    'End Function

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("PlanScheduleSum" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            'If .RowType = DataControlRowType.DataRow Then
            '    '4.On Time %
            '    Dim msg As String = .DataItem("Message").ToString.Trim
            '    If msg = "4.On Time %" Then
            '        .ForeColor = Drawing.Color.Blue
            '    End If
            'End If
        End With
    End Sub
End Class