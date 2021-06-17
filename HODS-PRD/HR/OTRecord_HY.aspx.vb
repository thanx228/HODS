
Imports System.Globalization
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class OTRecord_HY
    Inherits System.Web.UI.Page

    Dim cblCont As New CheckBoxListControl
    Dim ddlCnt As New DropDownListControl
    Dim whrCont As New WhereControl
    Dim sqlCont As New SQLControl
    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim gvCont As New GridviewControl
    Dim dateCont As New DateControl
    Dim deptcode As String = String.Empty
    Dim grp As String = String.Empty
    Dim UserOTRecord As String = "UserOTRecord"
    Dim OT_DateSet As String = "NormalSatOT"
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

            If ucDate.text = "" Then
                ucDate.text = Date.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
            End If

            Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
            deptcode = dtCont.IsDBNullDataRow(dr, "Dept")
            Dim chk As Boolean = False
            lbGrp.Text = String.Empty
            If deptcode = "" Then
                Response.Redirect(Server.UrlPathEncode("../Home.aspx"))
            Else
                deptcode = dtCont.IsDBNullDataRow(dr, "Dept")
                lbGrp.Text = dtCont.IsDBNullDataRow(dr, "Grp")
                chk = If(lbGrp.Text = "dept_head", True, False)
            End If

            cblCont.showCheckboxList(cblDept, getDept(deptcode.Replace(",", "','")), VarIni.DBMIS, "CodeName", "Code", 4)
            NewButtonSet(chk)
            rdlShift_SelectedIndexChanged(sender, e)
            btSave.Visible = False
            btUpdate.Visible = False
            btPrint.Visible = False
            btExcel.Visible = False
            btEdit.Visible = False
            btCancel.Visible = False
        End If
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollEdit", "gridviewScrollEdit();", True)
    End Sub

    '=============== Function ===================

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

    Function trailing_spaces(str As String)
        Return "RTRIM(" & str & ")"
    End Function

    Function getCodeInfo(str As String, Optional valstr As String = "", Optional Chk As Boolean = False)

        Dim SQL As String = VarIni.S & getTRIM(CODEINFO.Code) & " CODE," & getTRIM(CODEINFO.Code) & "+'-'+" & getTRIM(CODEINFO.Name) & " CODENAME" & VarIni.F & CODEINFO.TABLENAME
        SQL &= VarIni.W & VarIni.oneEqualOne
        SQL &= whrCont.Where(CODEINFO.CodeType, str,, False)
        If Chk = True Then
            SQL &= whrCont.Where(CODEINFO.Code, " <> N'" & valstr & "'")
        End If
        SQL &= VarIni.O & " Name DESC"
        Return SQL
    End Function

    Function getTRIM(str As String)
        Dim SQL As String = "RTRIM(" & str & ")"
        Return SQL
    End Function

    Function getDocNo() As String
        Dim DocNo As String = String.Empty
        Dim DateDoc As String = Date.Today.ToString("yyyyMMdd")
        Dim SQL As String = "select SUBSTRING(DOCNO,1,8),max(DOCNO) DOCNO from " & OT_Record & " where DOCNO like '" & DateDoc & "%' group by SUBSTRING(DOCNO,1,8)"
        Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        DocNo = (If(dt.Rows.Count > 0, CDec(dt.Rows(0).Item("DOCNO")) + 1, DateDoc & "0001"))
        Return DocNo
    End Function

    Function strFormat(ByVal val As Object) As String
        Dim show As String = String.Empty
        If val <> "0.0" Then
            show = String.Format("{0:n2}", val)
        End If

        Return show
    End Function

    Function GET_CONV_STRING(ByVal DATEVAL As Date)
        Dim DATE2 As String = String.Empty

        DATE2 = DATEVAL.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)

        Return DATE2
    End Function

    Function GET_CONV_DATE_TO_STRING(ByVal DATEVAL As Object)
        Dim DATE2 As String = String.Empty
        If DATEVAL <> String.Empty Then
            Dim DATE1 As Date = Date.ParseExact(DATEVAL.Trim, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            DATE2 = DATE1.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
        End If
        Return DATE2
    End Function

    '================== DataBound  ==================

    Function getCalculateDay(Shift As String, Val_Date As String) As String
        Dim OT_Type As String = "Work Day"
        Dim Day As DateTime = Date.ParseExact(Val_Date, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
        Dim DayOfWeek As String = Day.DayOfWeek()
        If DayOfWeek = "0" Then 'sunday
            OT_Type = "Holiday"
        ElseIf DayOfWeek = "6" Then
            If Shift = "Day(7)" Or Shift = "Day(8)" Or Shift = "Day(7-1)" Then
                Dim SQL As String = VarIni.S & " DateSat " & VarIni.F & OT_DateSet & VarIni.W & " DateSat='" & Val_Date & "'"
                Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                If dt.Rows.Count > 0 Then
                    OT_Type = "Work Day"
                Else
                    OT_Type = "Holiday"
                End If
            Else
                OT_Type = "Holiday"
            End If
        End If

        Return OT_Type
    End Function

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        Dim sql As String = ""
        With e.Row
            If .RowType = DataControlRowType.DataRow Then

                Dim shiftday As String = .DataItem("shiftday")
                Dim default_shift As String = .DataItem("shift").ToString.Trim
                Dim Work_Type As String = .DataItem("workType")
                Dim lbHoliday As TextBox = .FindControl("lbHoliday")
                Dim Work_Date As String = .DataItem("workdate")

                Dim ddlleaveCode As DropDownList = .FindControl("ddlleaveCode")
                Dim ddlLeaveCase As DropDownList = .FindControl("ddlLeaveCase")
                Dim tbStartTime As TextBox = .FindControl("tbOTStartTime")
                Dim tbStartDate As TextBox = .FindControl("tbOTStartDate")
                Dim tbEndTime As TextBox = .FindControl("tbOTEndTime")
                Dim tbEndDate As TextBox = .FindControl("tbOTEndDate")
                Dim ddlShiftDay As DropDownList = .FindControl("ddlShiftDay")
                Dim ddlBusLine As DropDownList = .FindControl("ddlBusLine")
                Dim Holiday As CheckBox = .FindControl("cbHoliday")


                showDDL(ddlleaveCode, getCodeInfo("LeaveCode"), VarIni.DBMIS, "CODENAME", "CODE", False) 'Leave Code
                lbHoliday.Text = getCalculateDay(default_shift, ucDate.text).ToString  'OT Type
                Dim OTEndTime As String = getOTStartTime(lbHoliday.Text, default_shift, True)

                If lbHoliday.Text.Trim = "Holiday" Then
                    Holiday.Enabled = True
                Else
                    Holiday.Enabled = False
                End If
                '-------------------------------------------------------------------
                'set time day or night
                '-------------------------------------------------------------------

                Dim OTEndDate_Cnv1 As Date = Date.ParseExact(Work_Date, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Dim OTStartDate As String = String.Empty
                If rdlShift.SelectedItem.Text = "Day" Then
                    If rdlDefault.SelectedItem.Text = "19.00" Then
                        tbEndTime.Text = "19.00"
                        tbEndDate.Text = GET_CONV_STRING(OTEndDate_Cnv1)
                    ElseIf rdlDefault.SelectedItem.Text = "21.00" Then
                        tbEndTime.Text = "21.00"
                        tbEndDate.Text = GET_CONV_STRING(OTEndDate_Cnv1)
                    Else
                        tbEndTime.Text = OTEndTime.Split("-").ToArray(1)
                        tbEndDate.Text = GET_CONV_STRING(OTEndDate_Cnv1)
                    End If
                    tbStartTime.Text = If(lbHoliday.Text.Trim = "Holiday", OTEndTime.Split("-").ToArray(0), OTEndTime.Split("-").ToArray(1))
                    tbStartDate.Text = GET_CONV_STRING(OTEndDate_Cnv1)
                Else
                    If rdlDefault.SelectedItem.Text = "07.00" Then
                        tbEndTime.Text = "07:00"
                        tbEndDate.Text = GET_CONV_STRING(DateAdd(DateInterval.Day, 1, OTEndDate_Cnv1))
                    Else
                        tbEndTime.Text = OTEndTime.Split("-").ToArray(1)
                        tbEndDate.Text = GET_CONV_STRING(DateAdd(DateInterval.Day, 1, OTEndDate_Cnv1))
                    End If
                    tbStartTime.Text = If(lbHoliday.Text.Trim = "Holiday", OTEndTime.Split("-").ToArray(0), OTEndTime.Split("-").ToArray(1))
                    tbStartDate.Text = GET_CONV_STRING(DateAdd(DateInterval.Day, 1, OTEndDate_Cnv1))
                End If
                '--------------------------------------------------
                'Shift Day
                '--------------------------------------------------
                'CODE   NAME         WC
                '1      Day(7)       PD
                '2      Day(7-17)    OFFICE
                '3      Day(8)       PD
                '4      Day(8-18)    OFFICE
                '5      Night        PD
                '--------------------------------------------------

                Dim Qry_Shift_Day As String = VarIni.S & HR_ATTENDANCE_RANK.CODE_SHIFT & " CODE" & VarIni.F & HR_ATTENDANCE_RANK.TABLENAME
                Qry_Shift_Day &= VarIni.W & VarIni.oneEqualOne
                Qry_Shift_Day &= whrCont.Where(HR_ATTENDANCE_RANK.CODE_SHIFT, If(rdlShift.SelectedValue = "D", " IN ('Day(7)','Day(8)','Day(7-1)','Day(8)Office','Day(7)Office') ", " IN ('Night(19)','Night(19-1)')"))
                Qry_Shift_Day &= whrCont.Where(HR_ATTENDANCE_RANK.CODE_SHIFT, "  <> '" & default_shift & "'")
                showDDL(ddlShiftDay, Qry_Shift_Day, VarIni.DBMIS, "CODE", "CODE", True, default_shift)

                '-------------------------------------------
                '--------------Bus Line-----------------
                ' --------------------------------------------
                Dim BusLine As String = String.Empty
                BusLine = .DataItem("pickup").ToString
                Dim Qry_Bus As String = VarIni.S & trailing_spaces(CODEINFO.Name) & " NAME" & VarIni.F & CODEINFO.TABLENAME
                Qry_Bus &= VarIni.W & VarIni.oneEqualOne
                Qry_Bus &= whrCont.Where(CODEINFO.CodeType, "PICKUP_LOCATION",, False)
                Qry_Bus &= whrCont.Where(CODEINFO.Code, "<>'" & BusLine & "'")
                Qry_Bus &= VarIni.O & " CAST(" & CODEINFO.Code & " AS INT)"
                showDDL(ddlBusLine, Qry_Bus, VarIni.DBMIS, "NAME", "NAME", True, If(BusLine = "", "==select==", BusLine))
            End If
        End With
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Private Sub gvEdit_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEdit.RowDataBound
        Dim sql As String = ""
        With e.Row
            If .RowType = DataControlRowType.DataRow Then

                Dim Shift As String = .DataItem("shift")
                Dim ShiftDay As String = .DataItem("shiftday")
                Dim Work_Type As String = .DataItem("workType")
                Dim Work_Date As String = .DataItem("workdate")

                Dim ddlleaveCode As DropDownList = .FindControl("ddlleaveCode")
                Dim ddlLeaveCase As DropDownList = .FindControl("ddlLeaveCase")
                'LEAVE 
                Dim tbLeaveStartDate As TextBox = .FindControl("tbLeaveStartDate")
                Dim tbLeaveStartTime As TextBox = .FindControl("tbLeaveStartTime")
                Dim tbLeaveEndDate As TextBox = .FindControl("tbLeaveEndDate")
                Dim tbLeaveEndTime As TextBox = .FindControl("tbLeaveEndTime")
                'WORK
                Dim tbWorkStartTime As TextBox = .FindControl("tbWorkStartTime")
                Dim tbWorkStartDate As TextBox = .FindControl("tbWorkStartDate")
                Dim tbWorkEndTime As TextBox = .FindControl("tbWorkEndTime")
                Dim tbWorkEndDate As TextBox = .FindControl("tbWorkEndDate")
                'OT
                Dim tbOTStartTime As TextBox = .FindControl("tbOTStartTime")
                Dim tbOTStartDate As TextBox = .FindControl("tbOTStartDate")
                Dim tbOTEndTime As TextBox = .FindControl("tbOTEndTime")
                Dim tbOTEndDate As TextBox = .FindControl("tbOTEndDate")
                'OT Lunch
                Dim cbOTLunch As CheckBox = .FindControl("cbOTLunch")
                Dim ddlShiftDay As DropDownList = .FindControl("ddlShiftDay")
                Dim ddlBusLine As DropDownList = .FindControl("ddlBusLine")
                Dim NeedPickup As CheckBox = .FindControl("cbBusLine")
                Dim default_shift As String = .DataItem("shiftday").ToString.Trim
                Dim lbHoliday As TextBox = .FindControl("lbHoliday")
                lbHoliday.Text = Work_Type.Trim
                Dim EndTime As String = getOTStartTime(lbHoliday.Text, ShiftDay.Trim, False)
                Dim leave_code As String = .DataItem("LEAVE_CODE")

                Dim cbHoliday As CheckBox = .FindControl("cbHoliday")
                If lbHoliday.Text.Trim = "Holiday" Then
                    cbHoliday.Enabled = True
                    cbHoliday.Checked = True
                Else
                    cbHoliday.Enabled = False
                    cbHoliday.Checked = False
                End If

                If leave_code.Trim <> "" Then

                    'Leave Code
                    Dim leave_start_date As String = GET_CONV_DATE_TO_STRING(.DataItem("leave_start_date").ToString.Trim)
                    Dim leave_end_date As String = GET_CONV_DATE_TO_STRING(.DataItem("leave_end_date").ToString.Trim)

                    tbLeaveStartDate.Text = leave_start_date
                    tbLeaveStartTime.Text = .DataItem("leave_start_time")

                    tbLeaveEndDate.Text = leave_end_date
                    tbLeaveEndTime.Text = .DataItem("leave_end_time")

                    ddlLeaveCase.Visible = True
                    tbLeaveStartDate.Visible = True
                    tbLeaveStartTime.Visible = True
                    tbLeaveEndDate.Visible = True
                    tbLeaveEndTime.Visible = True
                    ddlLeaveCase.SelectedValue = "D"
                End If
                showDDL(ddlleaveCode, getCodeInfo("LeaveCode", If(leave_code = "", "W", leave_code.Split("-").ToArray(0).Trim), True), VarIni.DBMIS, "CODENAME", "CODE", True, If(leave_code = "", "W-W:Work", leave_code))
                If CDec(.DataItem("lunch_hours")) > 0 Then
                    cbOTLunch.Checked = True
                End If
                'OVER TIME
                Dim OTStartTime As String = .DataItem("ot_start_time")
                Dim OTEndTime As String = .DataItem("ot_end_time")

                sql = String.Empty
                Dim fldName As New ArrayList
                fldName.Add(HR_ATTENDANCE_RANK.CODE_SHIFT)
                fldName.Add(HR_ATTENDANCE_RANK.WORK_BEGIN_TIME)
                fldName.Add(HR_ATTENDANCE_RANK.WORK_END_TIME)

                sql = VarIni.S & sqlCont.getFeild(fldName) & VarIni.F & HR_ATTENDANCE_RANK.TABLENAME
                sql &= VarIni.W & VarIni.oneEqualOne
                sql &= whrCont.Where(HR_ATTENDANCE_RANK.CODE_SHIFT, default_shift,, False)
                Dim dr As DataRow = dbConn.QueryDataRow(sql, VarIni.DBMIS, dbConn.WhoCalledMe)
                Dim WorkStartTime As String = dtCont.IsDBNullDataRow(dr, HR_ATTENDANCE_RANK.WORK_BEGIN_TIME)
                Dim WorkEndTime As String = dtCont.IsDBNullDataRow(dr, HR_ATTENDANCE_RANK.WORK_END_TIME)

                If .DataItem("ot_start_date").ToString.Trim = "" And lbHoliday.Text.Trim = "Work Day" Then
                    Dim OTEndTime1 As String = getOTStartTime(lbHoliday.Text, default_shift, True)
                    Dim STR_WORKDATE As String = GET_CONV_DATE_TO_STRING(dateCont.dateFormat2(Work_Date.Trim))

                    tbOTStartDate.Text = STR_WORKDATE
                    tbOTStartTime.Text = OTEndTime1.Split("-").ToArray(1).Trim
                    tbOTEndDate.Text = STR_WORKDATE
                    tbOTEndTime.Text = OTEndTime1.Split("-").ToArray(1).Trim

                ElseIf .DataItem("ot_start_date").ToString.Trim <> "" And lbHoliday.Text.Trim = "Work Day" Then

                    Dim OT_START_DATE As String = GET_CONV_DATE_TO_STRING(.DataItem("ot_start_date").ToString.Trim)
                    Dim OT_END_DATE As String = GET_CONV_DATE_TO_STRING(.DataItem("ot_end_date").ToString.Trim)
                    Dim OT_START_TIME As String = .DataItem("ot_start_time").ToString.Trim
                    Dim OT_END_TIME As String = .DataItem("ot_end_time").ToString.Trim

                    tbOTStartDate.Text = OT_START_DATE
                    tbOTStartTime.Text = OT_START_TIME.Trim
                    tbOTEndDate.Text = OT_END_DATE
                    tbOTEndTime.Text = OT_END_TIME.Trim

                ElseIf lbHoliday.Text.Trim = "Holiday" Then

                    Dim OT_START_DATE As String = GET_CONV_DATE_TO_STRING(.DataItem("ot_start_date").ToString.Trim)
                    Dim OT_END_DATE As String = GET_CONV_DATE_TO_STRING(.DataItem("ot_end_date").ToString.Trim)

                    tbOTStartDate.Text = OT_START_DATE
                    tbOTStartTime.Text = OTStartTime.Trim
                    tbOTEndDate.Text = OT_END_DATE
                    tbOTEndTime.Text = OTEndTime.Trim
                End If

                Dim Qry_Shift_Day As String = VarIni.S & HR_ATTENDANCE_RANK.CODE_SHIFT & " CODE" & VarIni.F & HR_ATTENDANCE_RANK.TABLENAME
                Qry_Shift_Day &= VarIni.W & VarIni.oneEqualOne
                Qry_Shift_Day &= whrCont.Where(HR_ATTENDANCE_RANK.CODE_SHIFT, If(rdlShift.SelectedValue = "D", " IN ('Day(7)','Day(8)','Day(7-1)','Day(8)Office','Day(7)Office') ", " IN ('Night(19)','Night(19-1)')"))
                Qry_Shift_Day &= whrCont.Where(HR_ATTENDANCE_RANK.CODE_SHIFT, "  <> '" & default_shift & "'")
                showDDL(ddlShiftDay, Qry_Shift_Day, VarIni.DBMIS, "CODE", "CODE", True, default_shift)

                '-------------------------------------------
                '--------------Bus Line-----------------
                ' --------------------------------------------
                Dim BusLine As String = String.Empty
                BusLine = .DataItem("pickup").ToString
                NeedPickup.Checked = If(BusLine = "", False, True)
                Dim Qry_Bus As String = VarIni.S & trailing_spaces(CODEINFO.Name) & " NAME" & VarIni.F & CODEINFO.TABLENAME
                Qry_Bus &= VarIni.W & VarIni.oneEqualOne
                Qry_Bus &= whrCont.Where(CODEINFO.CodeType, "PICKUP_LOCATION",, False)
                Qry_Bus &= whrCont.Where(CODEINFO.Code, "<>'" & BusLine & "'")
                Qry_Bus &= VarIni.O & " CAST(" & CODEINFO.Code & " AS INT)"
                showDDL(ddlBusLine, Qry_Bus, VarIni.DBMIS, "NAME", "NAME", True, If(BusLine = "", "==select==", BusLine))

            End If
        End With
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
        End If
    End Sub

    Function dateFormat2(ByVal dateForChange As String, Optional ByVal separate As String = "/", Optional today As Boolean = False) As String 'format yyyyMMdd
        If dateForChange = "" Then
            Return ""
        End If
        Dim tempDate As String = dateForChange.Substring(6, 2).Trim & "/" & dateForChange.Substring(4, 2).Trim & "/" & dateForChange.Substring(0, 4).Trim
        Return tempDate 'dd/MM/yyyy
    End Function

    Function getOTStartTime(ByVal OTType As String, ByVal ShiftDay As String, Chk As Boolean) As String
        Dim TIME As String = String.Empty
        If OTType = "Work Day" Then
            If ShiftDay = "Day(7)" Then
                TIME = If(Chk = True, "07.00-16.30", "16.30")
            ElseIf ShiftDay = "Day(7-1)" Then
                TIME = If(Chk = True, "07.00-16.00", "16.00")
            ElseIf ShiftDay = "Day(7)Office" Then
                TIME = If(Chk = True, "07.00-17.00", "17.00")
            ElseIf ShiftDay = "Day(8)" Then
                TIME = If(Chk = True, "08.00-17.30", "17.30")
            ElseIf ShiftDay = "Day(8)Office" Then
                TIME = If(Chk = True, "08.00-18.00", "18.00")
            ElseIf ShiftDay = "Night" Then
                TIME = If(Chk = True, "19.00-04.30", "04.30")
            End If
        ElseIf OTType = "Holiday" Then
            If ShiftDay = "Day(7)" Or ShiftDay = "Day(7-1)" Or ShiftDay = "Day(7)Office" Then
                TIME = If(Chk = True, "07.00-16.00", "07.00")
            ElseIf ShiftDay = "Day(8)" Or ShiftDay = "Day(8)Office" Then
                TIME = If(Chk = True, "08.00-18.00", "08.00")
            ElseIf ShiftDay = "Night" Then
                TIME = If(Chk = True, "19.00-04.00", "19.00")
            End If
        End If
        Return TIME
    End Function

    '================== Button  ==================

    Protected Sub btRecord_Click(sender As Object, e As EventArgs) Handles btRecord.Click
        If Session("userid") = "" Then
            Response.Redirect(VarIni.PageLogin)
        End If

        Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dr, "Dept")

        If deptcode = "" Then
            Response.Redirect(Server.UrlPathEncode("../Home.aspx"))
        End If

        Dim SQL As String = String.Empty
        Dim fldName As New ArrayList
        Dim Whr As String = String.Empty

        ''--------------------------------------- GridView Show
        fldName.Add("A.SHIFT" & VarIni.char8 & shift)
        fldName.Add("A.SHIFTDAY" & VarIni.char8 & shiftday)
        fldName.Add("A.CODE" & VarIni.char8 & empcode)
        fldName.Add("A.DEPT_CODE " & VarIni.char8 & dept)
        fldName.Add("A.EMP_NAME" & VarIni.char8 & name)
        fldName.Add("'" & ucDate.text.Replace("/", "-").Trim & "'" & VarIni.char8 & workdate)
        fldName.Add("''" & VarIni.char8 & work_type)
        fldName.Add("''" & VarIni.char8 & work_begin_tm)
        fldName.Add("''" & VarIni.char8 & work_end_tm)
        fldName.Add("A.PICKUP_LOCATION" & VarIni.char8 & pickup)

        Whr &= whrCont.Where("A.DEPT_CODE", cblDept, False, deptcode.Replace(",", "','"), True) 'DEPT
        Whr &= whrCont.Where("A.CODE", tbEmpNo) 'EMPCODE

        Whr &= whrCont.Where("A.EMP_STATE", " NOT IN ('3001','3002','3003') ")

        If rdlShift.SelectedValue = "D" Then
            Whr &= whrCont.Where("A.SHIFTDAY", "Day",, False)
        ElseIf rdlShift.SelectedValue = "N" Then
            Whr &= whrCont.Where("A.SHIFTDAY", "Night",, False)
        End If

        SQL = "SELECT " & sqlCont.getFeild(fldName, VarIni.char8) &
                    " From HR_EMPLOYEE_HY A
                        LEFT JOIN V_HR_DEPT D ON  D.Code = A.DEPT_CODE  collate Chinese_PRC_BIN  
                            WHERE  1=1" & Whr & " ORDER BY A.CODE"
        Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        gvCont.ShowGridView(gvShow, dt)
        If gvShow.Rows.Count > 0 Then
            gvShow.Visible = False
            lbSelect.Visible = False
        End If
        CountRow1.RowCount = gvCont.rowGridview(gvShow)
        System.Threading.Thread.Sleep(1000)

        gvReport.Visible = False
        gvShow.Visible = True
        gvEdit.Visible = False
        btRecord.Visible = True
        btEdit.Visible = False
        btSave.Visible = True
        btUpdate.Visible = False
        btCancel.Visible = True
        btShowRe.Visible = False
        btPrint.Visible = False
        btExcel.Visible = False

        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)

    End Sub

    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click

        If Session("userid") = "" Then
            Response.Redirect(VarIni.PageLogin)
        End If

        Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dr, "Dept")

        Dim setDate = Server.HtmlEncode(ucDate.text)
        Dim datetime As DateTime = datetime.ParseExact(setDate, "yyyyMMdd", CultureInfo.InvariantCulture)
        Dim getDate As String = datetime.ToString("yyyyMMdd")

        Dim sqlHash_gvShow As New ArrayList
        Dim LeaveStart As String = String.Empty
        Dim LeaveEnd As String = String.Empty
        Dim LeaveCode As String = String.Empty
        Dim OTType As String = String.Empty
        Dim numdate As Decimal = Decimal.Zero
        Dim LeaveTime As Decimal = Decimal.Zero

        For i As Integer = 0 To gvShow.Rows.Count - 1

            Dim EmpNo = gvShow.Rows(i).Cells(2).Text.Trim.ToString
            Dim EmpName = gvShow.Rows(i).Cells(3).Text.Trim.ToString
            Dim ShiftDays = gvShow.Rows(i).Cells(0).Text.Trim
            Dim ddlLeaveCode As DropDownList = gvShow.Rows(i).Cells(7).FindControl("ddlleaveCode")
            Dim tbLeaveStartDate As TextBox = gvShow.Rows(i).Cells(9).FindControl("tbLeaveStartDate")
            Dim tbLeaveStartTime As TextBox = gvShow.Rows(i).Cells(9).FindControl("tbLeaveStartTime")
            Dim tbLeaveEndDate As TextBox = gvShow.Rows(i).Cells(10).FindControl("tbLeaveEndDate")
            Dim tbLeaveEndTime As TextBox = gvShow.Rows(i).Cells(10).FindControl("tbLeaveEndTime")
            Dim ddlShiftDay As DropDownList = gvShow.Rows(i).Cells(6).FindControl("ddlShiftDay")
            Dim cbLunch As CheckBox = gvShow.Rows(i).Cells(11).FindControl("cbOTLunch")
            Dim tbOTBeginTime As TextBox = gvShow.Rows(i).Cells(11).FindControl("tbOTStartTime")
            Dim tbOTEndDate As TextBox = gvShow.Rows(i).Cells(12).FindControl("tbOTEndDate")
            Dim tbOTEndTime As TextBox = gvShow.Rows(i).Cells(12).FindControl("tbOTEndTime")
            Dim ddlBusLine As DropDownList = gvShow.Rows(i).Cells(13).FindControl("ddlBusLine")
            Dim cbPickup As CheckBox = gvShow.Rows(i).Cells(14).FindControl("cbBusLine")
            Dim LeaveStartDate As String = tbLeaveStartDate.Text.Replace("__/__/____", "").Trim
            Dim LeaveStartTime As String = tbLeaveStartTime.Text.Replace("__.__", "").Trim
            Dim LeaveEndDate As String = tbLeaveEndDate.Text.Replace("__/__/____", "").Trim
            Dim LeaveEndTime As String = tbLeaveEndTime.Text.Replace("__.__", "").Trim

            Dim LveChk As Boolean = False
            LeaveCode = ddlLeaveCode.SelectedValue.Split("-").ToArray(0)
            If LeaveCode <> "W" Then
                LveChk = If(LeaveStartDate = "", True, False)
                LveChk = If(LeaveStartDate = "", True, False)
                LveChk = If(LeaveEndDate = "", True, False)
                LveChk = If(LeaveEndTime = "", True, False)
            End If

            If LveChk = True Then
                show_message.ShowMessage(Page, "กรณีเลือก LeaveCode กรุณาระบุวันที่่และเวลาให้ถูกต้อง กรุณาตรวจสอบรหัส " & EmpNo & " " & EmpName & " อีกครั้ง", UpdatePanel1)
                Exit Sub
            End If

            '==========================
            'OT กรณีที่ใส่เวลา 24.00 ให้เป็นวันถัดไป
            '==========================
            Dim OT_Date As DateTime = datetime.ParseExact(dateCont.dateFormat2(tbOTEndDate.Text), "yyyyMMdd", CultureInfo.InvariantCulture)
            'Dim OT_TIME As String = tbOTEndTime.Text.Replace("24.00", "00:00").Replace(".", ":")
            'Dim OT_End As DateTime = If(tbOTEndTime.Text = "24.00", DateAdd(DateInterval.Day, 1, OT_Date) & " " & OT_TIME, OT_Date & " " & OT_TIME)
            Dim OT_BEGIN_TIME As String = tbOTBeginTime.Text.Replace("24.00", "00:00").Replace(".", ":")
            Dim OT_END_TIME As String = tbOTEndTime.Text.Replace("24.00", "00:00").Replace(".", ":")

            Dim OT_BEGIN As DateTime = If(tbOTBeginTime.Text = "24.00", DateAdd(DateInterval.Day, 1, OT_Date) & " " & OT_BEGIN_TIME, OT_Date & " " & OT_BEGIN_TIME)
            Dim OT_End As DateTime = If(tbOTEndTime.Text = "24.00", DateAdd(DateInterval.Day, 1, OT_Date) & " " & OT_END_TIME, OT_Date & " " & OT_END_TIME)
            '==========================

            Dim Work_Start As DateTime
            Dim Work_End As DateTime
            Dim Leave_Start As DateTime
            Dim Leave_End As DateTime

            Dim TimeBegin As String = String.Empty
            Dim TimeEnd As String = String.Empty

            Dim default_shift As DropDownList = gvShow.Rows(i).Cells(6).FindControl("ddlShiftDay")
            Dim lbHoliday As TextBox = gvShow.Rows(i).Cells(5).FindControl("lbHoliday")
            Dim WorkTime As String = getOTStartTime(lbHoliday.Text.Trim, default_shift.SelectedItem.Text.Trim, True)

            '===============================
            '[LEAVE TIME] กรณีที่ใส่เวลา 24.00 เปลี่ยน 00.00
            '===============================
            Dim ChkLunch As Boolean = True
            Dim LEAVE_TIME As String = LeaveEndTime.Replace("24.00", "00:00").Replace(".", ":")
            If LeaveCode <> "W" Then
                Leave_Start = DateTime.ParseExact(LeaveStartDate & " " & LeaveStartTime.Replace(".", ":"), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                Leave_End = DateTime.ParseExact(LeaveEndDate & " " & LEAVE_TIME.Replace(".", ":"), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)

                '===============================
                '[CHECK OT LUNCH]
                '===============================
                Dim lev_start_time As New TimeSpan(tbLeaveStartTime.Text.Substring(1, 2), tbLeaveStartTime.Text.Substring(3, 2), 0)
                Dim lev_end_time As New TimeSpan(tbLeaveEndTime.Text.Substring(0, 2), tbLeaveEndTime.Text.Substring(3, 2), 0)
                If ShiftDays = "Night" Then
                    Dim set_lev_start_time As New TimeSpan(23, 59, 0)
                    Dim set_lev_end_time As New TimeSpan(1, 1, 0)
                    If set_lev_start_time >= lev_start_time And set_lev_end_time <= lev_end_time Then
                        ChkLunch = False
                    End If
                Else
                    Dim set_lev_start_time As New TimeSpan(11, 59, 0)
                    Dim set_lev_end_time As New TimeSpan(13, 1, 0)
                    If set_lev_start_time >= lev_start_time And set_lev_end_time <= lev_end_time Then
                        ChkLunch = False
                    End If
                End If
            End If
            If cbLunch.Checked = True And ChkLunch = False Then
                show_message.ShowMessage(Page, "ไม่อนุญาติให้ติ๊กช่อง OT Lunch กรุณาตรวจสอบรหัส " & EmpNo & " " & EmpName & " อีกครั้ง", UpdatePanel1)
                Exit Sub
            End If

            '===============================
            TimeBegin = WorkTime.Split("-").ToArray(0)
            TimeEnd = WorkTime.Split("-").ToArray(1)

            Work_Start = If(ShiftDays = "Night", datetime & " " & TimeBegin.Replace(".", ":"), datetime & " " & TimeBegin.Replace(".", ":"))
            Work_End = If(ShiftDays = "Night", DateAdd(DateInterval.Day, 1, datetime) & " " & TimeEnd.Replace(".", ":"), datetime & " " & TimeEnd.Replace(".", ":"))

            If LeaveCode <> "W" And (Leave_Start < Work_Start Or Leave_Start > Work_End) Then
                show_message.ShowMessage(Page, "กรณีเลือก Leave Code ต้องระบุวันที่และเวลาของ Leave Start ให้ถูกต้อง  กรุณาตรวจสอบรหัส " & EmpNo & " " & EmpName & " อีกครั้ง", UpdatePanel1)
                Exit Sub
            End If

            If LeaveCode <> "W" And (Leave_End <= Work_Start Or Leave_End > Work_End) Then
                show_message.ShowMessage(Page, "กรณีเลือก Leave Code ต้องระบุวันที่และเวลาของ Leave End ให้ถูกต้อง  กรุณาตรวจสอบรหัส " & EmpNo & " " & EmpName & " อีกครั้ง", UpdatePanel1)
                Exit Sub
            End If

            Dim DiffDate As Decimal = Decimal.Zero
            Dim DiffLeave As Decimal = Decimal.Zero

            DiffLeave = DateDiff(DateInterval.Minute, Leave_Start, Leave_End)
            DiffDate = DateDiff(DateInterval.Minute, Work_Start, Work_End)

            If Leave_End > Work_End And LeaveCode <> "W" Then
                show_message.ShowMessage(Page, "รหัส " & EmpNo & " " & EmpName & " กรณีเวลา Leave End Time มากกว่าเวลาทำงานปกติ กรุณาระบุเวลาให้ถูกต้อง", UpdatePanel1)
                Exit Sub
            End If

            'ถ้าจำนวนชั่วโมงลางานเท่ากับจำนวนชั่วโมงที่ทำงาน ไม่อนุญาติให้ลงโอที
            If DiffLeave = DiffDate And OT_End > Work_End And LeaveCode <> "W" Then
                show_message.ShowMessage(Page, "รหัส " & EmpNo & " " & EmpName & " กรณีจำนวนชั่วโมงลางานเท่ากับจำนวนชั่วโมงที่ทำงาน ไม่อนุญาติให้ลงโอที", UpdatePanel1)
                Exit Sub
            End If

            'ถ้า Leave End เท่ากับ OT Begin ไม่อนุญาติให้ลงโอที
            If Leave_End = OT_BEGIN And OT_BEGIN <> OT_End And LeaveCode <> "W" Then
                show_message.ShowMessage(Page, "รหัส " & EmpNo & " " & EmpName & " ไม่อนุญาติให้ลงโอที", UpdatePanel1)
                Exit Sub
            End If

            If (ddlBusLine.SelectedItem.Text = "==select==" And cbPickup.Checked = True And DiffLeave = DiffDate) Or
                (ddlBusLine.SelectedItem.Text = "==select==" And cbPickup.Checked = False And DiffLeave = DiffDate) Or
                 (ddlBusLine.SelectedItem.Text <> "==select==" And cbPickup.Checked = True And DiffLeave = DiffDate) Or
                  (ddlBusLine.SelectedItem.Text <> "==select==" And cbPickup.Checked = False And DiffLeave = DiffDate) Then
            ElseIf ddlBusLine.SelectedItem.Text <> "==select==" And cbPickup.Checked = True And DiffLeave <> DiffDate Then
            Else
                show_message.ShowMessage(Page, "รหัส " & EmpNo & " " & EmpName & " กรุณาระบุ Pickup Location และเลือกช่อง Need Pickup ", UpdatePanel1)
                Exit Sub
            End If

            If DiffLeave <> DiffDate And LeaveCode <> "W" Then
                If cbPickup.Checked = True Then
                    If ddlBusLine.SelectedItem.Text = "==select==" Then
                        show_message.ShowMessage(Page, "รหัส " & EmpNo & " " & EmpName & " กรุณาระบุ Pickup Location ", UpdatePanel1)
                        Exit Sub
                    End If
                Else
                    If Leave_End <> OT_BEGIN Then
                        show_message.ShowMessage(Page, "รหัส " & EmpNo & " " & EmpName & " กรุณาระบุ Pickup Location ", UpdatePanel1)
                        Exit Sub
                    End If
                End If
            End If

            If DiffLeave <> DiffDate And cbPickup.Checked = True And LeaveCode <> "W" Then
                If ddlBusLine.SelectedItem.Text = "==select==" Then
                    show_message.ShowMessage(Page, "รหัส " & EmpNo & " กรุณาระบุ Pickup Location ", UpdatePanel1)
                    Exit Sub
                End If
            End If

            'If LeaveCode = "W" And cbPickup.Checked = False Then
            '    show_message.ShowMessage(Page, "กรณีไม่ได้ลางาน ต้องเลือกช่อง Need Pickup กรุณาตรวจสอบรหัส " & EmpNo & " อีกครั้ง", UpdatePanel1)
            '    Exit Sub
            'End If
        Next

        ''get doc number
        Dim Doc As String = String.Empty
        For i As Integer = 0 To gvShow.Rows.Count - 1

            Dim fld As Hashtable = New Hashtable
            Dim whr As Hashtable = New Hashtable
            Dim ShiftDays = gvShow.Rows(i).Cells(0).Text.Trim
            Dim EmpNo = gvShow.Rows(i).Cells(2).Text.Trim.ToString
            Dim tbWorkType As TextBox = gvShow.Rows(i).Cells(5).FindControl("lbHoliday")
            Dim ddlShiftDay As DropDownList = gvShow.Rows(i).Cells(6).FindControl("ddlShiftDay")
            Dim ddlLeaveCode As DropDownList = gvShow.Rows(i).Cells(7).FindControl("ddlleaveCode")

            Dim tbLeaveStartDate As TextBox = gvShow.Rows(i).Cells(9).FindControl("tbLeaveStartDate")
            Dim tbLeaveStartTime As TextBox = gvShow.Rows(i).Cells(9).FindControl("tbLeaveStartTime")

            Dim tbLeaveEndDate As TextBox = gvShow.Rows(i).Cells(10).FindControl("tbLeaveEndDate")
            Dim tbLeaveEndTime As TextBox = gvShow.Rows(i).Cells(10).FindControl("tbLeaveEndTime")

            Dim cbLunch As CheckBox = gvShow.Rows(i).Cells(11).FindControl("cbOTLunch")
            Dim cbPickup As CheckBox = gvShow.Rows(i).Cells(14).FindControl("cbBusLine")
            Dim tbOTEndDate As TextBox = gvShow.Rows(i).Cells(12).FindControl("tbOTEndDate")
            Dim tbOTEndTime As TextBox = gvShow.Rows(i).Cells(12).FindControl("tbOTEndTime")
            Dim ddlBusLine As DropDownList = gvShow.Rows(i).Cells(13).FindControl("ddlBusLine")
            Dim cbHoliday As CheckBox = gvShow.Rows(i).Cells(14).FindControl("cbHoliday")

            Dim LeaveStartDate As String = tbLeaveStartDate.Text.Replace("__/__/____", "").Trim
            Dim LeaveStartTime As String = tbLeaveStartTime.Text.Replace("__.__", "").Trim

            Dim LeaveEndDate As String = tbLeaveEndDate.Text.Replace("__/__/____", "").Trim
            Dim LeaveEndTime As String = tbLeaveEndTime.Text.Replace("__.__", "").Trim

            LeaveCode = ddlLeaveCode.SelectedValue.Split("-").ToArray(0)

            Dim BusLine As String = ddlBusLine.SelectedItem.Text.Trim.ToString
            Dim Work_Start As DateTime
            Dim Work_End As DateTime

            Dim Leave_Start_D As DateTime
            Dim Leave_End_D As DateTime

            Dim Diff_Lve As Decimal = Decimal.Zero
            Dim Diff_Wk As Decimal = Decimal.Zero
            Dim Diff_OT As Decimal = Decimal.Zero

            Dim default_shift As DropDownList = gvShow.Rows(i).Cells(6).FindControl("ddlShiftDay")
            Dim lbHoliday As TextBox = gvShow.Rows(i).Cells(5).FindControl("lbHoliday")
            Dim NORMAL_HOUR As String = getOTStartTime(lbHoliday.Text.Trim, default_shift.SelectedItem.Text.Trim, True)

            Dim TimeBegin As String = NORMAL_HOUR.Split("-").ToArray(0)
            Dim TimeEnd As String = NORMAL_HOUR.Split("-").ToArray(1)

            '======================================
            '[Overtime] กรณีที่ใส่เวลา 24.00 ให้เป็นวันถัดไป
            '======================================
            Dim OT_Date As DateTime = datetime.ParseExact(dateCont.dateFormat2(tbOTEndDate.Text), "yyyyMMdd", CultureInfo.InvariantCulture)
            Dim OT_TIME As String = tbOTEndTime.Text.Replace("24.00", "00:00").Replace(".", ":")
            'Dim OT_End As DateTime = If(tbOTEndTime.Text = "24.00", DateAdd(DateInterval.Day, 1, OT_Date) & " " & OT_TIME, OT_Date & " " & OT_TIME)
            Dim OT_End As DateTime = OT_Date & " " & OT_TIME
            '======================================

            '======================================
            '[Working] Calculate woking days in between two different dates
            '======================================
            Work_Start = If(ShiftDays = "Night", datetime & " " & TimeBegin.Replace(".", ":"), datetime & " " & TimeBegin.Replace(".", ":"))
            Work_End = If(ShiftDays = "Night", DateAdd(DateInterval.Day, 1, datetime) & " " & TimeEnd.Replace(".", ":"), datetime & " " & TimeEnd.Replace(".", ":"))
            Diff_Wk = DateDiff(DateInterval.Second, Work_Start, Work_End)

            '======================================
            '[Leave] Calculate Time Difference 
            '[LEAVE TIME] กรณีที่ใส่เวลา 24.00 เปลี่ยน 00.00
            '======================================
            Dim LEAVE_END_TIME As String = LeaveEndTime.Replace("24.00", "00:00").Replace(".", ":")
            If LeaveCode <> "W" Then
                Leave_Start_D = DateTime.ParseExact(LeaveStartDate & " " & LeaveStartTime.Replace(".", ":"), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                Leave_End_D = DateTime.ParseExact(LeaveEndDate & " " & LEAVE_END_TIME.Replace(".", ":").ToString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                Diff_Lve = DateDiff(DateInterval.Second, Leave_Start_D, Leave_End_D)
            End If
            '======================================

            Dim Sql As String = " SELECT EMPNO FROM " & OT_Record & " WHERE EMPNO = '" & EmpNo.Trim & "' AND WORK_DATE = '" & getDate.Trim & "'"
            Dim dt As DataTable = dbConn.Query(Sql, VarIni.DBMIS, dbConn.WhoCalledMe)
            Dim TypeRecord As String = String.Empty
            If dt.Rows.Count = 0 Then

                If lbHoliday.Text.Trim <> "Work Day" And cbHoliday.Checked = False Then
                Else
                    TypeRecord = "I"
                    If Doc = String.Empty Then
                        Doc = getDocNo()
                    Else
                        Doc = CDec(Doc) + 1
                    End If
                    fld.Add("DOCNO", Doc.Trim)
                    fld.Add("EMPNO", gvShow.Rows(i).Cells(2).Text.Trim)
                    fld.Add("CREATEBY", Session("UserName").Trim)
                    fld.Add("CREATEDATE", Date.Now.ToString("yyyyMMdd HH:mm").Trim)
                End If
            Else
                If lbHoliday.Text.Trim <> "Work Day" And cbHoliday.Checked = False Then
                    Dim Sql_Del As String = " DELETE  FROM " & OT_Record & " WHERE EMPNO = '" & EmpNo.Trim & "' AND WORK_DATE = '" & getDate.Trim & "'"
                    dbConn.TransactionSQL(Sql_Del, VarIni.DBMIS, dbConn.WhoCalledMe)
                    Exit For
                Else
                    TypeRecord = "U"
                    whr.Add("EMPNO", gvShow.Rows(i).Cells(2).Text.Trim)
                    whr.Add("WORK_DATE", getDate.Trim)
                    fld.Add("CHANGEBY", Session("UserName").Trim)
                    fld.Add("CHANGEDATE", Date.Now.ToString("yyyyMMdd HH:mm").Trim)
                End If

            End If
            fld.Add("SHIFT_DAY", ddlShiftDay.SelectedItem.Text.Trim)
            fld.Add("DEPT", gvShow.Rows(i).Cells(1).Text.Trim.Split(":").ToArray(0).Trim)
            'fld.Add("LINE", gvShow.Rows(i).Cells(2).Text.Trim.Replace("&nbsp;", "").Replace("&amp;", "&"))
            fld.Add("WORK_DATE", getDate.Trim)
            Dim NORMAL_TIME As Decimal = Decimal.Zero
            Dim LEAVE_TIME As Decimal = Decimal.Zero
            Dim MID_DAY As DateTime = datetime & " " & "12:00"

            '===============================
            '[LEAVE TIME] กรณีที่ใส่เวลา 24.00 เปลี่ยน 00.00
            '===============================
            Dim ChkLeave As Boolean = True
            Dim Leave_Start As DateTime
            Dim Leave_End As DateTime
            Dim LEAVE_TM As String = LeaveEndTime.Replace("24.00", "00:00").Replace(".", ":")
            If LeaveCode <> "W" Then
                Leave_Start = DateTime.ParseExact(LeaveStartDate & " " & LeaveStartTime.Replace(".", ":"), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                Leave_End = DateTime.ParseExact(LeaveEndDate & " " & LEAVE_TM.Replace(".", ":"), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                '===============================
                '[CHECK OT LUNCH]
                '===============================
                Dim lev_start_time As New TimeSpan(tbLeaveStartTime.Text.Substring(1, 2), tbLeaveStartTime.Text.Substring(3, 2), 0)
                Dim lev_end_time As New TimeSpan(tbLeaveEndTime.Text.Substring(0, 2), tbLeaveEndTime.Text.Substring(3, 2), 0)
                If ShiftDays = "Night" Then
                    Dim set_lev_start_time As New TimeSpan(23, 59, 0)
                    Dim set_lev_end_time As New TimeSpan(1, 1, 0)
                    If set_lev_start_time >= lev_start_time And set_lev_end_time <= lev_end_time Then
                        ChkLeave = False
                    End If
                Else
                    Dim set_lev_start_time As New TimeSpan(11, 59, 0)
                    Dim set_lev_end_time As New TimeSpan(13, 1, 0)
                    If set_lev_start_time >= lev_start_time And set_lev_end_time <= lev_end_time Then
                        ChkLeave = False
                    End If
                End If
            End If

            '==========================
            '[NORMAL TIME]
            '==========================
            If Diff_Lve = Diff_Wk Then
                fld.Add("NORMAL_TIME", "0")
                LEAVE_TIME = Diff_Lve - 3600
            Else
                If ChkLeave = False Then '07:00 < 12:00
                    NORMAL_TIME = ((Diff_Wk - 3600) - (Diff_Lve - 3600))
                    LEAVE_TIME = Diff_Lve - 3600
                Else
                    NORMAL_TIME = ((Diff_Wk - 3600) - Diff_Lve)
                    LEAVE_TIME = Diff_Lve
                End If
                fld.Add("NORMAL_TIME", NORMAL_TIME)
            End If

            '==========================
            'LEAVE_CODE
            'LEAVE_STARTED_DATE
            'LEAVE_FINISHED_DATE
            'LEAVE_STARTED_TIME
            'LEAVE_FINISHED_TIME
            'LEAVE_TIME
            'WORK_BEGIN_TIME'
            'WORK_BEGIN_DATE
            '==========================
            Dim Work_Begin As DateTime
            If LeaveCode = "W" Then
                fld.Add("LEAVE_CODE", "")
                fld.Add("LEAVE_STARTED_DATE", "")
                fld.Add("LEAVE_FINISHED_DATE", "")
                fld.Add("LEAVE_STARTED_TIME", "")
                fld.Add("LEAVE_FINISHED_TIME", "")
                fld.Add("LEAVE_TIME", "0")
                Work_Begin = Work_Start
                fld.Add("WORK_BEGIN_TIME", Work_Begin.ToString("HH:mm").Trim)
                fld.Add("WORK_BEGIN_DATE", Work_Begin.ToString("yyyyMMdd").Trim)
            Else
                fld.Add("LEAVE_CODE", LeaveCode)
                fld.Add("LEAVE_STARTED_DATE", dateCont.dateFormat2(LeaveStartDate).Trim)
                fld.Add("LEAVE_FINISHED_DATE", dateCont.dateFormat2(LeaveEndDate).Trim)
                fld.Add("LEAVE_STARTED_TIME", LeaveStartTime.Replace(".", ":").Trim)
                fld.Add("LEAVE_FINISHED_TIME", LeaveEndTime.Replace(".", ":").Trim)
                fld.Add("LEAVE_TIME", LEAVE_TIME)
                Work_Begin = If(Leave_Start_D = Work_Start, Leave_Start_D, Work_Start)
                fld.Add("WORK_BEGIN_TIME", Work_Begin.ToString("HH:mm").Trim)
                fld.Add("WORK_BEGIN_DATE", Work_Begin.ToString("yyyyMMdd").Trim)
            End If
            '==========================
            'LUNCH_TIME
            'PICKUP_LOCATION
            '==========================
            Dim OT_Lunch As Decimal = If(cbLunch.Checked = True, "3600", "0")
            If Diff_Wk = Diff_Lve Then
                fld.Add("PICKUP_LOCATION", "")
                fld.Add("LUNCH_TIME", "0")
            Else
                fld.Add("PICKUP_LOCATION:N", If(cbPickup.Checked = True, BusLine.Trim, ""))
                fld.Add("LUNCH_TIME", OT_Lunch)
            End If
            Diff_OT = DateDiff(DateInterval.Second, Work_End, OT_End)
            fld.Add("WORK_TYPE", tbWorkType.Text.Trim)
            '==========================
            'OT_STARTED_DATE
            'OT_FINISHED_DATE
            'OT_STARTED_TIME
            'OT_FINISHED_TIME
            'OVER_TIME
            '==========================
            If Diff_OT > 0 And tbWorkType.Text.Trim = "Work Day" Then
                Dim Started_Time As String = Work_End.ToString("HH:mm")
                Dim Finished_Time As String = OT_End.ToString("HH:mm")
                fld.Add("OT_STARTED_DATE", Work_End.ToString("yyyyMMdd").Trim)
                fld.Add("OT_FINISHED_DATE", OT_End.ToString("yyyyMMdd").Trim)
                fld.Add("OT_STARTED_TIME", Started_Time.Trim)
                fld.Add("OT_FINISHED_TIME", Finished_Time.Trim)
                fld.Add("OVER_TIME", Diff_OT)
            ElseIf tbWorkType.Text.Trim = "Holiday" Then
                Dim Started_Time As String = Work_Start.ToString("HH:mm")
                Dim Finished_Time As String = OT_End.ToString("HH:mm")
                'fld.Add("OT_STARTED_DATE", Work_End.ToString("yyyyMMdd").Trim)
                fld.Add("OT_STARTED_DATE", Work_Start.ToString("yyyyMMdd").Trim)

                fld.Add("OT_FINISHED_DATE", OT_End.ToString("yyyyMMdd").Trim)
                fld.Add("OT_STARTED_TIME", Started_Time.Trim)
                fld.Add("OT_FINISHED_TIME", Finished_Time.Trim)
                fld.Add("OVER_TIME", Diff_OT)
            Else
                fld.Add("OT_STARTED_DATE", "")
                fld.Add("OT_FINISHED_DATE", "")
                fld.Add("OT_STARTED_TIME", "")
                fld.Add("OT_FINISHED_TIME", "")
                fld.Add("OVER_TIME", "0")
            End If
            sqlHash_gvShow.Add(sqlCont.GetSQL(OT_Record, fld, whr, TypeRecord))
        Next

        If sqlHash_gvShow.Count > 0 Then
            dbConn.TransactionSQL(sqlHash_gvShow, VarIni.DBMIS, dbConn.WhoCalledMe)
            show_message.ShowMessage(Page, "บันทึกสำรเร็จ ", UpdatePanel1)
        End If

        gvShow.Visible = False
        gvReport.Visible = True
        gvEdit.Visible = False
        btSave.Visible = False
        btCancel.Visible = False
        btEdit.Visible = False
        btUpdate.Visible = False
        btRecord.Visible = True
        btShowRe.Visible = True
        ShowReport()
    End Sub

    Protected Sub btEdit_Click(sender As Object, e As EventArgs) Handles btEdit.Click
        Dim dc As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dc, "Dept")

        Dim SQL As String = String.Empty
        Dim fldName As New ArrayList
        Dim Whr As String = String.Empty

        Whr &= whrCont.Where("O.DEPT", cblDept, False, deptcode.Replace(",", "','"), True) 'DEPT
        Whr &= whrCont.Where("O.EMPNO", tbEmpNo) 'EMPCODE
        Whr &= whrCont.Where("O.WORK_DATE", ucDate.text,, False) 'DATE
        Whr &= whrCont.Where(" E.EMP_NAME", " is not null")
        If rdlShift.SelectedValue = "D" Then
            Whr &= whrCont.Where(" O.SHIFT_DAY ", "LIKE '%Day%'")
        ElseIf rdlShift.SelectedValue = "N" Then
            Whr &= whrCont.Where(" O.SHIFT_DAY", " NOT LIKE '%Day%'")
        End If

        fldName.Add("'" & rdlShift.SelectedItem.Text.Trim & "'" & VarIni.char8 & shift)
        fldName.Add("O.DEPT" & VarIni.char8 & dept)
        fldName.Add("O.EMPNO" & VarIni.char8 & empcode)
        fldName.Add("E.EMP_NAME" & VarIni.char8 & name)
        fldName.Add("RTRIM(substring(O.WORK_DATE,7,4))+'/'+substring(O.WORK_DATE,5,2)+'/'+substring(O.WORK_DATE,1,4)" & VarIni.char8 & workdate)
        fldName.Add("O.WORK_TYPE" & VarIni.char8 & work_type)
        fldName.Add("O.SHIFT_DAY" & VarIni.char8 & shiftday)
        fldName.Add("A.WORK_END_TIME" & VarIni.char8 & work_begin_tm)
        fldName.Add("A.WORK_BEGIN_TIME" & VarIni.char8 & work_end_tm)
        fldName.Add("CASE O.LEAVE_CODE WHEN '' THEN '' ELSE  RTRIM(C.Code)+'-'+RTRIM(C.Name) END" & VarIni.char8 & leave_code)
        'LEAVE
        fldName.Add("CASE O.LEAVE_CODE WHEN '' THEN '' ELSE O.LEAVE_STARTED_DATE END" & VarIni.char8 & leave_start_date)
        fldName.Add("O.LEAVE_STARTED_TIME " & VarIni.char8 & leave_start_time)
        fldName.Add("CASE O.LEAVE_CODE WHEN '' THEN '' ELSE O.LEAVE_FINISHED_DATE END" & VarIni.char8 & leave_end_date)
        fldName.Add("O.LEAVE_FINISHED_TIME" & VarIni.char8 & leave_end_time)
        fldName.Add("CAST(O.LEAVE_TIME/3600 AS DECIMAL(16,1))" & VarIni.char8 & leave_hours)
        'OT
        fldName.Add("CASE O.OT_STARTED_DATE WHEN '' THEN '' ELSE O.OT_STARTED_DATE END" & VarIni.char8 & ot_start_date)
        fldName.Add("O.OT_STARTED_TIME" & VarIni.char8 & ot_start_time)
        fldName.Add("CASE O.OT_FINISHED_DATE WHEN '' THEN '' ELSE O.OT_FINISHED_DATE END" & VarIni.char8 & ot_end_date)
        fldName.Add("O.OT_FINISHED_TIME" & VarIni.char8 & ot_end_time)

        fldName.Add("CAST(O.OVER_TIME/3600 AS DECIMAL(16,1))" & VarIni.char8 & ot_hours)
        fldName.Add("CAST(O.LUNCH_TIME/3600 AS DECIMAL(16,1))" & VarIni.char8 & lunch_hours)
        fldName.Add("CAST((O.OVER_TIME +O.LUNCH_TIME )/3600 AS DECIMAL(16,1))" & VarIni.char8 & ot_total)
        fldName.Add("O.PICKUP_LOCATION" & VarIni.char8 & pickup)
        fldName.Add("O.CREATEBY" & VarIni.char8 & create_by)
        fldName.Add("O.CREATEDATE" & VarIni.char8 & create_date)
        fldName.Add("O.CHANGEBY" & VarIni.char8 & change_by)
        fldName.Add("O.CHANGEDATE" & VarIni.char8 & change_date)

        SQL &= VarIni.S & sqlCont.getFeild(fldName, VarIni.char8) & VarIni.F & " HR_EMPLOYEE_HY E"
        SQL &= VarIni.L & " OVER_TIME_RECORD O " & VarIni.O2 & " CODE=EMPNO  " & COLL
        SQL &= VarIni.L & " V_HR_DEPT D " & VarIni.O2 & "D.Code  =DEPT_CODE " & COLL
        SQL &= VarIni.L & " CodeInfo C " & VarIni.O2 & " C.Code =LEAVE_CODE and CodeType = 'LeaveCode' " & COLL
        SQL &= VarIni.L & HR_ATTENDANCE_RANK.TABLENAME & " A" & VarIni.O2 & " O.SHIFT_DAY =A.CODE_SHIFT " & COLL
        SQL &= VarIni.W & " 1=1 " & Whr & VarIni.O & " O.EMPNO ASC"

        Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        gvCont.ShowGridView(gvEdit, dt)
        gvReport.Visible = False
        gvShow.Visible = False
        gvEdit.Visible = True

        btRecord.Visible = False
        btEdit.Visible = True
        btSave.Visible = False
        btUpdate.Visible = True
        btCancel.Visible = True
        btShowRe.Visible = False
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollEdit", "gridviewScrollEdit();", True)
    End Sub

    Protected Sub btShowRe_Click(sender As Object, e As EventArgs) Handles btShowRe.Click

        If Session("userid") = "" Then
            Response.Redirect(VarIni.PageLogin)
        End If

        gvShow.Visible = False
        gvEdit.Visible = False
        gvReport.Visible = True

        btRecord.Visible = True
        btEdit.Visible = True
        btSave.Visible = False
        btUpdate.Visible = False
        btCancel.Visible = True
        btShowRe.Visible = True
        btPrint.Visible = True
        btExcel.Visible = True
        ShowReport()

        Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)

        Dim chk As Boolean = False
        lbGrp.Text = String.Empty

        If dr Is Nothing Then
            Response.Redirect(Server.UrlPathEncode("../Home.aspx"))
        Else
            deptcode = dtCont.IsDBNullDataRow(dr, "dept")
            lbGrp.Text = dtCont.IsDBNullDataRow(dr, "grp")
            chk = If(lbGrp.Text = "dept_head", True, False)
        End If
        EditButtonSet(chk)

    End Sub

    Protected Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click

        If Session("userid") = "" Then
            Response.Redirect(VarIni.PageLogin)
        End If

        Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dr, "Dept")

        Dim setDate = Server.HtmlEncode(ucDate.text)
        Dim datetime As DateTime = datetime.ParseExact(setDate, "yyyyMMdd", CultureInfo.InvariantCulture)
        Dim getDate As String = datetime.ToString("yyyyMMdd")
        Dim sqlHash_gvShow As New ArrayList
        Dim LeaveStart As String = String.Empty
        Dim LeaveEnd As String = String.Empty
        Dim LeaveCode As String = String.Empty
        Dim OTType As String = String.Empty
        Dim numdate As Decimal = Decimal.Zero
        Dim LeaveTime As Decimal = Decimal.Zero

        For i As Integer = 0 To gvEdit.Rows.Count - 1
            Dim EmpName = gvEdit.Rows(i).Cells(3).Text.Trim.ToString
            Dim EmpNo = gvEdit.Rows(i).Cells(2).Text.Trim.ToString
            Dim ShiftDays = gvEdit.Rows(i).Cells(0).Text.Trim
            Dim ddlLeaveCode As DropDownList = gvEdit.Rows(i).Cells(7).FindControl("ddlleaveCode")
            Dim tbLeaveStartDate As TextBox = gvEdit.Rows(i).Cells(9).FindControl("tbLeaveStartDate")
            Dim tbLeaveStartTime As TextBox = gvEdit.Rows(i).Cells(9).FindControl("tbLeaveStartTime")
            Dim tbLeaveEndDate As TextBox = gvEdit.Rows(i).Cells(10).FindControl("tbLeaveEndDate")
            Dim tbLeaveEndTime As TextBox = gvEdit.Rows(i).Cells(10).FindControl("tbLeaveEndTime")
            Dim ddlShiftDay As DropDownList = gvEdit.Rows(i).Cells(6).FindControl("ddlShiftDay")
            Dim cbLunch As CheckBox = gvEdit.Rows(i).Cells(11).FindControl("cbOTLunch")
            Dim tbOTBeginTime As TextBox = gvEdit.Rows(i).Cells(11).FindControl("tbOTStartTime")
            Dim tbOTEndDate As TextBox = gvEdit.Rows(i).Cells(12).FindControl("tbOTEndDate")
            Dim tbOTEndTime As TextBox = gvEdit.Rows(i).Cells(12).FindControl("tbOTEndTime")
            Dim ddlBusLine As DropDownList = gvEdit.Rows(i).Cells(13).FindControl("ddlBusLine")
            Dim cbPickup As CheckBox = gvEdit.Rows(i).Cells(14).FindControl("cbBusLine")
            Dim LeaveStartDate As String = tbLeaveStartDate.Text.Replace("__/__/____", "").Trim
            Dim LeaveStartTime As String = tbLeaveStartTime.Text.Replace("__.__", "").Trim
            Dim LeaveEndDate As String = tbLeaveEndDate.Text.Replace("__/__/____", "").Trim
            Dim LeaveEndTime As String = tbLeaveEndTime.Text.Replace("__.__", "").Trim

            Dim LveChk As Boolean = False
            LeaveCode = ddlLeaveCode.SelectedValue.Split("-").ToArray(0)
            If LeaveCode <> "W" Then
                LveChk = If(LeaveStartDate = "", True, False)
                LveChk = If(LeaveStartDate = "", True, False)
                LveChk = If(LeaveEndDate = "", True, False)
                LveChk = If(LeaveEndTime = "", True, False)
            End If

            If LveChk = True Then
                show_message.ShowMessage(Page, "กรณีเลือก LeaveCode กรุณาระบุวันที่่และเวลาให้ถูกต้อง กรุณาตรวจสอบรหัส " & EmpNo & " " & EmpName & " อีกครั้ง", UpdatePanel1)
                Exit Sub
            End If

            '==========================
            'OT กรณีที่ใส่เวลา 24.00 ให้เป็นวันถัดไป
            '==========================
            Dim OT_Date As DateTime = datetime.ParseExact(dateCont.dateFormat2(tbOTEndDate.Text), "yyyyMMdd", CultureInfo.InvariantCulture)
            'Dim OT_TIME As String = tbOTEndTime.Text.Replace("24.00", "00:00").Replace(".", ":")
            'Dim OT_End As DateTime = If(tbOTEndTime.Text = "24.00", DateAdd(DateInterval.Day, 1, OT_Date) & " " & OT_TIME, OT_Date & " " & OT_TIME)
            Dim OT_BEGIN_TIME As String = tbOTBeginTime.Text.Replace("24.00", "00:00").Replace(".", ":")
            Dim OT_END_TIME As String = tbOTEndTime.Text.Replace("24.00", "00:00").Replace(".", ":")
            Dim OT_BEGIN As DateTime = If(tbOTBeginTime.Text = "24.00", DateAdd(DateInterval.Day, 1, OT_Date) & " " & OT_BEGIN_TIME, OT_Date & " " & OT_BEGIN_TIME)
            Dim OT_End As DateTime = If(tbOTEndTime.Text = "24.00", DateAdd(DateInterval.Day, 1, OT_Date) & " " & OT_END_TIME, OT_Date & " " & OT_END_TIME)
            '==========================

            Dim Work_Start As DateTime
            Dim Work_End As DateTime
            Dim Leave_Start As DateTime
            Dim Leave_End As DateTime

            Dim TimeBegin As String = String.Empty
            Dim TimeEnd As String = String.Empty
            'Dim WorkTime As String = getCalShift(EmpNo, True)
            Dim default_shift As DropDownList = gvEdit.Rows(i).Cells(6).FindControl("ddlShiftDay")
            Dim lbHoliday As TextBox = gvEdit.Rows(i).Cells(5).FindControl("lbHoliday")
            Dim WorkTime As String = getOTStartTime(lbHoliday.Text.Trim, default_shift.SelectedItem.Text.Trim, True)
            '===============================
            '[LEAVE TIME] กรณีที่ใส่เวลา 24.00 เปลี่ยน 00.00
            '===============================
            Dim ChkLunch As Boolean = True
            Dim LEAVE_TIME As String = LeaveEndTime.Replace("24.00", "00:00").Replace(".", ":")
            If LeaveCode <> "W" Then
                Leave_Start = DateTime.ParseExact(LeaveStartDate & " " & LeaveStartTime.Replace(".", ":"), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                Leave_End = DateTime.ParseExact(LeaveEndDate & " " & LEAVE_TIME.Replace(".", ":"), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)

                '===============================
                '[CHECK OT LUNCH]
                '===============================
                Dim lev_start_time As New TimeSpan(tbLeaveStartTime.Text.Substring(1, 2), tbLeaveStartTime.Text.Substring(3, 2), 0)
                Dim lev_end_time As New TimeSpan(tbLeaveEndTime.Text.Substring(0, 2), tbLeaveEndTime.Text.Substring(3, 2), 0)
                If ShiftDays = "Night" Then
                    Dim set_lev_start_time As New TimeSpan(23, 59, 0)
                    Dim set_lev_end_time As New TimeSpan(1, 1, 0)
                    If set_lev_start_time >= lev_start_time And set_lev_end_time <= lev_end_time Then
                        ChkLunch = False
                    End If
                Else
                    Dim set_lev_start_time As New TimeSpan(11, 59, 0)
                    Dim set_lev_end_time As New TimeSpan(13, 1, 0)
                    If set_lev_start_time >= lev_start_time And set_lev_end_time <= lev_end_time Then
                        ChkLunch = False
                    End If
                End If
            End If
            If cbLunch.Checked = True And ChkLunch = False Then
                show_message.ShowMessage(Page, "ไม่อนุญาติให้ติ๊กช่อง OT Lunch กรุณาตรวจสอบรหัส " & EmpNo & " " & EmpName & " อีกครั้ง", UpdatePanel1)
                Exit Sub
            End If

            '===============================
            TimeBegin = WorkTime.Split("-").ToArray(0)
            TimeEnd = WorkTime.Split("-").ToArray(1)

            Work_Start = If(ShiftDays = "Night", datetime & " " & TimeBegin.Replace(".", ":"), datetime & " " & TimeBegin.Replace(".", ":"))
            Work_End = If(ShiftDays = "Night", DateAdd(DateInterval.Day, 1, datetime) & " " & TimeEnd.Replace(".", ":"), datetime & " " & TimeEnd.Replace(".", ":"))

            If LeaveCode <> "W" And (Leave_Start < Work_Start Or Leave_Start > Work_End) Then
                show_message.ShowMessage(Page, "กรณีเลือก Leave Code ต้องระบุวันที่และเวลาของ Leave Start ให้ถูกต้อง  กรุณาตรวจสอบรหัส " & EmpNo & " " & EmpName & " อีกครั้ง", UpdatePanel1)
                Exit Sub
            End If

            If LeaveCode <> "W" And (Leave_End <= Work_Start Or Leave_End > Work_End) Then
                show_message.ShowMessage(Page, "กรณีเลือก Leave Code ต้องระบุวันที่และเวลาของ Leave End ให้ถูกต้อง  กรุณาตรวจสอบรหัส " & EmpNo & " " & EmpName & " อีกครั้ง", UpdatePanel1)
                Exit Sub
            End If

            Dim DiffDate As Decimal = Decimal.Zero
            Dim DiffLeave As Decimal = Decimal.Zero

            DiffLeave = DateDiff(DateInterval.Minute, Leave_Start, Leave_End)
            DiffDate = DateDiff(DateInterval.Minute, Work_Start, Work_End)

            If Leave_End > Work_End And LeaveCode <> "W" Then
                show_message.ShowMessage(Page, "รหัส " & EmpNo & " " & EmpName & " กรณีเวลา Leave End Time มากกว่าเวลาทำงานปกติ กรุณาระบุเวลาให้ถูกต้อง", UpdatePanel1)
                Exit Sub
            End If

            'ถ้าจำนวนชั่วโมงลางานเท่ากับจำนวนชั่วโมงที่ทำงาน ไม่อนุญาติให้ลงโอที
            If DiffLeave = DiffDate And OT_End > Work_End And LeaveCode <> "W" Then
                show_message.ShowMessage(Page, "รหัส " & EmpNo & " " & EmpName & " กรณีจำนวนชั่วโมงลางานเท่ากับจำนวนชั่วโมงที่ทำงาน ไม่อนุญาติให้ลงโอที", UpdatePanel1)
                Exit Sub
            End If

            'ถ้า Leave End เท่ากับ OT Begin ไม่อนุญาติให้ลงโอที
            If Leave_End = OT_BEGIN And OT_BEGIN <> OT_End And LeaveCode <> "W" Then
                show_message.ShowMessage(Page, "รหัส " & EmpNo & " " & EmpName & " ไม่อนุญาติให้ลงโอที", UpdatePanel1)
                Exit Sub
            End If

            If (ddlBusLine.SelectedItem.Text = "==select==" And cbPickup.Checked = True And DiffLeave = DiffDate) Or
                (ddlBusLine.SelectedItem.Text = "==select==" And cbPickup.Checked = False And DiffLeave = DiffDate) Or
                 (ddlBusLine.SelectedItem.Text <> "==select==" And cbPickup.Checked = True And DiffLeave = DiffDate) Or
                  (ddlBusLine.SelectedItem.Text <> "==select==" And cbPickup.Checked = False And DiffLeave = DiffDate) Then
            ElseIf ddlBusLine.SelectedItem.Text <> "==select==" And cbPickup.Checked = True And DiffLeave <> DiffDate Then
            Else
                show_message.ShowMessage(Page, "รหัส " & EmpNo & " " & EmpName & " กรุณาระบุ Pickup Location และเลือกช่อง Need Pickup ", UpdatePanel1)
                Exit Sub
            End If

            If DiffLeave <> DiffDate And LeaveCode <> "W" Then
                If cbPickup.Checked = True Then
                    If ddlBusLine.SelectedItem.Text = "==select==" Then
                        show_message.ShowMessage(Page, "รหัส " & EmpNo & " " & EmpName & " กรุณาระบุ Pickup Location ", UpdatePanel1)
                        Exit Sub
                    End If
                Else
                    If Leave_End <> OT_BEGIN Then
                        show_message.ShowMessage(Page, "รหัส " & EmpNo & " " & EmpName & " กรุณาระบุ Pickup Location ", UpdatePanel1)
                        Exit Sub
                    End If
                End If
            End If

            If LeaveCode = "W" And cbPickup.Checked = False Then
                show_message.ShowMessage(Page, "กรณีไม่ได้ลางาน ต้องเลือกช่อง Need Pickup กรุณาตรวจสอบรหัส " & EmpNo & " " & EmpName & " อีกครั้ง", UpdatePanel1)
                Exit Sub
            End If
            If LeaveCode = "W" And ddlBusLine.SelectedItem.Text = "==select==" Then
                show_message.ShowMessage(Page, "กรณีไม่ได้ลางาน ต้องเลือกระชุสายรถ กรุณาตรวจสอบรหัส " & EmpNo & " " & EmpName & " อีกครั้ง", UpdatePanel1)
                Exit Sub
            End If
        Next

        ''get doc number
        Dim Doc As String = String.Empty
        For i As Integer = 0 To gvEdit.Rows.Count - 1

            Dim fld As Hashtable = New Hashtable
            Dim whr As Hashtable = New Hashtable
            Dim ShiftDays = gvEdit.Rows(i).Cells(0).Text.Trim
            Dim EmpNo = gvEdit.Rows(i).Cells(2).Text.Trim.ToString
            Dim tbWorkType As TextBox = gvEdit.Rows(i).Cells(5).FindControl("lbHoliday")
            Dim ddlShiftDay As DropDownList = gvEdit.Rows(i).Cells(6).FindControl("ddlShiftDay")
            Dim ddlLeaveCode As DropDownList = gvEdit.Rows(i).Cells(7).FindControl("ddlleaveCode")

            Dim tbLeaveStartDate As TextBox = gvEdit.Rows(i).Cells(9).FindControl("tbLeaveStartDate")
            Dim tbLeaveStartTime As TextBox = gvEdit.Rows(i).Cells(9).FindControl("tbLeaveStartTime")

            Dim tbLeaveEndDate As TextBox = gvEdit.Rows(i).Cells(10).FindControl("tbLeaveEndDate")
            Dim tbLeaveEndTime As TextBox = gvEdit.Rows(i).Cells(10).FindControl("tbLeaveEndTime")

            Dim cbLunch As CheckBox = gvEdit.Rows(i).Cells(11).FindControl("cbOTLunch")
            Dim cbPickup As CheckBox = gvEdit.Rows(i).Cells(14).FindControl("cbBusLine")
            Dim tbOTEndDate As TextBox = gvEdit.Rows(i).Cells(12).FindControl("tbOTEndDate")
            Dim tbOTEndTime As TextBox = gvEdit.Rows(i).Cells(12).FindControl("tbOTEndTime")
            Dim ddlBusLine As DropDownList = gvEdit.Rows(i).Cells(13).FindControl("ddlBusLine")

            Dim LeaveStartDate As String = tbLeaveStartDate.Text.Replace("__/__/____", "").Trim
            Dim LeaveStartTime As String = tbLeaveStartTime.Text.Replace("__.__", "").Trim

            Dim LeaveEndDate As String = tbLeaveEndDate.Text.Replace("__/__/____", "").Trim
            Dim LeaveEndTime As String = tbLeaveEndTime.Text.Replace("__.__", "").Trim
            Dim cbHoliday As CheckBox = gvEdit.Rows(i).Cells(14).FindControl("cbHoliday")
            LeaveCode = ddlLeaveCode.SelectedValue.Split("-").ToArray(0)

            Dim BusLine As String = ddlBusLine.SelectedItem.Text.Trim.ToString
            Dim Work_Start As DateTime
            Dim Work_End As DateTime

            Dim Leave_Start_D As DateTime
            Dim Leave_End_D As DateTime

            Dim Diff_Lve As Decimal = Decimal.Zero
            Dim Diff_Wk As Decimal = Decimal.Zero
            Dim Diff_OT As Decimal = Decimal.Zero
            'Dim NORMAL_HOUR As String = getCalShift(gvEdit.Rows(i).Cells(2).Text.Trim, True)
            Dim default_shift As DropDownList = gvEdit.Rows(i).Cells(6).FindControl("ddlShiftDay")
            Dim lbHoliday As TextBox = gvEdit.Rows(i).Cells(5).FindControl("lbHoliday")
            Dim NORMAL_HOUR As String = getOTStartTime(lbHoliday.Text.Trim, default_shift.SelectedItem.Text.Trim, True)

            Dim TimeBegin As String = NORMAL_HOUR.Split("-").ToArray(0)
            Dim TimeEnd As String = NORMAL_HOUR.Split("-").ToArray(1)

            '======================================
            '[Overtime] กรณีที่ใส่เวลา 24.00 ให้เป็นวันถัดไป
            '======================================
            Dim OT_Date As DateTime = datetime.ParseExact(dateCont.dateFormat2(tbOTEndDate.Text), "yyyyMMdd", CultureInfo.InvariantCulture)
            Dim OT_TIME As String = tbOTEndTime.Text.Replace("24.00", "00:00").Replace(".", ":")
            'Dim OT_End As DateTime = If(ShiftDays = "Night", DateAdd(DateInterval.Day, 1, OT_Date) & " " & OT_TIME, OT_Date & " " & OT_TIME)
            Dim OT_End As DateTime = OT_Date & " " & OT_TIME
            '======================================

            '======================================
            '[Working] Calculate woking days in between two different dates
            '======================================
            Work_Start = If(ShiftDays = "Night", datetime & " " & TimeBegin.Replace(".", ":"), datetime & " " & TimeBegin.Replace(".", ":"))
            Work_End = If(ShiftDays = "Night", DateAdd(DateInterval.Day, 1, datetime) & " " & TimeEnd.Replace(".", ":"), datetime & " " & TimeEnd.Replace(".", ":"))
            Diff_Wk = DateDiff(DateInterval.Second, Work_Start, Work_End)

            '======================================
            '[Leave] Calculate Time Difference 
            '[LEAVE TIME] กรณีที่ใส่เวลา 24.00 เปลี่ยน 00.00
            '======================================
            Dim LEAVE_END_TIME As String = LeaveEndTime.Replace("24.00", "00:00").Replace(".", ":")
            If LeaveCode <> "W" Then
                Leave_Start_D = DateTime.ParseExact(LeaveStartDate & " " & LeaveStartTime.Replace(".", ":"), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                Leave_End_D = DateTime.ParseExact(LeaveEndDate & " " & LEAVE_END_TIME.Replace(".", ":").ToString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                Diff_Lve = DateDiff(DateInterval.Second, Leave_Start_D, Leave_End_D)
            End If
            '======================================
            Dim TypeRecord As String = String.Empty
            If lbHoliday.Text.Trim <> "Work Day" And cbHoliday.Checked = False Then
                Dim Sql_Del As String = " DELETE  FROM " & OT_Record & " WHERE EMPNO = '" & EmpNo.Trim & "' AND WORK_DATE = '" & getDate.Trim & "'"
                dbConn.TransactionSQL(Sql_Del, VarIni.DBMIS, dbConn.WhoCalledMe)
            End If
            TypeRecord = "U"
            whr.Add("EMPNO", gvEdit.Rows(i).Cells(2).Text.Trim)
            whr.Add("WORK_DATE", getDate.Trim)
            fld.Add("CHANGEBY", Session("UserName").Trim)
            fld.Add("CHANGEDATE", Date.Now.ToString("yyyyMMdd HH:mm").Trim)
            fld.Add("SHIFT_DAY", ddlShiftDay.SelectedItem.Text.Trim)
            fld.Add("DEPT", gvEdit.Rows(i).Cells(1).Text.Trim.Split(":").ToArray(0).Trim)
            fld.Add("WORK_DATE", getDate.Trim)
            Dim NORMAL_TIME As Decimal = Decimal.Zero
            Dim LEAVE_TIME As Decimal = Decimal.Zero
            Dim MID_DAY As DateTime = datetime & " " & "12:00"


            '===============================
            '[LEAVE TIME] กรณีที่ใส่เวลา 24.00 เปลี่ยน 00.00
            '===============================
            Dim ChkLeave As Boolean = True
            Dim Leave_Start As DateTime
            Dim Leave_End As DateTime
            Dim LEAVE_TM As String = LeaveEndTime.Replace("24.00", "00:00").Replace(".", ":")
            If LeaveCode <> "W" Then
                Leave_Start = DateTime.ParseExact(LeaveStartDate & " " & LeaveStartTime.Replace(".", ":").ToString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                Leave_End = DateTime.ParseExact(LeaveEndDate & " " & LEAVE_TM.Replace(".", ":").ToString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)

                '===============================
                '[CHECK OT LUNCH]
                '===============================
                Dim lev_start_time As New TimeSpan(tbLeaveStartTime.Text.Substring(0, 2), tbLeaveStartTime.Text.Substring(3, 2), 0)
                Dim lev_end_time As New TimeSpan(tbLeaveEndTime.Text.Substring(0, 2), tbLeaveEndTime.Text.Substring(3, 2), 0)

                If ShiftDays = "Night" Then
                    Dim set_lev_start_time As New TimeSpan(23, 59, 0)
                    Dim set_lev_end_time As New TimeSpan(1, 1, 0)
                    If set_lev_start_time >= lev_start_time And set_lev_end_time <= lev_end_time Then
                        ChkLeave = False
                    End If
                Else
                    Dim set_lev_start_time As New TimeSpan(11, 59, 0)
                    Dim set_lev_end_time As New TimeSpan(13, 1, 0)
                    If set_lev_start_time >= lev_start_time And set_lev_end_time <= lev_end_time Then
                        ChkLeave = False
                    End If
                End If
            End If

            '==========================
            '[NORMAL TIME]
            '==========================
            If Diff_Lve = Diff_Wk Then
                fld.Add("NORMAL_TIME", "0")
                LEAVE_TIME = Diff_Lve - 3600
            Else
                If ChkLeave = False Then '07:00 < 12:00
                    NORMAL_TIME = ((Diff_Wk - 3600) - (Diff_Lve - 3600))
                    LEAVE_TIME = Diff_Lve - 3600
                Else
                    NORMAL_TIME = ((Diff_Wk - 3600) - Diff_Lve)
                    LEAVE_TIME = Diff_Lve
                End If
                fld.Add("NORMAL_TIME", NORMAL_TIME)
            End If

            '==========================
            'LEAVE_CODE
            'LEAVE_STARTED_DATE
            'LEAVE_FINISHED_DATE
            'LEAVE_STARTED_TIME
            'LEAVE_FINISHED_TIME
            'LEAVE_TIME
            'WORK_BEGIN_TIME'
            'WORK_BEGIN_DATE
            '==========================
            Dim Work_Begin As DateTime
            If LeaveCode = "W" Then
                fld.Add("LEAVE_CODE", "")
                fld.Add("LEAVE_STARTED_DATE", "")
                fld.Add("LEAVE_FINISHED_DATE", "")
                fld.Add("LEAVE_STARTED_TIME", "")
                fld.Add("LEAVE_FINISHED_TIME", "")
                fld.Add("LEAVE_TIME", "0")
                Work_Begin = Work_Start
                fld.Add("WORK_BEGIN_TIME", Work_Begin.ToString("HH:mm").Trim)
                fld.Add("WORK_BEGIN_DATE", Work_Begin.ToString("yyyyMMdd").Trim)
            Else
                fld.Add("LEAVE_CODE", LeaveCode.Split("-").ToArray(0).Trim)
                fld.Add("LEAVE_STARTED_DATE", dateCont.dateFormat2(LeaveStartDate).Trim)
                fld.Add("LEAVE_FINISHED_DATE", dateCont.dateFormat2(LeaveEndDate).Trim)
                fld.Add("LEAVE_STARTED_TIME", LeaveStartTime.Replace(".", ":").Trim)
                fld.Add("LEAVE_FINISHED_TIME", LeaveEndTime.Replace(".", ":").Trim)
                fld.Add("LEAVE_TIME", LEAVE_TIME)
                Work_Begin = If(Leave_Start_D = Work_Start, Leave_Start_D, Work_Start)
                fld.Add("WORK_BEGIN_TIME", Work_Begin.ToString("HH:mm").Trim)
                fld.Add("WORK_BEGIN_DATE", Work_Begin.ToString("yyyyMMdd").Trim)
            End If
            '==========================
            'LUNCH_TIME
            'PICKUP_LOCATION
            '==========================

            Dim OT_Lunch As Decimal = If(cbLunch.Checked = True, "3600", "0")
            If Diff_Wk = Diff_Lve Then
                fld.Add("PICKUP_LOCATION", "")
                fld.Add("LUNCH_TIME", "0")
            Else
                fld.Add("PICKUP_LOCATION:N", If(cbPickup.Checked = True, BusLine.Trim, ""))
                fld.Add("LUNCH_TIME", OT_Lunch)
            End If

            Diff_OT = DateDiff(DateInterval.Second, Work_End, OT_End)
            fld.Add("WORK_TYPE", tbWorkType.Text.Trim)
            '==========================
            'OT_STARTED_DATE
            'OT_FINISHED_DATE
            'OT_STARTED_TIME
            'OT_FINISHED_TIME
            'OVER_TIME
            '==========================
            If Diff_OT > 0 And tbWorkType.Text.Trim = "Work Day" Then
                Dim Started_Time As String = Work_End.ToString("HH:mm")
                Dim Finished_Time As String = OT_End.ToString("HH:mm")
                fld.Add("OT_STARTED_DATE", Work_End.ToString("yyyyMMdd").Trim)
                fld.Add("OT_FINISHED_DATE", OT_End.ToString("yyyyMMdd").Trim)
                fld.Add("OT_STARTED_TIME", Started_Time.Trim)
                fld.Add("OT_FINISHED_TIME", Finished_Time.Trim)
                fld.Add("OVER_TIME", Diff_OT)
            ElseIf tbWorkType.Text.Trim = "Holiday" Then
                Dim Started_Time As String = Work_Start.ToString("HH:mm")
                Dim Finished_Time As String = OT_End.ToString("HH:mm")
                fld.Add("OT_STARTED_DATE", Work_Start.ToString("yyyyMMdd").Trim)
                fld.Add("OT_FINISHED_DATE", OT_End.ToString("yyyyMMdd").Trim)
                fld.Add("OT_STARTED_TIME", Started_Time.Trim)
                fld.Add("OT_FINISHED_TIME", Finished_Time.Trim)
                fld.Add("OVER_TIME", Diff_OT)
            Else
                fld.Add("OT_STARTED_DATE", "")
                fld.Add("OT_FINISHED_DATE", "")
                fld.Add("OT_STARTED_TIME", "")
                fld.Add("OT_FINISHED_TIME", "")
                fld.Add("OVER_TIME", "0")
            End If
            sqlHash_gvShow.Add(sqlCont.GetSQL(OT_Record, fld, whr, TypeRecord))
        Next

        If sqlHash_gvShow.Count > 0 Then
            dbConn.TransactionSQL(sqlHash_gvShow, VarIni.DBMIS, dbConn.WhoCalledMe)
            show_message.ShowMessage(Page, "บันทึกสำรเร็จ ", UpdatePanel1)
        End If

        gvShow.Visible = False
        gvReport.Visible = True
        gvEdit.Visible = False
        btRecord.Visible = True
        btEdit.Visible = True
        btSave.Visible = False
        btUpdate.Visible = False
        btCancel.Visible = False
        btShowRe.Visible = True
        btPrint.Visible = False
        btExcel.Visible = False
        ShowReport()
    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        btSave.Visible = False
        btUpdate.Visible = False
        btPrint.Visible = False
        btExcel.Visible = False
        btEdit.Visible = False
        btCancel.Visible = True
        btShowRe.Visible = True
        gvShow.Visible = False
        gvEdit.Visible = False
        gvReport.Visible = False
        CountRow1.Visible = False

        Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim chk As Boolean = False
        lbGrp.Text = String.Empty
        If dr Is Nothing Then
            Response.Redirect(Server.UrlPathEncode("../Home.aspx"))
        Else
            deptcode = dtCont.IsDBNullDataRow(dr, "Dept")
            lbGrp.Text = dtCont.IsDBNullDataRow(dr, "Grp")
            chk = If(lbGrp.Text = "dept_head", True, False)
        End If
        NewButtonSet(chk)
    End Sub

    Protected Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        Dim sql As String = ""
        Dim Dept As String = ""
        Dim WorkDate As String = ""
        Dim WorkTime As String = ""
        Dim whr As String = ""
        Dim paraName As String = String.Empty
        Dim chrConn As String = Chr(8)
        If Session("userid") = "" Then
            Response.Redirect(VarIni.PageLogin)
        End If

        Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session("userid")), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dr, "Dept")

        whr &= whrCont.Where("  O.DEPT", cblDept, False, deptcode.Replace(",", "','"), True) 'DEPT
        whr &= whrCont.Where("  O.WORK_DATE", ucDate.text,, False) 'DATE
        If rdlShift.SelectedValue = "D" Then
            whr &= whrCont.Where("SUBSTRING(O.WORK_BEGIN_TIME,1,2)", " <> '19'")
        ElseIf rdlShift.SelectedValue = "N" Then
            whr &= whrCont.Where("SUBSTRING(O.WORK_BEGIN_TIME,1,2)", " = '19'")
        End If
        whr &= whrCont.Where("Y.EMP_NAME", " Is Not null ")

        Dim WHR1 As String = String.Empty
        Dim fldName As New ArrayList
        fldName.Add(HR_DEPT.CODE & ": Code")
        fldName.Add(HR_DEPT.NAME & "+'-'+ isnull(" & HR_DEPT.SHORT_NAME & ",''):CodeName")
        WHR1 &= VarIni.oneEqualOne
        WHR1 &= whrCont.Where(HR_DEPT.CODE, cblDept, False, deptcode.Replace(",", "','"), True) 'DEPT
        sql = VarIni.S & sqlCont.getFeild(fldName) & VarIni.F & HR_DEPT.TABLENAME & VarIni.W & WHR1 & sqlCont.getOrderBy(HR_DEPT.CODE)
        Dim dt As DataTable = dbConn.Query(sql, VarIni.DBMIS, dbConn.WhoCalledMe)
        Dim StrDept As String = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            StrDept &= dt.Rows(i).Item("CodeName").ToString.Trim & ","
        Next
        Dim Dpt As String = StrDept.Substring(0, StrDept.Length - 1)
        Dim Code As String = "Code:" & Dpt
        paraName = "paraName:" & whr & chrConn & Code
        Randomize()
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=MIS&ReportName=OTRecord.rpt&paraName=" & Server.UrlEncode(paraName) & "&chrConn=" & Server.UrlEncode(chrConn) & "&encode=1&Rnd=" & (Int(Rnd() * 100 + 1)) & "');", True)

        'ChangeDate()
    End Sub

    '=============== Sub =====================

    Private Sub NewButtonSet(ByVal chk As Boolean)

        If chk = True Then
            btRecord.Visible = True
            Exit Sub
        End If
        Dim setDate = Server.HtmlEncode(ucDate.text)
        Dim datetime As DateTime = datetime.ParseExact(setDate, "yyyyMMdd", CultureInfo.InvariantCulture)
        Dim getDate = datetime.ToString("yyyyMMdd")
        Dim datetime_from As String = String.Empty
        Dim datetime_to As String = String.Empty
        datetime_from = getDate & " " & Date.Now.ToString("HH:mm").ToString
        datetime_to = Date.Now.ToString("yyyyMMdd").ToString & " 14:00"

        Dim setTimeShow As Boolean = False
        If getDate >= Date.Now.ToString("yyyyMMdd").ToString Then
            If datetime_from < datetime_to Then
                setTimeShow = True
            ElseIf getDate > Date.Now.ToString("yyyyMMdd").ToString Then
                setTimeShow = True
            End If
        Else
            setTimeShow = False
        End If
        btRecord.Visible = setTimeShow
    End Sub

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
        Whr &= whrCont.Where("O.WORK_DATE", ucDate.text,, False) 'DATE
        Whr &= whrCont.Where("E.EMP_STATE", " NOT IN ('3001','3002','3003') ")
        If rdlShift.SelectedValue = "D" Then
            Whr &= whrCont.Where(" O.SHIFT_DAY ", "LIKE '%Day%'")
        ElseIf rdlShift.SelectedValue = "N" Then
            Whr &= whrCont.Where(" O.SHIFT_DAY", " NOT LIKE '%Day%'")
        End If

        fldName.Add(" '" & rdlShift.SelectedItem.Text.Trim & "'" & VarIni.char8 & shift)
        fldName.Add("O.DEPT" & VarIni.char8 & dept)
        fldName.Add("O.EMPNO" & VarIni.char8 & empcode)
        fldName.Add("E.EMP_NAME" & VarIni.char8 & name)
        fldName.Add("RTRIM(substring(O.WORK_DATE,7,4))+'/'+substring(O.WORK_DATE,5,2)+'/'+substring(O.WORK_DATE,1,4)" & VarIni.char8 & workdate)
        fldName.Add("O.WORK_TYPE" & VarIni.char8 & work_type)
        fldName.Add("O.SHIFT_DAY" & VarIni.char8 & shiftday)
        fldName.Add("CASE O.LEAVE_CODE WHEN '' THEN '' ELSE RTRIM(C.Code) +'-'+ RTRIM(C.Name) END" & VarIni.char8 & leave_code)
        fldName.Add("CASE O.LEAVE_CODE WHEN '' THEN '' ELSE RTRIM(substring(O.LEAVE_STARTED_DATE,7,4)) +'/'+substring(O.LEAVE_STARTED_DATE,5,2)+'/'+substring(O.LEAVE_STARTED_DATE,1,4)+' '+O.LEAVE_STARTED_TIME END" & VarIni.char8 & leave_start_date)
        fldName.Add("CASE O.LEAVE_CODE WHEN '' THEN '' ELSE RTRIM(substring(O.LEAVE_FINISHED_DATE,7,4))+'/'+substring(O.LEAVE_FINISHED_DATE,5,2)+'/'+substring(O.LEAVE_FINISHED_DATE,1,4)+' '+O.LEAVE_FINISHED_TIME END" & VarIni.char8 & leave_end_date)
        fldName.Add("CAST(O.LEAVE_TIME/3600 AS DECIMAL(16,1))" & VarIni.char8 & leave_hours)
        fldName.Add("CASE O.OT_STARTED_DATE WHEN '' THEN '' ELSE RTRIM(substring(O.OT_STARTED_DATE,7,4))+'/'+substring(O.OT_STARTED_DATE,5,2)+'/'+substring(O.OT_STARTED_DATE,1,4)+' '+O.OT_STARTED_TIME END" & VarIni.char8 & ot_start_date)
        fldName.Add("CASE O.OT_FINISHED_DATE WHEN '' THEN '' ELSE RTRIM(substring(O.OT_FINISHED_DATE,7,4))+'/'+substring(O.OT_FINISHED_DATE,5,2)+'/'+substring(O.OT_FINISHED_DATE,1,4)+' '+O.OT_FINISHED_TIME END" & VarIni.char8 & ot_end_date)
        fldName.Add("CASE WHEN O.WORK_TYPE = 'Holiday' THEN CAST(O.NORMAL_TIME /3600 AS DECIMAL(16,1))  ELSE CAST(O.OVER_TIME/3600 AS DECIMAL(16,1)) END" & VarIni.char8 & ot_hours)
        fldName.Add("CAST(O.LUNCH_TIME/3600 AS DECIMAL(16,1))" & VarIni.char8 & lunch_hours)
        fldName.Add("CASE WHEN O.WORK_TYPE = 'Holiday' THEN CAST((O.OVER_TIME +O.LUNCH_TIME+O.NORMAL_TIME )/3600 AS DECIMAL(16,1)) ELSE CAST((O.OVER_TIME +O.LUNCH_TIME )/3600 AS DECIMAL(16,1))END" & VarIni.char8 & ot_total)
        fldName.Add("O.PICKUP_LOCATION" & VarIni.char8 & pickup)
        fldName.Add("O.CREATEBY" & VarIni.char8 & create_by)
        fldName.Add("O.CREATEDATE" & VarIni.char8 & create_date)
        fldName.Add("O.CHANGEBY" & VarIni.char8 & change_by)
        fldName.Add("O.CHANGEDATE" & VarIni.char8 & change_date)

        SQL &= VarIni.S & sqlCont.getFeild(fldName, VarIni.char8) & VarIni.F & "  HR_EMPLOYEE_HY E "
        SQL &= VarIni.L & " OVER_TIME_RECORD O " & VarIni.O2 & " CODE=EMPNO  " & COLL
        SQL &= VarIni.L & " V_HR_DEPT D " & VarIni.O2 & "D.Code  =DEPT_CODE " & COLL
        SQL &= VarIni.L & " CodeInfo C " & VarIni.O2 & " C.Code =LEAVE_CODE and CodeType = 'LeaveCode' " & COLL
        SQL &= VarIni.W & " 1=1 " & Whr & VarIni.O & " O.EMPNO ASC"

        Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Const Doublechar8 As String = "◘◘"
        For Each dr As DataRow In dt.Rows
            Dim dataFld = New ArrayList
            dataFld.Add(shift & Doublechar8 & dtCont.IsDBNullDataRow(dr, shift))
            dataFld.Add(dept & Doublechar8 & dtCont.IsDBNullDataRow(dr, dept))
            dataFld.Add(empcode & Doublechar8 & dtCont.IsDBNullDataRow(dr, empcode))
            dataFld.Add(name & Doublechar8 & dtCont.IsDBNullDataRow(dr, name))
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

        gvCont.GridviewInitial(gvReport, colName,,,,, VarIni.char8)
        gvCont.ShowGridView(gvReport, dtShow)
        CountRow1.RowCount = gvCont.rowGridview(gvReport)
        If gvReport.Rows.Count > 0 Then
            btPrint.Visible = True
        End If
    End Sub

    Public Sub showDDL(ByRef ControlDDL As DropDownList,
                           ByVal SQL As String,
                            ByVal dbType As String,
                           ByVal fldText As String,
                           ByVal fldValue As String,
                           Optional ByVal setAll As Boolean = False,
                           Optional ByVal headVal As String = "0")

        Dim PathFile As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
        Dim whoCall As String = dbConn.WhoCalledMe
        If SQL = String.Empty Or dbType = String.Empty Or
                fldText = String.Empty Or fldValue = String.Empty Then
            dbConn.returnPageError(PathFile, "Data input Is Empty", whoCall, SQL)
        End If

        With ControlDDL
            Try
                .DataSource = dbConn.Query(SQL, dbType, whoCall)
                .DataTextField = fldText
                .DataValueField = fldValue
                .DataBind()
                If setAll Then
                    .Items.Insert(0, headVal)
                End If
            Catch ex As Exception
                dbConn.returnPageError(PathFile, ex.Message, whoCall, SQL)
            End Try
        End With
    End Sub

    Private Sub EditButtonSet(ByVal chk As Boolean)
        If chk = True Then
            btRecord.Visible = True
            btEdit.Visible = True
            Exit Sub
        End If
        Dim setTimeShow As Boolean = False
        Dim setDate = Server.HtmlEncode(ucDate.text)
        Dim datetime As DateTime = datetime.ParseExact(setDate, "yyyyMMdd", CultureInfo.InvariantCulture)
        Dim getDate = datetime.ToString("yyyyMMdd")
        Dim datetime_from As String = String.Empty
        Dim datetime_to As String = String.Empty
        datetime_from = getDate & " " & Date.Now.ToString("HH:mm").ToString
        datetime_to = Date.Now.ToString("yyyyMMdd").ToString & " 14:00"

        If getDate = Date.Now.AddDays(-1).ToString("yyyyMMdd").ToString Then

            If datetime_from < Date.Now.AddDays(-1).ToString("yyyyMMdd").ToString & " 09:00" And rdlShift.SelectedIndex = 1 Then
                btEdit.Visible = True
            ElseIf datetime_from < Date.Now.AddDays(-1).ToString("yyyyMMdd").ToString & " 09:00" And rdlShift.SelectedIndex = 0 Then
                btEdit.Visible = True
            ElseIf datetime_from > Date.Now.AddDays(-1).ToString("yyyyMMdd").ToString & " 09:00" And rdlShift.SelectedIndex = 0 Then
                btEdit.Visible = False
            Else
                btEdit.Visible = False
            End If
        Else
            If getDate >= Date.Now.ToString("yyyyMMdd").ToString Then
                If datetime_from < datetime_to Then
                    setTimeShow = True
                ElseIf getDate > Date.Now.ToString("yyyyMMdd").ToString Then
                    setTimeShow = True
                End If
            Else
                setTimeShow = False
            End If

            btEdit.Visible = setTimeShow
            btCancel.Visible = setTimeShow
            btRecord.Visible = setTimeShow
        End If
        If chk = True Then
            btRecord.Visible = True
            btEdit.Visible = True
            Exit Sub
        End If

    End Sub

    '=============== Event =====================

    Protected Sub ucDate_ChangeEvent(ByVal sender As Object, ByVal e As EventArgs) Handles ucDate.ChangeEvent

        If lbGrp.Text = "dept_head" Then
            Exit Sub
        End If

        Dim setDate = Server.HtmlEncode(ucDate.text)
        Dim dt As DateTime = DateTime.ParseExact(setDate, "yyyyMMdd", CultureInfo.InvariantCulture)
        Dim getDate = dt.ToString("yyyyMMdd")
        Dim datetime_from As String = String.Empty
        Dim datetime_to As String = String.Empty
        datetime_from = getDate & " " & Date.Now.ToString("HH:mm").ToString
        datetime_to = Date.Now.ToString("yyyyMMdd").ToString & " 14:00"

        Dim setTimeShow As Boolean = False
        If getDate >= Date.Now.ToString("yyyyMMdd").ToString Then
            If datetime_from < datetime_to Or getDate > Date.Now.ToString("yyyyMMdd").ToString Then
                setTimeShow = True
            Else
                btEdit.Visible = False
                btSave.Visible = False
            End If
        Else
            btEdit.Visible = False
            btSave.Visible = False
            setTimeShow = False
        End If
        btRecord.Visible = setTimeShow

        If getDate = Date.Now.AddDays(-1).ToString("yyyyMMdd").ToString Then
            If datetime_from < Date.Now.AddDays(-1).ToString("yyyyMMdd").ToString & " 09:00" And rdlShift.SelectedIndex = 1 Then
                btEdit.Visible = True
            ElseIf datetime_from < Date.Now.AddDays(-1).ToString("yyyyMMdd").ToString & " 09:00" And rdlShift.SelectedIndex = 0 Then
                btEdit.Visible = True
            ElseIf datetime_from > Date.Now.AddDays(-1).ToString("yyyyMMdd").ToString & " 09:00" And rdlShift.SelectedIndex = 0 Then
                btEdit.Visible = False
            Else
                btEdit.Visible = False
            End If
        Else
            btEdit.Visible = False
        End If
    End Sub

    Protected Sub ddlLeaveCode_Selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs)

        For i As Integer = 0 To gvShow.Rows.Count - 1
            With gvShow.Rows(i)

                Dim ddlleaveCode As DropDownList = .Cells(7).FindControl("ddlleaveCode")
                Dim ddlLeaveCase As DropDownList = .Cells(8).FindControl("ddlLeaveCase")

                Dim tbLeaveStartTime As TextBox = .Cells(8).FindControl("tbLeaveStartTime")
                Dim tbLeaveStartDate As TextBox = .Cells(8).FindControl("tbLeaveStartDate")

                Dim tbLeaveEndTime As TextBox = .Cells(8).FindControl("tbLeaveEndTime")
                Dim tbLeaveEndDate As TextBox = .Cells(8).FindControl("tbLeaveEndDate")

                Dim LeaveCode As String = ddlleaveCode.SelectedValue.Split("-").ToArray(0)
                If LeaveCode = "W" Then
                    ddlLeaveCase.Visible = False
                    tbLeaveStartTime.Visible = False
                    tbLeaveStartDate.Visible = False
                    tbLeaveEndTime.Visible = False
                    tbLeaveEndDate.Visible = False

                    ddlLeaveCase.SelectedValue = String.Empty
                    tbLeaveStartTime.Text = String.Empty
                    tbLeaveStartDate.Text = String.Empty
                    tbLeaveEndTime.Text = String.Empty
                    tbLeaveEndDate.Text = String.Empty
                Else
                    ddlLeaveCase.Visible = True
                    tbLeaveStartTime.Visible = True
                    tbLeaveStartDate.Visible = True
                    tbLeaveEndTime.Visible = True
                    tbLeaveEndDate.Visible = True
                End If

            End With
        Next
        For j As Integer = 0 To gvEdit.Rows.Count - 1
            With gvEdit.Rows(j)

                Dim ddlleaveCode As DropDownList = .Cells(7).FindControl("ddlleaveCode")
                Dim ddlLeaveCase As DropDownList = .Cells(8).FindControl("ddlLeaveCase")

                Dim tbLeaveStartTime As TextBox = .Cells(8).FindControl("tbLeaveStartTime")
                Dim tbLeaveStartDate As TextBox = .Cells(8).FindControl("tbLeaveStartDate")

                Dim tbLeaveEndTime As TextBox = .Cells(8).FindControl("tbLeaveEndTime")
                Dim tbLeaveEndDate As TextBox = .Cells(8).FindControl("tbLeaveEndDate")
                Dim LeaveCode As String = ddlleaveCode.SelectedValue.Split("-").ToArray(0)
                If LeaveCode = "W" Then
                    ddlLeaveCase.Visible = False
                    tbLeaveStartTime.Visible = False
                    tbLeaveStartDate.Visible = False
                    tbLeaveEndTime.Visible = False
                    tbLeaveEndDate.Visible = False

                    ddlLeaveCase.SelectedValue = String.Empty
                    tbLeaveStartTime.Text = String.Empty
                    tbLeaveStartDate.Text = String.Empty
                    tbLeaveEndTime.Text = String.Empty
                    tbLeaveEndDate.Text = String.Empty
                Else
                    ddlLeaveCase.Visible = True
                    tbLeaveStartTime.Visible = True
                    tbLeaveStartDate.Visible = True
                    tbLeaveEndTime.Visible = True
                    tbLeaveEndDate.Visible = True
                End If

            End With
        Next
    End Sub

    Protected Sub ddlLeaveCase_Selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs)

        For i As Integer = 0 To gvShow.Rows.Count - 1
            With gvShow.Rows(i)

                Dim default_shift As DropDownList = .Cells(6).FindControl("ddlShiftDay")
                Dim lbHoliday As TextBox = .Cells(5).FindControl("lbHoliday")
                Dim Date_V1 As Date = Date.ParseExact(ucDate.text.ToString(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Dim Date_V2 As Date = If(.Cells(0).Text = "Night", DateAdd(DateInterval.Day, 1, Date_V1), Date_V1)
                Dim Time_V1 As String = getOTStartTime(lbHoliday.Text.Trim, default_shift.SelectedItem.Text.Trim, True)
                'rin
                Dim ddlLeaveCase As DropDownList = .Cells(10).FindControl("ddlLeaveCase")
                Dim tbLeaveStartTime As TextBox = .Cells(10).FindControl("tbLeaveStartTime")
                Dim tbLeaveStartDate As TextBox = .Cells(10).FindControl("tbLeaveStartDate")
                Dim tbLeaveEndTime As TextBox = .Cells(10).FindControl("tbLeaveEndTime")
                Dim tbLeaveEndDate As TextBox = .Cells(10).FindControl("tbLeaveEndDate")
                Dim tbOTEndDate As TextBox = .Cells(12).FindControl("tbOTEndDate")

                If ddlLeaveCase.SelectedValue = "D" Then 'Day 
                    'date
                    tbLeaveStartDate.Text = GET_CONV_STRING(Date_V1)
                    tbLeaveEndDate.Text = GET_CONV_STRING(Date_V2)
                    'time
                    tbLeaveStartTime.Text = If(tbLeaveStartTime.Text = "__.__", Time_V1.Split("-").ToArray(0), tbLeaveStartTime.Text)
                    tbLeaveEndTime.Text = If(tbLeaveEndTime.Text = "__.__", Time_V1.Split("-").ToArray(1), tbLeaveEndTime.Text)

                ElseIf ddlLeaveCase.SelectedValue = "H" Then 'Hour
                    'date
                    tbLeaveStartDate.Text = GET_CONV_STRING(Date_V1)
                    tbLeaveEndDate.Text = GET_CONV_STRING(Date_V2)
                    'time
                    tbLeaveStartTime.Text = ""
                    tbLeaveEndTime.Text = ""
                Else
                    'date
                    tbLeaveStartDate.Text = ""
                    tbLeaveEndDate.Text = ""
                    'time
                    tbLeaveStartTime.Text = ""
                    tbLeaveEndTime.Text = ""
                End If

            End With
        Next

        For j As Integer = 0 To gvEdit.Rows.Count - 1
            With gvEdit.Rows(j)

                Dim Date_V1 As Date = Date.ParseExact(ucDate.text.ToString(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Dim Date_V2 As Date = If(.Cells(0).Text = "Night", DateAdd(DateInterval.Day, 1, Date_V1), Date_V1)
                Dim default_shift As DropDownList = .Cells(6).FindControl("ddlShiftDay")
                Dim lbHoliday As TextBox = .Cells(5).FindControl("lbHoliday")
                Dim Time_V1 As String = getOTStartTime(lbHoliday.Text.Trim, default_shift.SelectedItem.Text.Trim, True)
                Dim ddlLeaveCase As DropDownList = .Cells(10).FindControl("ddlLeaveCase")
                Dim tbLeaveStartTime As TextBox = .Cells(10).FindControl("tbLeaveStartTime")
                Dim tbLeaveStartDate As TextBox = .Cells(10).FindControl("tbLeaveStartDate")
                Dim tbLeaveEndTime As TextBox = .Cells(10).FindControl("tbLeaveEndTime")
                Dim tbLeaveEndDate As TextBox = .Cells(10).FindControl("tbLeaveEndDate")
                Dim tbOTEndDate As TextBox = .Cells(12).FindControl("tbOTEndDate")

                If ddlLeaveCase.SelectedValue = "D" Then 'Day 
                    'date
                    tbLeaveStartDate.Text = GET_CONV_STRING(Date_V1)
                    tbLeaveEndDate.Text = GET_CONV_STRING(Date_V2)
                    'time
                    tbLeaveStartTime.Text = If(tbLeaveStartTime.Text = "__.__", Time_V1.Split("-").ToArray(0), tbLeaveStartTime.Text)
                    tbLeaveEndTime.Text = If(tbLeaveEndTime.Text = "__.__", Time_V1.Split("-").ToArray(1), tbLeaveEndTime.Text)

                ElseIf ddlLeaveCase.SelectedValue = "H" Then 'Hour
                    'date
                    tbLeaveStartDate.Text = GET_CONV_STRING(Date_V1)
                    tbLeaveEndDate.Text = GET_CONV_STRING(Date_V2)
                    'time
                    tbLeaveStartTime.Text = ""
                    tbLeaveEndTime.Text = ""
                Else
                    'date
                    tbLeaveStartDate.Text = ""
                    tbLeaveEndDate.Text = ""
                    'time
                    tbLeaveStartTime.Text = ""
                    tbLeaveEndTime.Text = ""
                End If

            End With
        Next
    End Sub

    Protected Sub ddlShiftDay_Selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim setDate = Server.HtmlEncode(ucDate.text)
        Dim datetime As DateTime = DateTime.ParseExact(setDate, "yyyyMMdd", CultureInfo.InvariantCulture)

        For i As Integer = 0 To gvShow.Rows.Count - 1
            With gvShow.Rows(i)

                Dim ddlleaveCode As DropDownList = .Cells(7).FindControl("ddlleaveCode")

                Dim tbStartTime As TextBox = .FindControl("tbOTStartTime")
                Dim tbStartDate As TextBox = .FindControl("tbOTStartDate")
                Dim tbEndTime As TextBox = .FindControl("tbOTEndTime")
                Dim tbEndDate As TextBox = .FindControl("tbOTEndDate")
                Dim default_shift As DropDownList = gvShow.Rows(i).Cells(6).FindControl("ddlShiftDay")
                Dim lbHoliday As TextBox = gvShow.Rows(i).Cells(5).FindControl("lbHoliday")
                ddlleaveCode.SelectedValue.Split("-").ToArray(0) = "W"
                ddlLeaveCode_Selectedindexchanged(sender, e)
                Dim WorkTime As String = getOTStartTime(lbHoliday.Text.Trim, default_shift.SelectedItem.Text.Trim, True)
                Dim DateBegin As DateTime
                Dim DateEnd As DateTime
                Dim TimeBegin As String = String.Empty
                Dim TimeEnd As String = String.Empty

                TimeBegin = WorkTime.Split("-").ToArray(0)
                TimeEnd = WorkTime.Split("-").ToArray(1)

                DateBegin = If(gvShow.Rows(i).Cells(0).Text.Trim = "Night", datetime, datetime)
                DateEnd = If(gvShow.Rows(i).Cells(0).Text.Trim = "Night", DateAdd(DateInterval.Day, 1, datetime), datetime)

                If rdlShift.SelectedItem.Text = "Day" Then
                    If rdlDefault.SelectedItem.Text = "19.00" Then
                        tbEndTime.Text = "19.00"
                        tbEndDate.Text = GET_CONV_STRING(DateEnd)
                    ElseIf rdlDefault.SelectedItem.Text = "21.00" Then
                        tbEndTime.Text = "21.00"
                        tbEndDate.Text = GET_CONV_STRING(DateEnd)
                    Else
                        tbEndTime.Text = TimeEnd
                        tbEndDate.Text = GET_CONV_STRING(DateEnd)
                    End If
                    tbStartTime.Text = TimeEnd
                    tbStartDate.Text = GET_CONV_STRING(DateBegin)
                Else
                    If rdlDefault.SelectedItem.Text = "07.00" Then
                        tbEndTime.Text = "07:00"
                        tbEndDate.Text = GET_CONV_STRING(DateEnd)
                    Else
                        tbEndTime.Text = TimeEnd
                        tbEndDate.Text = GET_CONV_STRING(DateEnd)
                    End If
                    tbStartTime.Text = TimeEnd
                    tbStartDate.Text = GET_CONV_STRING(DateBegin)
                End If
            End With
        Next

        For j As Integer = 0 To gvEdit.Rows.Count - 1
            With gvEdit.Rows(j)
                Dim ddlleaveCode As DropDownList = .Cells(7).FindControl("ddlleaveCode")
                Dim tbStartTime As TextBox = .FindControl("tbOTStartTime")
                Dim tbStartDate As TextBox = .FindControl("tbOTStartDate")
                Dim tbEndTime As TextBox = .FindControl("tbOTEndTime")
                Dim tbEndDate As TextBox = .FindControl("tbOTEndDate")
                Dim default_shift As DropDownList = .Cells(6).FindControl("ddlShiftDay")
                Dim lbHoliday As TextBox = .Cells(5).FindControl("lbHoliday")
                ddlleaveCode.SelectedValue.Split("-").ToArray(0) = "W"
                ddlLeaveCode_Selectedindexchanged(sender, e)
                Dim WorkTime As String = getOTStartTime(lbHoliday.Text.Trim, default_shift.SelectedItem.Text.Trim, True)
                Dim DateBegin As DateTime
                Dim DateEnd As DateTime
                Dim TimeBegin As String = String.Empty
                Dim TimeEnd As String = String.Empty

                TimeBegin = WorkTime.Split("-").ToArray(0)
                TimeEnd = WorkTime.Split("-").ToArray(1)

                DateBegin = If(.Cells(0).Text.Trim = "Night", datetime, datetime)
                DateEnd = If(.Cells(0).Text.Trim = "Night", DateAdd(DateInterval.Day, 1, datetime), datetime)

                If rdlShift.SelectedItem.Text = "Day" Then
                    If rdlDefault.SelectedItem.Text = "19.00" Then
                        tbEndTime.Text = "19.00"
                        tbEndDate.Text = GET_CONV_STRING(DateEnd)
                    ElseIf rdlDefault.SelectedItem.Text = "21.00" Then
                        tbEndTime.Text = "21.00"
                        tbEndDate.Text = GET_CONV_STRING(DateEnd)
                    Else
                        tbEndTime.Text = TimeEnd
                        tbEndDate.Text = GET_CONV_STRING(DateEnd)
                    End If
                    tbStartTime.Text = TimeEnd
                    tbStartDate.Text = GET_CONV_STRING(DateBegin)
                Else
                    If rdlDefault.SelectedItem.Text = "07.00" Then
                        tbEndTime.Text = "07:00"
                        tbEndDate.Text = GET_CONV_STRING(DateEnd)
                    Else
                        tbEndTime.Text = TimeEnd
                        tbEndDate.Text = GET_CONV_STRING(DateEnd)
                    End If
                    tbStartTime.Text = TimeEnd
                    tbStartDate.Text = GET_CONV_STRING(DateBegin)
                End If

            End With
        Next
    End Sub

    Protected Sub rdlShift_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdlShift.SelectedIndexChanged, UpdatePanel1.DataBinding

        Dim values As ArrayList = New ArrayList()
        If rdlShift.SelectedValue = "D" Then 'day
            values.Add("NO OT")
            values.Add("19.00")
            values.Add("21.00")
        Else 'night
            values.Add("NO OT")
            values.Add("07.00")
        End If
        rdlDefault.DataSource = values
        rdlDefault.DataBind()
        rdlDefault.SelectedValue = "NO OT"

    End Sub

    '=============== Excel =====================

    Protected Sub btExcel_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        gvCont.ExportGridViewToExcel("OTRECORD" & Session(VarIni.UserId), gvReport)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

End Class