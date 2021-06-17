Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class SelMOForMachine
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    Dim dbConn As New DataConnectControl
    Dim SelectDate As String = ""
    Dim WorkCenter As String = ""
    Dim NowSelectMachine As String
    Dim MachineCode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Session("UserName") = "500026"
            'Session("UserId") = "500026"
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

        End If
        NowSelectMachine = Trim(Request("NowSelectMachine"))
        WorkCenter = Trim(Request("WorkCenter"))
        SelectDate = Trim(Request("SelectDate"))
        ''StartTime = Request("StartTime")
        MachineCode = Trim(Request("NowSelectMachine"))
        ShowGridview()
    End Sub

    Private Sub ShowGridview()
        Dim dt = New DataTable,
        colName As New ArrayList

        dt.Columns.Add("A") '0
        colName.Add("Priority:A")

        dt.Columns.Add("B") '1
        colName.Add("MO Number:B")

        dt.Columns.Add("C") '1
        colName.Add("Name:C")

        dt.Columns.Add("D") '1
        colName.Add("Spec:D")

        'dt.Columns.Add("Emp Name") '3
        dt.Columns.Add("E") '3
        colName.Add("ProcessCode:E")

        dt.Columns.Add("F") '4
        colName.Add("Plan Qty:F")

        dt.Columns.Add("G") '5
        colName.Add("Std Time/Qty:G")

        dt.Columns.Add("H") '5
        colName.Add("TotalSec:H")

        dt.Columns.Add("I") '5
        colName.Add("TotalHour:I")

        dt.Columns.Add("J") '5
        colName.Add("QI:J")

        dt.Columns.Add("K") '5
        colName.Add("Machine Limit:K")

        'dt.Columns.Add("J") '5
        'colName.Add("BookingMachine:J")

        dt.Columns.Add("L") '5
        colName.Add(":L")

        'set format
        ControlForm.GridviewColWithLinkFirst(gvShow, colName)


        'SelectDate = Year(Now()) & Month(Now()).ToString.PadLeft(2, "0") & Day(Now()).ToString.PadLeft(2, "0")
        Dim strSql As String = "select MX002 from CMSMX where MX001='" & NowSelectMachine & "'"
        Dim dt1 As DataTable = dbConn.Query(strSql, VarIni.ERP, dbConn.WhoCalledMe)
        WorkCenter = Trim(dt1.Rows(0).Item(0))

        Dim SQL As String = "select * from PlanSchedule where PlanDate='" & SelectDate & "' and WorkCenter='" & WorkCenter & "' and RealMachine is null and PlanStatus='P' and MoType<>''"


        Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)

        For L_count As Integer = 0 To dt2.Rows().Count '- 1
            'gvShow.Rows

            Dim row = dt.NewRow
            Dim colSeq As Integer = 0

            If L_count < dt2.Rows().Count Then
                Dim strSQL1 As String = "select MB002,MB003,BOMMF.UDF02,BOMMF.UDF03 from INVMB left join MOCTA on TA006=MB001 left join BOMMF on MF001=MB001 where TA001='" & Trim(dt2.Rows(L_count).Item("MoType")) & "' and TA002='" & Trim(dt2.Rows(L_count).Item("MoNo")) & "' and MF003='" & Trim(dt2.Rows(L_count).Item("MoSeq")) & "'"
                Dim dt3 As DataTable = dbConn.Query(strSQL1, VarIni.ERP, dbConn.WhoCalledMe)

                row(colSeq) = dt2.Rows(L_count).Item("PlanSeq")

                colSeq += 1
                row(colSeq) = Trim(dt2.Rows(L_count).Item("MoType")) & "-" & Trim(dt2.Rows(L_count).Item("MoNo")) & "-" & Trim(dt2.Rows(L_count).Item("MoSeq"))  'emp Code

                If dt3.Rows.Count > 0 Then
                    colSeq += 1
                    row(colSeq) = dt3.Rows(0).Item(0)
                    colSeq += 1
                    row(colSeq) = dt3.Rows(0).Item(1)
                Else
                    colSeq += 1
                    row(colSeq) = ""
                    colSeq += 1
                    row(colSeq) = ""
                End If
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

                If dt3.Rows.Count > 0 Then
                    colSeq += 1
                    row(colSeq) = dt3.Rows(0).Item(2)

                    colSeq += 1
                    row(colSeq) = dt3.Rows(0).Item(3)
                Else
                    colSeq += 1
                    row(colSeq) = ""

                    colSeq += 1
                    row(colSeq) = ""
                End If
                'SerialNo += 1
            Else
                    row(colSeq) = "Break!!"

                colSeq += 1
                row(colSeq) = ""


                colSeq += 1
                row(colSeq) = ""
                colSeq += 1
                row(colSeq) = ""
                'Dim strSql As String = "select pmao004 from pmao_t where pmaoent=3 And pmao001='H025' and pmao002='" & dt2.Rows(0).Item("xmdl008") & "'"
                'Dim dt4 As DataTable = dbConn.Query(strSql, VarIni.T100, dbConn.WhoCalledMe)
                colSeq += 1
                row(colSeq) = ""

                'colSeq += 1
                'row(colSeq) = SelectDate & SerialNo.ToString.PadLeft(4, "0")

                colSeq += 1
                row(colSeq) = ""

                colSeq += 1
                row(colSeq) = ""

                colSeq += 1
                row(colSeq) = ""
                colSeq += 1
                row(colSeq) = ""
                colSeq += 1
                row(colSeq) = ""

            End If
            dt.Rows.Add(row)
        Next




        gvShow.DataSource = dt
        gvShow.DataBind()

        For i = 0 To gvShow.Rows.Count - 1
            Dim selectdata As New Button
            selectdata.ID = "alex" & i
            selectdata.Text = "select"
            'selectdata.Attributes.Add("onclick", "javascript:CloseWindows()") '.Add(("onclick", "javascript:CloseWindows()")
            If i < gvShow.Rows.Count Then
                selectdata.CommandArgument = i  'gvShow.Rows(i).Cells(1).Text
            Else
                selectdata.CommandArgument = "Break"
            End If
            gvShow.Rows(i).Cells(11).Controls.Add(selectdata)

            If gvShow.Rows(i).Cells(10).Text <> "&nbsp;" Then
                If gvShow.Rows(i).Cells(10).Text.Contains(NowSelectMachine) Then
                    gvShow.Rows(i).Cells(10).BackColor = Drawing.Color.Green
                Else
                    gvShow.Rows(i).Cells(10).BackColor = Drawing.Color.Red
                End If
            End If
        Next
        gvShow.Visible = True
    End Sub

    Protected Sub gvShow_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvShow.RowCommand
        Dim Sindex As String = e.CommandArgument
        Dim PlanSeq As String = Trim(sender.rows(Sindex).cells(0).text)
        If PlanSeq <> "Break!!" Then
            Dim Qty As Int16 = Int(sender.rows(Sindex).cells(5).text)
            Dim TotalTime As Int32 = Int(sender.rows(Sindex).cells(7).text)
            Dim MoNo As String = Trim(sender.rows(Sindex).cells(1).text)

            Dim StartTime As String = Hour(Now).ToString.PadLeft(2, "0") & Minute(Now).ToString.PadLeft(2, "0")
            Dim strSql As String = ""

            strSql = "insert into DailyMachineReal(WorkDate,WorkCenter,MachineCode,StartTime,MONo,TotalTime) "
            strSql = strSql & "values('" & SelectDate & "','" & Trim(WorkCenter) & "','" & MachineCode & "','" & StartTime & "','" & MoNo & "','" & TotalTime & "')"
            Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
            strSql = "insert into MachineLog (NowDate,MachineID,ChangeMemo,SFrom,STo) values('" &
            SelectDate & "','" & MachineCode & "','From Idle to Running MO" & MoNo & "','1','2')"
            Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
            strSql = "update NowMachine set NowStatus='2',MONo='" & MoNo & "' where NowDate='" & SelectDate & "' and MachineID='" & MachineCode & "'"
            Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
            strSql = "update PlanSchedule set RealMachine='" & MachineCode & "' where PlanDate='" & SelectDate & "' and WorkCenter='" & WorkCenter & "' and PlanSeq='" & PlanSeq & "'"
            Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
        Else
            Dim strSql As String = ""
            strSql = "insert into MachineLog (NowDate,MachineID,ChangeMemo,SFrom,STo) values('" &
            SelectDate & "','" & MachineCode & "','From Idle to Break','1','2')"
            Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
            strSql = "update NowMachine set NowStatus='99',MONo=null,EmpCode=null where NowDate='" & SelectDate & "' and MachineID='" & MachineCode & "'"
            Conn_SQL.Exec_Sql(strSql, Conn_SQL.MIS_ConnectionString)
        End If
        Response.Write("<script>window.opener.location.reload();</script>")
        Response.Write("<script>window.close();</script>")
    End Sub
End Class