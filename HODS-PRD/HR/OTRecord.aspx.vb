
Imports System.Globalization
Imports System.Drawing
Imports MIS_HTI.DataControl
Imports MIS_HTI.FormControl

Public Class OTRecord
    Inherits System.Web.UI.Page

    Dim cblCont As New CheckBoxListControl
    Dim ddlCnt As New DropDownListControl
    Dim whrCont As New WhereControl
    Dim sqlCont As New SQLControl
    Dim dbConn As New DataConnectControl
    Dim dtCont As New DataTableControl
    Dim gvCont As New GridviewControl
    Dim dateCont As New DateControl
    Dim outCont As New OutputControl

    Dim deptcode As String = String.Empty
    Dim grp As String = String.Empty
    Dim UserOTRecord As String = "UserOTRecord"
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
    Const DOCNO_OT As String = "DOCNO_OT"
    Const OT_Record As String = VarIni.TABLE_OT
    Public Const char8 As String = "◘"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session(VarIni.UserId) = "" Then
                Response.Redirect(Server.UrlPathEncode("../Login.aspx"))
            End If

            ucDate.Text = Date.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
            Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session(VarIni.UserId)), VarIni.DBMIS, dbConn.WhoCalledMe)
            deptcode = dtCont.IsDBNullDataRow(dr, "Dept")
            Dim chk As Boolean = False
            lbGrp.Text = String.Empty
            If deptcode = "" Then
                Response.Redirect(Server.UrlPathEncode("../Home.aspx"))
            Else
                deptcode = dtCont.IsDBNullDataRow(dr, "Dept")
                lbGrp.Text = dtCont.IsDBNullDataRow(dr, "Grp")

            End If

            cblCont.showCheckboxList(cblDept, getDept(deptcode.Replace(",", "','")), VarIni.DBMIS, "CodeName", "Code", 4)
            rdlShift_SelectedIndexChanged(sender, e)
            btSave.Visible = False
            btUpdate.Visible = False
            btPrint.Visible = False
            btExcel.Visible = False
            btEdit.Visible = False
            btCancel.Visible = False

            Dim SQL As String = CODEINFO.SQL("LeaveCode", " and " & CODEINFO.Code & "<>'W'", "rtrim(" & CODEINFO.Name & ")", CODEINFO.Name)
            Session("LeaveCode") = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
            SQL = CODEINFO.SQL("PICKUP_LOCATION",, "rtrim(" & CODEINFO.Name & ")", " CAST(" & CODEINFO.Code & " AS INT)")
            Session("PICKUP_LOCATION") = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)

            lbNote.Text = "บันทึกโอทีได้ถึงเวลา 13.30 ของทุกวัน และ สามารถแก้ไขของวันที่ทำงานล่าสุดได้ถึง 08.00"

            setButton()

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

    Function getTRIM(str As String)
        Dim SQL As String = "RTRIM(" & str & ")"
        Return SQL
    End Function

    Function getDocNo() As String
        Dim DocNo As String = String.Empty
        Dim DateDoc As String = Date.Today.ToString("yyyyMMdd")
        Dim SQL As String = "Select SUBSTRING(DOCNO, 1, 8), max(DOCNO) DOCNO from " & OT_Record & " where DOCNO Like '" & DateDoc & "%' group by SUBSTRING(DOCNO,1,8)"
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

    Function GET_CONV_STRING(ByVal DATEVAL As Date) As String
        Return DATEVAL.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
    End Function

    Private Sub setButton()
        Dim chk As Boolean = False
        Dim timeLimitNew As Decimal = "13.30"
        Dim timeLimitEdit As Decimal = "08.00"

        If lbGrp.Text = String.Empty Then
            Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session(VarIni.UserId)), VarIni.DBMIS, dbConn.WhoCalledMe)
            If dr Is Nothing Then
                Response.Redirect(Server.UrlPathEncode("../Home.aspx"))
            Else
                deptcode = dtCont.IsDBNullDataRow(dr, "dept")
                lbGrp.Text = dtCont.IsDBNullDataRow(dr, "grp")
            End If
        End If
        chk = If(lbGrp.Text = "dept_head", True, False)
        Dim showNew As Boolean = False
        Dim showEdit As Boolean = False
        Dim showSave As Boolean = False
        Dim showExport As Boolean = False
        Dim gvRowCount As Integer = gvShow.Rows.Count
        If chk Then 'admin authority
            showNew = True
            showEdit = True
        Else 'user authority
            Dim datePresent As Date = Date.Now
            Dim timePresent As Decimal = outCont.checkNumberic(datePresent.ToString("HH.mm"))
            Dim selectDate As Date = dateCont.strToDateTime(ucDate.text, "yyyyMMdd")
            Dim getDate As String = selectDate.ToString("yyyyMMdd")
            If (getDate = datePresent.ToString("yyyyMMdd") And timePresent <= timeLimitNew) Or getDate > datePresent.ToString("yyyyMMdd") Then 'new
                showNew = True
            End If
            Dim dDiff As Integer = DateDiff(DateInterval.Day, selectDate, Date.Now)
            'Dim aa As String = Date.Now.ToString("ddd")
            If ((Date.Now.ToString("ddd") = "Fri" And dDiff <= 3) Or dDiff = 1) And timePresent <= timeLimitEdit Then
                showEdit = True
            End If
        End If
        If showNew Or showEdit Then
            If gvShow.Visible And gvRowCount > 0 Then
                showSave = True
            End If
        End If
        If gvReport.Visible And gvReport.Rows.Count > 0 Then
            showExport = True
        End If
        btRecord.Visible = showNew 'new data
        btEdit.Visible = showEdit 'edit
        btSave.Visible = showSave 'save
        btCancel.Visible = True
        btShowRe.Visible = True
        btExcel.Visible = showExport
        btPrint.Visible = True
    End Sub

    '================== Button  ==================
    Protected Sub btRecord_Click(sender As Object, e As EventArgs) Handles btRecord.Click
        showEdit()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Protected Sub btEdit_Click(sender As Object, e As EventArgs) Handles btEdit.Click
        showEdit(True)

    End Sub

    Function tableEmployee() As String
        Dim fldNameHTI As New ArrayList From {
            "DAILY_SHIFT.SHIFTDAYS collate Chinese_PRC_BIN as SHIFTDAYS",
            "DAILY_SHIFT.EMP_CODE",
            "E.DEPT_CODE",
            "E.EMP_NAME",
            "E.POS_CODE",
            "rtrim(DAILY_SHIFT.WORK_DATE) WORK_DATE",
            "DAILY_SHIFT.WORK_TYPE",
            "L.LEAVE_CODE",
            "L.DATE_START",
            "L.TIME_STRAT",
            "L.DATE_END",
            "L.TIME_END",
            "'HTI' COMP_CODE",
            "E.EMP_STATE",
            "E.PICKUP_LOCATION",
            "DAILY_SHIFT.SHIFTDAYS2 collate Chinese_PRC_BIN as SHIFTDAYS2"
        }
        Dim strSQLHTI As New SQLString("V_HR_EMPLOYEE_DAILY_SHIFT DAILY_SHIFT", fldNameHTI)
        strSQLHTI.setLeftjoin(" LEFT JOIN V_HR_EMPLOYEE E ON  E.CODE = DAILY_SHIFT.EMP_CODE ")
        strSQLHTI.setLeftjoin(" LEFT JOIN V_HR_LEAVE_RECORD L ON L.EMP_CODE=DAILY_SHIFT.EMP_CODE  and DAILY_SHIFT.WORK_DATE between L.DATE_START and L.DATE_END ")

        Dim fldNameHY As New ArrayList From {
            "isnull(EMP_HY.SHIFT,'Rest') collate Chinese_PRC_BIN",
            "EMP_HY.CODE collate Chinese_PRC_BIN",
            "EMP_HY.DEPT_CODE collate Chinese_PRC_BIN",
            "EMP_HY.EMP_NAME collate Chinese_PRC_BIN",
            "EMP_HY.POS_CODE collate Chinese_PRC_BIN",
            "rtrim(isnull(B.WORK_DATE,'" & ucDate.text & "'))",
            "isnull(B.WORK_TYPE,'Holiday')",
            "NULL",
            "NULL",
            "NULL",
            "NULL",
            "NULL",
            "'HY'",
            "EMP_HY.EMP_STATE collate Chinese_PRC_BIN ",
            "EMP_HY.PICKUP_LOCATION collate Chinese_PRC_BIN ",
            "isnull(EMP_HY.SHIFT,'Rest') collate Chinese_PRC_BIN"
        }
        Dim strSQLHY As New SQLString("HR_EMPLOYEE_HY EMP_HY", fldNameHY)
        strSQLHY.setLeftjoin(" left join V_HR_DAILY_SHIFT B on B.SHIFTDAYS=EMP_HY.SHIFT collate Chinese_PRC_BIN AND B.WORK_DATE='" & ucDate.text & "'  ")
        Return "(" & strSQLHTI.GetSQLString & " UNION " & strSQLHY.GetSQLString & ")"
    End Function


    Sub showEdit(Optional edit As Boolean = False)
        If String.IsNullOrEmpty(Session(VarIni.UserId)) Then
            Response.Redirect(VarIni.PageLogin)
        End If
        Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session(VarIni.UserId)), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dr, "Dept")
        If deptcode = "" Then
            Response.Redirect(Server.UrlPathEncode("../Home.aspx"))
        End If
        Dim SQL As String = String.Empty
        Dim Whr As String = String.Empty
        '---------------------------------------GridView Show
        Dim fldName As New ArrayList From {
            "EMP_DATA.COMP_CODE",
            "EMP_DATA.SHIFTDAYS",
            "EMP_DATA.DEPT_CODE",
            "D.Name+'-'+ISNULL(D.ShortName,'')" & VarIni.char8 & "DEPT_NAME",
            "EMP_DATA.EMP_CODE",
            "EMP_DATA.EMP_NAME",
            "RTRIM(substring(EMP_DATA.WORK_DATE,7,4))+'/'+substring(EMP_DATA.WORK_DATE,5,2)+'/'+substring(EMP_DATA.WORK_DATE,1,4)+' '+R.WORK_BEGIN_TIME collate Chinese_PRC_BIN " & VarIni.char8 & "workStartDate",
            "' '+R.WORK_END_TIME" & VarIni.char8 & "workEndDate",
            "J.NAME JOB_NAME",
            "EMP_DATA.WORK_DATE",
            "EMP_DATA.WORK_TYPE",
            "R.WORK_BEGIN_TIME",
            "R.WORK_END_TIME",
            " case when OT.DOCNO IS NULL then isnull(OT.PICKUP_LOCATION,C.Name) else case when OT.PICKUP_LOCATION='' then  EMP_DATA.PICKUP_LOCATION else OT.PICKUP_LOCATION end end " & VarIni.char8 & "PICKUP_LOCATION",
            "OT.DOCNO" & VarIni.char8 & DOCNO_OT,
            "rtrim(isnull(EMP_DATA.LEAVE_CODE,OT.LEAVE_CODE))" & VarIni.char8 & "LEAVE_CODE",
            "rtrim(EMP_DATA.LEAVE_CODE)" & VarIni.char8 & "HR_LEAVE_CODE",
            "isnull(EMP_DATA.DATE_START,OT.LEAVE_STARTED_DATE)" & VarIni.char8 & "LEAVE_STARTED_DATE",
            "isnull(EMP_DATA.TIME_STRAT,OT.LEAVE_STARTED_TIME)" & VarIni.char8 & "LEAVE_STARTED_TIME",
            "isnull(EMP_DATA.DATE_END,OT.LEAVE_FINISHED_DATE)" & VarIni.char8 & "LEAVE_FINISHED_DATE",
            "isnull(EMP_DATA.TIME_END,OT.LEAVE_FINISHED_TIME)" & VarIni.char8 & "LEAVE_FINISHED_TIME",
            "rtrim(OT.OT_STARTED_DATE)" & VarIni.char8 & "OT_STARTED_DATE",
            "rtrim(OT.OT_STARTED_TIME)" & VarIni.char8 & "OT_STARTED_TIME",
            "rtrim(OT.OT_FINISHED_DATE)" & VarIni.char8 & "OT_FINISHED_DATE",
            "rtrim(OT.OT_FINISHED_TIME)" & VarIni.char8 & "OT_FINISHED_TIME"
        }
        Dim strSQL As New SQLString(tableEmployee() & " EMP_DATA", fldName)
        strSQL.setLeftjoin(" LEFT JOIN V_HR_ATTENDANCE_RANK R ON  R.CODE_SHIFT=EMP_DATA.SHIFTDAYS ")
        strSQL.setLeftjoin(" LEFT JOIN V_HR_JOB J ON  J.CODE=EMP_DATA.POS_CODE ")
        strSQL.setLeftjoin(" LEFT JOIN V_HR_DEPT D ON  D.Code = EMP_DATA.DEPT_CODE  ")
        strSQL.setLeftjoin(" LEFT JOIN CodeInfo C ON C.Name = EMP_DATA.PICKUP_LOCATION collate Chinese_PRC_BIN AND C.CodeType='PICKUP_LOCATION'")
        strSQL.setLeftjoin(" LEFT JOIN " & OT_Record & " OT on OT.EMPNO=EMP_DATA.EMP_CODE collate Chinese_PRC_BIN and OT.WORK_DATE=EMP_DATA.WORK_DATE collate Chinese_PRC_BIN ")
        Whr &= dbConn.WHERE_IN("EMP_DATA.DEPT_CODE", cblDept, False, True, deptcode) 'DEPT
        Whr &= dbConn.WHERE_LIKE("EMP_DATA.EMP_CODE", tbEmpNo) 'EMPCODE
        Whr &= dbConn.WHERE_EQUAL("EMP_DATA.WORK_DATE", ucDate.text) 'DATE
        Whr &= dbConn.WHERE_LIKE("EMP_DATA.EMP_STATE", "3____", False, False, True) 'status employee
        Whr &= dbConn.WHERE_LIKE("EMP_DATA.SHIFTDAYS2", rdlShift.SelectedValue, False)
        If edit Then
            Whr &= dbConn.WHERE_EQUAL("OT.DOCNO", " IS NOT NULL ", "", False)
        End If
        strSQL.SetWhere(Whr, True)
        strSQL.SetOrderBy("EMP_DATA.COMP_CODE,EMP_DATA.DEPT_CODE,EMP_DATA.EMP_CODE")

        Dim dt As DataTable = dbConn.Query(strSQL.GetSQLString, VarIni.DBMIS, dbConn.WhoCalledMe)
        gvCont.ShowGridView(gvShow, dt)
        'gvShow.DataSource = dt
        'gvShow.DataBind()
        If gvShow.Rows.Count > 0 Then
            gvShow.Visible = False
        End If
        CountRow1.RowCount = gvCont.rowGridview(gvShow)

        System.Threading.Thread.Sleep(1000)
        gvReport.Visible = False
        gvShow.Visible = True
        'gvEdit.Visible = False
        btRecord.Visible = True
        btEdit.Visible = False
        btSave.Visible = True
        btUpdate.Visible = False
        btCancel.Visible = True
        btShowRe.Visible = False
        btPrint.Visible = False
        btExcel.Visible = False
        setButton()
    End Sub

    '================== DataBound  ==================
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        With e
            If .Row.RowType = DataControlRowType.DataRow Then
                With New GridviewRowControl(.Row)
                    Dim ddlOTStartDate As DropDownList = .FindControl("ddlOTStartDate")
                    Dim tbStartTime As TextBox = .FindControl("tbOTStartTime")
                    Dim ddlOTEndDate As DropDownList = .FindControl("ddlOTEndDate")
                    Dim tbEndTime As TextBox = .FindControl("tbOTEndTime")
                    Dim ddlBusLine As DropDownList = .FindControl("ddlBusLine")
                    Dim cbBusLine As CheckBox = .FindControl("cbBusLine")
                    Dim cbOverTime As CheckBox = .FindControl("cbOverTime")
                    Dim cbOTLunch As CheckBox = .FindControl("cbOTLunch")
                    'leave
                    Dim ddlleaveCode As DropDownList = .FindControl("ddlleaveCode")
                    Dim ddlLeaveCase As DropDownList = .FindControl("ddlLeaveCase")
                    Dim ddlLeaveStartDate As DropDownList = .FindControl("ddlLeaveStartDate")
                    Dim tbLeaveStartTime As TextBox = .FindControl("tbLeaveStartTime")
                    Dim ddlLeaveEndDate As DropDownList = .FindControl("ddlLeaveEndDate")
                    Dim tbLeaveEndTime As TextBox = .FindControl("tbLeaveEndTime")

                    'Dim EmpCode As String = .Text("EMP_CODE").Trim
                    Dim WorkType As String = .Text("WORK_TYPE").Trim
                    Dim workTimeStart As String = .Text("WORK_BEGIN_TIME").Replace(":", ".")
                    Dim workTimeEnd As String = .Text("WORK_END_TIME").Replace(":", ".")

                    Dim defaultShift As String = .Text("SHIFTDAYS")
                    Dim DocNo As String = .Text("DOCNO_OT")

                    'cbOverTime.Enabled = If(WorkType = "Work Day", False, True)
                    Dim WorkDate As Date = dateCont.strToDateTime(.Text("WORK_DATE").Trim, "yyyyMMdd")
                    Dim valRdlDefault As String = rdlDefault.SelectedItem.Text
                    Dim valRdlShift As String = rdlShift.SelectedItem.Text

                    'set over time start and time end
                    Dim valDateStartOT As String = dateCont.dateShow2(.Text("OT_STARTED_DATE"), "/")
                    Dim valTimeStartOT As String = .Text("OT_STARTED_TIME")
                    Dim valDateEndOT As String = dateCont.dateShow2(.Text("OT_FINISHED_DATE"), "/")
                    Dim valTimeEndOT As String = .Text("OT_FINISHED_TIME")

                    'set val to ddl date start and end
                    ddlOTStartDate.Items.Clear()
                    ddlOTEndDate.Items.Clear()
                    For i As Integer = 0 To 1
                        Dim dd As Date = WorkDate.AddDays(i)
                        Dim itemList As New ListItem With {
                            .Text = dd.ToString("dd/MM/yyyy"),
                            .Value = dd.ToString("yyyyMMdd")
                        }
                        ddlOTStartDate.Items.Add(itemList)
                        ddlOTEndDate.Items.Add(itemList)
                        ddlLeaveStartDate.Items.Add(itemList)
                        ddlLeaveEndDate.Items.Add(itemList)
                    Next
                    'set datatable from session
                    ddlCnt.showDDL(ddlBusLine, Session("PICKUP_LOCATION"), CODEINFO.Name, CODEINFO.Name)
                    ddlBusLine.Text = .Text("PICKUP_LOCATION")
                    ddlCnt.showDDL(ddlleaveCode, Session("LeaveCode"), CODEINFO.Name, CODEINFO.Code, True)
                    'over time data
                    Dim chkOT As Boolean = True
                    If String.IsNullOrEmpty(DocNo) Then 'new
                        If WorkType = "Work Day" And valRdlDefault = "NO OT" Then
                            chkOT = False
                        End If
                    ElseIf String.IsNullOrEmpty(valTimeStartOT) And String.IsNullOrEmpty(valTimeEndOT) Then
                        chkOT = False
                    End If
                    cbOverTime.Checked = chkOT
                    tbStartTime.Text = valTimeStartOT
                    tbEndTime.Text = valTimeEndOT

                    'leave data
                    Dim leaveCase As String = "N"
                    Dim setVisible As Boolean = False
                    If WorkType = "Work Day" Then
                        Dim LEAVE_RECORD As String = .Text("LEAVE_CODE")
                        If Not String.IsNullOrEmpty(LEAVE_RECORD) Then
                            Dim leaveStartTime As String = .Text("LEAVE_STARTED_TIME").Replace(":", ".")
                            Dim leaveFinishTime As String = .Text("LEAVE_FINISHED_TIME").Replace(":", ".")
                            If leaveStartTime = workTimeStart And leaveFinishTime = workTimeEnd Then
                                leaveCase = "D"
                            Else
                                leaveCase = "H"
                            End If
                            'ddlLeaveStartDate.Text = dateCont.dateShow2(.Text("LEAVE_STARTED_DATE"), "/")
                            tbLeaveStartTime.Text = leaveStartTime
                            'ddlLeaveEndDate.Text = dateCont.dateShow2(.Text("LEAVE_FINISHED_DATE"), "/")
                            tbLeaveEndTime.Text = leaveFinishTime
                            ddlleaveCode.Text = LEAVE_RECORD

                            Dim setEnable As Boolean = If(String.IsNullOrEmpty(.Text("HR_LEAVE_CODE")), True, False)
                            ddlLeaveCase.Enabled = setEnable
                            ddlleaveCode.Enabled = setEnable
                            ddlLeaveStartDate.Enabled = setEnable
                            tbLeaveStartTime.Enabled = setEnable
                            ddlLeaveEndDate.Enabled = setEnable
                            tbLeaveEndTime.Enabled = setEnable
                            If leaveCase = "D" Then
                                tbStartTime.Enabled = False
                                tbEndTime.Enabled = False
                                cbBusLine.Enabled = False
                                cbBusLine.Checked = False
                                ddlBusLine.Enabled = False
                                cbOverTime.Enabled = False
                                cbOTLunch.Enabled = False
                            End If
                            setVisible = True
                        End If
                    Else
                        ddlLeaveCase.Enabled = False
                    End If
                    'set show about leave record
                    '=================
                    ddlleaveCode.Visible = setVisible
                    ddlLeaveStartDate.Visible = setVisible
                    tbLeaveStartTime.Visible = setVisible
                    ddlLeaveEndDate.Visible = setVisible
                    tbLeaveEndTime.Visible = setVisible

                    ddlLeaveCase.SelectedValue = leaveCase
                    ddlLeaveCase_Selectedindexchanged(ddlLeaveCase, e)
                End With
                e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            End If
        End With
    End Sub

    Protected Sub ddlLeaveCase_Selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlLeaveCase As DropDownList = DirectCast(sender, DropDownList)
        With New GridviewRowControl(DirectCast(ddlLeaveCase.Parent.Parent, GridViewRow))
            Dim workDate As Date = dateCont.strToDateTime(.Text(7), "yyyyMMdd") '"dd/MM/yyyy"
            Dim workType As String = .Text(3).Trim

            Dim tbEndDate As TextBox = .FindControl("tbOTEndDate")
            Dim tbEndTime As TextBox = .FindControl("tbOTEndTime")
            Dim ddlBusLine As DropDownList = .FindControl("ddlBusLine")
            Dim cbBusLine As CheckBox = .FindControl("cbBusLine")
            Dim cbOverTime As CheckBox = .FindControl("cbOverTime")
            Dim cbOTLunch As CheckBox = .FindControl("cbOTLunch")

            Dim ddlLeaveStartDate As DropDownList = .FindControl("ddlLeaveStartDate")
            Dim tbLeaveStartTime As TextBox = .FindControl("tbLeaveStartTime")
            Dim ddlLeaveEndDate As DropDownList = .FindControl("ddlLeaveEndDate")
            Dim tbLeaveEndTime As TextBox = .FindControl("tbLeaveEndTime")

            Dim tbOTEndDate As TextBox = .FindControl("tbOTEndDate")
            Dim ddlleaveCode As DropDownList = .FindControl("ddlleaveCode")          '

            'set enable and visible
            Dim setEnable As Boolean = True
            Dim setVisible As Boolean = True

            Dim enableLeaveStartDate As Boolean = False
            Dim enableLeaveStartTime As Boolean = False
            Dim enableLeaveEndDate As Boolean = False
            Dim enableLeaveEndTime As Boolean = False

            Dim leaveCase As String = ddlLeaveCase.SelectedValue
            Dim shift As String = .Text(2).Substring(0, 1)
            'value
            Dim valLeaveStartDate As String = ""
            Dim valLeaveEndDate As String = ""
            Dim valLeaveStartTime As String = ""
            Dim valLeaveEndTime As String = ""
            Dim checkBusLine As Boolean = True
            Dim setVisibleDate As Boolean = If(workType = "Work Day", False, True)
            If leaveCase <> "N" Then 'select leave between 'D' and 'H'
                valLeaveStartDate = GET_CONV_STRING(workDate)
                valLeaveEndDate = GET_CONV_STRING(workDate.AddDays(If(shift = "D", 0, 1)))
                valLeaveStartTime = .Text(9).Replace(":", ".")
                valLeaveEndTime = .Text(11).Replace(":", ".")
                Select Case leaveCase
                    Case "D"
                        checkBusLine = False
                        cbOTLunch.Checked = False
                        cbOverTime.Checked = False
                        setEnable = False
                    Case "H"
                        enableLeaveStartDate = True
                        enableLeaveStartTime = True
                        enableLeaveEndDate = True
                        enableLeaveEndTime = True
                        If shift = "D" Then
                            enableLeaveStartDate = False
                            enableLeaveEndDate = False
                            ddlLeaveEndDate.Text = workDate.AddDays(If(shift = "D", 0, 1)).ToString("yyyyMMdd")
                        End If
                End Select
            Else
                setVisible = False
                valLeaveStartDate = ""
                valLeaveEndDate = ""
                valLeaveStartTime = ""
                valLeaveEndTime = ""
            End If

            'set value
            cbBusLine.Checked = checkBusLine
            'ddlLeaveStartDate.Text = valLeaveStartDate
            tbLeaveStartTime.Text = valLeaveStartTime
            'ddlLeaveEndDate.Text = valLeaveEndDate
            tbLeaveEndTime.Text = valLeaveEndTime
            'set enable
            ddlLeaveStartDate.Enabled = enableLeaveStartDate
            tbLeaveStartTime.Enabled = enableLeaveStartTime
            ddlLeaveEndDate.Enabled = enableLeaveEndDate
            tbLeaveEndTime.Enabled = enableLeaveEndTime

            cbOTLunch.Enabled = setEnable
            cbOverTime.Enabled = setEnable
            cbBusLine.Enabled = setEnable
            ddlBusLine.Enabled = setEnable

            'set visible
            ddlleaveCode.Visible = setVisible
            ddlLeaveStartDate.Visible = setVisible AndAlso setVisibleDate
            tbLeaveStartTime.Visible = setVisible
            ddlLeaveEndDate.Visible = setVisible AndAlso setVisibleDate
            tbLeaveEndTime.Visible = setVisible
            cbOverTime_OnCheckedChanged(cbOverTime, e)
            cbBusLine_OnCheckedChanged(cbBusLine, e)
        End With
    End Sub

    Protected Sub cbOverTime_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cbOverTime As CheckBox = DirectCast(sender, CheckBox)
        With New GridviewRowControl(DirectCast(cbOverTime.Parent.Parent, GridViewRow))
            Dim ddlStartDate As DropDownList = .FindControl("ddlOTStartDate")
            Dim tbStartTime As TextBox = .FindControl("tbOTStartTime")
            Dim ddlEndDate As DropDownList = .FindControl("ddlOTEndDate")
            Dim tbEndTime As TextBox = .FindControl("tbOTEndTime")

            Dim workType As String = .Text(3).Trim
            'Dim workStartTime As String = .Text(9).Trim.Replace(":", ".")
            'Dim workEndTime As String = .Text(11).Trim.Replace(":", ".")

            Dim valRdlDefault As String = rdlDefault.SelectedItem.Text
            Dim workTimeStart As String = .Text(9).Replace(":", ".")
            Dim workTimeEnd As String = .Text(11).Replace(":", ".")
            'Dim valDateStartOT As String = dateCont.dateShow2(.Text("OT_STARTED_DATE"), "/")
            Dim valTimeStartOT As String = .Text(25)
            'Dim valDateEndOT As String = dateCont.dateShow2(.Text("OT_FINISHED_DATE"), "/")
            Dim valTimeEndOT As String = .Text(27)
            Dim VisibleStart As Boolean = False
            Dim VisibleFinished As Boolean = False

            If cbOverTime.Checked Then 'need over time
                If String.IsNullOrEmpty(.Text(23)) Then 'insert 
                    If workType = "Work Day" Then 'normal working day
                        If valRdlDefault = "NO OT" Then
                            valTimeStartOT = workTimeEnd
                            valTimeEndOT = workTimeEnd
                        Else
                            valTimeStartOT = GetTimeStartOverTime(workTimeEnd)
                            valTimeEndOT = valRdlDefault
                        End If
                    Else 'holiday
                        valTimeStartOT = workTimeStart
                        valTimeEndOT = If(workTimeEnd <> "16.00", "16.00", workTimeEnd)
                    End If
                Else 'update
                    If String.IsNullOrEmpty(valTimeStartOT) And String.IsNullOrEmpty(valTimeEndOT) Then 'check last record not over time
                        valTimeStartOT = GetTimeStartOverTime(workTimeEnd)
                        valTimeEndOT = valTimeStartOT
                    End If
                End If
            Else 'not need over time
                valTimeStartOT = workTimeEnd
                valTimeEndOT = workTimeEnd
            End If
            tbStartTime.Text = valTimeStartOT
            tbEndTime.Text = valTimeEndOT

            Dim setWorkDay As Boolean = If(workType = "Work Day", False, cbOverTime.Checked)
            Dim enableStartTime As Boolean = setWorkDay
            Dim enableFinishedTime As Boolean = cbOverTime.Checked

            Dim setVisible As Boolean = True
            If outCont.checkNumberic(workTimeStart) < outCont.checkNumberic(workTimeEnd) Then 'day
                setVisible = False
            End If
            ddlStartDate.Visible = setVisible
            ddlEndDate.Visible = setVisible

            'ddlStartDate.Enabled = enableStartDate
            tbStartTime.Enabled = enableStartTime
            'ddlEndDate.Enabled = enableFinishedDate
            tbEndTime.Enabled = enableFinishedTime
        End With
    End Sub

    Private Function GetTimeStartOverTime(workTimeEnd As String) As String
        Dim valReturn As String = ""
        If workTimeEnd = "16.00" Or workTimeEnd = "04.00" Then
            valReturn = workTimeEnd.Replace(".00", ".20")
        Else
            valReturn = workTimeEnd
        End If
        Return valReturn
    End Function


    Protected Sub cbBusLine_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cbBusLine As CheckBox = DirectCast(sender, CheckBox)
        With New GridviewRowControl(DirectCast(cbBusLine.Parent.Parent, GridViewRow))
            Dim ddlBusLine As DropDownList = .FindControl("ddlBusLine")
            ddlBusLine.Enabled = cbBusLine.Checked
            ddlBusLine.Visible = cbBusLine.Checked
        End With

    End Sub

    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If Session(VarIni.UserId) = "" Then
            Response.Redirect(VarIni.PageLogin)
        End If

        'check data before save
        For Each gr As GridViewRow In gvShow.Rows
            With New GridviewRowControl(gr)
                Dim isError As Boolean = False
                Dim EmpNo As String = .Text(4)
                Dim EmpName As String = .Text(4) & " " & .Text(5)
                Dim ShiftDays As String = .Text(2)
                Dim msg As String = "พนักงาน " & EmpName & " กรณีเลือกทำโอที"
                'check over time
                Dim cbOverTime As CheckBox = .FindControl("cbOverTime")
                If cbOverTime.Checked Then
                    Dim tbOTBeginTime As TextBox = .FindControl("tbOTStartTime")
                    Dim tbOTEndTime As TextBox = .FindControl("tbOTEndTime")
                    Dim valOtTimeStart As String = tbOTBeginTime.Text.Replace("__.__", "").Trim
                    Dim valOtTimeEnd As String = tbOTEndTime.Text.Replace("__.__", "").Trim
                    msg &= " ,กรณีเลือกทำโอที "
                    If valOtTimeStart = "" Or valOtTimeEnd = "" Then
                        msg &= " ต้องระบุเวลาทำโอที"
                        isError = True
                    Else
                        If outCont.checkNumberic(valOtTimeEnd) < outCont.checkNumberic(valOtTimeStart) Then
                            msg &= "  เวลาทำโอทีไม่ถูกต้อง "
                            isError = True
                        End If
                    End If
                End If
                'check bus line
                Dim cbBusLine As CheckBox = .FindControl("cbBusLine")
                If cbBusLine.Checked Then
                    msg &= " ,กรณีเลือกสายรถ "
                    Dim ddlBusLine As DropDownList = .FindControl("ddlBusLine")
                    If ddlBusLine.Text = "0" Then
                        msg &= " กรุณาระบุสายรถ "
                        isError = True
                    End If
                End If
                'check leave
                Dim ddlLeaveCase As DropDownList = .FindControl("ddlLeaveCase")
                If ddlLeaveCase.Text <> "N" Then
                    msg &= " ,กรณีเลือกลางาน "
                    Dim tbLeaveStartTime As TextBox = .FindControl("tbLeaveStartTime")
                    Dim tbLeaveEndTime As TextBox = .FindControl("tbLeaveEndTime")
                    Dim valLeaveStartTime As String = tbLeaveStartTime.Text.Replace("__.__", "").Trim
                    Dim valLeaveEndTime As String = tbLeaveEndTime.Text.Replace("__.__", "").Trim

                    If valLeaveStartTime = "" Or valLeaveEndTime = "" Then
                        msg &= " ต้องระบุเวลาลางาน"
                        isError = True
                    Else
                        Dim LeaveStartTime As Decimal = outCont.checkNumberic(valLeaveStartTime)
                        Dim LeaveEndTime As Decimal = outCont.checkNumberic(valLeaveEndTime)

                        If LeaveEndTime < LeaveStartTime Then
                            msg &= "  เวลาลางานไม่ถูกต้อง "
                            isError = True
                        End If
                        Dim workTimeStart As Decimal = outCont.checkNumberic(.Text(9).Replace(":", "."))
                        Dim workTimeEnd As Decimal = outCont.checkNumberic(.Text(11).Replace(":", "."))
                        If LeaveStartTime < workTimeStart Or workTimeStart > workTimeEnd Then
                            msg &= "  เวลาลางานเริ่มต้นไม่ถูกต้อง "
                            isError = True
                        End If
                        If LeaveEndTime < workTimeStart Or LeaveEndTime > workTimeEnd Then
                            msg &= "  เวลาลางานสิ้นสุดไม่ถูกต้อง "
                            isError = True
                        End If
                        If LeaveStartTime = 12 And LeaveEndTime = 13 Then
                            msg &= "  เวลาพักกลางวันไม่ใช่เวลาลางาน "
                            isError = True
                        End If

                    End If
                End If
                If isError Then
                    show_message.ShowMessage(Page, msg & " รบกวนตรวจสอบอีกครั้ง", UpdatePanel1)
                    Exit Sub
                End If
            End With
        Next
        'save and update
        Dim cntRecord As Integer = 0
        For Each gr As GridViewRow In gvShow.Rows
            With New GridviewRowControl(gr)
                'set control
                ''over time
                Dim cbOTLunch As CheckBox = .FindControl("cbOTLunch")
                Dim cbOverTime As CheckBox = .FindControl("cbOverTime")
                Dim ddlOTStartDate As DropDownList = .FindControl("ddlOTStartDate")
                Dim tbOTStartTime As TextBox = .FindControl("tbOTStartTime")
                Dim ddlOTEndDate As DropDownList = .FindControl("ddlOTEndDate")
                Dim tbOTEndTime As TextBox = .FindControl("tbOTEndTime")
                ''pickup
                Dim cbBusLine As CheckBox = .FindControl("cbBusLine")
                Dim ddlBusLine As DropDownList = .FindControl("ddlBusLine")

                ''leave
                Dim ddlLeaveCase As DropDownList = .FindControl("ddlLeaveCase")
                Dim ddlleaveCode As DropDownList = .FindControl("ddlleaveCode")
                Dim ddlLeaveStartDate As DropDownList = .FindControl("ddlLeaveStartDate")
                Dim tbLeaveStartTime As TextBox = .FindControl("tbLeaveStartTime")
                Dim ddlLeaveEndDate As DropDownList = .FindControl("ddlLeaveEndDate")
                Dim tbLeaveEndTime As TextBox = .FindControl("tbLeaveEndTime")

                Dim DOCNO As String = .Text(23)
                Dim DEPT As String = .Text(22)
                'set value
                Dim SHIFT_DAY As String = .Text(2)
                Dim WORK_TYPE As String = .Text(3)
                Dim EMPNO As String = .Text(4)
                Dim WORK_DATE As String = .Text(7)
                Dim NORMAL_TIME As Integer = 0
                If WORK_TYPE = "Work Day" Then
                    Dim dWork As String = .Text(7)
                    Dim dStart As Date = dateCont.strToDateTime(dWork & " " & .Text(9).Replace(".", ":"))
                    Dim dEnd As Date = dateCont.strToDateTime(dWork & " " & .Text(11).Replace(".", ":"))
                    NORMAL_TIME = DateDiff(DateInterval.Second, dStart, dEnd) - 3600
                End If

                Dim LEAVE_CODE As String = ""
                Dim LEAVE_STARTED_DATE As String = ""
                Dim LEAVE_FINISHED_DATE As String = ""
                Dim LEAVE_STARTED_TIME As String = ""
                Dim LEAVE_FINISHED_TIME As String = ""
                Dim LEAVE_TIME As Integer = 0
                Dim valLeaveCase As String = ddlLeaveCase.Text
                If WORK_TYPE = "Work Day" And valLeaveCase <> "N" Then
                    LEAVE_CODE = ddlleaveCode.Text
                    LEAVE_STARTED_DATE = ddlLeaveStartDate.Text
                    LEAVE_STARTED_TIME = tbLeaveStartTime.Text.Replace(".", ":")
                    LEAVE_FINISHED_DATE = ddlLeaveEndDate.Text
                    LEAVE_FINISHED_TIME = tbLeaveEndTime.Text.Replace(".", ":")
                    If valLeaveCase = "D" Then
                        LEAVE_TIME = NORMAL_TIME
                        NORMAL_TIME = 0
                    Else
                        Dim dStart As Date = dateCont.strToDateTime(LEAVE_STARTED_DATE & " " & LEAVE_STARTED_TIME)
                        Dim dEnd As Date = dateCont.strToDateTime(LEAVE_FINISHED_DATE & " " & LEAVE_FINISHED_TIME)
                        LEAVE_TIME = DateDiff(DateInterval.Second, dStart, dEnd)
                        If outCont.checkNumberic(LEAVE_STARTED_TIME.Replace(":", ".")) < 12 And outCont.checkNumberic(LEAVE_FINISHED_TIME.Replace(":", ".")) > 13 Then
                            LEAVE_TIME -= 3600
                        End If
                    End If
                End If
                Dim WORK_BEGIN_DATE As String = WORK_DATE
                Dim WORK_BEGIN_TIME As String = .Text(9)

                Dim LUNCH_TIME As Integer = 0
                If cbOTLunch.Checked And ((WORK_TYPE = "Work Day" And valLeaveCase <> "D") Or WORK_TYPE <> "Work Day") Then
                    LUNCH_TIME = 3600
                End If

                Dim OT_STARTED_DATE As String = ""
                Dim OT_FINISHED_DATE As String = ""
                Dim OT_STARTED_TIME As String = ""
                Dim OT_FINISHED_TIME As String = ""
                Dim OVER_TIME As Integer = 0
                If cbOverTime.Checked Then
                    OT_STARTED_DATE = ddlOTStartDate.Text
                    OT_STARTED_TIME = tbOTStartTime.Text
                    OT_FINISHED_DATE = ddlOTEndDate.Text
                    OT_FINISHED_TIME = tbOTEndTime.Text
                    Dim dStart As Date = dateCont.strToDateTime(OT_STARTED_DATE & " " & OT_STARTED_TIME.Replace(".", ":"))
                    Dim dEnd As Date = dateCont.strToDateTime(OT_FINISHED_DATE & " " & OT_FINISHED_TIME.Replace(".", ":"))
                    OVER_TIME = DateDiff(DateInterval.Second, dStart, dEnd)
                End If
                Dim PICKUP_LOCATION As String = ""
                If cbBusLine.Checked Then
                    PICKUP_LOCATION = ddlBusLine.Text
                End If

                Dim fldDate As String = "CHANGEBY"
                Dim fldUser As String = "CHANGEDATE"
                If String.IsNullOrEmpty(DOCNO) Then
                    fldUser = "CREATEBY"
                    fldDate = "CREATEDATE"
                End If
                Dim fld As Hashtable = New Hashtable From {
                    {"EMPNO", EMPNO},
                    {"DEPT", DEPT},
                    {"WORK_TYPE", WORK_TYPE},
                    {"SHIFT_DAY", SHIFT_DAY},
                    {"WORK_DATE", WORK_DATE},
                    {"LEAVE_CODE", LEAVE_CODE},
                    {"LEAVE_STARTED_DATE", LEAVE_STARTED_DATE},
                    {"LEAVE_FINISHED_DATE", LEAVE_FINISHED_DATE},
                    {"LEAVE_STARTED_TIME", LEAVE_STARTED_TIME},
                    {"LEAVE_FINISHED_TIME", LEAVE_FINISHED_TIME},
                    {"WORK_BEGIN_DATE", WORK_BEGIN_DATE},
                    {"WORK_BEGIN_TIME", WORK_BEGIN_TIME},
                    {"OT_STARTED_DATE", OT_STARTED_DATE},
                    {"OT_FINISHED_DATE", OT_FINISHED_DATE},
                    {"OT_STARTED_TIME", OT_STARTED_TIME},
                    {"OT_FINISHED_TIME", OT_FINISHED_TIME},
                    {"OVER_TIME", OVER_TIME},
                    {"NORMAL_TIME", NORMAL_TIME},
                    {"LUNCH_TIME", LUNCH_TIME},
                    {"LEAVE_TIME", LEAVE_TIME},
                    {"PICKUP_LOCATION:N", PICKUP_LOCATION},
                    {fldUser, Session(VarIni.UserName)},
                    {fldDate, Date.Now.ToString("yyyyMMdd HH:mm")},
                    {"COMPANY", .Text(0)}
                }
                Dim whr As New Hashtable
                Dim TypeRecord As String = String.Empty
                If String.IsNullOrEmpty(DOCNO) Then 'insert
                    DOCNO = getDocNo()
                    fld.Add("DOCNO", DOCNO)
                    If WORK_TYPE = "Work Day" Or (WORK_TYPE <> "Work Day" And cbOverTime.Checked) Then
                        TypeRecord = "I"
                    End If
                Else
                    whr = New Hashtable From {{"DOCNO", DOCNO}}
                    If WORK_TYPE = "Work Day" Or (WORK_TYPE <> "Work Day" And cbOverTime.Checked) Then 'update 
                        TypeRecord = "U"
                    ElseIf Not cbOverTime.Checked Then 'delete
                        TypeRecord = "D"
                    End If
                End If
                If Not String.IsNullOrEmpty(TypeRecord) Then
                    Dim SQL As String = dbConn.GetSQL(OT_Record, fld, whr, TypeRecord)
                    cntRecord += dbConn.TransactionSQL(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
                End If
            End With
        Next
        show_message.ShowMessage(Page, If(cntRecord = 0, "ไม่มีรายการบันทึก", "บันทึกสำเร็จ"), UpdatePanel1)
        gvShow.Visible = False
        gvReport.Visible = True
        btSave.Visible = False
        btCancel.Visible = False
        btUpdate.Visible = False
        btRecord.Visible = True
        btShowRe.Visible = True
        ShowReport()
        setButton()
    End Sub

    Protected Sub btShowRe_Click(sender As Object, e As EventArgs) Handles btShowRe.Click
        If Session(VarIni.UserId) = "" Then
            Response.Redirect(VarIni.PageLogin)
        End If
        gvShow.Visible = False
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
        setButton()
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
        gvReport.Visible = False
        CountRow1.Visible = False
        setButton()
    End Sub

    Protected Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        Dim sql As String = ""
        Dim Dept As String = ""
        Dim WorkDate As String = ""
        Dim WorkTime As String = ""
        Dim whr As String = ""
        Dim paraName As String = String.Empty
        Dim chrConn As String = Chr(8)

        If Session(VarIni.UserId) = "" Then
            Response.Redirect(VarIni.PageLogin)
        End If

        Dim dr As DataRow = dbConn.QueryDataRow(getUser(Session(VarIni.UserId)), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dr, "Dept")

        whr &= whrCont.Where("O.DEPT", cblDept, False, deptcode.Replace(",", "','"), True) 'DEPT
        whr &= whrCont.Where("O.WORK_DATE", ucDate.text,, False) 'DATE
        If rdlShift.SelectedValue = "D" Then
            whr &= whrCont.Where("SUBSTRING(O.WORK_BEGIN_TIME,1,2)", " <> '19'")
        ElseIf rdlShift.SelectedValue = "N" Then
            whr &= whrCont.Where("SUBSTRING(O.WORK_BEGIN_TIME,1,2)", " = '19'")
        End If

        Dim WHR1 As String = String.Empty
        Dim fldName As New ArrayList From {
            HR_DEPT.CODE,
            HR_DEPT.NAME & "+'-'+ isnull(" & HR_DEPT.SHORT_NAME & ",''):CodeName"
        }
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
        setButton()
    End Sub

    Private Sub ShowReport()
        Dim dc As DataRow = dbConn.QueryDataRow(getUser(Session(VarIni.UserId)), VarIni.DBMIS, dbConn.WhoCalledMe)
        deptcode = dtCont.IsDBNullDataRow(dc, "Dept")

        Dim SQL As String = String.Empty
        Dim Whr As String = String.Empty
        Dim Whr1 As String = String.Empty
        Dim Whr2 As String = String.Empty
        ''--------------------------------------- GridView Show
        Dim dtShow As New DataTable
        Dim colName As New ArrayList From {
            "Comp" & VarIni.char8 & "COMPANY",
            "Shift" & VarIni.char8 & shift,
            "Dept" & VarIni.char8 & dept,
            "Emp No" & VarIni.char8 & empcode,
            "Emp Name" & VarIni.char8 & name,
            "Work Date" & VarIni.char8 & workdate,
            "Work Type" & VarIni.char8 & work_type,
            "Shift Day" & VarIni.char8 & shiftday,
            "Leave Code" & VarIni.char8 & leave_code,
            "Leave Start" & VarIni.char8 & leave_start_date,
            "Leave Finish" & VarIni.char8 & leave_end_date,
            "Leave Hours" & VarIni.char8 & leave_hours & VarIni.char8 & "2",
            "OT Start" & VarIni.char8 & ot_start_date,
            "OT Finish" & VarIni.char8 & ot_end_date,
            "OT Hours" & VarIni.char8 & ot_hours & VarIni.char8 & "2",
            "Lunch Hours" & VarIni.char8 & lunch_hours & VarIni.char8 & "0",
            "OT Total" & VarIni.char8 & ot_total & VarIni.char8 & "2",
            "Pickup Location" & VarIni.char8 & pickup,
            "Create by" & VarIni.char8 & create_by,
            "Create Date" & VarIni.char8 & create_date,
            "Change by" & VarIni.char8 & change_by,
            "Change Date" & VarIni.char8 & change_date
        }
        dtShow = dtCont.setColDatatable(colName, VarIni.char8)
        Whr &= whrCont.Where("O.DEPT", cblDept, False, deptcode.Replace(",", "','"), True) 'DEPT
        Whr &= whrCont.Where("O.EMPNO", tbEmpNo) 'EMPCODE
        Whr &= whrCont.Where("O.WORK_DATE", ucDate.text,, False) 'DATE
        Whr &= whrCont.Where("E.EMP_STATE", " NOT IN ('3001','3002','3003') ")
        If rdlShift.SelectedValue = "D" Then
            Whr &= whrCont.Where("O.WORK_BEGIN_TIME", " <>'19:00'")
        ElseIf rdlShift.SelectedValue = "N" Then
            Whr &= whrCont.Where("O.WORK_BEGIN_TIME", " ='19:00'")
        End If

        Dim fldName As New ArrayList From {
            "O.COMPANY",
            "CASE O.WORK_BEGIN_TIME WHEN '19:00' THEN 'Night' ELSE 'Day' END" & VarIni.char8 & shift,
            "D.Code+':'+D.Name+'-'+ISNULL(D.ShortName,'')" & VarIni.char8 & dept,
            "O.EMPNO" & VarIni.char8 & empcode,
            "E.EMP_NAME" & VarIni.char8 & name,
            "RTRIM(substring(O.WORK_DATE,7,4))+'/'+substring(O.WORK_DATE,5,2)+'/'+substring(O.WORK_DATE,1,4)" & VarIni.char8 & workdate,
            "O.WORK_TYPE" & VarIni.char8 & work_type,
            "O.SHIFT_DAY" & VarIni.char8 & shiftday,
            "CASE O.LEAVE_CODE WHEN '' THEN '' ELSE RTRIM(O.LEAVE_CODE)+'-'+RTRIM(C.Name) END" & VarIni.char8 & leave_code,
            "CASE O.LEAVE_CODE WHEN '' THEN '' ELSE RTRIM(substring(O.LEAVE_STARTED_DATE,7,4)) +'/'+substring(O.LEAVE_STARTED_DATE,5,2)+'/'+substring(O.LEAVE_STARTED_DATE,1,4)+' '+O.LEAVE_STARTED_TIME END" & VarIni.char8 & leave_start_date,
            "CASE O.LEAVE_CODE WHEN '' THEN '' ELSE RTRIM(substring(O.LEAVE_FINISHED_DATE,7,4))+'/'+substring(O.LEAVE_FINISHED_DATE,5,2)+'/'+substring(O.LEAVE_FINISHED_DATE,1,4)+' '+O.LEAVE_FINISHED_TIME END" & VarIni.char8 & leave_end_date,
            "CAST(O.LEAVE_TIME/3600 AS DECIMAL(16,2))" & VarIni.char8 & leave_hours,
            "CASE O.OT_STARTED_DATE WHEN '' THEN '' ELSE RTRIM(substring(O.OT_STARTED_DATE,7,4))+'/'+substring(O.OT_STARTED_DATE,5,2)+'/'+substring(O.OT_STARTED_DATE,1,4)+' '+O.OT_STARTED_TIME END" & VarIni.char8 & ot_start_date,
            "CASE O.OT_FINISHED_DATE WHEN '' THEN '' ELSE RTRIM(substring(O.OT_FINISHED_DATE,7,4))+'/'+substring(O.OT_FINISHED_DATE,5,2)+'/'+substring(O.OT_FINISHED_DATE,1,4)+' '+O.OT_FINISHED_TIME END" & VarIni.char8 & ot_end_date,
            "CASE WHEN O.WORK_TYPE = 'Work Day' THEN CAST(O.OVER_TIME/3600 AS DECIMAL(16,1)) ELSE CAST((O.NORMAL_TIME+O.OVER_TIME) /3600 AS DECIMAL(16,1)) END" & VarIni.char8 & ot_hours,
            "CAST(O.LUNCH_TIME/3600 AS DECIMAL(16,1))" & VarIni.char8 & lunch_hours,
            "CASE WHEN O.WORK_TYPE = 'Work Day' THEN CAST((O.OVER_TIME +O.LUNCH_TIME )/3600 AS DECIMAL(16,1)) ELSE  CAST((O.OVER_TIME +O.LUNCH_TIME+O.NORMAL_TIME )/3600 AS DECIMAL(16,1)) END" & VarIni.char8 & ot_total,
            "O.PICKUP_LOCATION" & VarIni.char8 & pickup,
            "O.CREATEBY" & VarIni.char8 & create_by,
            "O.CREATEDATE" & VarIni.char8 & create_date,
            "O.CHANGEBY" & VarIni.char8 & change_by,
            "O.CHANGEDATE" & VarIni.char8 & change_date
        }
        SQL &= VarIni.S & sqlCont.getFeild(fldName, VarIni.char8) & VarIni.F & " " & OT_Record & " O"
        SQL &= VarIni.L & tableEmployee() & " E " & VarIni.O2 & " E.EMP_CODE=O.EMPNO " & COLL & " and O.WORK_DATE=E.WORK_DATE"
        SQL &= VarIni.L & " V_HR_DEPT D " & VarIni.O2 & "D.Code =O.DEPT " & COLL
        SQL &= VarIni.L & " CodeInfo C " & VarIni.O2 & " C.Code =O.LEAVE_CODE and CodeType = 'LeaveCode' " & COLL
        SQL &= VarIni.W & " 1=1 " & Whr & VarIni.O & "  O.COMPANY,O.DEPT,O.EMPNO ASC"

        Dim dt As DataTable = dbConn.Query(SQL, VarIni.DBMIS, dbConn.WhoCalledMe)
        Const Doublechar8 As String = "◘◘"

        For Each dr As DataRow In dt.Rows
            Dim dataFld = New ArrayList From {
                "COMPANY" & Doublechar8 & dtCont.IsDBNullDataRow(dr, "COMPANY"),
                shift & Doublechar8 & dtCont.IsDBNullDataRow(dr, shift),
                dept & Doublechar8 & dtCont.IsDBNullDataRow(dr, dept),
                empcode & Doublechar8 & dtCont.IsDBNullDataRow(dr, empcode),
                name & Doublechar8 & dtCont.IsDBNullDataRow(dr, name),
                workdate & Doublechar8 & dtCont.IsDBNullDataRow(dr, workdate),
                work_type & Doublechar8 & dtCont.IsDBNullDataRow(dr, work_type),
                shiftday & Doublechar8 & dtCont.IsDBNullDataRow(dr, shiftday),
                leave_code & Doublechar8 & dtCont.IsDBNullDataRow(dr, leave_code),
                leave_start_date & Doublechar8 & dtCont.IsDBNullDataRow(dr, leave_start_date),
                leave_end_date & Doublechar8 & dtCont.IsDBNullDataRow(dr, leave_end_date),
                leave_hours & Doublechar8 & dtCont.IsDBNullDataRowDecimal(dr, leave_hours),
                ot_start_date & Doublechar8 & dtCont.IsDBNullDataRow(dr, ot_start_date),
                ot_end_date & Doublechar8 & dtCont.IsDBNullDataRow(dr, ot_end_date),
                ot_hours & Doublechar8 & dtCont.IsDBNullDataRowDecimal(dr, ot_hours),
                lunch_hours & Doublechar8 & dtCont.IsDBNullDataRowDecimal(dr, lunch_hours),
                ot_total & Doublechar8 & dtCont.IsDBNullDataRowDecimal(dr, ot_total),
                pickup & Doublechar8 & dtCont.IsDBNullDataRow(dr, pickup),
                create_by & Doublechar8 & dtCont.IsDBNullDataRow(dr, create_by),
                create_date & Doublechar8 & dtCont.IsDBNullDataRow(dr, create_date),
                change_by & Doublechar8 & dtCont.IsDBNullDataRow(dr, change_by),
                change_date & Doublechar8 & dtCont.IsDBNullDataRow(dr, change_date)
            }
            dtCont.addDataRow(dtShow, dr, dataFld, VarIni.char8)
        Next
        gvCont.GridviewInitial(gvReport, colName,,,,, VarIni.char8)
        gvCont.ShowGridView(gvReport, dtShow)
        CountRow1.RowCount = gvCont.rowGridview(gvReport)
        If gvReport.Rows.Count > 0 Then
            btPrint.Visible = True
        End If
        setButton()
    End Sub

    '=============== Event =====================
    Protected Sub ucDate_ChangeEvent(ByVal sender As Object, ByVal e As EventArgs) Handles ucDate.ChangeEvent
        setButton()
        gvShow.DataSource = Nothing
        gvShow.DataBind()
    End Sub

    Protected Sub rdlShift_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdlShift.SelectedIndexChanged, UpdatePanel2.DataBinding
        Dim values As ArrayList = New ArrayList()
        values.Add("NO OT")
        If rdlShift.SelectedValue = "D" Then 'day
            values.Add("18:50")
            values.Add("20:50")
        Else 'night
            values.Add("06:50")
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