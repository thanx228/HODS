Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web

Public Class WorkDay
    Inherits System.Web.UI.Page

    Dim CreateTempTable As New CreateTempTable
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect("Login.aspx")
            End If
            Dim SQL As String = "Select MD001,MD001+':'+MD002 as MD002 from CMSMD order by MD001"
            ControlForm.showDDL(DDLWC, SQL, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString)
        End If
    End Sub

    Protected Sub BuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BuSearch.Click

        Dim TempTable As String = "TempWorkDay" & Session("UserName")
        Dim date1 As String = txtdate1.Text
        Dim date2 As String = txtdate2.Text
        Dim dateToday As Date = DateTime.Today

        Dim strDate As String = ""
        Dim endDate As String = ""

        Dim xd As String = ""
        Dim xm As String = ""
        'Begin date
        If date1 <> "" Then
            Dim temp1() As String = date1.Split("/")

            xd = temp1(1)
            If xd.Length = 1 Then
                xd = "0" & xd
            End If
            xm = temp1(0)
            If xm.Length = 1 Then
                xm = "0" & xm
            End If
            strDate = temp1(2) & xm & xd
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
            Dim temp1() As String = date2.Split("/")
            xd = temp1(1)
            If xd.Length = 1 Then
                xd = "0" & xd
            End If
            xm = temp1(0)
            If xm.Length = 1 Then
                xm = "0" & xm
            End If
            endDate = temp1(2) & xm & xd
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

        CreateTempTable.CreateTempWorkDay(TempTable, beginDate, dateWork)
        Dim wh As String = ""

        If DDLWC.Text <> "ALL" Then
            wh = " and S.TA006='" & DDLWC.Text & "'"
        End If
        ' get normal data on rounting item
        Dim SQL As String = "select S.TA008 as PDATE,S.TA006 as WC,SUM((B.MF010*M.TA015)+B.MF009) as SUMHOUR from SFCTA S " & _
                           "left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " & _
                           "left join BOMMF B on B.MF001=M.TA006 and B.MF003=S.TA003 and B.MF006=S.TA006 " & _
                           "where S.TA005='1'  " & wh & " and S.TA008<>'' and M.TA011 not in ('Y','y') and S.TA032='N' " & _
                           "and S.TA008 BETWEEN '" & strDate & "' and '" & endDate & "' and B.MF010>0 " & _
                           " group by S.TA008,S.TA006 " & _
                           " order by S.TA006,S.TA008 "
        Dim lstWC As String = ""
        Dim Program As New DataTable
        Dim shour As Integer = 0
        Dim smin As Integer = 0
        Dim sumAllHour As Integer = 0
        Dim SQLA As String = ""
        Dim SQLB As String = ""
        Dim SQLALL As String = ""
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            If lstWC <> Program.Rows(i).Item("WC") Then
                If lstWC <> "" Then
                    SQLALL = SQLA & ") " & SQLB & ")"
                    Conn_SQL.Exec_Sql(SQLALL, Conn_SQL.MIS_ConnectionString)
                End If
                SQLA = " Insert into " & TempTable & "(wc"
                SQLB = " VALUES('" & Program.Rows(i).Item("WC") & "'"
           End If
            SQLA = SQLA & "," & "hour" & Program.Rows(i).Item("PDATE")
            SQLB = SQLB & ",'" & Program.Rows(i).Item("SUMHOUR") & "'"
            lstWC = Program.Rows(i).Item("WC")
        Next
        If SQLA <> "" Then
            SQLALL = SQLA & ") " & SQLB & ")"
            Conn_SQL.Exec_Sql(SQLALL, Conn_SQL.MIS_ConnectionString)
        End If
        ' get normal data out of rounting item
        SQL = "select S.TA008 as PDATE,S.TA006 as WC,SUM(S.TA022) as SUMHOUR from SFCTA S " & _
              "left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " & _
              "left join BOMMF B on B.MF001=M.TA006 and B.MF003=S.TA003 " & _
              "where S.TA005='1'  " & wh & " and S.TA008<>'' and M.TA011 not in ('Y','y') and S.TA032='N' " & _
              "and S.TA008 BETWEEN '" & strDate & "' and '" & endDate & "' and S.TA022>0 and B.MF006<>S.TA006 " & _
              " group by S.TA008,S.TA006 " & _
              " order by S.TA006,S.TA008 "

        Dim USQL As String = ""
        Dim wc As String = ""
        Dim pdate As String = ""
        Dim fldName As String = ""
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            sumAllHour = Program.Rows(i).Item("SUMHOUR")
            wc = Program.Rows(i).Item("WC")
            pdate = Program.Rows(i).Item("PDATE")
            fldName = "hour" & pdate
            USQL = "update " & TempTable & " set " & fldName & "=" & fldName & "+'" & sumAllHour & "' " & _
                   " where wc='" & wc & "' IF @@ROWCOUNT = 0 INSERT INTO " & TempTable & "(wc," & fldName & ")" & _
                   " VALUES ('" & wc & "','" & sumAllHour & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        ' get normal data before select date on rounting item
        SQL = " select S.TA006 as WC,SUM((S.TA022/M.TA015)*(M.TA015-S.TA011)) as SUMHOUR from SFCTA S " & _
              " left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " & _
              " left join BOMMF B on B.MF001=M.TA006 and B.MF003=S.TA003 and B.MF006=S.TA006 " & _
              " where S.TA005='1'  " & wh & " and S.TA008<>'' and M.TA011 not in ('Y','y') and S.TA032='N' " & _
              " and S.TA008 < '" & strDate & "' and B.MF010>0 and M.TA015>S.TA011 and  M.TA015-S.TA011 > 0 " & _
              " group by S.TA006 " & _
              " order by S.TA006 "

        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            sumAllHour = Program.Rows(i).Item("SUMHOUR")
            wc = Program.Rows(i).Item("WC")
            USQL = "update " & TempTable & " set hourBefore='" & sumAllHour & "' " & _
                   " where wc='" & wc & "' IF @@ROWCOUNT = 0 INSERT INTO " & TempTable & "(wc,hourBefore)" & _
                   " VALUES ('" & wc & "','" & sumAllHour & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next
        ' get normal data before select date out of rounting item
        SQL = "select S.TA006 as WC,SUM((S.TA022/M.TA015)*(M.TA015-S.TA011)) as SUMHOUR from SFCTA S " & _
                          "left join MOCTA M on M.TA001=S.TA001 and M.TA002=S.TA002 " & _
                          "left join BOMMF B on B.MF001=M.TA006 and B.MF003=S.TA003 " & _
                          "where S.TA005='1'  " & wh & " and S.TA008<>'' and M.TA011 not in ('Y','y') and S.TA032='N' " & _
                          "and S.TA008 < '" & strDate & "' and B.MF010>0 and M.TA015>S.TA011 and  M.TA015-S.TA011 > 0 and B.MF006<>S.TA006 " & _
                          " group by S.TA006 " & _
                          " order by S.TA006 "
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            sumAllHour = Program.Rows(i).Item("SUMHOUR")
            wc = Program.Rows(i).Item("WC")
            USQL = "update " & TempTable & " set hourBefore=hourBefore+'" & sumAllHour & "' " & _
                   " where wc='" & wc & "' IF @@ROWCOUNT = 0 INSERT INTO " & TempTable & "(wc,hourBefore)" & _
                   " VALUES ('" & wc & "','" & sumAllHour & "')"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        SQL = "Select MD001,MD002 from CMSMD order by MD002"
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        For i As Integer = 0 To Program.Rows.Count - 1
            USQL = "update " & TempTable & " set wc_name='" & Program.Rows(i).Item("MD002") & "' where wc='" & Program.Rows(i).Item("MD001") & "'"
            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        Next

        'Dim com As SqlCommand
        Dim conn As New SqlConnection
        Dim strConnString As String
        strConnString = "Data Source=192.168.50.1;Initial Catalog= DBMIS;User Id=mis;Password=Mis2012;Max Pool Size=100"
        conn = New SqlConnection(strConnString)
        conn.Open()

        Dim fldShow As String = ""

        Dim fld_sel As String = getFldSql("hourBefore", "DayBefore", " M.capacity ")
        Dim fldSum As String = "("
        For i As Integer = 0 To dateWork
            fldName = "T.hour" & beginDate.AddDays(i).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            fldShow = "D" & beginDate.AddDays(i).ToString("ddMM", System.Globalization.CultureInfo.InvariantCulture)
            fld_sel = fld_sel & getFldSql(fldName, fldShow, "M.capacity", False)
            fldSum = fldSum & "isnull(" & fldName & ",0)+"
        Next
        fld_sel = fld_sel & getFldSql(fldSum.Substring(0, fldSum.Length - 1) & ")", "SUMMARY", " M.capacity ")

        SQL = " SELECT T.wc as WC,T.wc_name as WC_Name " & fld_sel & " FROM " & TempTable & " T " & _
              " left join MachineCapacity M on M.wc=T.wc  order by T.wc "
        ControlForm.ShowGridView(gvShow, SQL)
        System.Threading.Thread.Sleep(1000)
    End Sub
    Function getFldSql(fldName As String, fldCall As String, fldCap As String, Optional dayShow As Boolean = True) As String
        Dim fldReturn As String = ""
        If dayShow = True Then
            fldReturn = ",(convert(varchar,floor(" & fldName & "/3600)))+':'+(right('0'+convert(varchar,floor((round(" & fldName & "/3600,2)-floor(" & fldName & "/3600))*60)),2)) +((case when " & fldCap & "=0 then '' else(('(')+ convert(varchar,cast(ISNULL(round((round(" & fldName & "/60,4)/" & fldCap & "),2),0) as decimal(9,2))) )+(' Day)')end)) as " & fldCall
        Else
            fldReturn = ",(convert(varchar,floor(" & fldName & "/3600)))+':'+(right('0'+convert(varchar,floor((round(" & fldName & "/3600,2)-floor(" & fldName & "/3600))*60)),2)) +((case when " & fldCap & "=0 then '' else(('(')+ convert(varchar,cast(ISNULL(round((round(" & fldName & "/60,4)/" & fldCap & ")*100,2),0) as decimal(9,2))) )+('%)')end)) as " & fldCall
        End If
        Return fldReturn
    End Function

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "style.backgroundColor='#FFFFC0'")
            If e.Row.RowIndex Mod 2 = 0 Then
                e.Row.Attributes.Add("onmouseout", "style.backgroundColor='#F7F6F3'")
            Else
                e.Row.Attributes.Add("onmouseout", "style.backgroundColor='White'")
            End If
        End If
    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub
End Class