Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class ShowMachineTableList
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

        dt.Columns.Add("A") '5
        colName.Add("MachineID:A")

        dt.Columns.Add("B") '0
        colName.Add("Status:B")

        dt.Columns.Add("C") '1
        colName.Add("MO Number:C")

        dt.Columns.Add("D") '1
        colName.Add("Part No:D")

        dt.Columns.Add("E") '1
        colName.Add("Desc:E")

        'dt.Columns.Add("Emp Name") '3
        dt.Columns.Add("F") '3
        colName.Add("Spec:F")

        dt.Columns.Add("G") '4
        colName.Add("MO Qty:G")

        dt.Columns.Add("H") '5
        colName.Add("Process Qty:H")

        dt.Columns.Add("I") '5
        colName.Add("Finish Qty:I")

        dt.Columns.Add("J") '5
        colName.Add("Start Time:J")

        dt.Columns.Add("K") '5
        colName.Add("Std Pcs:K")

        'set format
        ControlForm.GridviewColWithLinkFirst(gvShow, colName)


        Dim SQL As String = "select * from NowMachine where NowDate='" & SelectDate & "' and MONo is not null order by MachineID"
        Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)

        For L_count As Integer = 0 To dt2.Rows().Count - 1
            'gvShow.Rows
            Dim MOGroup() As String = dt2.Rows(L_count).Item("MONo").ToString.Split("-")
            'Dim MOGroup() As String = ""

            Dim SqlData As String = "select MB001,MB002,MB003,MOC.TA015,SFC.TA003,SFC.TA004,MW002,SFC.TA006,MD002,BOM.MF010,BOM.MF025,MW006" &
            " From INVMB " &
    "Left join MOCTA MOC on TA006=MB001 " &
    "left join SFCTA SFC on MOC.TA001=SFC.TA001 and MOC.TA002=SFC.TA002 " &
    "left join BOMMF BOM on MF001=MB001 and MF003=SFC.TA003 " &
    "left join CMSMD on MD001=SFC.TA006 " &
    "left join CMSMW on MW001=SFC.TA004 " &
    "where MOC.TA001='" & MOGroup(0) & "' and MOC.TA002='" & MOGroup(1) & "' and SFC.TA003='" & MOGroup(2) & "'"
            Dim dtData As DataTable = dbConn.Query(SqlData, VarIni.ERP, dbConn.WhoCalledMe)

            Dim SQLPlanSchedule As String = "select * from PlanSchedule where PlanDate='" & SelectDate &
                 "' and MoType='" & MOGroup(0) & "' and MoNo='" & MOGroup(1) & "' and MoSeq='" & MOGroup(2) & "'"
            Dim dtSQLPlanSchedule As DataTable = dbConn.Query(SQLPlanSchedule, VarIni.DBMIS, dbConn.WhoCalledMe)
            Dim row = dt.NewRow
            Dim colSeq As Integer = 0

            row(colSeq) = dt2.Rows(L_count).Item("MachineID") & "(" & dtData.Rows(0).Item("MW006") & ")"  'A

            Dim strNowStatus As String = ""
            Select Case dt2.Rows(L_count).Item("NowStatus")
                Case "1"
                    strNowStatus = "Idle"
                Case "2"
                    strNowStatus = "Wait Setup Mold"
                Case "3"
                    strNowStatus = "Setting Mold"
                Case "4"
                    strNowStatus = "Wait Runningd"
                Case "5"
                    strNowStatus = "Running"
                Case "99"
                    strNowStatus = "Break"
                Case Else
                    strNowStatus = ""
            End Select
            colSeq += 1
            row(colSeq) = strNowStatus 'B

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("MONo")        'C


            colSeq += 1
            row(colSeq) = dtData.Rows(0).Item("MB001")          'D
            colSeq += 1
            row(colSeq) = dtData.Rows(0).Item("MB002")          'E
            colSeq += 1
            row(colSeq) = dtData.Rows(0).Item("MB003")          'F

            colSeq += 1
            row(colSeq) = dtData.Rows(0).Item("TA015")          'G

            colSeq += 1
            If dtSQLPlanSchedule.Rows.Count > 0 Then
                row(colSeq) = dtSQLPlanSchedule.Rows(0).Item("PlanQty") 'H
            Else
                row(colSeq) = ""
            End If

            colSeq += 1
            Dim sqlMOWorkRecord2 As String = "select sum(Qty) from MOWorkRecord where MONo='" & dt2.Rows(L_count).Item("MONo") & "' and NowDate='" & SelectDate & "' and MachineID='" & dt2.Rows(L_count).Item("MachineID") & "' and WorkType=2"
            Dim dtSMOWorkRecord2 As DataTable = dbConn.Query(sqlMOWorkRecord2, VarIni.DBMIS, dbConn.WhoCalledMe)
            If Not IsDBNull(dtSMOWorkRecord2.Rows(0).Item(0)) Then
                row(colSeq) = dtSMOWorkRecord2.Rows(0).Item(0)          'I
            Else
                row(colSeq) = ""
            End If

            Dim sqlMOWorkRecord As String = "select top 1 * from MOWorkRecord where MONo='" & dt2.Rows(L_count).Item("MONo") & "' and MachineID='" & dt2.Rows(L_count).Item("MachineID") & "' and WorkType=2 order by StartDate desc,StartTime desc"
            Dim dtSMOWorkRecord As DataTable = dbConn.Query(sqlMOWorkRecord, VarIni.DBMIS, dbConn.WhoCalledMe)
            colSeq += 1
            If dtSMOWorkRecord.Rows.Count > 0 Then
                row(colSeq) = dtSMOWorkRecord.Rows(0).Item("StartDate") & "  " & dtSMOWorkRecord.Rows(0).Item("StartTime")
            Else
                row(colSeq) = ""                                        'J
            End If

            colSeq += 1
            row(colSeq) = dtData.Rows(0).Item("MF025")                  'K
            dt.Rows.Add(row)

            'SerialNo += 1
        Next




        gvShow.DataSource = dt
        gvShow.DataBind()

        'For i = 0 To gvShow.Rows.Count - 1
        '    Dim selectdata As New Button
        '    selectdata.ID = "alex" & i
        '    selectdata.Text = "select"
        '    'selectdata.Attributes.Add("onclick", "javascript:CloseWindows()") '.Add(("onclick", "javascript:CloseWindows()")
        '    selectdata.CommandArgument = i 'gvShow.Rows(i).Cells(1).Text
        '    gvShow.Rows(i).Cells(8).Controls.Add(selectdata)
        'Next
        gvShow.Visible = True
    End Sub

    Protected Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click
        For Each row As GridViewRow In gvShow.Rows
            For Each cell As TableCell In row.Cells
                cell.Height = 20
            Next
        Next
        ExportsUtility.ExportGridviewToMsExcel("MachineTableList", gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
End Class