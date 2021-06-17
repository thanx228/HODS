Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl
Public Class OTReportBus
    Inherits System.Web.UI.Page
    Dim dt As DataTable = New DataTable
    Dim sqlCont As New SQLControl
    Dim whrCont As New WhereControl
    Dim dbConn As New DataConnectControl
    Dim gvCont As New GridviewControl
    Dim dtCont As New DataTableControl
    Dim dateCont As New DateControl
    Const ot_hours As String = "OT_Hours"
    Const leave_hours As String = "Leave_Hours"
    Const lunch_hours As String = "Lunch_Hours"
    Const leave_code As String = "Leave_Code"
    Const ot_total As String = "OT_Total"
    Const shiftday As String = "Shiftday" 'day(7),day(7-17),day(8),day(8-18)
    Const shift As String = "Shift"
    Const dept As String = "Dept"
    Const name As String = "Name"
    Const pickup As String = "Pickup_Location"
    Const ot_start_date As String = "OT_start_date"
    Const ot_start_time As String = "OT_start_time"
    Const ot_end_date As String = "OT_end_date"
    Const ot_end_time As String = "OT_end_time"
    Const leave_start_date As String = "Leave_Start_Date"
    Const leave_start_time As String = "Leave_Start_Time"
    Const leave_end_date As String = "Leave_End_Date"
    Const leave_end_time As String = "Leave_End_Time"
    Const workdate As String = "Workdate"
    Const work_begin_tm As String = "Work_Begin_Time"
    Const work_end_tm As String = "Work_End_Time"
    Const work_begin_dt As String = "Work_Eegin_Date"
    Const work_end_dt As String = "Work_End_Date"
    Const create_date As String = "Create_Date"
    Const create_by As String = "Create_By"
    Const change_date As String = "Change_Date"
    Const change_by As String = "Change_By"
    Const COLL As String = "collate Chinese_PRC_BIN"
    Const empcode As String = "Empno"
    Const work_type As String = "WorkType"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim SQL As String = ""
        Dim Program As New Data.DataTable
        Dim BusLine As String = Request.QueryString("BusLine").Trim()
        Dim Num As String = Request.QueryString("Num").Trim()
        Dim PickupTime As String = Request.QueryString("PickupTime").Trim()
        Dim OTDateFrom As String = Request.QueryString("OTDateFrom").Trim()
        Dim OTDateTo As String = Request.QueryString("OTDateTo").Trim()
        Dim Time As String = String.Empty

        Time = "('" & PickupTime.Substring(0, PickupTime.Count).Replace("/", "','") & "')"
        Dim fldName As New ArrayList,
        whr As String = String.Empty,
        grpby As String = String.Empty

        whr &= whrCont.Where("O.WORK_DATE", dateCont.dateFormat2(OTDateFrom), dateCont.dateFormat2(OTDateTo), True) 'DATE
        whr &= whrCont.Where("O.PICKUP_LOCATION", "= N'" & BusLine & "'")

        If Num = 0 Then
            whr &= whrCont.Where("O.OT_FINISHED_TIME", "",, False)
            whr &= whrCont.Where("R.WORK_END_TIME", " IN ('16:00','16:30','17:00','17:30','18:00','04:00','04:30')")
        ElseIf Num = 1 Then
            whr &= whrCont.Where("O.OT_FINISHED_TIME", " IN " & Time)
        ElseIf Num = 2 Then
            whr &= whrCont.Where("O.OT_FINISHED_TIME", " NOT IN " & Time)
            whr &= whrCont.Where("R.WORK_END_TIME", " IN ('16:00','16:30','17:00','17:30','18:00','04:00','04:30')")
        End If

        fldName.Add("CASE O.WORK_BEGIN_TIME WHEN '19:00' THEN 'Night' ELSE 'Day' END" & VarIni.char8 & shift)
        fldName.Add("O.DEPT" & VarIni.char8 & dept)
        fldName.Add("O.EMPNO" & VarIni.char8 & empcode)
        fldName.Add("E.EMP_NAME" & VarIni.char8 & name)
        fldName.Add("RTRIM(substring(O.WORK_DATE,7,4))+'-'+substring(O.WORK_DATE,5,2)+'-'+substring(O.WORK_DATE,1,4)" & VarIni.char8 & workdate)
        fldName.Add("O.WORK_TYPE" & VarIni.char8 & work_type)
        fldName.Add("O.SHIFT_DAY" & VarIni.char8 & shiftday)
        fldName.Add("CASE O.LEAVE_CODE WHEN '' THEN '' ELSE RTRIM(LEAVE_CODE)+'-'+RTRIM(C.Name) END" & VarIni.char8 & leave_code)
        fldName.Add("CASE O.LEAVE_CODE WHEN '' THEN '' ELSE RTRIM(substring(O.LEAVE_STARTED_DATE,7,4)) +'-'+substring(O.LEAVE_STARTED_DATE,5,2)+'-'+substring(O.LEAVE_STARTED_DATE,1,4)+' '+O.LEAVE_STARTED_TIME END" & VarIni.char8 & leave_start_date)
        fldName.Add("CASE O.LEAVE_CODE WHEN '' THEN '' ELSE RTRIM(substring(O.LEAVE_FINISHED_DATE,7,4))+'-'+substring(O.LEAVE_FINISHED_DATE,5,2)+'-'+substring(O.LEAVE_FINISHED_DATE,1,4)+' '+O.LEAVE_FINISHED_TIME END" & VarIni.char8 & leave_end_date)
        fldName.Add("CAST(O.LEAVE_TIME/3600 AS DECIMAL(16,1))" & VarIni.char8 & leave_hours)
        fldName.Add("CASE O.OT_STARTED_DATE WHEN '' THEN '' ELSE RTRIM(substring(O.OT_STARTED_DATE,7,4))+'-'+substring(O.OT_STARTED_DATE,5,2)+'-'+substring(O.OT_STARTED_DATE,1,4)+' '+O.OT_STARTED_TIME END" & VarIni.char8 & ot_start_date)
        fldName.Add("CASE O.OT_FINISHED_DATE WHEN '' THEN '' ELSE RTRIM(substring(O.OT_FINISHED_DATE,7,4))+'-'+substring(O.OT_FINISHED_DATE,5,2)+'-'+substring(O.OT_FINISHED_DATE,1,4)+' '+O.OT_FINISHED_TIME END" & VarIni.char8 & ot_end_date)
        fldName.Add("CASE WHEN O.WORK_TYPE = 'Work Day' THEN CAST(O.OVER_TIME/3600 AS DECIMAL(16,1))    ELSE CAST((O.NORMAL_TIME+O.OVER_TIME) /3600 AS DECIMAL(16,1)) END" & VarIni.char8 & ot_hours)
        fldName.Add("CAST(O.LUNCH_TIME/3600 AS DECIMAL(16,1))" & VarIni.char8 & lunch_hours)
        fldName.Add("CASE WHEN O.WORK_TYPE = 'Work Day' THEN CAST((O.OVER_TIME +O.LUNCH_TIME )/3600 AS DECIMAL(16,1)) ELSE  CAST((O.OVER_TIME +O.LUNCH_TIME+O.NORMAL_TIME )/3600 AS DECIMAL(16,1)) END" & VarIni.char8 & ot_total)
        fldName.Add("O.PICKUP_LOCATION" & VarIni.char8 & pickup)
        fldName.Add("O.CREATEBY" & VarIni.char8 & create_by)
        fldName.Add("O.CREATEDATE" & VarIni.char8 & create_date)
        fldName.Add("O.CHANGEBY" & VarIni.char8 & change_by)
        fldName.Add("O.CHANGEDATE" & VarIni.char8 & change_date)

        SQL &= VarIni.S & sqlCont.getFeild(fldName, VarIni.char8) & VarIni.F & " OVER_TIME_RECORD O"
        SQL &= VarIni.L & "V_HR_EMPLOYEE E " & VarIni.O2 & " CODE=EMPNO  " & COLL
        SQL &= VarIni.L & " V_HR_DEPT D " & VarIni.O2 & "D.Code  =DEPT_CODE " & COLL
        SQL &= VarIni.L & " CodeInfo C " & VarIni.O2 & " C.Code =LEAVE_CODE and CodeType = 'LeaveCode' " & COLL
        SQL &= VarIni.L & " V_HR_ATTENDANCE_RANK R ON R.CODE_SHIFT = O.SHIFT_DAY  collate Chinese_PRC_BIN "
        SQL &= VarIni.W & " 1=1 " & whr

        Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        gvCont.ShowGridView(gvShow, dt)

    End Sub
End Class