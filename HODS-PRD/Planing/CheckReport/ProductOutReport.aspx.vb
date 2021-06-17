Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Imports System.Data.SqlClient
Imports System.Collections.Generic
Public Class ProductOutReport
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim dbConn As New DataConnectControl
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    'Dim NowSelectMachine As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then
            'Session("UserName") = "500026"
            'Session("UserId") = "500026"
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../../Login.aspx"))
            End If
        Else
        End If
    End Sub

    Protected Sub ReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ReportType.SelectedIndexChanged
        Select Case ReportType.SelectedValue
            Case "All"
                CBLSecCate.Visible = False
            Case "WC"
                CBLSecCate.Items.Clear()
                Dim SQL As String = "select MD001,MD002 from CMSMD where MD001 in ('W01','W02','W03','W04','W05','W06')"
                Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
                For L_count As Integer = 0 To dt2.Rows.Count - 1
                    Dim alex As New ListItem
                    alex.Value = Trim(dt2.Rows(L_count).Item(0))
                    alex.Text = dt2.Rows(L_count).Item(1)
                    CBLSecCate.Items.Add(alex)
                Next
                CBLSecCate.Visible = True
            Case "Employee"
                CBLSecCate.Items.Clear()
                Dim SQLHR As String = "select emp.Code,cnname +'-' + enName from employee emp left join Job JB on emp.JobId=JB.JOBID left join Department Dept on Dept.departmentid=emp.DepartmentId where emp.LastWorkDate>=getdate() and JB.Name='Operator' and Dept.ShortName ='Stamping' order by emp.code"
                Dim dtHR As DataTable = dbConn.Query(SQLHR, VarIni.HRMHT, dbConn.WhoCalledMe)
                For L_count As Integer = 0 To dtHR.Rows.Count - 1
                    Dim alex As New ListItem
                    alex.Value = Trim(dtHR.Rows(L_count).Item(0))
                    alex.Text = "  " & Trim(dtHR.Rows(L_count).Item(1)) & "(" & Trim(dtHR.Rows(L_count).Item(0)) & ")  "
                    CBLSecCate.Items.Add(alex)
                Next
                CBLSecCate.Visible = True
            Case "Machine"
                CBLSecCate.Items.Clear()
                Dim SQL As String = "select MX001,MX006 from CMSMX where MX002 in ('W01','W02','W03','W04','W05','W06') order by MX006"
                Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.ERP, dbConn.WhoCalledMe)
                For L_count As Integer = 0 To dt2.Rows.Count - 1
                    Dim alex As New ListItem
                    alex.Value = Trim(dt2.Rows(L_count).Item(0))
                    alex.Text = "  " & Trim(dt2.Rows(L_count).Item(1)) & "(" & Trim(dt2.Rows(L_count).Item(0)) & ")  "
                    CBLSecCate.Items.Add(alex)
                Next
                CBLSecCate.Visible = True
        End Select
    End Sub

    Protected Sub BtnShow_Click(sender As Object, e As EventArgs) Handles BtnShow.Click
        Select Case ReportType.SelectedValue
            Case "All"
                ShowGridviewAll()
            Case "WC"
                ShowGridviewWC()
            Case "Employee"
                ShowGridviewEmployeeDetail()
                ShowGridviewEmployeeTotal()
            Case "Machine"
                ShowGridviewMachine()
        End Select

    End Sub

    Private Sub ShowGridviewAll()
        Dim dt = New DataTable,
        colName As New ArrayList

        Dim dtTotal = New DataTable,
        colNameTotal As New ArrayList

        dt.Columns.Add("A") '0
        colName.Add("Date:A")
        dt.Columns.Add("B") '1
        colName.Add("MO:B")
        dt.Columns.Add("C") '1
        colName.Add("Desc/Spec:C")
        dt.Columns.Add("D") '1
        colName.Add("Machine:D")
        dt.Columns.Add("E") '1
        colName.Add("Start:E")
        dt.Columns.Add("F") '1
        colName.Add("End:F")
        dt.Columns.Add("G") '1
        colName.Add("Employee:G")
        dt.Columns.Add("H") '1
        colName.Add("Qty:H")
        dt.Columns.Add("I") '1
        colName.Add("Std Pcs:I")
        dt.Columns.Add("J") '1
        colName.Add("OutPut(Hour):J")
        dt.Columns.Add("K") '1
        colName.Add("Work Center:K")


        dtTotal.Columns.Add("A") '0
        colNameTotal.Add("Date:A")
        dtTotal.Columns.Add("B") '0
        colNameTotal.Add("Total MO:B")
        dtTotal.Columns.Add("C") '0
        colNameTotal.Add("Total Qty:C")
        dtTotal.Columns.Add("D") '0
        colNameTotal.Add("Total Output:D")

        'set format
        ControlForm.GridviewColWithLinkFirst(gvShow1, colName)
        ControlForm.GridviewColWithLinkFirst(gvShow, colNameTotal)


        Dim SQL As String = "select * from MOWorkRecord where 1=1 and WorkType=2 "
        If DateFrom.Text <> "" Then
            SQL = SQL & " and NowDate>='" & DateFrom.Text & "'"
        End If

        If DateEnd.Text <> "" Then
            SQL = SQL & " and NowDate<='" & DateEnd.Text & "'"
        End If

        Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)

        Dim GroupDate As String = ""
        Dim GroupWorkCenter As String = ""
        Dim GroupMachine As String = ""
        Dim TotalMO As Integer = 0
        Dim TotalQty As Integer = 0
        Dim TotalOutput As Double = 0
        For L_count As Integer = 0 To dt2.Rows().Count - 1
            'gvShow.Rows
            If GroupDate <> dt2.Rows(L_count).Item("NowDate").ToString Then
                GroupDate = dt2.Rows(L_count).Item("NowDate")
                If L_count <> 0 Then
                    Dim rowtotal = dtTotal.NewRow
                    Dim colSeqtotal As Integer = 0
                    rowtotal(colSeqtotal) = dt2.Rows(L_count - 1).Item("NowDate")
                    colSeqtotal += 1
                    rowtotal(colSeqtotal) = TotalMO
                    colSeqtotal += 1
                    rowtotal(colSeqtotal) = TotalQty
                    colSeqtotal += 1
                    rowtotal(colSeqtotal) = Math.Round(TotalOutput, 1)
                    dtTotal.Rows.Add(rowtotal)
                    TotalMO = 0
                    TotalQty = 0
                    TotalOutput = 0
                End If
            Else

            End If

            TotalMO += 1

            Dim row = dt.NewRow
            Dim colSeq As Integer = 0

            row(colSeq) = dt2.Rows(L_count).Item("NowDate")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("MONo")

            Dim MOGroup() As String = dt2.Rows(L_count).Item("MONo").ToString.Split("-")
            Dim SQLData1 As String = "select MF025,SFC.TA006,MOC.TA034,MOC.TA035 from MOCTA MOC" &
 " left join SFCTA SFC on SFC.TA001=MOC.TA001 and SFC.TA002=MOC.TA002" &
