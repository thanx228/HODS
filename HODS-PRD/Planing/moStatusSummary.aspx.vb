Imports System.Globalization

Public Class moStatusSummary
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            ControlForm.showDDL(ddlSoType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click

        Dim showType As String = ddlShow.Text
        Dim SaleType As String = ddlSoType.Text
        Dim DateType As String = ddlDateType.Text
        Dim DateFrom As String = configDate.dateFormat2(tbDateFrom.Text)
        Dim DateTo As String = configDate.dateFormat2(tbDateTo.Text)
        Dim tempTable As String = "", SQL As String = ""

        Select Case showType
            Case "1"
                tempTable = "tempMoStatusSummary" & Session("UserName")
                CreateTempTable.CreateTempMoSummary(tempTable)
                MO_Status(tempTable, SaleType, DateType, DateFrom, DateTo)
                SQL = "select MoMonth as 'Year Month',cast(MoTotal as decimal(10)) as 'MO Total',cast(MoFinish as decimal(10)) as Finish,cast(MoManual as decimal(10)) as 'Manual Close',cast(MoUnclose as decimal(10)) as 'Unclosed',cast((MoUnclose/MoTotal)*100 as decimal(5,2)) as 'Unclosed%' from " & tempTable & " order by MoMonth "
                ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
            Case "2"
                tempTable = "tempMoDelay" & Session("UserName")
                CreateTempTable.CreateTempMoDelay(tempTable)
                MO_Delay(tempTable, SaleType, DateType, DateFrom, DateTo)
        End Select
        lbCount.Text = gvShow.Rows.Count
        System.Threading.Thread.Sleep(1000)
    End Sub

    Function selFldDate(condition As String, dateType As String) As String
        If condition = "" And dateType = "" Then
            Return ""
        End If

        Dim fldDate As String = ""
        Select Case condition
            Case "1"  ' from start date
                Select Case dateType
                    Case "1" ' plan date
                        fldDate = "TA009"
                    Case "2" ' actual date
                        fldDate = "TA012"
                End Select
            Case "2" 'from complete date
                Select Case dateType
                    Case "1" ' plan date
                        fldDate = "TA010"
                    Case "2" ' actual date
                        fldDate = "TA014"
                End Select
        End Select
        Return fldDate
    End Function

    Function strWhr(condition As String, dateType As String, dateFrom As String, dateTo As String) As String
        If dateFrom = "" And dateTo = "" Then
            Return ""
        End If
        Return configDate.DateWhere(selFldDate(condition, dateType), dateFrom, dateTo)
    End Function

    Sub MO_Status(tempTable As String, SaleType As String, DateType As String, DateFrom As String, DateTo As String)

        Dim whr0 As String = "", SQL As String = "", whr As String = ""
        Dim USQL As String = ""
        Dim fldDate As String = ""
        Dim Program As New DataTable
        Dim moStatus As String = "", cntMo As Integer = 0, fldYM As String = ""
        Dim lastYM As String = ""
        Dim moTotal As Integer = 0
        Dim moFinish As Integer = 0
        Dim moManualClose As Integer = 0
        Dim moUnclose As Integer = 0

        If SaleType <> "ALL" Then
            whr = whr & "and TA026='" & SaleType & "' "
        End If
        whr = whr & strWhr("1", DateType, DateFrom, DateTo)

        'from start date
        fldDate = " SUBSTRING(" & selFldDate("1", DateType) & ",1,6) "
        SQL = " select TA011 as moStatus," & fldDate & " as fldYM ,count(*) as cntMO  from MOCTA where TA013 = 'Y' and " & fldDate & "<>'' " & whr & " group by " & fldDate & ",TA011 order by " & fldDate & ",TA011"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                moStatus = .Item("moStatus")
                fldYM = .Item("fldYM")
                cntMo = .Item("cntMO")
            End With
            If lastYM <> fldYM Then
                If lastYM <> "" Then
                    USQL = " insert into " & tempTable & "(MoMonth,MoTotal,MoUnclose,MoFinish,MoManual)values " & _
                           " ('" & lastYM & "','" & moTotal & "','" & moUnclose & "','" & moFinish & "','" & moManualClose & "')"
                    Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
                End If
                moTotal = 0
                moUnclose = 0
                moFinish = 0
                moManualClose = 0
            End If

            Select Case moStatus
                Case "Y" 'Finish by system
                    moFinish = moFinish + cntMo
                Case "y" 'Finish by people 
                    moManualClose = moManualClose + cntMo
                Case Else '
                    moUnclose = moUnclose + cntMo
            End Select

            'If moStatus <> "Y" And moStatus <> "y" Then
            '    moUnclose = moUnclose + cntMo
            'End If
            moTotal = moTotal + cntMo
            lastYM = fldYM
        Next

        If Program.Rows.Count > 0 Then
            USQL = " insert into " & tempTable & "(MoMonth,MoTotal,MoUnclose,MoFinish,MoManual)values " & _
                   " ('" & lastYM & "','" & moTotal & "','" & moUnclose & "','" & moFinish & "','" & moManualClose & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        End If

        'USQL = " if exists(select * from " & tempTable & " where MoMonth='" & fldYM & "' ) " & _
        '                  " update " & tempTable & " set issueQty=issueQty+'" & Qty & "' where fldYM='" & fldYM & "' else " & _
        '                  " insert into " & tempTable & "(item,issueQty)values ('" & item & "','" & Qty & "')"

        'from complete date
        'whr = ""
        'If SaleType <> "ALL" Then
        '    whr = whr & "and TA026='" & SaleType & "' "
        'End If
        'whr = whr & strWhr("2", DateType, DateFrom, DateTo)

        'lastYM = ""
        'SQL = " select TA011 as moStatus," & fldDate & " as fldYM ,count(*) as cntMO  from MOCTA where TA013 ='Y' and TA011 in ('Y','y') and " & fldDate & "<>'' " & whr & " group by " & fldDate & ",TA011 order by " & fldDate & ",TA011 "
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        'For i As Integer = 0 To Program.Rows.Count - 1
        '    With Program.Rows(i)
        '        moStatus = .Item("moStatus")
        '        fldYM = .Item("fldYM")
        '        cntMo = .Item("cntMO")
        '    End With
        '    If lastYM <> fldYM Then
        '        If lastYM <> "" Then
        '            USQL = " update " & tempTable & " set MoFinish='" & moFinish & "',MoManual='" & moManualClose & "' where MoMonth='" & lastYM & "' "
        '            'USQL = " if exists(select * from " & tempTable & " where MoMonth='" & fldYM & "' ) " & _
        '            '       " update " & tempTable & " set MoFinish='" & moFinish & "',MoManual='" & moManualClose & "' where MoMonth='" & lastYM & "' else " & _
        '            '       " insert into " & tempTable & "(MoMonth,MoFinish,moManualClose)values ('" & lastYM & "','" & moFinish & "','" & moManualClose & "')"
        '            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        '        End If
        '        moFinish = 0
        '        moManualClose = 0
        '    End If
        '    If moStatus = "Y" Then ' finish
        '        moFinish = moFinish + cntMo
        '    Else ' manual close
        '        moManualClose = moManualClose + cntMo
        '    End If
        '    lastYM = fldYM
        'Next
        'If Program.Rows.Count > 0 Then
        '    USQL = " update " & tempTable & " set MoFinish='" & moFinish & "',MoManual='" & moManualClose & "' where MoMonth='" & lastYM & "' "
        '    'USQL = " if exists(select * from " & tempTable & " where MoMonth='" & fldYM & "' ) " & _
        '    '       " update " & tempTable & " set MoFinish='" & moFinish & "',MoManual='" & moManualClose & "' where MoMonth='" & lastYM & "' else " & _
        '    '       " insert into " & tempTable & "(MoMonth,MoFinish,moManualClose)values ('" & lastYM & "','" & moFinish & "','" & moManualClose & "')"
        '    Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        'End If
    End Sub
    Sub MO_Delay(tempTable As String, SaleType As String, DateType As String, DateFrom As String, DateTo As String)

        Dim SQL As String = "", SQL1 As String = ""
        Dim WHR As String = ""
        Dim D01 As Integer = 0, D08 As Integer = 0
        Dim D15 As Integer = 0, D22 As Integer = 0
        Dim D31 As Integer = 0

        If SaleType <> "ALL" Then
            WHR = WHR & "and TA026='" & SaleType & "' "
        End If
        WHR = WHR & configDate.DateWhere("TA003", configDate.dateFormat2(tbDateFrom.Text), configDate.dateFormat2(tbDateTo.Text))
        Dim Program As New DataTable
        SQL1 = " select count(*) as cntMO  from MOCTA where TA013 = 'Y' " & WHR
        Program = Conn_SQL.Get_DataReader(SQL1, Conn_SQL.ERP_ConnectionString)
        Dim AllMo As Integer = 0
        If Program.Rows.Count > 0 Then
            AllMo = Program.Rows(0).Item("cntMO")
        End If
        SQL = " select TA010,TA014,count(*) as cntMO  from MOCTA where TA013 = 'Y'and TA011 in ('Y','y') and TA010<>'' and TA014>TA010 " & WHR & "  group by TA010,TA014  order by TA010,TA014"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            Dim PlanDate As Date = DateTime.ParseExact(Program.Rows(i).Item("TA010"), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim ActualDate As Date = DateTime.ParseExact(Program.Rows(i).Item("TA014"), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim cntMO As Integer = Program.Rows(i).Item("cntMO")
            Dim dateDelay As Integer = DateDiff(DateInterval.Day, PlanDate, ActualDate)
            If dateDelay > 0 Then
                If dateDelay < 8 Then '1-7
                    D01 += cntMO
                ElseIf dateDelay < 15 Then '8-14
                    D08 += cntMO
                ElseIf dateDelay < 21 Then '15-21
                    D15 += cntMO
                ElseIf dateDelay < 32 Then '22-31
                    D22 += cntMO
                Else '>31
                    D31 += cntMO
                End If
            End If
        Next
        Dim Percent As Double = 0
        Dim dt As Data.DataTable = New DataTable
        dt.Columns.Add(New DataColumn("Delay Day"))
        dt.Columns.Add(New DataColumn("MO Delay"))
        dt.Columns.Add(New DataColumn("Delay%"))
        'Dim AllMo As Integer = Program.Rows.Count

        Dim dr As DataRow = dt.NewRow()
        dr = dt.NewRow()
        dr("Delay Day") = "Delay 1 - 7 "
        dr("MO Delay") = FormatNumber(D01.ToString, 0, TriState.False, TriState.False)
        dr("Delay%") = CalPercent(D01, AllMo)
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Delay Day") = "Delay 8 - 14 "
        dr("MO Delay") = FormatNumber(D08.ToString, 0, TriState.False, TriState.False)
        dr("Delay%") = CalPercent(D08, AllMo)
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Delay Day") = "Delay 15 - 21 "
        dr("MO Delay") = FormatNumber(D15.ToString, 0, TriState.False, TriState.False)
        dr("Delay%") = CalPercent(D15, AllMo)
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Delay Day") = "Delay 22-31 "
        dr("MO Delay") = FormatNumber(D22.ToString, 0, TriState.False, TriState.False)
        dr("Delay%") = CalPercent(D22, AllMo)
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Delay Day") = "Over 31 "
        dr("MO Delay") = FormatNumber(D31.ToString, 0, TriState.False, TriState.False)
        dr("Delay%") = CalPercent(D31, AllMo)
        dt.Rows.Add(dr)

        Dim allDelay As Integer = D01 + D08 + D15 + D22 + D31
        dr = dt.NewRow()
        dr("Delay Day") = "Total Delay"
        dr("MO Delay") = FormatNumber(allDelay.ToString, 0, TriState.False, TriState.False)
        dr("Delay%") = CalPercent(allDelay, AllMo)
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Delay Day") = "ALL MO"
        dr("MO Delay") = FormatNumber(AllMo.ToString, 0, TriState.False, TriState.False)
        dr("Delay%") = ""
        dt.Rows.Add(dr)

        gvShow.DataSource = dt
        gvShow.DataBind()
    End Sub
    Function CalPercent(Delay As Integer, DelayAll As Integer) As Double
        Dim Percent As Double = 0
        If Delay > 0 Then
            Percent = (Delay / DelayAll) * 100
        End If
        Return Math.Round(Percent, 2)
    End Function
End Class