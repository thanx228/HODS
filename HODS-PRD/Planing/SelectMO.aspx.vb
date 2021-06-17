Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class SelectMO
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    Dim dbConn As New DataConnectControl
    Dim SelectDate As String = ""
    Dim WorkCenter As String = ""
    Dim StartTime As String
    Dim MachineCode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Session("UserName") = "500026"
            'Session("UserId") = "500026"
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

        End If
        LBMONo.Text = Request("MONo")
        WorkCenter = Request("WorkCenter")
        SelectDate = Request("SelectDate")
        StartTime = Request("StartTime")
        MachineCode = Request("MachineCode")
        ShowGridview()
    End Sub

    Private Sub ShowGridview()
        Dim dt = New DataTable,
        colName As New ArrayList

        dt.Columns.Add("A") '0
        colName.Add("Priority:A")

        dt.Columns.Add("B") '1
        colName.Add("MO Number:B")

        'dt.Columns.Add("Emp Name") '3
        dt.Columns.Add("C") '3
        colName.Add("Name:C")

        dt.Columns.Add("D") '3
        colName.Add("Spec:D")

        dt.Columns.Add("E") '3
        colName.Add("ProcessCode:E")

        dt.Columns.Add("F") '4
        colName.Add("Plan Qty:F")

        dt.Columns.Add("G") '5
        colName.Add("Std Time/Qty:G")

        dt.Columns.Add("H") '5
        colName.Add("TotalTime:H")

        dt.Columns.Add("I") '5
        colName.Add("TotalTime(Hour):I")

        dt.Columns.Add("J") '5
        colName.Add(":J")

        'set format
        ControlForm.GridviewColWithLinkFirst(gvShow, colName)


        'SelectDate = Year(Now()) & Month(Now()).ToString.PadLeft(2, "0") & Day(Now()).ToString.PadLeft(2, "0")

        'WorkCenter = "W01"
        Dim SQL As String = "select * from PlanSchedule where PlanDate='" & SelectDate & "' and WorkCenter='" & WorkCenter & "' and PlanMachine is null"

        Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)

        For L_count As Integer = 0 To dt2.Rows().Count - 1
            'gvShow.Rows
            Dim row = dt.NewRow
            Dim colSeq As Integer = 0
            Dim strSql As String = "select MB002,MB003 from INVMB left join MOCTA on TA006=MB001 where TA001='" & Trim(dt2.Rows(L_count).Item("MoType")) & "' and TA002='" & Trim(dt2.Rows(L_count).Item("MoNo")) & "'"
            Dim dt1 As DataTable = dbConn.Query(strSql, VarIni.ERP, dbConn.WhoCalledMe)

            row(colSeq) = dt2.Rows(L_count).Item("PlanSeq")

            colSeq += 1
            row(colSeq) = Trim(dt2.Rows(L_count).Item("MoType")) & "-" & Trim(dt2.Rows(L_count).Item("MoNo")) & "-" & Trim(dt2.Rows(L_count).Item("MoSeq"))  'emp Code

            colSeq += 1
            row(colSeq) = dt1.Rows(0).Item(0)
            colSeq += 1
            row(colSeq) = dt1.Rows(0).Item(1)
            'Dim strSql As String = "select pmao004 from pmao_t where pmaoent=3 And pmao001='H025' and pmao002='" & dt2.Rows(0).Item("xmdl008") & "'"
            'Dim dt4 As DataTable = dbConn.Query(strSql, VarIni.T100, dbConn.WhoCalledMe)
            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("ProcessCode") 'dt4.Rows(0).Item("pmao004")

            'colSeq += 1
            'row(colSeq) = SelectDate & SerialNo.ToString.PadLeft(4, "0")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("PlanQty")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("PlanTimeStd")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("PlanTime")

            Dim HoursValue As Integer = Math.Floor((dt2.Rows(L_count).Item("PlanTime")) / 3600)
            Dim MinValue As Integer = Math.Ceiling(dt2.Rows(L_count).Item("PlanTime") / 60) Mod 60

            colSeq += 1
            row(colSeq) = HoursValue & "H " & MinValue & "M"

            dt.Rows.Add(row)

            'SerialNo += 1
        Next




        gvShow.DataSource = dt
        gvShow.DataBind()

        For i = 0 To gvShow.Rows.Count - 1
            Dim selectdata As New Button
            selectdata.ID = "alex" & i
            selectdata.Text = "select"
            'selectdata.Attributes.Add("onclick", "javascript:CloseWindows()") '.Add(("onclick", "javascript:CloseWindows()")
            selectdata.CommandArgument = i 'gvShow.Rows(i).Cells(1).Text
            gvShow.Rows(i).Cells(9).Controls.Add(selectdata)
            If LBMONo.Text = gvShow.Rows(i).Cells(1).Text Then
                gvShow.Rows(i).Cells(1).BackColor = Drawing.Color.Red
            End If
        Next
        gvShow.Attributes.Add("unLoad", "javascript:CloseWindows()")
    End Sub

    Protected Sub gvShow_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvShow.RowCommand
        Dim Sindex As String = e.CommandArgument
        Dim SQL As String = "select * from DailyMachinePlan where WorkDate='" & SelectDate & "' and WorkCenter='" & WorkCenter & "'"
        SQL = SQL & " and MachineCode='" & MachineCode & "' and PlanStartTime='" & StartTime & "'"

        Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim Qty As Int16 = Int(sender.rows(Sindex).cells(5).text)
        Dim TotalTime As Int32 = Int(sender.rows(Sindex).cells(7).text)
        Dim MoNo As String = sender.rows(Sindex).cells(1).text
        Dim PlanSeq As String = sender.rows(Sindex).cells(0).text
        Dim strSql As String = ""
        If dt2.Rows.Count <= 0 Then
            strSql = "insert into DailyMachinePlan(WorkDate,WorkCenter,MachineCode,Qty,PlanStartTime,TotalTime,MONo) "
            strSql = strSql & "values('" & SelectDate & "','" & WorkCenter & "','" & MachineCode & "','" & Qty & "','" & StartTime & "','" & TotalTime & "','" & MoNo & "')"
        Else
            strSql = "update DailyMachinePlan set Qty='" & Qty & "',TotalTime='" & TotalTime & "' where WorkDate='" & SelectDate & "' and WorkCenter='" & WorkCenter & "'"
            strSql = strSql & " and MachineCode='" & MachineCode & "' and PlanStartTime='" & StartTime & "'"
        End If
        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
        strSql = "update PlanSchedule set PlanMachine='" & MachineCode & "' where PlanDate='" & SelectDate & "' and WorkCenter='" & WorkCenter & "' and PlanSeq='" & PlanSeq & "'"
        Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
        Response.Write("<script>window.opener.location.reload();</script>")
        Response.Write("<script>window.close();</script>")
    End Sub
End Class