" left join BOMMF on MF001=MOC.TA006 and MF003=SFC.TA003 and MF002='01'" &
" where SFC.TA001='" & MOGroup(0) & "' and SFC.TA002='" & MOGroup(1) & "' and SFC.TA003='" & MOGroup(2) & "'"
            Dim dtDataERP As DataTable = dbConn.Query(SQLData1, VarIni.ERP, dbConn.WhoCalledMe)

            colSeq += 1
            row(colSeq) = dtDataERP.Rows(0).Item("TA034") & " / " & dtDataERP.Rows(0).Item("TA035")

            colSeq += 1
            Dim SQLMachine As String = "select MX001,MX006 from CMSMX where MX001='" & dt2.Rows(L_count).Item("MachineID") & "'"
            Dim dtMachine As DataTable = dbConn.Query(SQLMachine, VarIni.ERP, dbConn.WhoCalledMe)
            row(colSeq) = dtMachine.Rows(0).Item("MX006") & "(" & dtMachine.Rows(0).Item("MX001") & ")"

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("StartDate") & "  " & dt2.Rows(L_count).Item("StartTime")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("EndDate") & "  " & dt2.Rows(L_count).Item("EndTime")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("EmpCode")


            colSeq += 1
            Dim MOQty As Integer = 0
            If dt2.Rows(L_count).Item("Qty") <> "0" Then
                MOQty = dt2.Rows(L_count).Item("Qty")
            End If
            row(colSeq) = MOQty
            TotalQty = TotalQty + MOQty




            colSeq += 1
            Dim StdPcs As Integer = 0
            If Not IsDBNull(dtDataERP.Rows(0).Item(0)) Then
                StdPcs = Trim(dtDataERP.Rows(0).Item(0))
            End If

            row(colSeq) = StdPcs
            TotalOutput = TotalOutput + (StdPcs * MOQty) / 3600

            colSeq += 1
            row(colSeq) = Math.Round((StdPcs * MOQty) / 3600, 1)

            colSeq += 1
            row(colSeq) = Trim(dtDataERP.Rows(0).Item(1))

            dt.Rows.Add(row)
        Next

        gvShow1.DataSource = dt
        gvShow1.DataBind()


        'If dtTotal.Rows.Count = 0 Then
        Dim rowtotal1 = dtTotal.NewRow
        Dim colSeqtotal1 As Integer = 0
        rowtotal1(colSeqtotal1) = GroupDate
        colSeqtotal1 += 1
        rowtotal1(colSeqtotal1) = TotalMO
        colSeqtotal1 += 1
        rowtotal1(colSeqtotal1) = TotalQty
        colSeqtotal1 += 1
        rowtotal1(colSeqtotal1) = Math.Round(TotalOutput, 1)

        dtTotal.Rows.Add(rowtotal1)
        'End If
        gvShow.DataSource = dtTotal
        gvShow.DataBind()

    End Sub


    Private Sub ShowGridviewWC()
        Dim dt = New DataTable,
        colName As New ArrayList

        Dim dtTotal = New DataTable,
        colNameTotal As New ArrayList

        dt.Columns.Add("A") '0
        colName.Add("Date:A")
        dt.Columns.Add("B") '1
        colName.Add("MO:B")
        dt.Columns.Add("C") '1
        colName.Add("Desc/Spec:C")
        dt.Columns.Add("D") '1
        colName.Add("Macine:D")
        dt.Columns.Add("E") '1
        colName.Add("Start:E")
        dt.Columns.Add("F") '1
        colName.Add("End:F")
        dt.Columns.Add("G") '1
        colName.Add("Employee:G")
        dt.Columns.Add("H") '1
        colName.Add("Qty:H")
        dt.Columns.Add("I") '1
        colName.Add("Std Pcs:I")
        dt.Columns.Add("J") '1
        colName.Add("OutPut(Hour):J")
        dt.Columns.Add("K") '1
        colName.Add("Work Center:K")


        dtTotal.Columns.Add("A") '0
        colNameTotal.Add("Date:A")
        dtTotal.Columns.Add("B") '0
        colNameTotal.Add("Work Center:B")
        dtTotal.Columns.Add("C") '0
        colNameTotal.Add("Total MO:C")
        dtTotal.Columns.Add("D") '0
        colNameTotal.Add("Total Qty:D")
        dtTotal.Columns.Add("E") '0
        colNameTotal.Add("Total Output:E")

        'set format
        ControlForm.GridviewColWithLinkFirst(gvShow1, colName)
        ControlForm.GridviewColWithLinkFirst(gvShow, colNameTotal)


        Dim SQL As String = "select * from MOWorkRecord MO left join HOOTHAI.dbo.SFCTA " &
