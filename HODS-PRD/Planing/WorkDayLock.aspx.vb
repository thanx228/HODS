Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web

Public Class WorkDayLock
    Inherits System.Web.UI.Page

    Dim CreateTempTable As New CreateTempTable
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim ConfigDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim SQL As String = "Select MD001,MD001+':'+MD002 as MD002 from CMSMD order by MD001"
            ControlForm.showDDL(DDLWC, SQL, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString)
            'If Session("UserName") = "" Then
            '    Response.Redirect("../Login.aspx")
            'End If
        End If
    End Sub

    Protected Sub BuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click

        Dim TempTable As String = "TempWorkDay" & Session("UserName"),
            date1 As String = txtdate1.Text,
            date2 As String = txtdate2.Text,
            dateToday As Date = DateTime.Today,
            strDate As String = "",
            endDate As String = "",
            xd As String = "",
            xm As String = ""
        'Begin date
        If date1 <> "" Then
            strDate = ConfigDate.dateFormat2(date1)
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
        'End date
        If date2 <> "" Then
            endDate = ConfigDate.dateFormat2(date2)
        Else
            xd = dateToday.Day
            If xd.Length = 1 Then
                xd = "0" & xd
            End If
            xm = dateToday.Month
            If xm.Length = 1 Then
                xm = "0" & xm
            End If
            endDate = dateToday.Year & xm & xd
        End If

        Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim lastDate As Date = DateTime.ParseExact(endDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        Dim dateWork As Short = DateDiff(DateInterval.Day, beginDate, lastDate)

        CreateTempTable.CreateTempWorkDayLock(TempTable, beginDate, dateWork)
        Dim wh As String = ""

        If DDLWC.Text <> "ALL" Then
            wh = " and S.TA006='" & DDLWC.Text & "'"
        End If
        ' get normal data on rounting item
        Dim SQL As String = " select S.TA008 as PDATE,S.TA006 as WC,M.TA050 as 'moStatus', " & _
                            " SUM((B.MF010*M.TA015)+B.MF009) as sumHour from SFCTA S " & _
                            " left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " & _
                            " left join BOMMF B on B.MF001=M.TA006 and B.MF003=S.TA003 and B.MF006=S.TA006 " & _
                            " where S.TA005='1'  " & wh & " and S.TA008<>'' and M.TA011 not in ('Y','y') and S.TA032='N' " & _
                            " and S.TA008 BETWEEN '" & strDate & "' and '" & endDate & "' and B.MF010>0 " & _
                            " group by S.TA008,S.TA006,M.TA050 " & _
                            " order by S.TA006,S.TA008,M.TA050 "

        Dim lstWC As String = "",
            lstStatus As String = ""
        Dim Program As New DataTable
        Dim shour As Integer = 0
        Dim smin As Integer = 0
        Dim SQLA As String = "", SQLB As String = "",
            SQLC As String = "", SQLD As String = ""
        Dim SQLALL As String = "", SQLALL2 As String = ""
        'Dim fldInsHashLock As Hashtable = New Hashtable,
        '    whrHashLock As Hashtable = New Hashtable
        Dim InsertHash As Hashtable = New Hashtable,
            whrHash As Hashtable = New Hashtable,
            updHash As Hashtable = New Hashtable
        Dim workDate As New Dictionary(Of String, Integer)

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim moStatus As String = .Item("moStatus").ToString.Trim
                If moStatus <> "1" And moStatus <> "2" And moStatus <> "3" Then
                    moStatus = "4"
                End If
                If lstWC <> .Item("WC") Or lstStatus <> moStatus Then
                    
                    If lstWC <> "" And lstStatus <> "" Then
                        saveData(TempTable, lstWC, lstStatus, workDate)
                    End If
                    workDate = New Dictionary(Of String, Integer)
                End If
                Dim fld As String = "hour" & .Item("PDATE")
                If workDate.ContainsKey(fld) Then
                    workDate(fld) = workDate(fld) + .Item("sumHour")
                Else
                    workDate.Add(fld, .Item("sumHour"))
                End If

                lstWC = .Item("WC")
                lstStatus = moStatus
            End With
        Next
        If lstWC <> "" Then
            saveData(TempTable, lstWC, lstStatus, workDate)
        End If

        'get normal data out of rounting item
        SQL = " select S.TA008 as PDATE,S.TA006 as WC,M.TA050 as 'moStatus'," & _
              " SUM(S.TA022) as sumHour from SFCTA S " & _
              " left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " & _
              " left join BOMMF B on B.MF001=M.TA006 and B.MF003=S.TA003 " & _
              " where S.TA005='1'  " & wh & " and S.TA008<>'' and M.TA011 not in ('Y','y') and S.TA032='N' " & _
              " and S.TA008 BETWEEN '" & strDate & "' and '" & endDate & "' and S.TA022>0 and B.MF006<>S.TA006 " & _
              " group by S.TA008,S.TA006,M.TA050 " & _
              " order by S.TA006,S.TA008,M.TA050 "

        lstWC = ""
        lstStatus = ""
        workDate = New Dictionary(Of String, Integer)
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim moStatus As String = .Item("moStatus").ToString.Trim
                If moStatus <> "1" And moStatus <> "2" And moStatus <> "3" Then
                    moStatus = "4"
                End If
                If lstWC <> .Item("WC") Or lstStatus <> moStatus Then
                    If lstWC <> "" And lstStatus <> "" Then
                        saveData(TempTable, lstWC, lstStatus, workDate)
                    End If
                    workDate = New Dictionary(Of String, Integer)
                End If
                Dim fld As String = "hour" & .Item("PDATE")
                If workDate.ContainsKey(fld) Then
                    workDate(fld) = workDate(fld) + .Item("sumHour")
                Else
                    workDate.Add(fld, .Item("sumHour"))
                End If
                lstWC = .Item("WC")
                lstStatus = moStatus
            End With
        Next
        If lstWC <> "" Then
            saveData(TempTable, lstWC, lstStatus, workDate)
        End If

        ' get normal data before select date on rounting item
        SQL = " select S.TA006 as WC,M.TA050 as 'moStatus'," & _
              " SUM((S.TA022/M.TA015)*(M.TA015-S.TA011)) as sumHour " & _
              " from SFCTA S " & _
              " left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " & _
              " left join BOMMF B on B.MF001=M.TA006 and B.MF003=S.TA003 and B.MF006=S.TA006 " & _
              " where S.TA005='1'  " & wh & " and S.TA008<>'' and M.TA011 not in ('Y','y') and S.TA032='N' " & _
              " and S.TA008 < '" & strDate & "' and B.MF010>0 and M.TA015>S.TA011 and  M.TA015-S.TA011 > 0" & _
              " group by S.TA006,M.TA050 " & _
              " order by S.TA006,M.TA050 "

        lstWC = ""
        lstStatus = ""
        workDate = New Dictionary(Of String, Integer)
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim moStatus As String = .Item("moStatus").ToString.Trim
                If moStatus <> "1" And moStatus <> "2" And moStatus <> "3" Then
                    moStatus = "4"
                End If
                If lstWC <> .Item("WC") Or lstStatus <> moStatus Then
                    If lstWC <> "" And lstStatus <> "" Then
                        saveData(TempTable, lstWC, lstStatus, workDate)
                    End If
                    workDate = New Dictionary(Of String, Integer)
                End If
                Dim fld As String = "hourBefore"
                If workDate.ContainsKey(fld) Then
                    workDate(fld) = workDate(fld) + .Item("sumHour")
                Else
                    workDate.Add(fld, .Item("sumHour"))
                End If
                lstWC = .Item("WC")
                lstStatus = moStatus
            End With
        Next
        If lstWC <> "" Then
            saveData(TempTable, lstWC, lstStatus, workDate)
        End If

        ' get normal data before select date out of rounting item
        SQL = " select S.TA006 as WC,M.TA050 as 'moStatus'," & _
              " SUM((S.TA022/M.TA015)*(M.TA015-S.TA011)) as sumHour " & _
              " from SFCTA S " & _
              " left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " & _
              " left join BOMMF B on B.MF001=M.TA006 and B.MF003=S.TA003 " & _
              " where S.TA005='1'  " & wh & " and S.TA008<>'' and M.TA011 not in ('Y','y') and S.TA032='N' " & _
              " and S.TA008 < '" & strDate & "' and B.MF010>0 and M.TA015>S.TA011 and  M.TA015-S.TA011 > 0 and B.MF006<>S.TA006 " & _
              " group by S.TA006,M.TA050 " & _
              " order by S.TA006,M.TA050 "

        lstWC = ""
        lstStatus = ""
        workDate = New Dictionary(Of String, Integer)
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            With Program.Rows(i)
                Dim moStatus As String = .Item("moStatus").ToString.Trim
                If moStatus <> "1" And moStatus <> "2" And moStatus <> "3" Then
                    moStatus = "4"
                End If
                If lstWC <> .Item("WC") Or lstStatus <> moStatus Then
                    If lstWC <> "" And lstStatus <> "" Then
                        saveData(TempTable, lstWC, lstStatus, workDate)
                    End If
                    workDate = New Dictionary(Of String, Integer)
                End If
                Dim fld As String = "hourBefore"
                If workDate.ContainsKey(fld) Then
                    workDate(fld) = workDate(fld) + .Item("sumHour")
                Else
                    workDate.Add(fld, .Item("sumHour"))
                End If
                lstWC = .Item("WC")
                lstStatus = moStatus
            End With
        Next
        If lstWC <> "" Then
            saveData(TempTable, lstWC, lstStatus, workDate)
        End If
        Dim USQL As String = ""
        SQL = "Select MD001,MD002 from CMSMD order by MD002"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            USQL = "update " & TempTable & " set wc_name='" & Program.Rows(i).Item("MD002") & "' where wc='" & Program.Rows(i).Item("MD001") & "'"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'Dim com As SqlCommand
        'Dim conn As New SqlConnection
        'Dim strConnString As String
        'strConnString = "Data Source=192.168.50.1;Initial Catalog= DBMIS;User Id=mis;Password=Mis2012;Max Pool Size=100"
        'conn = New SqlConnection(strConnString)
        'conn.Open()

        Dim fldShowLock As String = "",
            fldShow As String = "",
            fldName As String = ""

        Dim fld_sel As String = getFldSql("hourBefore", "'Day Before'", " M.capacity ")
        Dim fldSum As String = "("
        For i As Integer = 0 To dateWork
            Dim nDate As String = beginDate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim sDate As String = beginDate.AddDays(i).ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture)

            'fldNameLock = "T.hourLock" & nDate
            'fldShowLock = " '" & sDate & "(Lock)'"
            'fld_sel = fld_sel & getFldSql(fldNameLock, fldShowLock, "M.capacity", False)
            'fldSum = fldSum & "isnull(" & fldNameLock & ",0)+"

            fldName = "T.hour" & nDate
            fldShow = "'" & sDate & "'"
            fld_sel = fld_sel & getFldSql(fldName, fldShow, "M.capacity", False)
            fldSum = fldSum & "isnull(" & fldName & ",0)+"
        Next
        fld_sel = fld_sel & getFldSql(fldSum.Substring(0, fldSum.Length - 1) & ")", "SUMMARY", " M.capacity ")

        SQL = " SELECT T.wc as WC,T.wc_name as 'WC Name',T.moStatus as 'MO Status' " & fld_sel & " FROM " & TempTable & " T " & _
              " left join MachineCapacity M on M.wc=T.wc  order by T.wc,T.moStatus "
        ControlForm.ShowGridView(gvShow, SQL)
        System.Threading.Thread.Sleep(1000)
    End Sub
    Function getFldSql(ByVal fldName As String, ByVal fldCall As String, ByVal fldCap As String, Optional ByVal dayShow As Boolean = True) As String
        Dim fldReturn As String = ""
        If dayShow = True Then
            fldReturn = ",(convert(varchar,floor(" & fldName & "/3600)))+':'+(right('0'+convert(varchar,floor((round(" & fldName & "/3600,2)-floor(" & fldName & "/3600))*60)),2)) +((case when " & fldCap & "=0 then '' else(('(')+ convert(varchar,cast(ISNULL(round((round(" & fldName & "/60,4)/" & fldCap & "),2),0) as decimal(9,2))) )+(' Day)')end)) as " & fldCall
        Else
            fldReturn = ",(convert(varchar,floor(" & fldName & "/3600)))+':'+(right('0'+convert(varchar,floor((round(" & fldName & "/3600,2)-floor(" & fldName & "/3600))*60)),2)) +((case when " & fldCap & "=0 then '' else(('(')+ convert(varchar,cast(ISNULL(round((round(" & fldName & "/60,4)/" & fldCap & ")*100,2),0) as decimal(9,2))) )+('%)')end)) as " & fldCall
        End If
        Return fldReturn
    End Function

    Private Sub gvShow_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvShow.DataBound
        ControlForm.MergeGridview(gvShow, 2)
    End Sub

    Private Sub gvShow_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onmouseover", "style.backgroundColor='#FFFFC0'")
            'If e.Row.RowIndex Mod 2 = 0 Then
            '    e.Row.Attributes.Add("onmouseout", "style.backgroundColor='#F7F6F3'")
            'Else
            '    e.Row.Attributes.Add("onmouseout", "style.backgroundColor='White'")
            'End If
        End If
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
          
                If .RowIndex = 0 Then
                    .Style.Add("height", "40px")
                End If
                Dim wc As String = .DataItem("WC").ToString.Trim
                Dim lock As String = .DataItem("MO Status").ToString.Trim
                Dim fDate As String = ConfigDate.dateFormat2(gvShow.HeaderRow.Cells(4).Text.ToString.Trim, "-")
                Dim tDate As String = ConfigDate.dateFormat2(gvShow.HeaderRow.Cells(gvShow.HeaderRow.Cells.Count - 2).Text.ToString.Trim, "-")
                '.Cells.Item("MO Status").Attributes.Add("onclick", "alert('test')")
                With .Cells(3)
                    Dim selDate As String = ConfigDate.dateFormat2(gvShow.HeaderRow.Cells(4).Text.ToString.Trim, "-")
                    .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                    .Attributes.Add("onclick", "NewWindow('WorkDayLockPopup.aspx?wc=" & wc & "&lock=" & lock & "&fDate=" & selDate & "&tDate=','WorkDayLock',800,500,'yes')")
                End With

                For i As Decimal = 4 To gvShow.HeaderRow.Cells.Count - 2
                    With .Cells(i)
                        Dim selDate As String = ConfigDate.dateFormat2(gvShow.HeaderRow.Cells(i).Text.ToString.Trim, "-")
                        .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                        .Attributes.Add("onclick", "NewWindow('WorkDayLockPopup.aspx?wc=" & wc & "&lock=" & lock & "&fDate=&tDate=" & selDate & "','WorkDayLock',800,500,'yes')")
                    End With
                Next

                With .Cells(gvShow.HeaderRow.Cells.Count - 1)
                    .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                    .Attributes.Add("onclick", "NewWindow('WorkDayLockPopup.aspx?wc=" & wc & "&lock=" & lock & "&fDate=" & fDate & "&tDate=" & tDate & "','WorkDayLock',800,500,'yes')")
                End With
            End If
        End With
    End Sub
    Protected Sub saveData(temptable As String, wc As String, moStatus As String, ByRef dataDic As Dictionary(Of String, Integer))

        Dim insertHash = New Hashtable,
            whereHash = New Hashtable,
            updateHash = New Hashtable,
            pair As KeyValuePair(Of String, Integer)
        whereHash.Add("wc", wc)
        whereHash.Add("moStatus", moStatus)
        For Each pair In dataDic
            insertHash.Add(pair.Key, pair.Value)
            updateHash.Add(pair.Key, pair.Key & "+'" & pair.Value & "' ")
        Next
        Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(temptable, insertHash, updateHash, whereHash), Conn_SQL.MIS_ConnectionString)
    End Sub

End Class