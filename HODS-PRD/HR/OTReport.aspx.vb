Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class OTReport
    Inherits System.Web.UI.Page

    Dim CreateTable As New CreateTable
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Dim Table As String = "OTRecord"
    Dim chrConn As String = Chr(8)
    Dim UserOTRecord As String = "UserOTRecord"
    Dim cblCont As New CheckBoxListControl
    Dim ddlCnt As New DropDownListControl
    Dim whrCont As New WhereControl
    Dim sqlCont As New SQLControl
    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim gvCont As New GridviewControl
    Dim dateCont As New DateControl
    Dim hashCont As New HashtableControl
    Dim deptcode As String = String.Empty
    Dim grp As String = String.Empty
    Dim Total_Col1 As Decimal = 0
    Dim Total_Col2 As Decimal = 0
    Dim Total_Col3 As Decimal = 0
    Dim Total_Col4 As Decimal = 0
    Dim Total_Col5 As Decimal = 0
    Dim Total_Col6 As Decimal = 0
    Dim Total_Col7 As Decimal = 0
    Dim Total_Col8 As Decimal = 0
    Dim Total_Col9 As Decimal = 0
    Const empcode As String = "empno"
    Const work_type As String = "worktype"
    Const ot_hours As String = "ot_hours"
    Const leave_hours As String = "leave_hours"
    Const lunch_hours As String = "lunch_hours"
    Const leave_code As String = "leave_code"
    Const ot_total As String = "ot_total"
    Const shiftday As String = "shiftday" 'day(7),day(7-17),day(8),day(8-18)
    Const shift As String = "shift"
    Const dept As String = "dept"
    Const line As String = "line"
    Const name As String = "name"
    Const pickup As String = "pickup"
    Const status As String = "status"
    Const ot_start_date As String = "ot_start_date"
    Const ot_start_time As String = "ot_start_time"
    Const ot_end_date As String = "ot_end_date"
    Const ot_end_time As String = "ot_end_time"
    Const leave_start_date As String = "leave_start_date"
    Const leave_start_time As String = "leave_start_time"
    Const leave_end_date As String = "leave_end_date"
    Const leave_end_time As String = "leave_end_time"
    Const workdate As String = "workdate"
    Const work_begin_tm As String = "work_begin_time"
    Const work_end_tm As String = "work_end_time"
    Const work_begin_dt As String = "work_begin_date"
    Const work_end_dt As String = "work_end_date"
    Const create_date As String = "create_date"
    Const create_by As String = "create_by"
    Const change_date As String = "change_date"
    Const change_by As String = "change_by"
    Const COLL As String = "collate Chinese_PRC_BIN"
    Dim OT_Record As String = "OVER_TIME_RECORD"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If
            If ucDateFrom.text = "" Then
                ucDateFrom.Text = Date.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
            End If

            If ucDateTo.text = "" Then
                ucDateTo.Text = Date.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
            End If
            Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
            deptcode = dtCont.IsDBNullDataRow(dr, "Dept")
            Dim chk As Boolean = False

            If deptcode = "" Then
                Response.Redirect(Server.UrlPathEncode("../Home.aspx"))
            Else
                deptcode = dtCont.IsDBNullDataRow(dr, "Dept")
            End If
            cblCont.showCheckboxList(cblDept, getDept(deptcode.Replace(",", "','")), VarIni.DBMIS, "CodeName", "Code", 4)
            btExport.Visible = False
            btExcel.Visible = False
        End If
        getControl()
    End Sub

    '============== Function =========

    Function getUser(userid As String)
        Dim SQL As String = String.Empty
        Dim fldName As New ArrayList
        fldName.Add("Dept")
        fldName.Add("Grp")
        SQL = VarIni.S & sqlCont.getFeild(fldName) & VarIni.F & UserOTRecord
        SQL &= VarIni.W & " 1=1 "
        SQL &= whrCont.Where("Id", userid,, False)
        Return SQL
    End Function

    Function getDept(code As String)

        Dim SQL As String = String.Empty
        Dim WHR As String = String.Empty
        Dim fldName As New ArrayList

        fldName.Add(HR_DEPT.CODE & ":Code")
        fldName.Add(HR_DEPT.CODE & "+'-'+" & HR_DEPT.NAME & "+'-'+ isnull(" & HR_DEPT.SHORT_NAME & ",''):CodeName")
        WHR &= VarIni.oneEqualOne
        WHR &= whrCont.Where(HR_DEPT.CODE, " in ('" & code & "')")

        SQL = VarIni.S & sqlCont.getFeild(fldName) & VarIni.F & HR_DEPT.TABLENAME & VarIni.W & WHR & sqlCont.getOrderBy(HR_DEPT.CODE)
        Return SQL
    End Function

    Function getNOOT()

        Dim fldName As New ArrayList,
        whr As String = String.Empty,
        grpby As String = String.Empty

        whr &= whrCont.Where("WORK_DATE", dateCont.dateFormat2(ucDateFrom.Text), dateCont.dateFormat2(ucDateTo.Text), True) 'DATE
        whr &= whrCont.Where("WORK_END_TIME", " IN ('16:00','16:30','17:00','17:30','18:00','04:00','04:30')")
        whr &= whrCont.Where("OT_FINISHED_TIME", "",, False)

        Dim SQL As String = String.Empty
        fldName.Add("PICKUP_LOCATION")
        fldName.Add("WORK_END_TIME")
        fldName.Add("'0'" & VarIni.char8 & "NOOT")
        fldName.Add("COUNT(PICKUP_LOCATION)" & VarIni.char8 & "NUM")

        SQL &= VarIni.S & sqlCont.getFeild(fldName, VarIni.char8) & VarIni.F & " OVER_TIME_RECORD "
        SQL &= VarIni.L & " V_HR_ATTENDANCE_RANK ON CODE_SHIFT = SHIFT_DAY  collate Chinese_PRC_BIN "
        SQL &= VarIni.W & " 1=1 " & whr
        SQL &= VarIni.G & " PICKUP_LOCATION,WORK_END_TIME "
        SQL &= VarIni.O & " WORK_END_TIME asc"

        Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim qtyHash As New Hashtable
        Dim hashCont As New HashtableControl
        For Each dr As DataRow In dt.Rows
            hashCont.addDataHash(qtyHash, dtCont.IsDBNullDataRow(dr, "PICKUP_LOCATION") & "-" & dtCont.IsDBNullDataRow(dr, "WORK_END_TIME") & "-" & dtCont.IsDBNullDataRow(dr, "NOOT"), dtCont.IsDBNullDataRow(dr, "NUM"))
        Next
        Return qtyHash

    End Function

    Function getOT()

        Dim fldName As New ArrayList,
        whr As String = String.Empty

        whr &= whrCont.Where("WORK_DATE", dateCont.dateFormat2(ucDateFrom.Text), dateCont.dateFormat2(ucDateTo.Text), True) 'DATE
        whr &= whrCont.Where("OT_FINISHED_TIME", " IN ('19:00','21:00','07:00') ")
        Dim SQL As String = String.Empty
        fldName.Add("PICKUP_LOCATION")
        fldName.Add("OT_FINISHED_TIME")
        fldName.Add("COUNT(PICKUP_LOCATION)" & VarIni.char8 & "NUM")

        SQL &= VarIni.S & sqlCont.getFeild(fldName, VarIni.char8) & VarIni.F & " OVER_TIME_RECORD "
        SQL &= VarIni.L & " V_HR_ATTENDANCE_RANK ON CODE_SHIFT = SHIFT_DAY  collate Chinese_PRC_BIN "
        SQL &= VarIni.W & " 1=1 " & whr
        SQL &= VarIni.G & " PICKUP_LOCATION,OT_FINISHED_TIME "
        SQL &= VarIni.O & " OT_FINISHED_TIME asc"

        Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim qtyHash As New Hashtable
        Dim hashCont As New HashtableControl
        For Each dr As DataRow In dt.Rows
            hashCont.addDataHash(qtyHash, dtCont.IsDBNullDataRow(dr, "PICKUP_LOCATION") & "-" & dtCont.IsDBNullDataRow(dr, "OT_FINISHED_TIME"), dtCont.IsDBNullDataRow(dr, "NUM"))
        Next
        Return qtyHash
    End Function

    Function getOTOTHER()

        Dim fldName As New ArrayList,
        whr As String = String.Empty,
        grpby As String = String.Empty
        whr &= whrCont.Where("WORK_DATE", dateCont.dateFormat2(ucDateFrom.Text), dateCont.dateFormat2(ucDateTo.Text), True) 'DATE
        whr &= whrCont.Where("WORK_END_TIME", " IN ('16:00','16:30','17:00','17:30','18:00','04:00','04:30')")
        whr &= whrCont.Where("OT_FINISHED_TIME", " NOT IN ('','19:00','21:00','07:00')")
        Dim SQL As String = String.Empty
        fldName.Add("PICKUP_LOCATION")
        fldName.Add("COUNT(PICKUP_LOCATION)" & VarIni.char8 & "NUM")

        SQL &= VarIni.S & sqlCont.getFeild(fldName, VarIni.char8) & VarIni.F & " OVER_TIME_RECORD "
        SQL &= VarIni.L & " V_HR_ATTENDANCE_RANK ON CODE_SHIFT = SHIFT_DAY  collate Chinese_PRC_BIN "
        SQL &= VarIni.W & " 1=1 " & whr
        SQL &= VarIni.G & " PICKUP_LOCATION"
        SQL &= VarIni.O & " PICKUP_LOCATION asc"

        Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim qtyHash As New Hashtable
        Dim hashCont As New HashtableControl
        For Each dr As DataRow In dt.Rows
            hashCont.addDataHash(qtyHash, dtCont.IsDBNullDataRow(dr, "PICKUP_LOCATION"), dtCont.IsDBNullDataRow(dr, "NUM"))
        Next
        Return qtyHash

    End Function

    Function getCalTotalOvertime(ByRef Fld As String, ByRef Func As String, Optional Chk As Boolean = False)
        'TOTAL PERSONS IN DEPT
        'LEAVE HOURS
        'LUNCH HOURS
        'OT HOURS
        Dim dc As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dc, "dept")

        Dim whr As String = String.Empty
        Dim SQL As String = String.Empty
        Dim fldName As New ArrayList

        whr &= whrCont.Where("WORK_DATE", dateCont.dateFormat2(ucDateFrom.Text), dateCont.dateFormat2(ucDateTo.Text), True) 'DATE
        whr &= whrCont.Where("DEPT", cblDept, False, deptcode.Replace(",", "','"), True)
        whr &= whrCont.Where("WORK_BEGIN_TIME", cbShift)
        whr &= whrCont.Where("EMPNO", tbEmpNo)
        If Chk Then
            whr &= whrCont.Where("NORMAL_TIME > LEAVE_TIME", "")
        End If

        fldName.Add("WORK_DATE")
        fldName.Add("DEPT")
        fldName.Add("" & Func & "(" & Fld & " )" & VarIni.char8 & "QTY")

        SQL &= VarIni.S & sqlCont.getFeild(fldName, VarIni.char8) & VarIni.F & " OVER_TIME_RECORD "
        SQL &= VarIni.W & " 1=1 " & whr
        SQL &= VarIni.G & " WORK_DATE,DEPT"
        SQL &= VarIni.O & " WORK_DATE ASC"

        Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim qtyHash As New Hashtable
        Dim hashCont As New HashtableControl
        For Each dr As DataRow In dt.Rows
            hashCont.addDataHash(qtyHash, dtCont.IsDBNullDataRow(dr, "WORK_DATE") & "-" & dtCont.IsDBNullDataRow(dr, "DEPT"), dtCont.IsDBNullDataRow(dr, "QTY"))
        Next
        Return qtyHash
    End Function

    Private Function strFormat(ByVal val As Object) As String
        Dim show As String = String.Empty
        If val <> "0.0" Then
            show = String.Format("{0:n2}", val)
        End If

        Return show
    End Function

    Private Sub getControl()

        Dim deptcode As String = String.Empty
        Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dr, "Dept")
        Dim Grpcode As String = dtCont.IsDBNullDataRow(dr, "Grp")

        If deptcode = "" Then
            Response.Redirect(Server.UrlPathEncode("../Home.aspx"))
        Else
            deptcode = dtCont.IsDBNullDataRow(dr, "Dept")
            If Grpcode.Trim = "dept_head" Then
                btLocation.Visible = True
            Else
                btLocation.Visible = False
            End If
        End If

    End Sub

    '============== Export Excel =========

    Protected Sub btExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExcel.Click
        ControlForm.ExportGridViewToExcel("OTReport" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    '==============  RowDataBound =========

    Private Sub gvSum_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSum.RowDataBound
        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                .Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub

    Private Sub gvSumPickup_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSumPickup.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Total_Col1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "time_16"))
            Total_Col2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "time_17"))
            Total_Col3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "time_17_30"))
            Total_Col4 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "time_18"))
            Total_Col5 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "time_19"))
            Total_Col6 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "time_21"))
            Total_Col7 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "time_04"))
            Total_Col8 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "time_07"))
            Total_Col9 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "time_other"))

            For i As Decimal = 2 To gvSumPickup.HeaderRow.Cells.Count - 1
                'With e.Row.Cells(i)
                Dim OTEndTime As String = gvSumPickup.HeaderRow.Cells(i).Text.Trim.Substring(0, 2)
                Dim PickUp As String = e.Row.Cells(0).Text
                Dim PickUp_Time As String = gvSumPickup.HeaderRow.Cells(i).Text.Trim
                Dim Num As Integer = 0
                If PickUp_Time = "19:00" Then
                    Num = 1
                ElseIf PickUp_Time = "21:00" Then
                    Num = 1
                ElseIf PickUp_Time = "07:00" Then
                    Num = 1
                ElseIf PickUp_Time = "TimeOther" Then
                    Num = 2
                End If

                e.Row.Cells(i).Attributes.Add("onclick", "NewWindow('OTReportBus.aspx?BusLine=" & PickUp & "&OTDateFrom=" & ucDateFrom.text.Trim &
                 "&OTDateTo=" & ucDateTo.text.Trim &
                 "&PickupTime=" & PickUp_Time & "&Num=" & Num & "','_blank',800,500,'yes')")
            Next

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "Total"
            e.Row.Cells(2).Text = Total_Col1.ToString
            e.Row.Cells(3).Text = Total_Col2.ToString
            e.Row.Cells(4).Text = Total_Col3.ToString
            e.Row.Cells(5).Text = Total_Col4.ToString
            e.Row.Cells(6).Text = Total_Col5.ToString
            e.Row.Cells(7).Text = Total_Col6.ToString
            e.Row.Cells(8).Text = Total_Col7.ToString
            e.Row.Cells(9).Text = Total_Col8.ToString
            e.Row.Cells(10).Text = Total_Col9.ToString
            'e.Row.Font.Bold = True

        End If
    End Sub


    '==============  Button =========

    Protected Sub btLocation_Click(sender As Object, e As EventArgs) Handles btLocation.Click

        Dim fldName As New ArrayList
        Dim dtShow As New DataTable
        Dim colName As New ArrayList
        Dim SQL As String = String.Empty
        Dim Whr As String = String.Empty

        Const pick_location As String = "pick_location"
        Const work_date As String = "work_date"
        Const time_16 As String = "time_16"
        Const time_17 As String = "time_17"
        Const time_17_30 As String = "time_17_30"
        Const time_18 As String = "time_18"
        Const time_19 As String = "time_19"
        Const time_21 As String = "time_21"
        Const time_04 As String = "time_04"
        Const time_07 As String = "time_07"
        Const time_other As String = "time_other"


        colName.Add("Pickup Location" & VarIni.char8 & pick_location)
        colName.Add("Work Date" & VarIni.char8 & work_date)
        colName.Add("16:00/16:30" & VarIni.char8 & time_16)
        colName.Add("17:00" & VarIni.char8 & time_17)
        colName.Add("17:30" & VarIni.char8 & time_17_30)
        colName.Add("18:00" & VarIni.char8 & time_18)
        colName.Add("19:00" & VarIni.char8 & time_19)
        colName.Add("21:00" & VarIni.char8 & time_21)

        colName.Add("04:00/04:30" & VarIni.char8 & time_04)
        colName.Add("07:00" & VarIni.char8 & time_07)

        colName.Add("TimeOther" & VarIni.char8 & time_other)
        dtShow = dtCont.setColDatatable(colName, VarIni.char8)
        fldName.Add("Name")
        SQL &= VarIni.S & sqlCont.getFeild(fldName, VarIni.char8) & VarIni.F & " CodeInfo "
        SQL &= VarIni.W & " CodeType = 'PICKUP_LOCATION' order by cast(Code as int) asc"

        Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Const Doublechar8 As String = "◘◘"

        Dim HashNOOT As Hashtable = getNOOT()
        Dim HashOT_OTHER As Hashtable = getOTOTHER()
        Dim HashOT As Hashtable = getOT()

        For Each dr As DataRow In dt.Rows
            Dim dataFld = New ArrayList
            dataFld.Add(pick_location & Doublechar8 & dtCont.IsDBNullDataRow(dr, "Name"))

            Dim str_start As DateTime = Date.ParseExact(dateCont.dateFormat2(ucDateFrom.Text.Trim), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Dim str_end As DateTime = Date.ParseExact(dateCont.dateFormat2(ucDateTo.Text), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Dim strstart As String = str_start.ToString("yyyy/MM/dd")
            Dim strend As String = str_end.ToString("yyyy/MM/dd")
            Dim amtDay As Short = DateDiff(DateInterval.Day, str_start, str_end)
            If amtDay = 0 Then
                dataFld.Add(work_date & Doublechar8 & strend)
            Else
                dataFld.Add(work_date & Doublechar8 & strstart & "-" & strend)
            End If
            Dim PICKUP As String = dtCont.IsDBNullDataRow(dr, "Name").Trim

            Dim TIME_NOOT_16 As Decimal = Decimal.Zero
            Dim TIME_NOOT_16_30 As Decimal = Decimal.Zero
            Dim TIME_NOOT_17 As Decimal = Decimal.Zero
            Dim TIME_NOOT_17_30 As Decimal = Decimal.Zero
            Dim TIME_NOOT_18 As Decimal = Decimal.Zero
            Dim TIME_NOOT_04 As Decimal = Decimal.Zero
            Dim TIME_NOOT_04_30 As Decimal = Decimal.Zero

            Dim TIME_19_DAY As Decimal = Decimal.Zero
            Dim TIME_21_DAY As Decimal = Decimal.Zero
            Dim TIME_07_NIGHT As Decimal = Decimal.Zero

            Dim TIME_OTHER_DAY_NIGHT As Decimal = Decimal.Zero

            '[DAY] 16.00
            If hashCont.existDataHash(HashNOOT, PICKUP & "-16:00-0") Then
                TIME_NOOT_16 = HashNOOT.Item(PICKUP & "-16:00-0")
            End If
            '[DAY] 16.30
            If hashCont.existDataHash(HashNOOT, PICKUP & "-16:30-0") Then
                TIME_NOOT_16_30 = HashNOOT.Item(PICKUP & "-16:30-0")
            End If
            '[DAY] 17.00
            If hashCont.existDataHash(HashNOOT, PICKUP & "-17:00-0") Then
                TIME_NOOT_17 = HashNOOT.Item(PICKUP & "-17:00-0")
            End If
            '[DAY] 17.30
            If hashCont.existDataHash(HashNOOT, PICKUP & "-17:30-0") Then
                TIME_NOOT_17_30 = HashNOOT.Item(PICKUP & "-17:30-0")
            End If
            '[DAY] 18.00
            If hashCont.existDataHash(HashNOOT, PICKUP & "-18:00-0") Then
                TIME_NOOT_18 = HashNOOT.Item(PICKUP & "-18:00-0")
            End If
            '[NIGHT] 04.00
            If hashCont.existDataHash(HashNOOT, PICKUP & "-04:00-0") Then
                TIME_NOOT_04 = HashNOOT.Item(PICKUP & "-04:00-0")
            End If
            '[NIGHT] 04.30
            If hashCont.existDataHash(HashNOOT, PICKUP & "-04:30-0") Then
                TIME_NOOT_04_30 = HashNOOT.Item(PICKUP & "-04:30-0")
            End If


            '[DAY] TIME 19.00 
            If hashCont.existDataHash(HashOT, PICKUP.Trim & "-19:00") Then
                TIME_19_DAY = HashOT.Item(PICKUP.Trim & "-19:00")
            End If

            '[DAY] TIME 21.00 
            If hashCont.existDataHash(HashOT, PICKUP.Trim & "-21:00") Then
                TIME_21_DAY = HashOT.Item(PICKUP.Trim & "-21:00")
            End If
            '[NIGHT] TIME 07.00 
            If hashCont.existDataHash(HashOT, PICKUP.Trim & "-07:00") Then
                TIME_07_NIGHT = HashOT.Item(PICKUP.Trim & "-07:00")
            End If

            'OTHER [DAY]
            If hashCont.existDataHash(HashOT_OTHER, PICKUP.Trim) Then
                TIME_OTHER_DAY_NIGHT = HashOT_OTHER.Item(PICKUP.Trim)
            End If

            dataFld.Add(time_16 & Doublechar8 & TIME_NOOT_16 + TIME_NOOT_16_30)
            dataFld.Add(time_17 & Doublechar8 & TIME_NOOT_17)
            dataFld.Add(time_17_30 & Doublechar8 & TIME_NOOT_17_30)
            dataFld.Add(time_18 & Doublechar8 & TIME_NOOT_18)
            dataFld.Add(time_19 & Doublechar8 & TIME_19_DAY)
            dataFld.Add(time_21 & Doublechar8 & TIME_21_DAY)

            dataFld.Add(time_04 & Doublechar8 & TIME_NOOT_04 + TIME_NOOT_04_30)
            dataFld.Add(time_07 & Doublechar8 & TIME_07_NIGHT)

            dataFld.Add(time_other & Doublechar8 & TIME_OTHER_DAY_NIGHT)
            dtCont.addDataRow(dtShow, dr, dataFld, VarIni.char8)
        Next
        gvCont.GridviewInitial(gvSumPickup, colName,,, True,, VarIni.char8)
        gvCont.ShowGridView(gvSumPickup, dtShow)
        cntRowPickup.RowCount = gvCont.rowGridview(gvSumPickup)
        cntRowPickup.Visible = True
        cntRowShow.Visible = False
        cntRowSum.Visible = False
        gvSumPickup.Visible = True
        gvShow.Visible = False
        gvSum.Visible = False
    End Sub

    Protected Sub btShowRe_Click(sender As Object, e As EventArgs) Handles btShowRe.Click

        ShowReport()
        ShowSum()
        gvSumPickup.Visible = False
        gvShow.Visible = True
        gvSum.Visible = True
        cntRowPickup.Visible = False
        cntRowShow.Visible = True
        cntRowSum.Visible = True
        btExcel.Visible = True
    End Sub

    '==============  Sub =========

    Private Sub ShowReport()

        Dim dc As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dc, "Dept")

        Dim SQL As String = String.Empty
        Dim fldName As New ArrayList
        Dim Whr As String = String.Empty
        Dim Whr1 As String = String.Empty
        Dim Whr2 As String = String.Empty
        ''--------------------------------------- GridView Show
        Dim dtShow As New DataTable
        Dim colName As New ArrayList

        colName.Add("Shift" & VarIni.char8 & shift)
        colName.Add("Dept" & VarIni.char8 & dept)
        colName.Add("Emp No" & VarIni.char8 & empcode)
        colName.Add("Emp Name" & VarIni.char8 & name)
        colName.Add("Work Date" & VarIni.char8 & workdate)
        colName.Add("Work Type" & VarIni.char8 & work_type)
        colName.Add("Shift Day" & VarIni.char8 & shiftday)
        colName.Add("Leave Code" & VarIni.char8 & leave_code)
        colName.Add("Leave Start" & VarIni.char8 & leave_start_date)
        colName.Add("Leave Finish" & VarIni.char8 & leave_end_date)
        colName.Add("Leave Hours" & VarIni.char8 & leave_hours)

        colName.Add("OT Start" & VarIni.char8 & ot_start_date)
        colName.Add("OT Finish" & VarIni.char8 & ot_end_date)
        colName.Add("OT Hours" & VarIni.char8 & ot_hours)
        colName.Add("Lunch Hours" & VarIni.char8 & lunch_hours)
        colName.Add("OT Total" & VarIni.char8 & ot_total)
        colName.Add("Pickup Location" & VarIni.char8 & pickup)
        colName.Add("Create by" & VarIni.char8 & create_by)
        colName.Add("Create Date" & VarIni.char8 & create_date)
        colName.Add("Change by" & VarIni.char8 & change_by)
        colName.Add("Change Date" & VarIni.char8 & change_date)
        dtShow = dtCont.setColDatatable(colName, VarIni.char8)

        Whr &= whrCont.Where("O.DEPT", cblDept, False, deptcode.Replace(",", "','"), True) 'DEPT
        Whr &= whrCont.Where("O.EMPNO", tbEmpNo) 'EMPCODE
        Whr &= whrCont.Where("O.WORK_DATE", dateCont.dateFormat2(ucDateFrom.Text), dateCont.dateFormat2(ucDateTo.Text), True) 'DATE
        Whr &= whrCont.Where("O.WORK_BEGIN_TIME", cbShift)

        fldName.Add("CASE O.WORK_BEGIN_TIME WHEN '19:00' THEN 'Night' ELSE 'Day' END" & VarIni.char8 & shift)
        fldName.Add("O.DEPT" & VarIni.char8 & dept)
        fldName.Add("O.EMPNO" & VarIni.char8 & empcode)

        fldName.Add("E.EMP_NAME" & VarIni.char8 & name)
        fldName.Add("Y.EMP_NAME" & VarIni.char8 & "NAME_HY")

        fldName.Add("RTRIM(substring(O.WORK_DATE,7,4))+'-'+substring(O.WORK_DATE,5,2)+'-'+substring(O.WORK_DATE,1,4)" & VarIni.char8 & workdate)
        fldName.Add("O.WORK_TYPE" & VarIni.char8 & work_type)
        fldName.Add("O.SHIFT_DAY" & VarIni.char8 & shiftday)
        fldName.Add("CASE O.LEAVE_CODE WHEN '' THEN '' ELSE RTRIM(LEAVE_CODE)+'-'+RTRIM(C.Name) END" & VarIni.char8 & leave_code)
        fldName.Add("CASE O.LEAVE_CODE WHEN '' THEN '' ELSE RTRIM(substring(O.LEAVE_STARTED_DATE,7,4)) +'-'+substring(O.LEAVE_STARTED_DATE,5,2)+'-'+substring(O.LEAVE_STARTED_DATE,1,4)+' '+O.LEAVE_STARTED_TIME END" & VarIni.char8 & leave_start_date)
        fldName.Add("CASE O.LEAVE_CODE WHEN '' THEN '' ELSE RTRIM(substring(O.LEAVE_FINISHED_DATE,7,4))+'-'+substring(O.LEAVE_FINISHED_DATE,5,2)+'-'+substring(O.LEAVE_FINISHED_DATE,1,4)+' '+O.LEAVE_FINISHED_TIME END" & VarIni.char8 & leave_end_date)
        fldName.Add("CAST(O.LEAVE_TIME/3600 AS DECIMAL(16,1))" & VarIni.char8 & leave_hours)
        fldName.Add("CASE O.OT_STARTED_DATE WHEN '' THEN '' ELSE RTRIM(substring(O.OT_STARTED_DATE,7,4))+'-'+substring(O.OT_STARTED_DATE,5,2)+'-'+substring(O.OT_STARTED_DATE,1,4)+' '+O.OT_STARTED_TIME END" & VarIni.char8 & ot_start_date)
        fldName.Add("CASE O.OT_FINISHED_DATE WHEN '' THEN '' ELSE RTRIM(substring(O.OT_FINISHED_DATE,7,4))+'-'+substring(O.OT_FINISHED_DATE,5,2)+'-'+substring(O.OT_FINISHED_DATE,1,4)+' '+O.OT_FINISHED_TIME END" & VarIni.char8 & ot_end_date)
        fldName.Add("CAST(O.OVER_TIME/3600 AS DECIMAL(16,1))" & VarIni.char8 & ot_hours)
        fldName.Add("CAST(O.LUNCH_TIME/3600 AS DECIMAL(16,1))" & VarIni.char8 & lunch_hours)
        fldName.Add("CAST((O.OVER_TIME +O.LUNCH_TIME )/3600 AS DECIMAL(16,1))" & VarIni.char8 & ot_total)
        fldName.Add("O.PICKUP_LOCATION" & VarIni.char8 & pickup)
        fldName.Add("O.CREATEBY" & VarIni.char8 & create_by)
        fldName.Add("O.CREATEDATE" & VarIni.char8 & create_date)
        fldName.Add("O.CHANGEBY" & VarIni.char8 & change_by)
        fldName.Add("O.CHANGEDATE" & VarIni.char8 & change_date)

        SQL &= VarIni.S & sqlCont.getFeild(fldName, VarIni.char8) & VarIni.F & " OVER_TIME_RECORD O"
        SQL &= VarIni.L & "V_HR_EMPLOYEE E " & VarIni.O2 & " CODE=EMPNO  " & COLL
        SQL &= " LEFT JOIN HR_EMPLOYEE_HY Y ON Y.CODE = EMPNO "
        SQL &= VarIni.L & " V_HR_DEPT D " & VarIni.O2 & "D.Code  =E.DEPT_CODE " & COLL
        SQL &= VarIni.L & " CodeInfo C " & VarIni.O2 & " C.Code =LEAVE_CODE and CodeType = 'LeaveCode' " & COLL
        SQL &= VarIni.W & " 1=1 " & Whr

        Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Const Doublechar8 As String = "◘◘"

        For Each dr As DataRow In dt.Rows
            Dim dataFld = New ArrayList
            dataFld.Add(shift & Doublechar8 & dtCont.IsDBNullDataRow(dr, shift))
            dataFld.Add(dept & Doublechar8 & dtCont.IsDBNullDataRow(dr, dept))
            dataFld.Add(empcode & Doublechar8 & dtCont.IsDBNullDataRow(dr, empcode))
            Dim STRNAME As String = If(dtCont.IsDBNullDataRow(dr, name) = "", dtCont.IsDBNullDataRow(dr, "NAME_HY"), dtCont.IsDBNullDataRow(dr, name))

            dataFld.Add(name & Doublechar8 & STRNAME)

            dataFld.Add(workdate & Doublechar8 & dtCont.IsDBNullDataRow(dr, workdate))
            dataFld.Add(work_type & Doublechar8 & dtCont.IsDBNullDataRow(dr, work_type))
            dataFld.Add(shiftday & Doublechar8 & dtCont.IsDBNullDataRow(dr, shiftday))
            dataFld.Add(leave_code & Doublechar8 & dtCont.IsDBNullDataRow(dr, leave_code))
            dataFld.Add(leave_start_date & Doublechar8 & dtCont.IsDBNullDataRow(dr, leave_start_date))
            dataFld.Add(leave_end_date & Doublechar8 & dtCont.IsDBNullDataRow(dr, leave_end_date))
            dataFld.Add(leave_hours & Doublechar8 & strFormat(dtCont.IsDBNullDataRowDecimal(dr, leave_hours)))
            dataFld.Add(ot_start_date & Doublechar8 & dtCont.IsDBNullDataRow(dr, ot_start_date))
            dataFld.Add(ot_end_date & Doublechar8 & dtCont.IsDBNullDataRow(dr, ot_end_date))

            dataFld.Add(ot_hours & Doublechar8 & strFormat(dtCont.IsDBNullDataRowDecimal(dr, ot_hours)))
            dataFld.Add(lunch_hours & Doublechar8 & strFormat(dtCont.IsDBNullDataRowDecimal(dr, lunch_hours)))
            dataFld.Add(ot_total & Doublechar8 & strFormat(dtCont.IsDBNullDataRowDecimal(dr, ot_total)))

            dataFld.Add(pickup & Doublechar8 & dtCont.IsDBNullDataRow(dr, pickup))

            dataFld.Add(create_by & Doublechar8 & dtCont.IsDBNullDataRow(dr, create_by))
            dataFld.Add(create_date & Doublechar8 & dtCont.IsDBNullDataRow(dr, create_date))
            dataFld.Add(change_by & Doublechar8 & dtCont.IsDBNullDataRow(dr, change_by))
            dataFld.Add(change_date & Doublechar8 & dtCont.IsDBNullDataRow(dr, change_date))
            dtCont.addDataRow(dtShow, dr, dataFld, VarIni.char8)

        Next

        gvCont.GridviewInitial(gvShow, colName,,,,, VarIni.char8)
        gvCont.ShowGridView(gvShow, dtShow)
        cntRowShow.RowCount = gvCont.rowGridview(gvShow)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Private Sub ShowSum()

        Dim dc As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dc, "Dept")

        Dim dtShow As New DataTable
        Dim colName As New ArrayList
        Const Work As String = "Work"
        Const Total_Persons As String = "Total_Persons"
        Const Leave_Hours As String = "Leave_Hours"
        Const Lunch_Hours As String = "Lunch_Hours"
        Const OT_Hours As String = "OT_Hours"

        colName.Add("Shift" & VarIni.char8 & shift)
        colName.Add("DateOfOT" & VarIni.char8 & workdate)
        colName.Add("Dept" & VarIni.char8 & dept)
        colName.Add("Total Persons in Dept" & VarIni.char8 & Total_Persons)
        colName.Add("Working/Person" & VarIni.char8 & Work)
        colName.Add("Leave Hours" & VarIni.char8 & Leave_Hours)
        colName.Add("Lunch Hours" & VarIni.char8 & Lunch_Hours)
        colName.Add("OT Hours" & VarIni.char8 & OT_Hours)
        dtShow = dtCont.setColDatatable(colName, VarIni.char8)

        Dim fldName As New ArrayList
        Dim Sql As String = String.Empty
        Dim Whr As String = String.Empty

        Whr &= whrCont.Where("WORK_DATE", dateCont.dateFormat2(ucDateFrom.Text), dateCont.dateFormat2(ucDateTo.Text), True) 'DATE
        Whr &= whrCont.Where("DEPT", cblDept, False, deptcode.Replace(",", "','"), True) 'DEPT
        Whr &= whrCont.Where("EMPNO", tbEmpNo)

        fldName.Add("DISTINCT WORK_DATE")
        fldName.Add("DEPT")

        Sql &= VarIni.S & sqlCont.getFeild(fldName, VarIni.char8) & VarIni.F & OT_Record
        Sql &= VarIni.W & " 1=1 " & Whr
        Dim dt As DataTable = dbConn.Query(Sql, VarIni.DBMIS, dbConn.WhoCalledMe)
        Const Doublechar8 As String = "◘◘"

        'TOTAL PERSON IN DEPT
        Dim Hash_PERSON_DEPT As Hashtable = getCalTotalOvertime("WORK_DATE", "COUNT")
        'WORKING
        Dim Hash_WORKING As Hashtable = getCalTotalOvertime("WORK_DATE", "COUNT", True)
        'LEAVE HOURS
        Dim Hash_LEAVE As Hashtable = getCalTotalOvertime("LEAVE_TIME", "SUM")
        'LUNCH HOURS 
        Dim Hash_LUNCH As Hashtable = getCalTotalOvertime("LUNCH_TIME", "SUM")
        'OT HOURS 
        Dim Hash_OT As Hashtable = getCalTotalOvertime("OVER_TIME", "SUM")

        For Each dr As DataRow In dt.Rows
            Dim dataFld = New ArrayList
            Dim WORK_DATE As String = dtCont.IsDBNullDataRow(dr, "WORK_DATE")
            Dim DEPARMENT As String = dtCont.IsDBNullDataRow(dr, "DEPT")

            Dim PERSON_DEPT As Decimal = Decimal.Zero
            Dim WORKING As Decimal = Decimal.Zero
            Dim LEAVE As Decimal = Decimal.Zero
            Dim LUNCH As Decimal = Decimal.Zero
            Dim OT As Decimal = Decimal.Zero

            'TOTAL PERSON IN DEPT
            If hashCont.existDataHash(Hash_PERSON_DEPT, WORK_DATE & "-" & DEPARMENT) Then
                PERSON_DEPT = Hash_PERSON_DEPT.Item(WORK_DATE & "-" & DEPARMENT)
            End If
            'WORKING
            If hashCont.existDataHash(Hash_WORKING, WORK_DATE & "-" & DEPARMENT) Then
                WORKING = Hash_WORKING.Item(WORK_DATE & "-" & DEPARMENT)
            End If
            'LEAVE HOURS
            If hashCont.existDataHash(Hash_LEAVE, WORK_DATE & "-" & DEPARMENT) Then
                LEAVE = Hash_LEAVE.Item(WORK_DATE & "-" & DEPARMENT)
            End If
            'LUNCH HOURS 
            If hashCont.existDataHash(Hash_LUNCH, WORK_DATE & "-" & DEPARMENT) Then
                LUNCH = Hash_LUNCH.Item(WORK_DATE & "-" & DEPARMENT)
            End If
            'OT HOURS 
            If hashCont.existDataHash(Hash_OT, WORK_DATE & "-" & DEPARMENT) Then
                OT = Hash_OT.Item(WORK_DATE & "-" & DEPARMENT)
            End If

            Dim ShiftSel As String = String.Empty
            Dim Val As Boolean = False
            For Each boxItem As ListItem In cbShift.Items
                Dim boxVal As String = CStr(boxItem.Text.Trim)
                If boxItem.Selected = True Then
                    ShiftSel &= boxVal & ","
                    Val = True
                End If
            Next

            dataFld.Add(shift & Doublechar8 & If(Val = False, "Day,Night", ShiftSel))
            dataFld.Add(workdate & Doublechar8 & WORK_DATE)
            dataFld.Add(dept & Doublechar8 & DEPARMENT)
            dataFld.Add(Total_Persons & Doublechar8 & PERSON_DEPT)
            dataFld.Add(Work & Doublechar8 & WORKING)
            dataFld.Add(Leave_Hours & Doublechar8 & LEAVE / 3600)
            dataFld.Add(Lunch_Hours & Doublechar8 & LUNCH / 3600)
            dataFld.Add(OT_Hours & Doublechar8 & OT / 3600)
            dtCont.addDataRow(dtShow, dr, dataFld, VarIni.char8)
        Next
        gvCont.GridviewInitial(gvSum, colName,,,,, VarIni.char8)
        gvCont.ShowGridView(gvSum, dtShow)
        cntRowSum.RowCount = gvCont.rowGridview(gvSum)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollSum", "gridviewScrollSum();", True)
    End Sub

End Class