"  on TA001=SUBSTRING(MO.MONo,1,4) and TA002=SUBSTRING(MO.MONo,6,11) and TA003=RIGHT(MONo,4) where 1=1 and WorkType=2  "
        If DateFrom.Text <> "" Then
            SQL = SQL & " and NowDate>='" & DateFrom.Text & "'"
        End If

        If DateEnd.Text <> "" Then
            SQL = SQL & " and NowDate<='" & DateEnd.Text & "'"
        End If

        Dim SelectWorkCenter As String = ""
        For CBLCount As Integer = 0 To CBLSecCate.Items.Count - 1
            If CBLSecCate.Items(CBLCount).Selected Then
                If SelectWorkCenter = "" Then
                    SelectWorkCenter = SelectWorkCenter & "'" & CBLSecCate.Items(CBLCount).Value & "'"
                Else
                    SelectWorkCenter = SelectWorkCenter & ",'" & CBLSecCate.Items(CBLCount).Value & "'"
                End If
            End If
        Next
        If SelectWorkCenter <> "" Then
            SQL = SQL & " and TA006 in (" & SelectWorkCenter & ")"
        End If
        SQL = SQL & " Order by NowDate,TA006"



        Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)

        Dim GroupDate As String = ""
        Dim GroupWorkCenter As String = ""
        Dim GroupMachine As String = ""
        Dim TotalMO As Integer = 0
        Dim TotalQty As Integer = 0
        Dim TotalOutput As Double = 0
        For L_count As Integer = 0 To dt2.Rows().Count - 1
            'gvShow.Rows
            If GroupDate <> dt2.Rows(L_count).Item("NowDate").ToString Or GroupWorkCenter <> dt2.Rows(L_count).Item("TA006").ToString Then
                GroupDate = dt2.Rows(L_count).Item("NowDate")
                GroupWorkCenter = dt2.Rows(L_count).Item("TA006").ToString
                If L_count <> 0 Then
                    Dim rowtotal = dtTotal.NewRow
                    Dim colSeqtotal As Integer = 0
                    rowtotal(colSeqtotal) = dt2.Rows(L_count - 1).Item("NowDate")
                    colSeqtotal += 1
                    rowtotal(colSeqtotal) = dt2.Rows(L_count - 1).Item("TA006")
                    colSeqtotal += 1
                    rowtotal(colSeqtotal) = TotalMO
                    colSeqtotal += 1
                    rowtotal(colSeqtotal) = TotalQty
                    colSeqtotal += 1
                    rowtotal(colSeqtotal) = Math.Round(TotalOutput, 1)
                    dtTotal.Rows.Add(rowtotal)
                    TotalMO = 0
                    TotalQty = 0
                    TotalOutput = 0
                End If
            Else

            End If

            TotalMO += 1

            Dim row = dt.NewRow
            Dim colSeq As Integer = 0

            row(colSeq) = dt2.Rows(L_count).Item("NowDate")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("MONo")

            Dim MOGroup() As String = dt2.Rows(L_count).Item("MONo").ToString.Split("-")
            Dim SQLData1 As String = "select MF025,SFC.TA006,MOC.TA034,MOC.TA035 from MOCTA MOC" &
 " left join SFCTA SFC on SFC.TA001=MOC.TA001 and SFC.TA002=MOC.TA002" &
" left join BOMMF on MF001=MOC.TA006 and MF003=SFC.TA003 and MF002='01'" &
" where SFC.TA001='" & MOGroup(0) & "' and SFC.TA002='" & MOGroup(1) & "' and SFC.TA003='" & MOGroup(2) & "'"
            Dim dtDataERP As DataTable = dbConn.Query(SQLData1, VarIni.ERP, dbConn.WhoCalledMe)

            colSeq += 1
            row(colSeq) = dtDataERP.Rows(0).Item("TA034") & " / " & dtDataERP.Rows(0).Item("TA035")

            colSeq += 1
            Dim SQLMachine As String = "select MX001,MX006 from CMSMX where MX001='" & dt2.Rows(L_count).Item("MachineID") & "'"
            Dim dtMachine As DataTable = dbConn.Query(SQLMachine, VarIni.ERP, dbConn.WhoCalledMe)
            If dtMachine.Rows.Count > 0 Then
                row(colSeq) = dtMachine.Rows(0).Item("MX006") & "(" & dtMachine.Rows(0).Item("MX001") & ")"
            Else
                row(colSeq) = ""
            End If

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("StartDate") & "  " & dt2.Rows(L_count).Item("StartTime")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("EndDate") & "  " & dt2.Rows(L_count).Item("EndTime")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("EmpCode")


            colSeq += 1
            Dim MOQty As Integer = 0
            If dt2.Rows(L_count).Item("Qty") <> "0" Then
                MOQty = dt2.Rows(L_count).Item("Qty")
            End If
            row(colSeq) = MOQty
            TotalQty = TotalQty + MOQty




            colSeq += 1
            Dim StdPcs As Integer = 0
            If Not IsDBNull(dtDataERP.Rows(0).Item(0)) Then
                StdPcs = Trim(dtDataERP.Rows(0).Item(0))
            End If

            row(colSeq) = StdPcs
            TotalOutput = TotalOutput + (StdPcs * MOQty) / 3600

            colSeq += 1
            row(colSeq) = Math.Round((StdPcs * MOQty) / 3600, 1)

            colSeq += 1
            row(colSeq) = Trim(dtDataERP.Rows(0).Item(1))

            dt.Rows.Add(row)
        Next

        gvShow1.DataSource = dt
        gvShow1.DataBind()


        'If dtTotal.Rows.Count = 0 Then
        Dim rowtotal1 = dtTotal.NewRow
        Dim colSeqtotal1 As Integer = 0
        rowtotal1(colSeqtotal1) = GroupDate
        colSeqtotal1 += 1
        rowtotal1(colSeqtotal1) = GroupWorkCenter
        colSeqtotal1 += 1
        rowtotal1(colSeqtotal1) = TotalMO
        colSeqtotal1 += 1
        rowtotal1(colSeqtotal1) = TotalQty
        colSeqtotal1 += 1
        rowtotal1(colSeqtotal1) = Math.Round(TotalOutput, 1)

        dtTotal.Rows.Add(rowtotal1)
        'End If
        gvShow.DataSource = dtTotal
        gvShow.DataBind()

    End Sub

    Private Sub ShowGridviewMachine()
        Dim dt = New DataTable,
        colName As New ArrayList

        Dim dtTotal = New DataTable,
        colNameTotal As New ArrayList

        dt.Columns.Add("A") '0
        colName.Add("Date:A")
        dt.Columns.Add("B") '1
        colName.Add("MO:B")
        dt.Columns.Add("C") '1
        colName.Add("Desc/Spec:C")
        dt.Columns.Add("D") '1
        colName.Add("Machine:D")
        dt.Columns.Add("E") '1
        colName.Add("Start:E")
        dt.Columns.Add("F") '1
        colName.Add("End:F")
        dt.Columns.Add("G") '1
        colName.Add("Employee:G")
        dt.Columns.Add("H") '1
        colName.Add("Qty:H")
        dt.Columns.Add("I") '1
        colName.Add("Std Pcs:I")
        dt.Columns.Add("J") '1
        colName.Add("OutPut(Hour):J")
        dt.Columns.Add("K") '1
        colName.Add("Work Center:K")


        dtTotal.Columns.Add("A") '0
        colNameTotal.Add("Date:A")
        dtTotal.Columns.Add("B") '0
        colNameTotal.Add("Machine:B")
        dtTotal.Columns.Add("C") '0
        colNameTotal.Add("Total MO:C")
        dtTotal.Columns.Add("D") '0
        colNameTotal.Add("Total Qty:D")
        dtTotal.Columns.Add("E") '0
        colNameTotal.Add("Total Output:E")

        'set format
        ControlForm.GridviewColWithLinkFirst(gvShow1, colName)
        ControlForm.GridviewColWithLinkFirst(gvShow, colNameTotal)


        Dim SQL As String = "select * from MOWorkRecord  where 1=1 and WorkType=2  "
        If DateFrom.Text <> "" Then
            SQL = SQL & " and NowDate>='" & DateFrom.Text & "'"
        End If

        If DateEnd.Text <> "" Then
            SQL = SQL & " and NowDate<='" & DateEnd.Text & "'"
        End If

        Dim SelectWorkCenter As String = ""
        For CBLCount As Integer = 0 To CBLSecCate.Items.Count - 1
            If CBLSecCate.Items(CBLCount).Selected Then
                If SelectWorkCenter = "" Then
                    SelectWorkCenter = SelectWorkCenter & "'" & CBLSecCate.Items(CBLCount).Value & "'"
                Else
                    SelectWorkCenter = SelectWorkCenter & ",'" & CBLSecCate.Items(CBLCount).Value & "'"
                End If
            End If
        Next
        If SelectWorkCenter <> "" Then
            SQL = SQL & " and MachineID in (" & SelectWorkCenter & ")"
        End If
        SQL = SQL & " Order by NowDate,MachineID"



        Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)

        Dim GroupDate As String = ""
        Dim GroupWorkCenter As String = ""
        Dim GroupMachine As String = ""
        Dim TotalMO As Integer = 0
        Dim TotalQty As Integer = 0
        Dim TotalOutput As Double = 0
        For L_count As Integer = 0 To dt2.Rows().Count - 1
            'gvShow.Rows
            If GroupDate <> dt2.Rows(L_count).Item("NowDate").ToString Or GroupMachine <> dt2.Rows(L_count).Item("MachineID").ToString Then
                GroupDate = dt2.Rows(L_count).Item("NowDate")
                GroupMachine = dt2.Rows(L_count).Item("MachineID").ToString
                If L_count <> 0 Then
                    Dim rowtotal = dtTotal.NewRow
                    Dim colSeqtotal As Integer = 0
                    rowtotal(colSeqtotal) = dt2.Rows(L_count - 1).Item("NowDate")

                    colSeqtotal += 1
                    Dim SQLMachine1 As String = "select MX001,MX006 from CMSMX where MX001='" & dt2.Rows(L_count - 1).Item("MachineID") & "'"
                    Dim dtMachine1 As DataTable = dbConn.Query(SQLMachine1, VarIni.ERP, dbConn.WhoCalledMe)
                    If dtMachine1.Rows.Count > 0 Then
                        rowtotal(colSeqtotal) = dtMachine1.Rows(0).Item("MX006") & "(" & dtMachine1.Rows(0).Item("MX001") & ")"
                    Else
                        rowtotal(colSeqtotal) = ""
                    End If
                    colSeqtotal += 1
                    rowtotal(colSeqtotal) = TotalMO
                    colSeqtotal += 1
                    rowtotal(colSeqtotal) = TotalQty
                    colSeqtotal += 1
                    rowtotal(colSeqtotal) = Math.Round(TotalOutput, 1)
                    dtTotal.Rows.Add(rowtotal)
                    TotalMO = 0
                    TotalQty = 0
                    TotalOutput = 0
                End If
            Else

            End If

            TotalMO += 1

            Dim row = dt.NewRow
            Dim colSeq As Integer = 0

            row(colSeq) = dt2.Rows(L_count).Item("NowDate")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("MONo")

            Dim MOGroup() As String = dt2.Rows(L_count).Item("MONo").ToString.Split("-")
            Dim SQLData1 As String = "select MF025,SFC.TA006,MOC.TA034,MOC.TA035 from MOCTA MOC" &
 " left join SFCTA SFC on SFC.TA001=MOC.TA001 and SFC.TA002=MOC.TA002" &
" left join BOMMF on MF001=MOC.TA006 and MF003=SFC.TA003 and MF002='01'" &
" where SFC.TA001='" & MOGroup(0) & "' and SFC.TA002='" & MOGroup(1) & "' and SFC.TA003='" & MOGroup(2) & "'"
            Dim dtDataERP As DataTable = dbConn.Query(SQLData1, VarIni.ERP, dbConn.WhoCalledMe)

            colSeq += 1
            row(colSeq) = dtDataERP.Rows(0).Item("TA034") & " / " & dtDataERP.Rows(0).Item("TA035")

            colSeq += 1
            Dim SQLMachine As String = "select MX001,MX006 from CMSMX where MX001='" & dt2.Rows(L_count).Item("MachineID") & "'"
            Dim dtMachine As DataTable = dbConn.Query(SQLMachine, VarIni.ERP, dbConn.WhoCalledMe)
            If dtMachine.Rows.Count > 0 Then
                row(colSeq) = dtMachine.Rows(0).Item("MX006") & "(" & dtMachine.Rows(0).Item("MX001") & ")"
            Else
                row(colSeq) = ""
            End If

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("StartDate") & "  " & dt2.Rows(L_count).Item("StartTime")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("EndDate") & "  " & dt2.Rows(L_count).Item("EndTime")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("EmpCode")


            colSeq += 1
            Dim MOQty As Integer = 0
            If dt2.Rows(L_count).Item("Qty") <> "0" Then
                MOQty = dt2.Rows(L_count).Item("Qty")
            End If
            row(colSeq) = MOQty
            TotalQty = TotalQty + MOQty

            colSeq += 1
            Dim StdPcs As Integer = 0
            If Not IsDBNull(dtDataERP.Rows(0).Item(0)) Then
                StdPcs = Trim(dtDataERP.Rows(0).Item(0))
            End If

            row(colSeq) = StdPcs
            TotalOutput = TotalOutput + (StdPcs * MOQty) / 3600

            colSeq += 1
            row(colSeq) = Math.Round((StdPcs * MOQty) / 3600, 1)

            colSeq += 1
            row(colSeq) = Trim(dtDataERP.Rows(0).Item(1))

            dt.Rows.Add(row)
        Next

        gvShow1.DataSource = dt
        gvShow1.DataBind()


        'If dtTotal.Rows.Count = 0 Then
        Dim rowtotal1 = dtTotal.NewRow
        Dim colSeqtotal1 As Integer = 0
        rowtotal1(colSeqtotal1) = GroupDate
        colSeqtotal1 += 1
        Dim SQLMachine2 As String = "select MX001,MX006 from CMSMX where MX001='" & GroupMachine & "'"
        Dim dtMachine2 As DataTable = dbConn.Query(SQLMachine2, VarIni.ERP, dbConn.WhoCalledMe)

        If dtMachine2.Rows.Count > 0 Then
            rowtotal1(colSeqtotal1) = dtMachine2.Rows(0).Item("MX006") & "(" & dtMachine2.Rows(0).Item("MX001") & ")"
        Else
            rowtotal1(colSeqtotal1) = ""
        End If
        colSeqtotal1 += 1
        rowtotal1(colSeqtotal1) = TotalMO
        colSeqtotal1 += 1
        rowtotal1(colSeqtotal1) = TotalQty
        colSeqtotal1 += 1
        rowtotal1(colSeqtotal1) = Math.Round(TotalOutput, 1)

        dtTotal.Rows.Add(rowtotal1)
        'End If
        gvShow.DataSource = dtTotal
        gvShow.DataBind()

    End Sub


    Private Sub ShowGridviewEmployeeDetail()
        Dim dt = New DataTable,
        colName As New ArrayList


        dt.Columns.Add("A") '0
        colName.Add("Date:A")
        dt.Columns.Add("B") '1
        colName.Add("MO:B")
        dt.Columns.Add("C") '1
        colName.Add("Desc/Spec:C")
        dt.Columns.Add("D") '1
        colName.Add("Machine:D")
        dt.Columns.Add("E") '1
        colName.Add("Start:E")
        dt.Columns.Add("F") '1
        colName.Add("End:F")
        dt.Columns.Add("G") '1
        colName.Add("Employee:G")
        dt.Columns.Add("H") '1
        colName.Add("Qty:H")
        dt.Columns.Add("I") '1
        colName.Add("Std Pcs:I")
        dt.Columns.Add("J") '1
        colName.Add("OutPut(Hour):J")
        dt.Columns.Add("K") '1
        colName.Add("Work Center:K")



        'set format
        ControlForm.GridviewColWithLinkFirst(gvShow1, colName)


        Dim SQL As String = "select * from MOWorkRecord  where 1=1 and WorkType=2  "
        If DateFrom.Text <> "" Then
            SQL = SQL & " and NowDate>='" & DateFrom.Text & "'"
        End If

        If DateEnd.Text <> "" Then
            SQL = SQL & " and NowDate<='" & DateEnd.Text & "'"
        End If

        Dim SelectWorkCenter As String = ""
        For CBLCount As Integer = 0 To CBLSecCate.Items.Count - 1
            If CBLSecCate.Items(CBLCount).Selected Then
                If SelectWorkCenter = "" Then
                    SelectWorkCenter = SelectWorkCenter & " and (EmpCode like '%" & CBLSecCate.Items(CBLCount).Value & "%'"
                Else
                    SelectWorkCenter = SelectWorkCenter & " or EmpCode Like '%" & CBLSecCate.Items(CBLCount).Value & "%'"
                End If
            End If
        Next
        If SelectWorkCenter <> "" Then
            SelectWorkCenter = SelectWorkCenter & ")"
        End If
        SQL = SQL & SelectWorkCenter
        SQL = SQL & "order by EmpCode,NowDate"




        Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)


        For L_count As Integer = 0 To dt2.Rows().Count - 1
            'gvShow.Rows


            Dim row = dt.NewRow
            Dim colSeq As Integer = 0

            row(colSeq) = dt2.Rows(L_count).Item("NowDate")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("MONo")

            Dim MOGroup() As String = dt2.Rows(L_count).Item("MONo").ToString.Split("-")
            Dim SQLData1 As String = "select MF025,SFC.TA006,MOC.TA034,MOC.TA035 from MOCTA MOC" &
 " left join SFCTA SFC on SFC.TA001=MOC.TA001 and SFC.TA002=MOC.TA002" &
" left join BOMMF on MF001=MOC.TA006 and MF003=SFC.TA003 and MF002='01'" &
" where SFC.TA001='" & MOGroup(0) & "' and SFC.TA002='" & MOGroup(1) & "' and SFC.TA003='" & MOGroup(2) & "'"
            Dim dtDataERP As DataTable = dbConn.Query(SQLData1, VarIni.ERP, dbConn.WhoCalledMe)

            colSeq += 1
            row(colSeq) = dtDataERP.Rows(0).Item("TA034") & " / " & dtDataERP.Rows(0).Item("TA035")

            colSeq += 1
            Dim SQLMachine As String = "select MX001,MX006 from CMSMX where MX001='" & dt2.Rows(L_count).Item("MachineID") & "'"
            Dim dtMachine As DataTable = dbConn.Query(SQLMachine, VarIni.ERP, dbConn.WhoCalledMe)
            If dtMachine.Rows.Count > 0 Then
                row(colSeq) = dtMachine.Rows(0).Item("MX006") & "(" & dtMachine.Rows(0).Item("MX001") & ")"
            Else
                row(colSeq) = ""
            End If

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("StartDate") & "  " & dt2.Rows(L_count).Item("StartTime")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("EndDate") & "  " & dt2.Rows(L_count).Item("EndTime")

            colSeq += 1
            row(colSeq) = dt2.Rows(L_count).Item("EmpCode")


            colSeq += 1
            Dim MOQty As Integer = 0
            If dt2.Rows(L_count).Item("Qty") <> "0" Then
                MOQty = dt2.Rows(L_count).Item("Qty")
            End If
            row(colSeq) = MOQty

            colSeq += 1
            Dim StdPcs As Integer = 0
            If Not IsDBNull(dtDataERP.Rows(0).Item(0)) Then
                StdPcs = Trim(dtDataERP.Rows(0).Item(0))
            End If

            row(colSeq) = StdPcs


            colSeq += 1
            row(colSeq) = Math.Round((StdPcs * MOQty) / 3600, 1)

            colSeq += 1
            row(colSeq) = Trim(dtDataERP.Rows(0).Item(1))

            dt.Rows.Add(row)
        Next

        gvShow1.DataSource = dt
        gvShow1.DataBind()


        'If dtTotal.Rows.Count = 0 Then


    End Sub

    Private Sub ShowGridviewEmployeeTotal()
        Dim dtTotal = New DataTable,
        colNameTotal As New ArrayList

        dtTotal.Columns.Add("A") '0
        colNameTotal.Add("Employee:A")
        dtTotal.Columns.Add("B") '0
        colNameTotal.Add("Total MO:B")
        dtTotal.Columns.Add("C") '0
        colNameTotal.Add("Total Qty:C")
        dtTotal.Columns.Add("D") '0
        colNameTotal.Add("Total Output:D")

        'set format

        ControlForm.GridviewColWithLinkFirst(gvShow, colNameTotal)


        Dim SQL As String = "select emp.Code,cnname +'-' + enName from employee emp left join Job JB on emp.JobId=JB.JOBID left join Department Dept on Dept.departmentid=emp.DepartmentId where emp.LastWorkDate>=getdate() and JB.Name='Operator' and Dept.ShortName ='Stamping' "


        'If DateFrom.Text <> "" Then
        '    SQL = SQL & " and NowDate>='" & DateFrom.Text & "'"
        'End If

        'If DateEnd.Text <> "" Then
        '    SQL = SQL & " and NowDate<='" & DateEnd.Text & "'"
        'End If

        Dim SelectWorkCenter As String = ""
        For CBLCount As Integer = 0 To CBLSecCate.Items.Count - 1
            If CBLSecCate.Items(CBLCount).Selected Then
                If SelectWorkCenter = "" Then
                    SelectWorkCenter = SelectWorkCenter & " and (emp.Code='" & CBLSecCate.Items(CBLCount).Value & "'"
                Else
                    SelectWorkCenter = SelectWorkCenter & " or emp.Code='" & CBLSecCate.Items(CBLCount).Value & "'"
                End If
            End If
        Next
        If SelectWorkCenter <> "" Then
            SelectWorkCenter = SelectWorkCenter & ")"
        End If
        SQL = SQL & SelectWorkCenter
        SQL = SQL & "order by emp.code"

        Dim dt2 As DataTable = dbConn.Query(SQL, VarIni.HRMHT, dbConn.WhoCalledMe)


        For L_count As Integer = 0 To dt2.Rows().Count - 1
            'gvShow.Rows

            Dim rowtotal = dtTotal.NewRow
            Dim colSeqtotal As Integer = 0

            Dim SQLMOWorkRecord As String = "select * from MOWorkRecord where EmpCode like '%" & dt2.Rows(L_count).Item("code") & "%' and WorkType=2 and MONo is not null"
            If DateFrom.Text <> "" Then
                SQLMOWorkRecord = SQLMOWorkRecord & " and NowDate>='" & DateFrom.Text & "'"
            End If

            If DateEnd.Text <> "" Then
                SQLMOWorkRecord = SQLMOWorkRecord & " and NowDate<='" & DateEnd.Text & "'"
            End If
            Dim dtMOWorkRecord As DataTable = dbConn.Query(SQLMOWorkRecord, VarIni.DBMIS, dbConn.WhoCalledMe)

            Dim TotalMO As Integer = 0
            Dim TotalQty As Integer = 0
            Dim TotalOutput As Double = 0
            For k_count As Integer = 0 To dtMOWorkRecord.Rows.Count - 1
                TotalMO += 1
                TotalQty += dtMOWorkRecord.Rows(k_count).Item("Qty")

                Dim MOGroup() As String = dtMOWorkRecord.Rows(k_count).Item("MONo").ToString.Split("-")
                Dim SQLData1 As String = "select MF025,SFC.TA006 from MOCTA MOC" &
     " left join SFCTA SFC on SFC.TA001=MOC.TA001 and SFC.TA002=MOC.TA002" &
    " left join BOMMF on MF001=MOC.TA006 and MF003=SFC.TA003 and MF002='01'" &
    " where SFC.TA001='" & MOGroup(0) & "' and SFC.TA002='" & MOGroup(1) & "' and SFC.TA003='" & MOGroup(2) & "'"

                Dim dtDataERP As DataTable = dbConn.Query(SQLData1, VarIni.ERP, dbConn.WhoCalledMe)

                Dim StdPcs As Integer = 0
                If dtDataERP.Rows.Count > 0 Then
                    If Not IsDBNull(dtDataERP.Rows(0).Item(0)) Then
                        StdPcs = Trim(dtDataERP.Rows(0).Item(0))
                    End If
                End If
                TotalOutput = TotalOutput + (StdPcs * dtMOWorkRecord.Rows(k_count).Item("Qty")) / 3600
            Next


            rowtotal(colSeqtotal) = dt2.Rows(L_count).Item("code")

            colSeqtotal += 1


            rowtotal(colSeqtotal) = TotalMO
            colSeqtotal += 1
            rowtotal(colSeqtotal) = TotalQty
            colSeqtotal += 1
            rowtotal(colSeqtotal) = Math.Round(TotalOutput, 1)
            dtTotal.Rows.Add(rowtotal)


        Next
        'End If
        gvShow.DataSource = dtTotal
        gvShow.DataBind()

    End Sub
End Class